using Demofirst.Authentication;
using Demofirst.Dao;
using System;
using System.Security.Cryptography;
using System.Web.UI;

namespace Demofirst
{
    public partial class Signup : System.Web.UI.Page
    {
        private readonly LoginDao _dao = new LoginDao(); // Database access layer

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Page Load logic
            }
        }

        protected void submitbtn_Click(object sender, EventArgs args)
        {
            string username = Username.Text;
            string email = Email.Text;
            string password = Password.Text;
            string role = Buyer.Checked ? "Buyer" : "Seller";

            // Hash the password before storing it
            string hashedPassword = Auth.HashPassword(password);

            var dr = _dao.RegisterUser(username, email, hashedPassword, role);
            if (dr["code"].ToString().Equals("0")) // Success
            {
                //store session variable for username and role
                Session["Username"] = username;
                Session["Role"] = role;
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess","toastr.success('User registered successfully!'); setTimeout(function(){ window.location.href='Login.aspx'; }, 2000);",true);
                Response.Redirect("Login.aspx");
            }
            else // Failure
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Failed to register user. Please try again.');", true);
            }
        }

    }
}
