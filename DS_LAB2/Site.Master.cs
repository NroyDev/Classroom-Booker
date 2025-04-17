using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isloggedin"] == null || !(bool)Session["isloggedin"])
            {
                Response.Redirect("login.aspx");
            }

            LabelUserName.Text = "歡迎, " + Session["realname"].ToString();
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            if (Session["isloggedin"] != null && (bool)Session["isloggedin"])
            {
                Session.Clear();
                Response.Redirect("login.aspx");
            }
        }

    }
}