using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demofirst
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserLogin"] != null)
                {
                    string username = Request.Cookies["UserLogin"]["Username"];
                    lblWelcome.Text = "Welcome, " + username; // Display username
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["UserLogin"] != null)
            {
                HttpCookie userCookie = new HttpCookie("UserLogin");
                userCookie.Expires = DateTime.Now.AddDays(-1); // Expire cookie
                Response.Cookies.Add(userCookie);
            }

            Response.Redirect("Login.aspx");
        }

    }
}