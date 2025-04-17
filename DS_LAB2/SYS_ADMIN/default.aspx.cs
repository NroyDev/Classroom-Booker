using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2.SYS_ADMIN
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["isloggedin_admin"] == null || !(bool)Session["isloggedin_admin"])
            {
                Response.Redirect("login.aspx");
            }

        }


        // -------------------------------------------- 讀取資料表 --------------------------------------------
        protected void gvUser_Load()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = "SELECT [id],[realname],[email],[phone],[roomid] FROM [user] WHERE [id] like @id;";
            using (OleDbConnection connection = new OleDbConnection(connectionString)) { 
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@id", "%" + tbKeyword.Text + "%");
                try
                {
                    connection.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvUser.DataSource = dataTable;
                    gvUser.DataBind();
                }
                catch (Exception ex) { 
                    Response.Write(ex.ToString());
                }
            }
        }
        protected void gvDept_Load()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = "SELECT [dept_id],[dept_name],[account],[email] FROM [dept] WHERE [account] like @acco;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@acco", "%" + tbKeyword.Text + "%");
                try
                {
                    connection.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvDept.DataSource = dataTable;
                    gvDept.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
        // -------------------------------------------- Change password btn click --------------------------------------------

        protected void changepwd_user_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            LabelUserid.Text = gvr.Cells[0].Text;
        }

        protected void btnChangeUserPwd_Click(object sender, EventArgs e)
        {
            if (tbPwdUser.Text != tbPwdUserConfirm.Text)
            {
                Response.Write("<script>window.alert(\"新密碼與確認新密碼的密碼不相符!\");</script>");
            }
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string update = "UPDATE [user] SET [password] = @pwd WHERE [id] = @id";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(update, connection);
                command.Parameters.AddWithValue("@pwd", tbPwdUser.Text);
                command.Parameters.AddWithValue("@id", LabelUserid.Text);
                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("Update pwd Failed!");
                    }
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }



        protected void changepwd_dept_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            LabelDeptid.Text = gvr.Cells[0].Text;
        }

        protected void btnChangeDeptPwd_Click(object sender, EventArgs e)
        {
            if (tbPwdDept.Text!=tbPwdDeptConfirm.Text)
            {
                Response.Write("<script>window.alert(\"新密碼與確認新密碼的密碼不相符!\");</script>");
                return;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string update = "UPDATE [dept] SET [password]=@pwd WHERE [dept_id] = @id";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(update, connection);
                command.Parameters.AddWithValue("@pwd", tbPwdDept.Text);
                command.Parameters.AddWithValue("@id", LabelDeptid.Text);
                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("Update pwd Failed!");
                    }
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if(rblSearchConstraint.SelectedIndex == 0)
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                gvUser_Load();
            }
            else if(rblSearchConstraint.SelectedIndex == 1)
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                gvDept_Load();
            }
        }

        // -------------------------------------------- Logout button --------------------------------------------

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("login.aspx");
        }

        // -------------------------------------------- Change --------------------------------------------
        protected void changeInfo_dept_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            LabelDeptid_ChangeInfo.Text = gvr.Cells[0].Text;
            tbDeptNewName.Text = gvr.Cells[1].Text;
            tbDeptNewAcco.Text = gvr.Cells[2].Text;
            tbDeptNewEmail.Text = gvr.Cells[3].Text;
        }

        protected void btnChangeDeptInfo_Click(object sender, EventArgs e)
        {
            string connecitonString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string update = "UPDATE [dept] SET [dept_name]=@name,[account]=@acco,[email]=@email WHERE [dept_id]=@deptid;";
            using (OleDbConnection connection = new OleDbConnection(connecitonString))
            {
                OleDbCommand command = new OleDbCommand(update, connection);
                command.Parameters.AddWithValue("@name", tbDeptNewName.Text);
                command.Parameters.AddWithValue("@acco", tbDeptNewAcco.Text);
                command.Parameters.AddWithValue("@email", tbDeptNewEmail.Text);
                command.Parameters.AddWithValue("@deptid", LabelDeptid_ChangeInfo.Text);
                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("UPDATE FAILED!");
                    }
                    else
                    {
                        Response.Redirect(Request.RawUrl);
                    }
                }
                catch(Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
    }
}