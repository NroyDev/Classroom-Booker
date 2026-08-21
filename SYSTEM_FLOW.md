# 中山大學教室與設備借用系統 ─ 系統架構與頁面流程說明文件

本文件詳細說明「中山大學教室與設備借用系統」的系統架構設計、核心模組互動關係，以及各使用者角色的操作頁面流程。

---

## 🏗️ 1. 系統架構圖 (System Architecture)

系統採用傳統的三層式架構 (3-Tier Architecture) 設計，配合 ASP.NET Web Forms 網頁生命週期與 OLE DB 連線至 MS Access 本地資料庫：

```mermaid
graph TD
    %% 前端層
    subgraph Client_Side ["前端展示層 (Client-Side)"]
        UI["ASP.NET Server Controls (.aspx)<br/>HTML5 / CSS3 / JavaScript / Bootstrap"]
    end

    %% 後端層
    subgraph Server_Side ["後端邏輯層 (Server-Side)"]
        CodeBehind["C# Code-Behind (.aspx.cs)<br/>Business Logic / Session Management"]
        OleDb["System.Data.OleDb<br/>OLE DB Connection Provider"]
        Smtp["SMTP Mail Client<br/>Gmail SMTP Server (Port 587)"]
    end

    %% 資料層
    subgraph Data_Side ["資料儲存層 (Data-Side)"]
        AccessDB["Microsoft Access Database<br/>(AccessDB.accdb)"]
    end

    %% 連接關係
    UI <-->|HTTP Request / PostBack| CodeBehind
    CodeBehind <-->|SQL Queries via OLE DB| OleDb
    OleDb <-->|Read / Write| AccessDB
    CodeBehind -->|Send Overdue Notification| Smtp
```

---

## 👥 2. 使用者角色與流程 (User Flows)

本系統依權限劃分三個核心操作流程：**一般使用者流程**、**系所管理者流程**、**系統管理員流程**。

### A. 一般使用者流程 (Student / Teacher Flow)

一般師生主要進行教室與設備的**預約申請**與**印出借用單**：

```mermaid
sequenceDiagram
    actor User as 一般使用者 (師生)
    participant Login as 登入/註冊頁面<br/>(login.aspx / register.aspx)
    participant Index as 個人首頁<br/>(index.aspx)
    participant Apply as 申請借用頁面<br/>(Apply.aspx)
    participant Print as 借用單頁面<br/>(printForm.aspx)
    participant DB as 資料庫 (AccessDB)

    User->>Login: 輸入帳密 / 註冊帳號
    Login->>DB: 驗證使用者帳密
    DB-->>Login: 驗證成功
    Login->>Index: 導向首頁
    Index->>DB: 讀取個人「尚未歸還」的借用紀錄
    DB-->>Index: 顯示借用清單

    %% 申請借用
    User->>Index: 點擊「申請借用」
    Index->>Apply: 載入申請頁面
    User->>Apply: 選擇日期與查詢條件，點擊查詢
    Apply->>DB: 查詢該日教室/設備的已借用與不開放時段
    DB-->>Apply: 回傳課表狀態 (粉紅:已借 / 灰:不開放)
    User->>Apply: 勾選可用時段，填寫「借用用途」，送出申請
    Apply->>DB: 寫入 [bw] 借用紀錄表 (Transaction 防止衝突)
    DB-->>Apply: 儲存成功
    Apply->>Index: 重導回個人首頁

    %% 列印借用單
    User->>Index: 點擊「列印」特定借用單
    Index->>Print: 開啟新分頁，載入 printForm?bid={id}
    Print->>DB: 讀取該筆借用細節 (借用人資訊、借用時間、用途)
    DB-->>Print: 回傳細節
    Print-->>User: 顯示借用單格式，供瀏覽器輸出為 PDF 或紙本列印
```

---

### B. 系所管理者流程 (Department Admin Flow)

系所管理員負責審核所屬單位的教室與設備、設定不開放時間，以及確認歸還：

```mermaid
sequenceDiagram
    actor Dept as 系所管理者
    participant Login as 系所登入<br/>(/dept/login.aspx)
    participant Default as 系所首頁<br/>(/dept/default.aspx)
    participant Record as 借用歷史審核<br/>(/dept/BorrowRecord.aspx)
    participant Class as 教室/設備時段設定<br/>(/dept/AllClassroom.aspx)
    participant DB as 資料庫 (AccessDB)

    Dept->>Login: 帳密登入
    Login->>DB: 驗證 dept 資料表
    DB-->>Login: 登入成功 (存入 Session['deptid'])
    Login->>Default: 導向系所首頁

    %% 今日狀態與逾期
    Default->>DB: 查詢今日申請數與逾期未歸還統計
    DB-->>Default: 渲染統計數字與逾期清單 (姓名/電話/用途)

    %% 時段限制與課表變更
    Dept->>Class: 管理教室與課表
    Class->>DB: 載入最近 7 日之二維借用課表
    DB-->>Class: 顯示借用狀況
    Dept->>Class: 點擊「變更可借用時段」，勾選並儲存
    Class->>DB: 更新 [cla_ava] (以星期與節次限定不開放時間)
    DB-->>Class: 儲存成功

    %% 歸還審核
    Dept->>Record: 進入借用歷史與審核頁面
    Record->>DB: 查詢所屬系所的借用清單 (未歸還/已歸還)
    DB-->>Record: 顯示 GridView 清單，附帶「確認歸還」按鈕
    Dept->>Record: 點擊「確認歸還」(或取消確認)
    Record->>DB: 更新 [bw] 的 rdate (設定為今日) 與確認系所 ID
    DB-->>Record: 更新頁面顯示
```

---

### C. 系統管理員流程 (System Admin Flow)

系統管理員擁有最高權限，負責初始化系所、教室與設備資料：

```mermaid
graph LR
    Admin[系統管理員] -->|登入| LoginAdmin["SYS_ADMIN/login.aspx"]
    LoginAdmin -->|驗證通過| DefaultAdmin["SYS_ADMIN/default.aspx<br/>(管理面板)"]
    
    DefaultAdmin -->|管理系所| AddDept["AddDept.aspx<br/>(新增系所帳密與信箱)"]
    DefaultAdmin -->|管理教室| AddClass["AddClassroom.aspx<br/>(新增教室與上傳照片)"]
    DefaultAdmin -->|管理設備| AddDevice["AddDevice.aspx<br/>(新增設備與上傳照片)"]

    AddClass -->|寫入資料庫| DB[("AccessDB<br/>[classroom] & [cla_ava]")]
    AddClass -->|儲存照片| File1["/img/classroom/{cid}.jpg"]

    AddDevice -->|寫入資料庫| DB
    AddDevice -->|儲存照片| File2["/img/device/{did}.jpg"]

    AddDept -->|寫入資料庫| DB
```

---

## 📧 3. 逾期通知信件排程流程 (Overdue Email Notification)

系統內建逾期通知模組 (`MailWeb.aspx`)，可由伺服器排程工作 (Windows Task Scheduler) 定期觸發：

```mermaid
graph TD
    Start["排程觸發 /MailWeb.aspx"] --> DB_Query["查詢資料庫 [bw] 借用紀錄表"]
    DB_Query --> Filter{"篩選條件：<br/>rdate is null (未歸還)<br/>mailed = false (尚未寄信通知)<br/>借用時間已小於或等於目前時間"}
    
    Filter -->|有逾期紀錄| Loop["遍歷逾期紀錄"]
    Loop --> Mail1["發送郵件給借用人 (提示速至系辦歸還)"]
    Loop --> Mail2["發送郵件給該系所管理者 (副本備查)"]
    
    Mail1 & Mail2 --> DB_Update["更新資料庫：將該筆記錄的 mailed 欄位設為 true"]
    DB_Update --> Next{"是否還有下一筆？"}
    Next -->|是| Loop
    Next -->|否| End["發送完成，結束執行"]
    
    Filter -->|無逾期紀錄| End
```

---

## 📌 4. 關鍵網頁頁面清單 (Page Routing Reference)

| 實體路徑 (URL) | 權限要求 | 頁面用途與說明 |
| :--- | :--- | :--- |
| `/login.aspx` | 無 | 一般使用者登入頁面 |
| `/register.aspx` | 無 | 一般使用者註冊頁面 (寫入 `user` 表) |
| `/index.aspx` | 一般使用者 | 個人首頁，顯示目前借用中的項目與列印借用單按鈕 |
| `/Apply.aspx` | 一般使用者 | 線上借用申請頁面，即時查詢可用時段並送出申請 |
| `/printForm.aspx` | 一般使用者 | 借用單列印版面 (傳入 `bid` 與類型參數) |
| `/MailWeb.aspx` | 伺服器/管理員 | 系統自動發送逾期通知信之觸發端點 |
| `/dept/login.aspx` | 無 | 系所管理員登入頁面 |
| `/dept/default.aspx` | 系所管理者 | 系所儀表板，顯示今日借用動態與逾期清單 |
| `/dept/BorrowRecord.aspx` | 系所管理者 | 系所歷史借用紀錄查詢與歸還審核 (Toggle rdate) |
| `/dept/AllClassroom.aspx` | 系所管理者 | 該系所教室借用狀態二維課表檢視與開放時段修改 |
| `/dept/AllDevice.aspx` | 系所管理者 | 該系所設備借用狀態二維課表檢視與開放時段修改 |
| `/SYS_ADMIN/login.aspx` | 無 | 系統管理員登入頁面 |
| `/SYS_ADMIN/default.aspx` | 系統管理員 | 管理主頁，可查詢/重設使用者與系所管理員之密碼 |
| `/SYS_ADMIN/AddDept.aspx` | 系統管理員 | 新增系所管理員帳號與基本資料 |
| `/SYS_ADMIN/AddClassroom.aspx`| 系統管理員 | 新增教室資料，上傳教室照片並初始化 `cla_ava` 表 |
| `/SYS_ADMIN/AddDevice.aspx` | 系統管理員 | 新增設備資料，上傳設備照片並初始化 `dev_ava` 表 |
