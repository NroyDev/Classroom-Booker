using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isloggedin"] != null && (bool)Session["isloggedin"])
            {
                // logged in
                Response.Redirect("index.aspx");
            }
        }

        protected bool isVaild(out string errorMsg)
        {
            errorMsg = "";
            if (userTextBox.Text == "")
            {
                errorMsg += "學號或職位編號不可為空!\n";
            }
            if (passwordTextBox.Text == "")
            {
                errorMsg += "密碼欄位不可為空!\n";
            }

            if (errorMsg != "")
            {
                return false;
            }
            return true;
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            if (isVaild(out errorMsg))
            {
                ServerMSGLabel.Text = "";
                //資料庫連接字串
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
                string loginquery = "SELECT [id],[realname] FROM [user] WHERE ([id]=(@id) AND [password]=(@password));";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(loginquery, connection);
                    command.Parameters.AddWithValue("@id", userTextBox.Text);
                    command.Parameters.AddWithValue("@password", passwordTextBox.Text);
                    try
                    {
                        connection.Open();
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                Session["isloggedin"] = true;
                                Session["userid"] = reader["id"].ToString();
                                Session["realname"] = reader["realname"].ToString();
                                Session.Timeout = 60;
                                Response.Redirect("index.aspx");
                            }
                            else
                            {
                                ServerMSGLabel.Text += "錯誤的ID或密碼\n";
                            }
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
            else
            {
                ServerMSGLabel.Text = errorMsg;
            }
        }
    }
}