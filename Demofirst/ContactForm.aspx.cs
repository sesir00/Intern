using Demofirst.Dao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Demofirst
{
	public partial class ContactForm : System.Web.UI.Page
	{
		private readonly UserDao _dao = new UserDao();
        protected void Page_Load(object sender, EventArgs e)
		{
			//Page Load logic (if needed)
		}
		protected void btnSubmit_Click(Object Sender, EventArgs e)
		{
			//string name = Name.Text;
			//string email = Email.Text;
			//string message = Message.Text;

			////simulate saving to the database and sending an email
			//conResult.Text = "Thank you " + name + "! your message have beeen received.";
			if (Page.IsValid)
			{
                string name = Name.Text;
                string email = Email.Text;
                string message = Message.Text;

				//var res = _dao.AddProduct(name, email, message);

				//if(res != null)
				//{
				//	conResult.Text = res["message"].ToString();

    //            }
				//else
				//{
				//	conResult.Text = "cannot connect to db";

    //            }

				//get connection from the web config file

				//string connString = ConfigurationManager.ConnectionStrings["ContactDBConnection"].ConnectionString;

				//using (SqlConnection conn = new SqlConnection(connString))
				//{
				//	using (SqlCommand cmd = new SqlCommand("InsertContact", conn))
				//	{
				//		cmd.CommandType = CommandType.StoredProcedure;

    //                    // Pass parameters to the stored procedure
    //                    cmd.Parameters.AddWithValue("@Name", name);
    //                    cmd.Parameters.AddWithValue("@Email", email);
    //                    cmd.Parameters.AddWithValue("@Message", message);

				//		conn.Open();
				//		int rowsAffected = cmd.ExecuteNonQuery();
				//		conn.Close();

				//		if(rowsAffected > 0)
				//		{
				//			conResult.Text = "Thank you, " + name + "! Your message has been saved.";
				//			ClearForm();
				//		}
				//		else
				//		{
				//			conResult.Text = "Submission failed. Please try again.";
							
    //                    }

    //                }
    //            }
            }
        }
        private void ClearForm()
        {
            Name.Text = "";
            Email.Text = "";
            Message.Text = "";
        }
    }
}