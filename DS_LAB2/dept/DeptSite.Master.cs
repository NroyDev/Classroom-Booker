using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2.dept
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isloggedin_dept"] == null || !(bool)Session["isloggedin_dept"])
            {
                Response.Redirect("login.aspx");
            }
            LabelDeptName.Text = "歡迎, " + Session["deptname"].ToString();
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            if (Session["isloggedin_dept"] != null && (bool)Session["isloggedin_dept"])
            {
                Session.Clear();
                Response.Redirect("login.aspx");
            }
        }
    }
}