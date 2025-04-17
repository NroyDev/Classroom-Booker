using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Security.Cryptography;

namespace DS_LAB2
{
    public partial class Apply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvClassroom_Load();
            gvClassroom_Borrow();
            if (cbNeedDevice.Checked)
            {
                gvDevice_Load();
                gvDevice_Borrow();
            }
            else
            {
                gvDevice.DataBind();
            }
            pAfterSearch.Visible = true;
        }

        void gvClassroom_Load()     // 讀取所有教室的清單(即所有教室的名字) 並弄出CheckBox
        {
            DataTable rstData = new DataTable();
            //ADD 20 columns
            rstData.Columns.Add("");
            rstData.Columns.Add("教室");
            rstData.Columns.Add("A", typeof(bool));
            rstData.Columns.Add("1", typeof(bool));
            rstData.Columns.Add("2", typeof(bool));
            rstData.Columns.Add("3", typeof(bool));
            rstData.Columns.Add("4", typeof(bool));
            rstData.Columns.Add("B", typeof(bool));
            rstData.Columns.Add("5", typeof(bool));
            rstData.Columns.Add("6", typeof(bool));
            rstData.Columns.Add("7", typeof(bool));
            rstData.Columns.Add("8", typeof(bool));
            rstData.Columns.Add("9", typeof(bool));
            rstData.Columns.Add("C", typeof(bool));
            rstData.Columns.Add("D", typeof(bool));
            rstData.Columns.Add("E", typeof(bool));
            rstData.Columns.Add("F", typeof(bool));
            rstData.Columns.Add("G", typeof(bool));


            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT cid,cname
                FROM classroom
                WHERE cname LIKE @search
                ORDER BY cname
            ;";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@search", "%" + tbCnameKeyword.Text + "%");
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DataRow OneRow = rstData.NewRow();
                        OneRow[0] = reader["cid"].ToString();
                        OneRow[1] = reader["cname"].ToString();
                        rstData.Rows.Add(OneRow);
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    // 錯誤處理
                    Response.Write("Error: " + ex.Message);
                }
            }

            gvClassroom.DataSource = rstData;
            gvClassroom.DataBind();
        }
        protected void gvClassroom_RowDataBound(object sender, GridViewRowEventArgs e)      //把所有格子打開(DataBind()自動執行)
        {
            e.Row.Cells[0].Visible = false;
            //只有當資料行（非 header）時才執行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    TableCell c = e.Row.Cells[i];
                    CheckBox ck = c.Controls[0] as CheckBox;
                    ck.Enabled = true;
                }
            }
        }

        protected void gvClassroom_Borrow() //讀取該日借用狀況, 關閉被借用的格子
        {
            GridView gv = gvClassroom;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT cid,starttime,endtime
                FROM bw
                WHERE bdate=@bdate
            ;";
            string query2 = "SELECT cid,not_avaliable FROM cla_ava WHERE @week=week;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@bdate", tbDate.Text);
                    connection.Open();

                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foreach (GridViewRow row in gv.Rows)
                        {
                            if (row.Cells[0].Text == reader.GetInt32(0).ToString())
                            {
                                int start = reader.GetDateTime(1).Hour;
                                int end = reader.GetDateTime(2).Hour;
                                for (int i = start - 7; i < end - 7; i++)
                                {
                                    TableCell c = row.Cells[i + 2];
                                    CheckBox ck = c.Controls[0] as CheckBox;
                                    ck.Enabled = false;

                                    ck.Visible = false;
                                    c.BackColor = Color.LightPink;
                                    Label label_bw = new Label();
                                    label_bw.Text = "借";

                                    c.Controls.Add(label_bw);
                                }
                                break;
                            }
                        }
                    }
                    reader.Close();



                    command = new OleDbCommand(query2, connection);
                    command.Parameters.AddWithValue("@week", Convert.ToDateTime(tbDate.Text).DayOfWeek);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string temp = reader.GetString(1);
                        if (temp == "-1")
                        {
                            continue;
                        }
                        var list = temp.Split(',');
                        foreach (GridViewRow row in gv.Rows)
                        {
                            if (row.Cells[0].Text == reader.GetInt32(0).ToString())
                            {

                                foreach (var item in list)
                                {
                                    int i = Convert.ToInt32(item);
                                    TableCell c = row.Cells[i + 2];
                                    CheckBox ck = c.Controls[0] as CheckBox;
                                    ck.Enabled = false;

                                    ck.Visible = false;
                                    c.BackColor = Color.DarkGray;
                                    Label label_bw = new Label();
                                    label_bw.Text = "不";

                                    c.Controls.Add(label_bw);
                                }
                                break;
                            }
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // 錯誤處理
                    Response.Write("Error: " + ex.Message);
                }
            }
        }


        void gvDevice_Load()     // 讀取所有教室的清單(即所有教室的名字) 並弄出CheckBox
        {
            DataTable rstData = new DataTable();
            //ADD 20 columns
            rstData.Columns.Add("");
            rstData.Columns.Add("設備");
            rstData.Columns.Add("A", typeof(bool));
            rstData.Columns.Add("1", typeof(bool));
            rstData.Columns.Add("2", typeof(bool));
            rstData.Columns.Add("3", typeof(bool));
            rstData.Columns.Add("4", typeof(bool));
            rstData.Columns.Add("B", typeof(bool));
            rstData.Columns.Add("5", typeof(bool));
            rstData.Columns.Add("6", typeof(bool));
            rstData.Columns.Add("7", typeof(bool));
            rstData.Columns.Add("8", typeof(bool));
            rstData.Columns.Add("9", typeof(bool));
            rstData.Columns.Add("C", typeof(bool));
            rstData.Columns.Add("D", typeof(bool));
            rstData.Columns.Add("E", typeof(bool));
            rstData.Columns.Add("F", typeof(bool));
            rstData.Columns.Add("G", typeof(bool));


            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT did,dname
                FROM device
                WHERE dname LIKE @search
                ORDER BY dname
            ;";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@search", "%" + tbDnameKeyword.Text + "%");
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DataRow OneRow = rstData.NewRow();
                        OneRow[0] = reader["did"].ToString();
                        OneRow[1] = reader["dname"].ToString();
                        rstData.Rows.Add(OneRow);
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    // 錯誤處理
                    Response.Write("Error: " + ex.Message);
                }
            }

            gvDevice.DataSource = rstData;
            gvDevice.DataBind();
        }
        protected void gvDevice_RowDataBound(object sender, GridViewRowEventArgs e)      //把所有格子打開(DataBind()自動執行)
        {
            e.Row.Cells[0].Visible = false;
            //只有當資料行（非 header）時才執行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    TableCell c = e.Row.Cells[i];
                    CheckBox ck = c.Controls[0] as CheckBox;
                    ck.Enabled = true;
                }
            }
        }
        protected void gvDevice_Borrow() //讀取該日借用狀況, 關閉被借用的格子
        {
            GridView gv = gvDevice;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT did,starttime,endtime
                FROM bw
                WHERE bdate=@bdate
            ;";
            string query2 = "SELECT did,not_avaliable FROM dev_ava WHERE @week=week;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@bdate", tbDate.Text);
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foreach (GridViewRow row in gv.Rows)
                        {
                            if (row.Cells[0].Text == reader.GetInt32(0).ToString())
                            {
                                int start = reader.GetDateTime(1).Hour;
                                int end = reader.GetDateTime(2).Hour;
                                for (int i = start - 7; i < end - 7; i++)
                                {
                                    TableCell c = row.Cells[i + 2];
                                    CheckBox ck = c.Controls[0] as CheckBox;
                                    ck.Enabled = false;

                                    ck.Visible = false;
                                    c.BackColor = Color.LightPink;
                                    Label label_bw = new Label();
                                    label_bw.Text = "借";

                                    c.Controls.Add(label_bw);
                                }
                                break;
                            }
                        }
                    }
                    reader.Close();


                    command = new OleDbCommand(query2, connection);
                    command.Parameters.AddWithValue("@week", Convert.ToDateTime(tbDate.Text).DayOfWeek);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string temp = reader.GetString(1);
                        if (temp == "-1")
                        {
                            continue;
                        }
                        var list = temp.Split(',');
                        foreach (GridViewRow row in gv.Rows)
                        {
                            if (row.Cells[0].Text == reader.GetInt32(0).ToString())
                            {

                                foreach (var item in list)
                                {
                                    int i = Convert.ToInt32(item);
                                    TableCell c = row.Cells[i + 2];
                                    CheckBox ck = c.Controls[0] as CheckBox;
                                    ck.Enabled = false;

                                    ck.Visible = false;
                                    c.BackColor = Color.DarkGray;
                                    Label label_bw = new Label();
                                    label_bw.Text = "不";

                                    c.Controls.Add(label_bw);
                                }
                                break;
                            }
                        }
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    // 錯誤處理
                    Response.Write("Error: " + ex.Message);
                }
            }
        }


        bool submitIsVaild()
        {
            if (tbUsage.Text == "")
            {
                return false;
            }
            return true;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)  //處理Submit button時該做的事情 (可同時多筆處理)
        {
            // 若同一教室，借用的時間段沒連續 視為連續申請多筆教室借用 (方便系辦在按歸還時不會有問題)
            // 就算不同教室 同一時間段 也視為多筆連續申請
            if (!submitIsVaild())
            {
                return;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            bool issuccess = false;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        GridView gv = gvClassroom;
                        string insertQuery = "INSERT INTO [bw] ([berid],[cid],[usage],[bdate],[starttime],[endtime]) VALUES (@berid , @cid , @usage , @bdate , @starttime , @endtime );";
                        string query2 = @"
                            SELECT TOP 1 *
                            FROM bw
                            WHERE bdate = @bdate AND
                            (
                            (starttime < @starttime AND @starttime < endtime ) OR
                            (starttime < @endtime AND @endtime < endtime) OR
                            (@starttime <= starttime AND endtime <= @endtime)
                            ) AND cid = @cid
                        ;";
                        //Classroom借用的申請submit 
                        foreach (GridViewRow row in gv.Rows)
                        {
                            if (row.RowType != DataControlRowType.DataRow)
                            {
                                continue;
                            }

                            // 取連續的時間段
                            int start_idx = -1;
                            for (int i = 2; i < row.Cells.Count; i++)
                            {
                                TableCell c = row.Cells[i];
                                CheckBox ck = c.Controls[0] as CheckBox;
                                if (ck.Checked == true && start_idx == -1)
                                {
                                    start_idx = i;
                                }
                                else if ((ck.Checked == false && start_idx != -1))
                                {

                                    OleDbCommand command = new OleDbCommand(insertQuery, connection, transaction);
                                    OleDbCommand command2 = new OleDbCommand(query2, connection, transaction);
                                    int cid;
                                    if (!int.TryParse(row.Cells[0].Text, out cid))
                                    {
                                        throw new Exception("PARSE ERROR");
                                    }

                                    command2.Parameters.AddWithValue("@bdate", tbDate.Text);
                                    command2.Parameters.AddWithValue("@starttime", $"{start_idx + 7 - 2:00}:00");
                                    command2.Parameters.AddWithValue("@endtime", $"{i + 7 - 2:00}:00");
                                    command2.Parameters.AddWithValue("@cid", cid);
                                    OleDbDataReader reader2 = command2.ExecuteReader();
                                    reader2.Read();
                                    if (reader2.HasRows)
                                    {
                                        throw new Exception("EXIST");
                                    }
                                    reader2.Close();


                                    DateTime bdate;
                                    DateTime starttime = new DateTime(), endtime = new DateTime();
                                    if (!DateTime.TryParse(tbDate.Text, out bdate) || !DateTime.TryParse($"{start_idx + 7 - 2:00}:00", out starttime) || !DateTime.TryParse($"{i + 7 - 2:00}:00", out endtime))
                                    {
                                        throw new Exception("Phrase ERROR");
                                    }
                                    command.Parameters.AddWithValue("@berid", Session["userid"]);
                                    command.Parameters.AddWithValue("@cid", cid);
                                    command.Parameters.AddWithValue("@usage", tbUsage.Text);
                                    command.Parameters.AddWithValue("@bdate", bdate);
                                    command.Parameters.AddWithValue("@starttime", starttime);
                                    command.Parameters.AddWithValue("@endtime", endtime);
                                    if (command.ExecuteNonQuery() < 0)
                                    {
                                        throw new Exception("ExecuteNonQuery Failed!");
                                    }



                                    start_idx = -1;
                                }
                            }

                            if (start_idx != -1)
                            {
                                OleDbCommand command = new OleDbCommand(insertQuery, connection, transaction);
                                OleDbCommand command2 = new OleDbCommand(query2, connection, transaction);
                                int cid;
                                if (!int.TryParse(row.Cells[0].Text, out cid))
                                {
                                    throw new Exception("PARSE ERROR");
                                }

                                command2.Parameters.AddWithValue("@bdate", tbDate.Text);
                                command2.Parameters.AddWithValue("@starttime", $"{start_idx + 7 - 2:00}:00");
                                command2.Parameters.AddWithValue("@endtime", $"{row.Cells.Count + 7 - 2:00}:00");
                                command2.Parameters.AddWithValue("@cid", cid);
                                OleDbDataReader reader2 = command2.ExecuteReader();
                                reader2.Read();
                                if (reader2.HasRows)
                                {
                                    throw new Exception("EXIST");
                                }
                                reader2.Close();


                                DateTime bdate;
                                DateTime starttime = new DateTime(), endtime = new DateTime();
                                if (!DateTime.TryParse(tbDate.Text, out bdate) || !DateTime.TryParse($"{start_idx + 7 - 2:00}:00", out starttime) || !DateTime.TryParse($"{row.Cells.Count + + 7 - 2:00}:00", out endtime))
                                {
                                    throw new Exception("Phrase ERROR");
                                }
                                command.Parameters.AddWithValue("@berid", Session["userid"]);
                                command.Parameters.AddWithValue("@cid", cid);
                                command.Parameters.AddWithValue("@usage", tbUsage.Text);
                                command.Parameters.AddWithValue("@bdate", bdate);
                                command.Parameters.AddWithValue("@starttime", starttime);
                                command.Parameters.AddWithValue("@endtime", endtime);
                                if (command.ExecuteNonQuery() < 0)
                                {
                                    throw new Exception("ExecuteNonQuery Failed!");
                                }



                                start_idx = -1;
                            }
                        }


                        // 處理Device的借用
                        if(cbNeedDevice.Checked == true)
                        {
                            gv = gvDevice;
                            insertQuery = "INSERT INTO [bw] ([berid],[did],[usage],[bdate],[starttime],[endtime]) VALUES (@berid , @did , @usage , @bdate , @starttime , @endtime );";
                            query2 = @"
                                SELECT TOP 1 *
                                FROM bw
                                WHERE bdate = @bdate AND
                                (
                                (starttime < @starttime AND @starttime < endtime ) OR
                                (starttime < @endtime AND @endtime < endtime) OR
                                (@starttime <= starttime AND endtime <= @endtime)
                                ) AND did = @did
                            ;";
                            //Device借用的申請submit 
                            foreach (GridViewRow row in gv.Rows)
                            {
                                if (row.RowType != DataControlRowType.DataRow)
                                {
                                    continue;
                                }

                                // 取連續的時間段
                                int start_idx = -1;
                                for (int i = 2; i < row.Cells.Count; i++)
                                {
                                    TableCell c = row.Cells[i];
                                    CheckBox ck = c.Controls[0] as CheckBox;
                                    if (ck.Checked == true && start_idx == -1)
                                    {
                                        start_idx = i;
                                    }
                                    else if ((ck.Checked == false && start_idx != -1))
                                    {

                                        OleDbCommand command = new OleDbCommand(insertQuery, connection, transaction);
                                        OleDbCommand command2 = new OleDbCommand(query2, connection, transaction);
                                        int did;
                                        if (!int.TryParse(row.Cells[0].Text, out did))
                                        {
                                            throw new Exception("PARSE ERROR");
                                        }

                                        command2.Parameters.AddWithValue("@bdate", tbDate.Text);
                                        command2.Parameters.AddWithValue("@starttime", $"{start_idx + 7 - 2:00}:00");
                                        command2.Parameters.AddWithValue("@endtime", $"{i + 7 - 2:00}:00");
                                        command2.Parameters.AddWithValue("@did", did);
                                        OleDbDataReader reader2 = command2.ExecuteReader();
                                        reader2.Read();
                                        if (reader2.HasRows)
                                        {
                                            throw new Exception("EXIST");
                                        }
                                        reader2.Close();


                                        DateTime bdate;
                                        DateTime starttime = new DateTime(), endtime = new DateTime();
                                        if (!DateTime.TryParse(tbDate.Text, out bdate) || !DateTime.TryParse($"{start_idx + 7 - 2:00}:00", out starttime) || !DateTime.TryParse($"{i + 7 - 2:00}:00", out endtime))
                                        {
                                            throw new Exception("Phrase ERROR");
                                        }
                                        command.Parameters.AddWithValue("@berid", Session["userid"]);
                                        command.Parameters.AddWithValue("@did", did);
                                        command.Parameters.AddWithValue("@usage", tbUsage.Text);
                                        command.Parameters.AddWithValue("@bdate", bdate);
                                        command.Parameters.AddWithValue("@starttime", starttime);
                                        command.Parameters.AddWithValue("@endtime", endtime);
                                        if (command.ExecuteNonQuery() < 0)
                                        {
                                            throw new Exception("ExecuteNonQuery Failed!");
                                        }



                                        start_idx = -1;
                                    }
                                }
                                if (start_idx!=-1)
                                {

                                    OleDbCommand command = new OleDbCommand(insertQuery, connection, transaction);
                                    OleDbCommand command2 = new OleDbCommand(query2, connection, transaction);
                                    int did;
                                    if (!int.TryParse(row.Cells[0].Text, out did))
                                    {
                                        throw new Exception("PARSE ERROR");
                                    }

                                    command2.Parameters.AddWithValue("@bdate", tbDate.Text);
                                    command2.Parameters.AddWithValue("@starttime", $"{start_idx + 7 - 2:00}:00");
                                    command2.Parameters.AddWithValue("@endtime", $"{row.Cells.Count + 7 - 2:00}:00");
                                    command2.Parameters.AddWithValue("@did", did);
                                    OleDbDataReader reader2 = command2.ExecuteReader();
                                    reader2.Read();
                                    if (reader2.HasRows)
                                    {
                                        throw new Exception("EXIST");
                                    }
                                    reader2.Close();


                                    DateTime bdate;
                                    DateTime starttime = new DateTime(), endtime = new DateTime();
                                    if (!DateTime.TryParse(tbDate.Text, out bdate) || !DateTime.TryParse($"{start_idx + 7 - 2:00}:00", out starttime) || !DateTime.TryParse($"{row.Cells.Count + 7 - 2:00}:00", out endtime))
                                    {
                                        throw new Exception("Phrase ERROR");
                                    }
                                    command.Parameters.AddWithValue("@berid", Session["userid"]);
                                    command.Parameters.AddWithValue("@did", did);
                                    command.Parameters.AddWithValue("@usage", tbUsage.Text);
                                    command.Parameters.AddWithValue("@bdate", bdate);
                                    command.Parameters.AddWithValue("@starttime", starttime);
                                    command.Parameters.AddWithValue("@endtime", endtime);
                                    if (command.ExecuteNonQuery() < 0)
                                    {
                                        throw new Exception("ExecuteNonQuery Failed!");
                                    }



                                    start_idx = -1;
                                }
                            }
                        }
                        


                        transaction.Commit();
                        issuccess = true;
                    }
                    catch (Exception ex) {
                        transaction.Rollback();
                        Response.Write(ex.ToString());
                    }
                }
            }

            if (issuccess)
            {
                Response.Redirect(Request.RawUrl);
            }
        }

    }
}