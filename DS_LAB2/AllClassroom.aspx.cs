using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2
{
    public partial class AllClassroom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvClassroom_Load();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvClassroom_Load();
            gvBorrow_status.DataBind();
        }
        protected void gvClassroom_Load()
        {
            string connecitonString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = "SELECT cid, cname,location,capacity,dept_name FROM classroom INNER JOIN dept ON classroom.dept = dept.dept_id WHERE cname LIKE @search;";
            using (OleDbConnection connection = new OleDbConnection(connecitonString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@search", "%" + tbCnameKeyword.Text + "%");
                try
                {
                    connection.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gvClassroom.DataSource = dataTable;
                    gvClassroom.DataBind();
                }
                catch (Exception ex) { 
                    Response.Write(ex.Message);
                }
            }
        }

        // -------------------------------------------------- 查詢教室可借用狀況 --------------------------------------------------
        protected void btnSearchAvaliable(object sender,EventArgs e)    //gvClassroom上按鈕執行的function
        {
            //Get the button that raised the event
            Button btn = (Button)sender;
            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            gvBorrow_status_Load(gvr.Cells[0].Text, gvr.Cells[1].Text);
        }
        protected void gvBorrow_status_Load(string cid, string cname)     //讀取指定cid 近7日借用資料
        {
            DataTable rstData = new DataTable();
            rstData.Columns.Add("教室 " + cname);
            rstData.Columns.Add("A");
            rstData.Columns.Add("1");
            rstData.Columns.Add("2");
            rstData.Columns.Add("3");
            rstData.Columns.Add("4");
            rstData.Columns.Add("B");
            rstData.Columns.Add("5");
            rstData.Columns.Add("6");
            rstData.Columns.Add("7");
            rstData.Columns.Add("8");
            rstData.Columns.Add("9");
            rstData.Columns.Add("C");
            rstData.Columns.Add("D");
            rstData.Columns.Add("E");
            rstData.Columns.Add("F");
            rstData.Columns.Add("G");

            int searchDays = 7;

            DateTime dataTime_temp = DateTime.Now;
            for (int i = 0; i < searchDays; i++)
            {
                DataRow OneRow = rstData.NewRow();
                OneRow[0] = dataTime_temp.ToString("yyyy-MM-dd");
                rstData.Rows.Add(OneRow);
                dataTime_temp = dataTime_temp.AddDays(1);
            }
            gvBorrow_status.DataSource = rstData;
            gvBorrow_status.DataBind();


            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = "SELECT starttime, endtime FROM bw WHERE cid = @cid AND bdate = @bdate";
            string query2 = "SELECT not_avaliable FROM cla_ava WHERE cid = @cid AND @week=week;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    foreach (GridViewRow row in gvBorrow_status.Rows)
                    {
                        OleDbCommand command = new OleDbCommand(query, connection);
                        command.Parameters.AddWithValue("@cid", cid);
                        command.Parameters.AddWithValue("@bdate", row.Cells[0].Text);

                        OleDbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int start = reader.GetDateTime(0).Hour;
                            int end = reader.GetDateTime(1).Hour;
                            for (int i = start - 7; i < end - 7; i++)
                            {
                                row.Cells[i + 1].BackColor = Color.LightPink;
                                Label labeltemp = new Label();
                                labeltemp.Text = "借";
                                row.Cells[i + 1].Controls.Add(labeltemp);
                            }
                        }
                        reader.Close();

                        command = new OleDbCommand(query2,connection);
                        command.Parameters.AddWithValue("@cid", cid);
                        command.Parameters.AddWithValue("@week", Convert.ToDateTime(row.Cells[0].Text).DayOfWeek);
                        reader = command.ExecuteReader();
                        while (reader.Read()) { 
                            string temp = reader.GetString(0);
                            if(temp == "-1")
                            {
                                continue;
                            }
                            var list = temp.Split(',');
                            foreach (var item in list) {
                                int i = Convert.ToInt32(item);
                                row.Cells[i + 1].BackColor = Color.DarkGray;
                                Label labeltemp = new Label();
                                labeltemp.Text = "不";
                                row.Cells[i + 1].Controls.Add(labeltemp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }


        }

        // -------------------------------------------------- 查看教室照片 --------------------------------------------------
        protected void btnImage_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;
            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            if(System.IO.File.Exists(HttpContext.Current.Server.MapPath("img/classroom/" + gvr.Cells[0].Text + ".jpg")))
            {
                imgClassroom.ImageUrl = "~/img/classroom/" + gvr.Cells[0].Text + ".jpg";
            }
            else
            {
                imgClassroom.ImageUrl = "img/404img.png";
            }
        }
    }
}