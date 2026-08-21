# 中山大學教室與設備借用系統 (NSYSU Classroom & Device Booker)

本專案是一個基於 **ASP.NET Web Forms** 開發的教室與設備借用管理系統。系統旨在為學校師生、系所管理員與系統管理員提供一個便捷、高效的線上預約與審核平台，簡化傳統紙本借用與人工作業流程。

---

## 🛠️ 技術棧與環境要求

* **後端框架**：ASP.NET Web Forms (.NET Framework 4.7.2)
* **開發語言**：C#
* **資料庫**：Microsoft Access Database (`.accdb`)，使用 OLE DB 連線驅動 (`Microsoft.ACE.OLEDB.12.0`)
* **前端技術**：HTML5, CSS3, JavaScript, Bootstrap, ASP.NET 伺服器控制項 (GridView 等)
* **開發工具**：Visual Studio 2019 / 2022
* **運行伺服器**：IIS Express (本地偵錯) 或 IIS 伺服器

---

## 👥 系統角色與核心功能

本系統共分為三種角色，各自擁有不同的管理與操作權限：

### 1. 一般使用者 (Student / Teacher)
一般師生在註冊並登入後，可進行基本的查詢與借用申請：
* **首頁儀表板**：顯示個人目前「已借用且未歸還」的教室與設備清單，並提供「列印借用單」功能。
* **瀏覽教室/設備**：即時查看所有教室與設備的詳細資訊（如容納人數、位置、照片與配備）。
* **線上申請借用**：選擇特定日期與時段（節次），填寫借用用途以送出申請。
* **借用單列印**：產生美觀的 PDF 或網頁格式借用申請表（`printForm.aspx`），方便輸出紙本簽核。

### 2. 系所管理者 (Department Admin) ─ `/dept/`
系所管理人員負責管理該系所名下的教室與設備，並審核借用流程：
* **今日狀態統計**：首頁統計「今日借用申請數」與「逾期未歸還筆數」，快速掌握當日借用動態。
* **設備與教室狀態**：
  * 查看即時租借課表（以二維課表格式呈現最近一週各節次的租借狀態，已借用時段顯示為粉紅色，不開放時段顯示為灰色）。
  * 變更可借用時段：可自由設定哪些星期的哪些節次開放或不開放借用。
* **借用審核與簽收**：針對借用人歸還教室或設備時進行「確認歸還 / 取消確認」操作。
* **歷史紀錄查詢**：可依借用單號、借用人 ID、教室名稱或特定日期區間，篩選並查詢歷史借用紀錄。
* **密碼管理**：系所管理員可自行修改登入密碼。

### 3. 系統管理員 (System Admin) ─ `/SYS_ADMIN/`
負責全校系統的基礎資料維護：
* **系所管理**：新增與編輯各學術/行政單位（系所）之管理帳號。
* **教室管理**：新增教室基本資料（名稱、位置、容納人數），並支援上傳實體教室照片。
* **設備管理**：新增校產設備資料（名稱、廠牌、型號等）以供出借。

---

## 📂 專案目錄結構

```text
LAB_NSYSUClassroomBooker/
├── LAB_NSYSUClassroomBooker.sln  # Visual Studio 方案檔
└── DS_LAB2/                      # 網頁程式主要原始碼目錄
    ├── App_Data/
    │   └── AccessDB.accdb        # 系統 Access 資料庫檔
    ├── App_Start/                # 路由與資源包 (Bundle) 設定
    ├── Content/                  # CSS 樣式表
    ├── Scripts/                  # JavaScript 腳本檔
    ├── css/                      # 客製化 CSS 樣式
    ├── img/                      # 系統靜態圖片與教室照片目錄
    │   └── classroom/            # 存放上傳的教室實體照片 (檔名為教室ID.jpg)
    ├── dept/                     # 系所管理者後台網頁
    │   ├── default.aspx          # 系所儀表板 (逾期統計)
    │   ├── AllClassroom.aspx     # 管理教室課表與開放時段
    │   ├── AllDevice.aspx        # 管理設備狀態
    │   └── BorrowRecord.aspx     # 審核與歷史紀錄查詢
    ├── SYS_ADMIN/                # 系統管理員後台網頁
    │   ├── default.aspx          # 系統管理主頁
    │   ├── AddClassroom.aspx     # 新增教室
    │   ├── AddDept.aspx          # 新增系所
    │   └── AddDevice.aspx        # 新增設備
    ├── Site.Master               # 前台母片頁面 (導覽列與頁尾)
    ├── index.aspx                # 前台個人首頁
    ├── login.aspx / register.aspx# 一般用戶登入與註冊頁面
    ├── Apply.aspx                # 送出借用申請頁面
    ├── printForm.aspx            # 產生借用單列印頁面
    └── Web.config                # 系統組態檔 (內含資料庫連接字串)
```

---

## 🚀 快速開始與部署指南

### Step 1: 複製專案
將專案複製（Clone）或下載解壓縮至本地端工作目錄。

### Step 2: 安裝與配置 Access Database Engine
由於系統使用 Microsoft Access 驅動程式，執行本專案的電腦必須註冊相關驅動。
1. 若在 Visual Studio 執行時遇到類似 `未在本地電腦上註冊 'Microsoft.ACE.OLEDB.12.0' 提供者` 的錯誤，請至微軟官方網站下載並安裝 **Microsoft Access Database Engine 2010 或 2016 轉轉驅動程式 (Redistributable)**。
2. ⚠️ **注意**：下載時請根據您 Visual Studio / IIS Express 的位元版本（32位元或64位元）選擇對應的安裝檔（通常 Visual Studio 本地偵錯使用的是 32 位元 / x86 版本）。

### Step 3: 用 Visual Studio 開啟專案
1. 雙擊 `LAB_NSYSUClassroomBooker.sln` 檔案開啟 Visual Studio。
2. 確認方案總管中已載入 `DS_LAB2` 專案。
3. 檢查 `DS_LAB2/Web.config` 中的連線字串：
   ```xml
   <connectionStrings>
       <add name="AccessDB" connectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\AccessDB.accdb" providerName="System.Data.OleDb" />
   </connectionStrings>
   ```
   *註：`|DataDirectory|` 為 ASP.NET 內建變數，會自動指向 `App_Data` 資料夾。*

### Step 4: 建置與執行
1. 在 Visual Studio 上方點擊 **建置方案 (Build Solution)**，確保專案無編譯錯誤。
2. 點擊 **IIS Express (Google Chrome / Edge)** 或按下 `F5` 啟動專案。
3. 瀏覽器將會自動開啟並導向首頁 (`index.aspx`)。

---

## 🔑 預設測試帳號

為方便快速測試，您可以使用以下帳號進行登入體驗：

| 角色 | 登入網址 | 帳號 | 密碼 |
| :--- | :--- | :--- | :--- |
| **一般使用者** | `/login.aspx` | `B103040001` (範例) | *(請參考資料庫 user 資料表)* |
| **系所管理者** | `/dept/login.aspx` | `1` (範例系所ID) | *(請參考資料庫 dept 資料表)* |
| **系統管理員** | `/SYS_ADMIN/login.aspx` | `admin` | `admin` (或資料庫中設定的值) |

---

## 🔒 授權條款
本專案為學術練習/實驗室專案，僅供學習與學術交流使用。
