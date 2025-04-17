using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2.SYS_ADMIN
{
    public partial class AddDevice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isloggedin_admin"] == null || !(bool)Session["isloggedin_admin"])
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                ddlDevice_Load();
            }

        }
        protected void ddlDevice_Load()
        {
            string connectionStirng = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = "SELECT [dept_id],[dept_name] FROM [dept];";
            using (OleDbConnection connection = new OleDbConnection(connectionStirng))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    ddlDevice.DataSource = reader;
                    ddlDevice.DataTextField = "dept_name";
                    ddlDevice.DataValueField = "dept_id";
                    ddlDevice.DataBind();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }

                ddlDevice.Items.Insert(0, "<-- 選擇系辦 -->");
            }
        }

        protected void btnAddDevice_Click(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string insert = "INSERT INTO [device] ([dname],[dept]) VALUES (@name,@id);";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(insert, connection);
                command.Parameters.AddWithValue("@name", tbDname.Text);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(ddlDevice.SelectedValue));
                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("INSERT FAILED");
                    }


                    string did = "-1";
                    string query = "SELECT TOP 1 did FROM device ORDER BY did DESC;";
                    OleDbCommand command1 = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command1.ExecuteReader();
                    reader.Read();
                    did = reader.GetInt32(0).ToString();
                    reader.Close();

                    string fileName = Server.HtmlEncode(FileUpload_DeviceImg.FileName);
                    string extension = System.IO.Path.GetExtension(fileName);
                    if (FileUpload_DeviceImg.HasFile && (extension == ".jpg" || extension == ".png"))
                    {
                        string appPath = Request.PhysicalApplicationPath;//取得目錄完整位址
                        string savePath = appPath + "img/device/" + did + ".jpg";
                        FileUpload_DeviceImg.SaveAs(savePath);
                    }

                    string insertAva = "INSERT INTO [dev_ava] (did,week,not_avaliable) VALUES (@did,@week,@na);";
                    for (int i = 0; i <= 6; i++)
                    {
                        OleDbCommand command2 = new OleDbCommand(insertAva, connection);
                        command2.Parameters.AddWithValue("@did", Convert.ToInt32(did));
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