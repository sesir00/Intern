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
    public partial class Checkout : System.Web.UI.Page
    {
        private readonly ProductSalesDao _dao = new ProductSalesDao();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // You can fetch the cart items from session or database to calculate subtotal, tax, shipping, etc.
                // For this example, subtotal, tax, and shipping are hardcoded.
                //if (Session["CartTotal"] != null)
                //{
                //    double subtotal = Convert.ToDouble(Session["CartTotal"]);
                //    double vat = subtotal * 0.13;   //13%
                //    double shipping = subtotal * 0.1;   //10%

                //    // Calculate total
                //    double total = subtotal + vat + shipping;

                //    //Display the values in page
                //    lblSubtotal.Text = subtotal.ToString("F2");
                //    lblTax.Text = vat.ToString("F2");
                //    lblShipping.Text = shipping.ToString("F2");
                //    lblTotal.Text = total.ToString("F2");

                //    //F2 = format specifier in C#. It controls how a numeric value (like a double or float) is converted to a string for display. Specifically, "F2" means:
                //    //"F": Stands for "Fixed-point" notation.
                //    //"2": Specifies the number of decimal places to display(in this case, 2).
                //}
                //else
                //{
                //    // Handle case when no cart total is available
                //    Response.Redirect("Cart.aspx");
                //}
                if (Session["Username"] != null)
                {
                    string username = Session["Username"].ToString();
                    DataSet ds = _dao.AddtoCheckout(username);
                    if (ds.Tables.Count > 0 && ds.Tables[1].Rows[0]["code"].ToString().Equals("0"))
                    {
                        double subtotal = Convert.ToDouble(ds.Tables[0].Rows[0]["total_cart_price"]);
                        double vat = subtotal * 0.13;   //13%
                        double shipping = subtotal * 0.1;   //10%

                        double total = subtotal + vat + shipping;

                        //Display the values in page
                        lblSubtotal.Text = subtotal.ToString("F2");
                            lblTax.Text = vat.ToString("F2");
                            lblShipping.Text = shipping.ToString("F2");
                            lblTotal.Text = total.ToString("F2");
                    }

                }
                else
                {
                    Response.Redirect("Cart.aspx");
                }
            }
        }

        protected void ProceedBtn_Click(object sender, EventArgs e)
        {
            // Get the values from the form
            string name = txtName.Text;
            string country = ddlCountry.SelectedValue;
            string city = txtCity.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            // Manually capture the selected payment method
            string paymentMethod = Request.Form["paymentMethod"];

            if (IsValidForm())
            {
                string username = Session["Username"].ToString();
                DataSet ds = _dao.CartFetch(username);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["code"].ToString().Equals("0"))
                {
                    DataTable cartTable = ds.Tables[1];

                    // Process each product in the cart
                    bool allProductsProcessed = true;
                    foreach (DataRow row in cartTable.Rows)
                    {
                        int productId = Convert.ToInt32(row["id"]);
                        int quantity = Convert.ToInt32(row["quantity"]);
                        double price = Convert.ToDouble(row["price"]);

                        var dr = _dao.CheckoutProceed(username, quantity, price, productId, username);
                        if (!dr["code"].ToString().Equals("0"))
                        {
                            allProductsProcessed = false;
                            ScriptManager.RegisterStartupScript(this, GetType(), "swalBuyError",
                                $"Swal.fire('Error', 'Problem processing product {productId}', 'error');", true);
                            break;
                        }
                    }
                    if (allProductsProcessed)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "swalBuySuccess",
                            $"Swal.fire('Success', 'Your order has been processed successfully!', 'success');", true);
                        Response.Redirect("LandingPage.aspx");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy",
                                $"Swal.fire('Error', 'Could not retrieve cart items!', 'error');", true);
                }

            }
            else
            {
                // Form validation failed, show popup
                ScriptManager.RegisterStartupScript(this, GetType(), "swalValidation", "Swal.fire('Validation Error', 'Please fill all the required fields correctly.', 'warning');", true);
            }

        }

        private bool IsValidForm()
        {
            // Add form validation here
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                // Show error message (you can use validation controls or client-side validation)
                return false;
            }

            return true;
        }

    }
}