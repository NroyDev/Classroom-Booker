
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvBorrowClassrom_Load();
                gvBorrowDevice_Load();
            }
        }


        protected void gvBorrowClassrom_Load()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT bid,cname,bdate,starttime,endtime,usage
                FROM bw inner join classroom ON bw.cid = classroom.cid
                WHERE berid = @berid AND rdate is null
                ORDER BY bdate, classroom.cid, starttime, bid
            ;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@berid", Session["userid"]);
                try
                {
                    connection.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gvBorrowClassrom.DataSource = dataTable;
                    gvBorrowClassrom.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
        protected void gvBorrowDevice_Load()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT bid,dname,bdate,starttime,endtime,usage
                FROM bw inner join device ON bw.did = device.did
                WHERE berid = @berid AND rdate is null
                ORDER BY bdate, device.did, starttime, bid
            ;";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@berid", Session["userid"]);
                try
                {
                    connection.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gvBorrowDevice.DataSource = dataTable;
                    gvBorrowDevice.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }

        protected void btnPrintClassroom_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string script = $"window.open('printForm?bid={gvr.Cells[0].Text}&cd=c', '_blank');";
            ClientScript.RegisterStartupScript(this.GetType(), "OpenNewTab", script, true);
        }
        protected void btnPrintDevice_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string script = $"window.open('printForm?bid={gvr.Cells[0].Text}&cd=d', '_blank');";
            ClientScript.RegisterStartupScript(this.GetType(), "OpenNewTab", script, true);
        }

    }
}