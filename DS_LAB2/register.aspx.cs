using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isloggedin"] != null && (bool)Session["isloggedin"])
            {
                // logged in
                Response.Redirect("index.aspx");
            }
        }

        protected bool IsVaildSubmit(string connectionString, out string errorMsg)
        {
            errorMsg = "";
            //檢查是否有空的必填欄位
            if (userTextBox.Text == "")
            {
                errorMsg += "學號或職位編號不可為空!\n";
            }
            if (passwordTextBox.Text == "")
            {
                errorMsg += "密碼欄位不可為空!\n";
            }
            if (nameTextBox.Text == "")
            {
                errorMsg += "姓名欄位不可為空!\n";
            }
            if (emailBox.Text == "")
            {
                errorMsg += "E-Mail欄位不可為空!\n";
            }
            if (phoneTextBox.Text == "")
            {
                errorMsg += "電話欄位不可為空!\n";
            }


            //檢查密碼是否正確
            if (passwordConfirmTextBox.Text != passwordTextBox.Text)
            {
                errorMsg += "密碼不相符\n";
            }

            //檢驗ID是否已註冊
            if (userTextBox.Text != "")
            {
                string idquery = "SELECT [id] FROM [user] WHERE [id]=(@id);";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(idquery, connection);
                    command.Parameters.AddWithValue("@id", userTextBox.Text);
                    try
                    {
                        connection.Open();
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        if (result.Rows.Count > 0)
                        {
                            errorMsg += userTextBox.Text + " 已在系統中被註冊過了!\n";
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        ServerMSGLabel.Text = "Server Error: " + ex.Message;
                    }
                    finally
                    {
                        // 確保即使發生錯誤也能關閉資料庫連接
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }

            if (errorMsg != "")
            {
                return false;
            }
            return true;
        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            //資料庫連接字串
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            if (IsVaildSubmit(connectionString, out errorMsg))
            {
                ServerMSGLabel.Text = "";
                string id = userTextBox.Text;
                string password = passwordTextBox.Text;
                string realname = nameTextBox.Text;
                string email = emailBox.Text;
                string phone = phoneTextBox.Text;
                string roomid = roomidBox.Text;

                string insertQuery = "INSERT INTO [user] ([id],[password],[realname],[email],[phone],[roomid]) VALUES (@id,@password,@realname,@email,@phone,@roomid);";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@realname", realname);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@roomid", roomid);
                    try
                    {
                        connection.Open();
                        int rowAffected = command.ExecuteNonQuery();
                        if (rowAffected > 0)
                        {
                            // 成功插入
                            connection.Close();
                            Response.Redirect("login.aspx");
                        }
                        else
                        {
                            ServerMSGLabel.Text = "Server Error: INSERT FAILED";
                        }
                    }
                    catch (Exception ex)
                    {
                        ServerMSGLabel.Text = "Server Error: " + ex.Message;
                    }
                    finally
                    {
                        // 確保即使發生錯誤也能關閉資料庫連接
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            else
            {
                ServerMSGLabel.Text = errorMsg;
                return;
            }
        }
    }
}