using Demofirst.Authentication;
using Demofirst.Dao;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demofirst
{
	public partial class Login : System.Web.UI.Page
	{
        private readonly LoginDao _dao = new LoginDao(); // Database access layer
        protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void submitbtn_Click(object sender, EventArgs args)
		{
            string username = Username.Text;
            string enteredPassword = Password.Text;

            var hashPwd = Auth.HashPassword(enteredPassword);

            var dr = _dao.LoginUser(username, hashPwd);
            string role = dr["role"].ToString().ToLower();
            if(dr.Table.Rows.Count > 0)
            {
                HttpCookie userCookie = new HttpCookie("UserLogin");
                userCookie.Values["Username"] = username;
                userCookie.Expires = DateTime.Now.AddHours(1);  //set expiration date
                Response.Cookies.Add(userCookie);



                //store session variable for username and role
                Session["Username"] = username;
                Session["Role"] = role;
                if(role.ToLower() == "seller")
                {
                    Response.Redirect("SellerDashboard.aspx");
                }
                else if(role.ToLower() == "admin")
                {
                    Response.Redirect("List.aspx");
                }
                else
                {
                    Response.Redirect("LandingPage.aspx");
                }
                //if(Session["role"] != null)
                //{
                //    string role = Session
                //}
                //string hashedPassword = dr["password"].ToString();

                //if(Auth.VerifyPassword(enteredPassword, hashedPassword))
                //{
                //    // ✅ Login successful
                //    //Session["User"] = username; // Store user session
                //    //Response.Redirect("Dashboard.aspx"); // Redirect to dashboard
                //    ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess", "toastr.success('User login successfully!');", true);
                //}
                //else
                //{
                //    // ❌ Incorrect password
                //    ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Failed to login user. Please try again.');", true);
                //}

                // Show Toastr success message and redirect after delay
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess",
                    "toastr.success('User login successfully!'); setTimeout(function() { window.location.href='Default.aspx'; }, 2000);",
                    true);
            }
            else
            {
                // ❌ Username does not exist
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Username does not exist. Please try again.');", true);
            }


        }

    }
}