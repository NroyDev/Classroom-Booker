using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2
{
    public partial class test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvLoad();
        }

        protected void gvLoad()
        {
            string connectionStrign = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = "SELECT [bid],[mailed] FROM [bw]";
            using (OleDbConnection connection = new OleDbConnection(connectionStrign))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    if (reader.GetBoolean(1))
                    {
                        Label1.Text += reader.GetInt32(0);
                    }
                }
                
            }
        }
    }
}