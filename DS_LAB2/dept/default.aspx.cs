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
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvClassroomOverdue_Load();
                gvDeviceOverdue_Load();
                Label_statusToday();
            }
        }

        protected void Label_statusToday()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query1 = "SELECT COUNT(*) FROM bw INNER JOIN classroom ON bw.cid = classroom.cid WHERE bdate = Date() AND classroom.dept = @dept;";
            string query2 = "SELECT COUNT(*) FROM bw INNER JOIN device ON bw.did = device.did WHERE bdate = DATE() AND device.dept = @deptid;";
            string query3 = @"
                SELECT COUNT(*)
                FROM bw INNER JOIN classroom ON bw.cid=classroom.cid
                WHERE   classroom.dept = @deptid AND rdate is null 
                    AND (bdate < Date() OR (bdate = Date() AND Hour([endtime]) <= Hour(Now()) ))
            ;";
            string query4 = @"
                SELECT COUNT(*)
                FROM bw INNER JOIN device ON bw.did=device.did
                WHERE   device.dept = @deptid AND rdate is null 
                    AND (bdate < Date() OR (bdate = Date() AND Hour([endtime]) <= Hour(Now()) ))
            ;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command1 = new OleDbCommand(query1, connection);
                command1.Parameters.AddWithValue("@deptid", Session["deptid"]);
                OleDbCommand command2 = new OleDbCommand(query2, connection);
                command2.Parameters.AddWithValue("@deptid", Session["deptid"]);
                OleDbCommand command3 = new OleDbCommand(query3, connection);
                command3.Parameters.AddWithValue("@deptid", Session["deptid"]);
                OleDbCommand command4 = new OleDbCommand(query4, connection);
                command4.Parameters.AddWithValue("@deptid", Session["deptid"]);
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command1.ExecuteReader();
                    if (reader.Read()) {
                        Label_BorrowClassroomToday.Text = $"今天有 {reader.GetInt32(0)} 筆 借用教室的申請";
                    }
                    reader.Close();

                    reader = command2.ExecuteReader();
                    if (reader.Read()) {
                        Label_BorrowDeviceToday.Text = $"今天有 {reader.GetInt32(0)} 筆 借用設備的申請";
                    }
                    reader.Close();

                    reader = command3.ExecuteReader();
                    if (reader.Read()) {
                        Label_ClassroomOverdue.Text = $"有 {reader.GetInt32(0)} 筆 逾期的借用教室申請";
                    }
                    reader.Close();

                    reader = command4.ExecuteReader();
                    if (reader.Read()) {
                        Label_DeviceOverdue.Text = $"有 {reader.GetInt32(0)} 筆 逾期的借用設備申請";
                    }
                    reader.Close();
                }
                catch (Exception ex) { 
                    Response.Write(ex.ToString());
                }
            }
        }
        protected void gvClassroomOverdue_Load()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT bw.bid,bw.cid,cname,bw.bdate,starttime,endtime,bw.berid,usage,email,phone
                FROM (bw INNER JOIN classroom ON bw.cid=classroom.cid ) INNER JOIN [user] ON bw.berid = [user].id
                WHERE   classroom.dept = @dept AND rdate is null 
                    AND (bdate < Date() OR (bdate = Date() AND Hour([endtime]) <= Hour(Now()) ))
            ;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@dept", Session["deptid"]);
                try
                {
                    connection.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvClassroomOverude.DataSource = dataTable;
                    gvClassroomOverude.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
        protected void gvDeviceOverdue_Load()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT bw.bid,bw.did,dname,bw.bdate,starttime,endtime,bw.berid,usage,email,phone
                FROM (bw INNER JOIN device ON bw.did=device.did ) INNER JOIN [user] ON bw.berid = [user].id
                WHERE   device.dept = @dept AND rdate is null 
                    AND (bdate < Date() OR (bdate = Date() AND Hour([endtime]) <= Hour(Now()) ))
            ;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@dept", Session["deptid"]);
                try
                {
                    connection.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvDeviceOverude.DataSource = dataTable;
                    gvDeviceOverude.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
    }
}