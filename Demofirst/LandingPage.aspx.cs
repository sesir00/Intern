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
    public partial class LandingPage : System.Web.UI.Page
    {
        private readonly ProductSalesDao _dao = new ProductSalesDao();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            DataSet ds = _dao.FetchProduct();
            if(ds.Tables.Count>0 && ds.Tables[1].Rows[0]["code"].ToString().Equals("0"))
            {
                rptProducts.DataSource = ds.Tables[0];
                rptProducts.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "swalSuccess", "Swal.fire('Success', 'Product loaded successfully!', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "swalError", "Swal.fire('Error', 'Error loading Product!', 'error');", true);
            }

        }

        protected void btnBuy_Click (object sender, EventArgs e)
        {
            string BuyerName = Session["Username"].ToString();
            Button btn = (Button)sender;

            // Retrieve multiple arguments by splitting the CommandArgument string
            string[] args = btn.CommandArgument.Split(',');

            // Extract individual values
            int productId = Convert.ToInt32(args[0]);
            double price = Convert.ToDouble(args[1]);
            string productName = args[2];
            int quantity = Convert.ToInt32(args[3]);

            // Now, you have the productId, price, and productName, and you can proceed with your purchase logic.
            // Example: save the purchase details to the database, etc.
            // SavePurchase(buyerName, productId, productName, price);

            //int productId = Convert.ToInt32(btn.CommandArgument);
            if(quantity > 0)
            {
                DataRow dr = _dao.AddSales(BuyerName, 1, price, productId);
                if (dr["code"].ToString().Equals("0"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Product {productId} bought successfully!', 'success');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Error Buying Product {productId} !', 'Failure');", true);
                }
            }
            else
            {
                // SweetAlert2 error alert for out-of-stock condition
                ScriptManager.RegisterStartupScript(this, GetType(), "swalOutOfStock",
                    $"Swal.fire({{ title: 'Out of Stock', text: 'Sorry, the product {productName} is out of stock.', icon: 'warning' }});", true);
            }

            LoadProducts();
            // Add product to cart or handle the buying logic here
            // You might need to create a Cart table or session-based cart handling

            // Example: Showing a success message after the product is added to cart
            
        }
        protected string GetQuantityText(object quantity)
        {
            int qty = Convert.ToInt32(quantity);
            return qty > 0 ? $"{qty} Remaining" : "<span class='out-of-stock'>Out Of Stock</span>";
        }

        protected void btnCart_Click(object sender, EventArgs e)
        {
            string username = Session["Username"].ToString();
            //Button btn = (Button)sender;
            //double price = Convert.ToDouble(btn.CommandArgument);
            // Retrieve the button and the CommandArgument 
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');

            // Extract the individual values from the CommandArgument
            int productId = Convert.ToInt32(args[0]);
            double price = Convert.ToDouble(args[1]);
            string productName = Convert.ToString(args[2]);
            int quantity = Convert.ToInt32(args[3]);

            // Construct the query string for redirection to Cart.aspx
            string url = $"Cart.aspx?productId={productId}&price={price}&productName={productName}&quantity={quantity}";

            //check whether it already exists in cart or not
            DataSet ds = _dao.CartFetch(username);

            //change the cart_value to 1 
            var dr = _dao.AddtoCart(productId, 1, price, username);
            if (dr["code"].ToString().Equals("0"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Product {productId} added to cart  successfully!', 'success');", true);

                // Redirect to Cart.aspx with the product details
                //Response.Redirect(url);
                Response.Redirect("Cart.aspx");
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Purchased', 'Error adding to cart Product {productId} !', 'Failure');", true);
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Added', 'Product Added to Cart successfully!', 'success');", true);

            // Redirect to Cart.aspx with the product details
            Response.Redirect(url);
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            //clear session data
            Session.Clear();
            // Optionally, you can also abandon the session to ensure it's completely cleared
            Session.Abandon();
            //redirect user to login page
            Response.Redirect("Login.aspx");
            //    if (Request.Cookies["UserLogin"] != null)
            //    {
            //        HttpCookie userCookie = new HttpCookie("UserLogin");
            //        userCookie.Expires = DateTime.Now.AddDays(-1); // Expire cookie
            //        Response.Cookies.Add(userCookie);
            //    }

            //    Response.Redirect("Login.aspx");
        }
    }
    
}