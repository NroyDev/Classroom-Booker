using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2
{
    public partial class printForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isloggedin"] == null || (bool)Session["isloggedin"] == false)
            {
                Response.Redirect("login.aspx");
            }
            FillInForm();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "printForm_comment", "print()", true);
        }

        protected void FillInForm()
        {
            string bid = Request.QueryString["bid"];
            string cd = Request.QueryString["cd"];

            if (cd == "c")
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
                string query = "SELECT bw.cid,cname,usage,bdate,starttime,endtime,berid,phone,email FROM (bw INNER JOIN classroom ON bw.cid=classroom.cid) INNER JOIN [user] ON bw.[berid]=[user].[id] WHERE bid = @bid";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@bid", bid);
                    try
                    {
                        connection.Open();
                        OleDbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetString(6) != Session["userid"].ToString())
                            {
                                Response.Redirect("index.aspx");
                            }
                            Label_id_name.Text = $"編號 {reader.GetInt32(0)} / 名稱 {reader.GetString(1)}";
                            Label_Today.Text = DateTime.Now.ToString("yyyy/MM/dd");
                            Label_usage.Text = reader.GetString(2);
                            Label_start.Text = $"自 {reader.GetDateTime(3).ToString("yyyy 年 MM 月 dd 日")}{reader.GetDateTime(4).ToString(" HH 時 mm 日")}";
                            Label_end.Text = $"至 {reader.GetDateTime(3).ToString("yyyy 年 MM 月 dd 日")}{reader.GetDateTime(5).ToString(" HH 時 mm 日")}";
                            Label_berid.Text = reader.GetString(6);
                            cbClassroom.Checked = true;
                            Label_phone.Text = reader.GetString(7);
                            Label_email.Text = reader.GetString(8);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                }
            }
            else if(cd=="d")
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
                string query = "SELECT bw.did,dname,usage,bdate,starttime,endtime,berid,phone,email FROM (bw INNER JOIN device ON bw.did=device.did) INNER JOIN [user] ON bw.[berid]=[user].[id] WHERE bid = @bid";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@bid", bid);
                    try
                    {
                        connection.Open();
                        OleDbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetString(6) != Session["userid"].ToString())
                            {
                                Response.Redirect("index.aspx");
                            }
                            Label_id_name.Text = $"編號 {reader.GetInt32(0)} / 名稱 {reader.GetString(1)}";
                            Label_Today.Text = DateTime.Now.ToString("yyyy/MM/dd");
                            Label_usage.Text = reader.GetString(2);
                            Label_start.Text = $"自 {reader.GetDateTime(3).ToString("yyyy 年 MM 月 dd 日")}{reader.GetDateTime(4).ToString(" HH 時 mm 日")}";
                            Label_end.Text = $"至 {reader.GetDateTime(3).ToString("yyyy 年 MM 月 dd 日")}{reader.GetDateTime(5).ToString(" HH 時 mm 日")}";
                            Label_berid.Text = reader.GetString(6);
                            cbDevice.Checked = true;
                            Label_phone.Text = reader.GetString(7);
                            Label_email.Text = reader.GetString(8);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                }
            }
        }
    }
}