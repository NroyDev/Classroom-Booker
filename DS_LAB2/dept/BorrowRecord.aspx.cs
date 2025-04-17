using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2.dept
{
    public partial class BorrowRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbSearchStartDate.Text = $"{DateTime.Now:yyyy-MM-dd}";
                tbSearchEndDate.Text = $"{DateTime.Now:yyyy-MM-dd}";
                rblSearchConstraint.SelectedIndex = 0;


                gvBorrowClassroomList_Load();
                gvBorrowDeviceList_Load();
            }
        }

        protected void gvBorrowClassroomList_Load()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT bid,cname,bdate,starttime,endtime,berid,usage,IIF(rdate is null ,'否','是') as returned   
                FROM bw INNER JOIN classroom on bw.cid=classroom.cid 
                WHERE classroom.dept = @dept AND
                        bid like @bid AND cname like @cname AND berid like @berid AND
                        @startdate <= bdate AND bdate <= @enddate
                ORDER BY classroom.cid,bdate DESC,starttime,bw.bid
            ;";
            if(rblSearchConstraint.SelectedIndex == 1)
            {
                query = @"
                SELECT bid,cname,bdate,starttime,endtime,berid,usage,IIF(rdate is null ,'否','是') as returned   
                FROM bw INNER JOIN classroom on bw.cid=classroom.cid 
                WHERE classroom.dept = @dept AND
                        bid like @bid AND cname like @cname AND berid like @berid AND
                        @startdate <= bdate AND bdate <= @enddate AND
                        rdate is not null
                ORDER BY classroom.cid,bdate DESC,starttime,bw.bid
                ;";
            }else if (rblSearchConstraint.SelectedIndex == 2)
            {
                query = @"
                SELECT bid,cname,bdate,starttime,endtime,berid,usage,IIF(rdate is null ,'否','是') as returned   
                FROM bw INNER JOIN classroom on bw.cid=classroom.cid 
                WHERE classroom.dept = @dept AND
                        bid like @bid AND cname like @cname AND berid like @berid AND
                        @startdate <= bdate AND bdate <= @enddate AND
                        rdate is null
                ORDER BY classroom.cid,bdate DESC,starttime,bw.bid
                ;";
            }
            using (OleDbConnection connection = new OleDbConnection(connectionString)) { 
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@dept", Session["deptid"]);
                command.Parameters.AddWithValue("@bid", "%" + tbBidKeyword.Text + "%");
                command.Parameters.AddWithValue("@cname", "%" + tbCnameKeyword.Text + "%");
                command.Parameters.AddWithValue("@berid", "%" + tbBeridKeyword.Text + "%");
                command.Parameters.AddWithValue("@startdate", tbSearchStartDate.Text);
                command.Parameters.AddWithValue("@enddate", tbSearchEndDate.Text);
                try
                {
                    connection.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvBorrowClassroomList.DataSource = dataTable;
                    gvBorrowClassroomList.DataBind();
                }
                catch (Exception ex) { 
                    Response.Write(ex.ToString());
                }
            }
        }
        protected void gvBorrowDeviceList_Load()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT bid,dname,bdate,starttime,endtime,berid,usage,IIF(rdate is null ,'否','是') as returned   
                FROM bw INNER JOIN device on bw.did=device.did 
                WHERE device.dept = @dept AND
                        bid like @bid AND dname like @dname AND berid like @berid AND
                        @startdate <= bdate AND bdate <= @enddate
                ORDER BY device.did,bdate DESC,starttime,bw.bid
            ;";
            if (rblSearchConstraint.SelectedIndex == 1)
            {
                query = @"
                SELECT bid,dname,bdate,starttime,endtime,berid,usage,IIF(rdate is null ,'否','是') as returned   
                FROM bw INNER JOIN device on bw.did=device.did 
                WHERE device.dept = @dept AND
                        bid like @bid AND dname like @dname AND berid like @berid AND
                        @startdate <= bdate AND bdate <= @enddate AND
                        rdate is not null
                ORDER BY device.did,bdate DESC,starttime,bw.bid
                ;";
            }
            else if (rblSearchConstraint.SelectedIndex == 2)
            {
                query = @"
                SELECT bid,dname,bdate,starttime,endtime,berid,usage,IIF(rdate is null ,'否','是') as returned   
                FROM bw INNER JOIN device on bw.did=device.did 
                WHERE device.dept = @dept AND
                        bid like @bid AND dname like @dname AND berid like @berid AND
                        @startdate <= bdate AND bdate <= @enddate AND
                        rdate is null
                ORDER BY device.did,bdate DESC,starttime,bw.bid
                ;";
            }
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@dept", Session["deptid"]);
                command.Parameters.AddWithValue("@bid", "%" + tbBidKeyword.Text + "%");
                command.Parameters.AddWithValue("@dname", "%" + tbDnameKeyword.Text + "%");
                command.Parameters.AddWithValue("@berid", "%" + tbBeridKeyword.Text + "%");
                command.Parameters.AddWithValue("@startdate", tbSearchStartDate.Text);
                command.Parameters.AddWithValue("@enddate", tbSearchEndDate.Text);
                try
                {
                    connection.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvBorrowDeviceList.DataSource = dataTable;
                    gvBorrowDeviceList.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }

        protected void gvBorrowClassroomList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btn = (Button)e.Row.Cells[8].Controls[0];
                if(e.Row.Cells[7].Text == "是")
                {
                    btn.Text = "取消確認";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvBorrowClassroomList_Load();
            gvBorrowDeviceList_Load();
        }

        protected void gvBorrowClassroomList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Toggle_return_status")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = gvBorrowClassroomList.Rows[index];

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command;
                    if (selectedRow.Cells[7].Text == "否")
                    {
                        string updateSQL = "UPDATE bw SET rdate = Date(), dept = @deptid WHERE bid = @bid";
                        command = new OleDbCommand(updateSQL, connection);
                        command.Parameters.AddWithValue("@dept", Session["deptid"]);
                        command.Parameters.AddWithValue("@bid", selectedRow.Cells[0].Text);
                    }
                    else
                    {
                        string updateSQL = "UPDATE bw SET rdate = null, dept = 0 WHERE bid = @bid";
                        command = new OleDbCommand(updateSQL, connection);
                        command.Parameters.AddWithValue("@bid", selectedRow.Cells[0].Text);
                    }
                    try
                    {
                        connection.Open();
                        if (command.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("No affect!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                }

                gvBorrowClassroomList_Load();
            }
        }
        protected void gvBorrowDeviceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Toggle_return_status")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = gvBorrowDeviceList.Rows[index];

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command;
                    if (selectedRow.Cells[7].Text == "否")
                    {
                        string updateSQL = "UPDATE bw SET rdate = Date(), dept = @deptid WHERE bid = @bid";
                        command = new OleDbCommand(updateSQL, connection);
                        command.Parameters.AddWithValue("@dept", Session["deptid"]);
                        command.Parameters.AddWithValue("@bid", selectedRow.Cells[0].Text);
                    }
                    else
                    {
                        string updateSQL = "UPDATE bw SET rdate = null, dept = 0 WHERE bid = @bid";
                        command = new OleDbCommand(updateSQL, connection);
                        command.Parameters.AddWithValue("@bid", selectedRow.Cells[0].Text);
                    }
                    try
                    {
                        connection.Open();
                        if (command.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("No affect!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                }

                gvBorrowDeviceList_Load();
            }
        }

        protected void gvBorrowDeviceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btn = (Button)e.Row.Cells[8].Controls[0];
                if (e.Row.Cells[7].Text == "是")
                {
                    btn.Text = "取消確認";
                }
            }
        }
    }
}