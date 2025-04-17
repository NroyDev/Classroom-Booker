using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2.SYS_ADMIN
{
    public partial class AddDept : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isloggedin_admin"] == null || !(bool)Session["isloggedin_admin"])
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (tbDeptPwd.Text != tbDeptConfirmPwd.Text)
            {
                Response.Write(" 密碼與確認密碼欄位並不相符!! ");
                return;
            }


            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string insert = "INSERT INTO [dept] ([dept_name],[account],[password],[email]) VALUES (@name,@acco,@pwd,@email);";
            using (OleDbConnection conneciton = new OleDbConnection(connectionString)) { 
                OleDbCommand command = new OleDbCommand(insert, conneciton);
                command.Parameters.AddWithValue("@name",tbName.Text);
                command.Parameters.AddWithValue("@acco", tbDeptAcco.Text);
                command.Parameters.AddWithValue("@pwd", tbDeptPwd.Text);
                command.Parameters.AddWithValue("@email",tbEmail.Text);
                try
                {
                    conneciton.Open();
                    if (command.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("INSERT FAILED");
                    }
                    else
                    {
                        Response.Redirect("default.aspx");
                    }
                }
                    catch (Exception ex) {
                    Response.Write(ex.ToString());
                }
            }
        }
    }
}