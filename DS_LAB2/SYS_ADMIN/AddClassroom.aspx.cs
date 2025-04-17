using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace DS_LAB2.SYS_ADMIN
{
    public partial class AddClassroom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isloggedin_admin"] == null || !(bool)Session["isloggedin_admin"])
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                ddlDept_Load();
            }
        }

        protected void ddlDept_Load()
        {
            string connectionStirng = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = "SELECT [dept_id],[dept_name] FROM [dept];";
            using (OleDbConnection connection = new OleDbConnection(connectionStirng)) {
                OleDbCommand command = new OleDbCommand(query, connection);
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    ddlDept.DataSource = reader;
                    ddlDept.DataTextField = "dept_name";
                    ddlDept.DataValueField = "dept_id";
                    ddlDept.DataBind();
                    reader.Close();
                }
                catch (Exception ex) {
                    Response.Write(ex.ToString());
                }

                ddlDept.Items.Insert(0, "<-- 選擇系辦 -->");
            } 
        }

        protected void btnAddClassroom_Click(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string insert = "INSERT INTO [classroom] ([cname],[location],[capacity],[dept]) VALUES (@name,@loc,@cap,@id);";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(insert, connection);
                command.Parameters.AddWithValue("@name",tbCname.Text);
                command.Parameters.AddWithValue("@loc",tbLocation.Text);
                command.Parameters.AddWithValue("@cap",tbCapacity.Text);
                command.Parameters.AddWithValue("@id",Convert.ToInt32(ddlDept.SelectedValue));
                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("INSERT FAILED");
                    }


                    string cid = "-1";
                    string query = "SELECT TOP 1 cid FROM classroom ORDER BY cid DESC;";
                    OleDbCommand command1 = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command1.ExecuteReader();
                    reader.Read();
                    cid = reader.GetInt32(0).ToString();
                    reader.Close();

                    string fileName = Server.HtmlEncode(FileUpload_Classroom.FileName);
                    string extension = System.IO.Path.GetExtension(fileName);
                    if (FileUpload_Classroom.HasFile && (extension == ".jpg"|| extension == ".png"))
                    {
                        string appPath = Request.PhysicalApplicationPath;//取得目錄完整位址
                        string savePath = appPath + "img/classroom/" + cid + ".jpg";
                        FileUpload_Classroom.SaveAs(savePath);
                    }

                    string insertAva = "INSERT INTO [cla_ava] (cid,week,not_avaliable) VALUES (@cid,@week,@na);";
                    for(int i = 0; i <= 6; i++)
                    {
                        OleDbCommand command2 = new OleDbCommand(insertAva, connection);
                        command2.Parameters.AddWithValue("@cid", Convert.ToInt32(cid));
                        command2.Parameters.AddWithValue("@week", i);
                        command2.Parameters.AddWithValue("na", "-1");
                        command2.ExecuteNonQuery();
                    }

                    Response.Redirect(Request.RawUrl);
                }
                    catch (Exception ex)
                    {
                    Response.Write(ex.ToString());
                }

        }
        }
    }
}