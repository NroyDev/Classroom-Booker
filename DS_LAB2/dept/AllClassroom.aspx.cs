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

namespace DS_LAB2.dept
{
    public partial class AllClassroom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbSearchStartDate.Text = $"{DateTime.Now:yyyy-MM-dd}";
                tbSearchEndDate.Text = $"{DateTime.Now.AddDays(7):yyyy-MM-dd}";
                gvClassroom_Load();
            }
        }

        // --------------------------------------------------------- 讀入教室資料         ---------------------------------------------------------
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvClassroom_Load();
            gvBorrow_status.DataBind();
        }
        protected void gvClassroom_Load()
        {
            string connecitonString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = "SELECT cid, cname,location,capacity FROM classroom WHERE cname LIKE @search and dept = @dept;";
            using (OleDbConnection connection = new OleDbConnection(connecitonString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@search", "%" + tbCnameKeyword.Text + "%");
                command.Parameters.AddWithValue("@dept", Session["deptid"]);
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

        // 
        // --------------------------------------------------------- 查詢指定時段借用狀況 ---------------------------------------------------------
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
            DateTime startdate, enddate;
            if (!DateTime.TryParse(tbSearchStartDate.Text, out startdate) || !DateTime.TryParse(tbSearchEndDate.Text, out enddate))
            {
                Response.Write("PARSE FAILED");
                return;
            }
            for (DateTime dateTime_temp = startdate; dateTime_temp <= enddate; dateTime_temp = dateTime_temp.AddDays(1))
            {
                rstData.Columns.Add(dateTime_temp.ToString("yyyy-MM-dd"));
            }

            // 製作時間的Row
            {
                DataRow OneRow;
                OneRow = rstData.NewRow();
                OneRow[0] = "A";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "1";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "2";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "3";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "4";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "B";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "5";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "6";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "7";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "8";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "9";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "C";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "D";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "E";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "F";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "G";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = "SELECT starttime, endtime,berid,bid FROM bw WHERE cid = @cid AND bdate = @bdate";
            string query2 = "SELECT not_avaliable FROM cla_ava WHERE cid = @cid AND @week=week;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    for (int i = 1; i < rstData.Columns.Count; i++) {    // i => column
                        OleDbCommand command = new OleDbCommand(query, connection);
                        command.Parameters.AddWithValue("@cid", cid);
                        command.Parameters.AddWithValue("@bdate", rstData.Columns[i].ColumnName);

                        OleDbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int start = reader.GetDateTime(0).Hour;
                            int end = reader.GetDateTime(1).Hour;
                            for (int j = start - 7; j < end - 7; j++)       // j => row
                            {
                                rstData.Rows[j][i] = reader.GetString(2) + "\n(" + reader.GetInt32(3).ToString() + ")";
                            }
                        }
                        reader.Close();


                        command = new OleDbCommand(query2, connection);
                        command.Parameters.AddWithValue("@cid", cid);
                        command.Parameters.AddWithValue("@Wweek", Convert.ToDateTime(rstData.Columns[i].ColumnName).DayOfWeek);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string temp = reader.GetString(0);
                            if (temp == "-1")
                            {
                                continue;
                            }
                            var list = temp.Split(',');

                            foreach (var item in list)
                            {
                                int j = Convert.ToInt32(item);
                                rstData.Rows[j][i] = "不";
                            }
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
            gvBorrow_status.DataSource = rstData;
            gvBorrow_status.DataBind();

            GridView gv = gvBorrow_status;
            foreach(GridViewRow row in gv.Rows)
            {
                for(int i = 1; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Text == "不")
                    {
                        row.Cells[i].BackColor = Color.DarkGray;
                    }
                    else if (row.Cells[i].Text != "&nbsp;")
                    {
                        row.Cells[i].BackColor = Color.LightPink;
                    }
                }
            }
        }


        // --------------------------------------------------------- 變更可借用時段        ---------------------------------------------------------
        protected void ChangeAvaliable_Click(object sender, EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;
            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            Label_ava_cid.Text = gvr.Cells[0].Text;

            gvClassroomAvaliable_Load();
            gvClassroomAvaliable_Status_Load();
        }
        protected void gvClassroomAvaliable_Load()      //把gridview的輪廓建構出來
        {   
            // columns
            GridView gv = gvClassroomAvaliable;
            DataTable rstData = new DataTable();
            rstData.Columns.Add("節次");
            rstData.Columns.Add("禮拜日", typeof(bool));
            rstData.Columns.Add("禮拜一", typeof(bool));
            rstData.Columns.Add("禮拜二", typeof(bool));
            rstData.Columns.Add("禮拜三", typeof(bool));
            rstData.Columns.Add("禮拜四", typeof(bool));
            rstData.Columns.Add("禮拜五", typeof(bool));
            rstData.Columns.Add("禮拜六", typeof(bool));

            // 製作時間的Row
            {
                DataRow OneRow;
                OneRow = rstData.NewRow();
                OneRow[0] = "A";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "1";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "2";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "3";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "4";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "B";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "5";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "6";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "7";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "8";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "9";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "C";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "D";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "E";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "F";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
                OneRow[0] = "G";
                rstData.Rows.Add(OneRow);
                OneRow = rstData.NewRow();
            }


            gv.DataSource = rstData;
            gv.DataBind();
        }
        protected void gvClassroomAvaliable_RowDataBound(object sender, GridViewRowEventArgs e) //把勾選欄位打開
        {
            //只有當資料行（非 header）時才執行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    TableCell c = e.Row.Cells[i];
                    CheckBox ck = c.Controls[0] as CheckBox;
                    ck.Enabled = true;
                }
            }
        }
        protected void gvClassroomAvaliable_Status_Load()   //從資料庫讀取勾選狀態
        {
            GridView gv = gvClassroomAvaliable;
            int cid = Convert.ToInt32(Label_ava_cid.Text);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = "SELECT week,not_avaliable FROM cla_ava WHERE cid = @cid";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@cid", cid);
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string temp = reader.GetString(1);
                        if (temp == "-1")
                        {
                            continue;
                        }

                        string[] list = temp.Split(',');
                        int i = reader.GetInt32(0);
                        foreach (string item in list)
                        {
                            int j = Convert.ToInt32(item);
                            CheckBox cb = gv.Rows[j].Cells[i + 1].Controls[0] as CheckBox;
                            cb.Checked = true;

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
        protected void BtnSaveChange_Click(object sender, EventArgs e)  //按下儲存按鈕後 將狀態存回資料庫中
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string update = "UPDATE cla_ava SET not_avaliable = @not_avaliable WHERE cid = @cid AND week = @week";
            GridView gv = gvClassroomAvaliable;
            int cid = Convert.ToInt32(Label_ava_cid.Text);
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    for (int j = 1; j < 8; j++)
                    {
                        OleDbCommand command = new OleDbCommand(update, connection);
                        List<int> list = new List<int>();
                        for (int i = 0; i < gv.Rows.Count; i++)
                        {
                            CheckBox cb = gv.Rows[i].Cells[j].Controls[0] as CheckBox;
                            if (cb.Checked)
                            {
                                list.Add(i);
                            }
                        }
                        string not_avaliable = "-1";
                        if (list.Count > 0)
                        {
                            not_avaliable = string.Join(",", list.ToArray());
                        }

                        command.Parameters.AddWithValue("@bot_avaliable", not_avaliable);
                        command.Parameters.AddWithValue("@cid", cid);
                        command.Parameters.AddWithValue("@week", j - 1);
                        if (command.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Update Failed");
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

            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/img/classroom/" + gvr.Cells[0].Text + ".jpg")))
            {
                imgClassroom.ImageUrl = "~/img/classroom/" + gvr.Cells[0].Text + ".jpg";
            }
            else
            {
                imgClassroom.ImageUrl = "~/img/404img.png";
            }
        }

    }
}