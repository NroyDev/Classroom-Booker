using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2.dept
{
    public partial class changepwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(tbNewPwd.Text != tbNewPwdConfirm.Text)
            {
                LabelErrorMsg.Text = "錯誤! 新密碼不相符(確認新密碼與新密碼欄位的資料不相符)";
                return;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string verifySQL = "SELECT [dept_id] FROM [dept] WHERE [dept_id] = @deptid AND [password] = @oldpassword;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(verifySQL, connection);
                command.Parameters.AddWithValue("@deptid", Session["deptid"]);
                command.Parameters.AddWithValue("@oldpassword", tbCurrentPwd.Text);
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        string changepwdSQL = "UPDATE [dept] SET [password] = @newpassword WHERE dept_id = @deptid";
                        OleDbCommand command1 = new OleDbCommand(changepwdSQL, connection);
                        command1.Parameters.AddWithValue("@newpassword", tbNewPwd.Text);  // Set the new password
                        command1.Parameters.AddWithValue("deptid", Session["deptid"]);     // Set the dept_id
                        if (command1.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("UPDATE ERROR!");
                        }
                        LabelErrorMsg.Text = "成功修改密碼";
                        Response.Redirect("default.aspx");
                    }
                    else
                    {
                        LabelErrorMsg.Text = "錯誤的密碼!";
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
    }
}