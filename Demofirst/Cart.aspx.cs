using Demofirst.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demofirst
{
    public partial class Cart : System.Web.UI.Page
    {
        private readonly ProductSalesDao _dao = new ProductSalesDao();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //retrieve from query string values
                string productId = Request.QueryString["productId"];
                string price = Request.QueryString["price"];
                string productName = Request.QueryString["productName"];
                string quantity = Request.QueryString["quantity"];

                LoadCartItems();
            }
        }
        private void LoadCartItems()
        {
            if (Session["Username"] != null)
            {
                string username = Session["Username"].ToString();
                DataSet ds = _dao.CartFetch(username);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["code"].ToString().Equals("0"))
                {
                    // Bind data to the GridView
                    gvCart.DataSource = ds.Tables[1]; // Assuming your cart items are in the first table
                    gvCart.DataBind();

                    //ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Product Loaded successfully!', 'success');", true);
                }
                else
                {
                    // Handle empty cart scenario
                    gvCart.DataSource = null;
                    gvCart.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Error Loading Product !', 'Failure');", true);
                }
            }
            else
            {
                Response.Redirect("LandingPage.aspx");
                ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Error Loading Cart !', 'Failure');", true);
            }


        }

        protected void btnDecrease_Click(object sender, EventArgs args)
        {
            Button btnDecrease = (Button)sender;
            int productId = Convert.ToInt32(btnDecrease.CommandArgument);

            string username = Session["Username"].ToString();

            //Decrease the quantity by 1
            var dr = _dao.UpdateCartQuantity(productId, -1, username);
            if (dr["code"].ToString().Equals("0"))
            {
                LoadCartItems();
                //ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Product Updated Successfully!', 'success');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Error Updating Product !', 'Failure');", true);

            }

        }

        protected void btnIncrease_Click(object sender, EventArgs args)
        {
            Button btnIncrease = (Button)sender;
            int productId = Convert.ToInt32(btnIncrease.CommandArgument);

            string username = Session["Username"].ToString();

            var dr = _dao.UpdateCartQuantity(productId, 1, username);
            if (dr["code"].ToString().Equals("0"))
            {
                LoadCartItems();
                //ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Product Loaded successfully!', 'success');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Error Loading Product !', 'Failure');", true);

            }
        }
        protected void btnRemove_Click(object sender, EventArgs args)
        {
            // Get the button that triggered the event
            Button btnRemove = (Button)sender;

            // Retrieve the ProductId from the CommandArgument
            string productID = btnRemove.CommandArgument;

            // Convert ProductId to integer (if required)
            int productId = Convert.ToInt32(productID);
            var dr = _dao.CartDelete(productId);

            if (dr["code"].ToString().Equals("0"))
            {
                // Reload the cart after deletion
                LoadCartItems();
                ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Deleted', 'Product Deleted from Cart successfully!', 'success');", true);
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs args)
        {
            if (Session["Username"] != null)
            {
                //double cartTotal = 0;
                //foreach (GridViewRow row in gvCart.Rows)
                //{
                //    // Get the total amount for this row (from the 5th column - index 4)
                //    string totalText = row.Cells[4].Text.Replace("$", "").Trim();
                //    double rowTotal;

                //    cartTotal += Convert.ToDouble(totalText);
                //    //if (Double.TryParse(totalText, out rowTotal))
                //    //{
                //    //    cartTotal += rowTotal;
                //    //}
                //}
                //// Store the cart total in session
                //Session["CartTotal"] = cartTotal;

                Response.Redirect("Checkout.aspx");
            }
            else
            {
                Response.Redirect("LandingPage.aspx");
            }

        }
    }
}