using Demofirst.Dao;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demofirst
{
    public partial class SellerDashboard : System.Web.UI.Page
    {
        private readonly ProductSalesDao _dao = new ProductSalesDao();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString().ToLower() != "seller")
            {
                Response.Redirect("Login.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                return;
            }
            if (!IsPostBack)
            {
                LoadSellerProducts();
                BindCategoryDropdown();
            }
        }

        private void ClearField()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
        }

        private void LoadSellerProducts()
        {
            try
            {
                DataSet dt = _dao.FetchProduct();
                if (dt.Tables.Count > 0 && dt.Tables[1].Rows[0]["code"].ToString().Equals("0"))
                {
                    gvSellerProducts.DataSource = dt;
                    gvSellerProducts.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess", "toastr.success('Product loaded successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastrFailure", "toastr.error('Error loading Product!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", $"toastr.error('Error loading products: {ex.Message}');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int productId = string.IsNullOrEmpty(txtProductID.Text) ? 0 : Convert.ToInt32(txtProductID.Text);
                string name = txtProductName.Text.Trim();
                int quantity = Convert.ToInt32(txtQuantity.Text);
                double price = Convert.ToDouble(txtPrice.Text);
                string added_by = Session["Username"]?.ToString() ?? "Unknown"; // Null-safe
                string imagePath = string.Empty;

                if (fileUpload.HasFile)
                {
                    // Match web.config limit (10 MB = 10485760 bytes)
                    const int maxFileSizeBytes = 10485760;
                    if (fileUpload.PostedFile.ContentLength > maxFileSizeBytes)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('File exceeds 10 MB limit. Please upload a smaller file.');", true);
                        return;
                    }
                    string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (allowedExtensions.Contains(fileExtension))
                    {
                        string folderPath = Server.MapPath("~/Images/"); // Fixed from "~/Images/th.jfif/"
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string fileName = Guid.NewGuid().ToString() + fileExtension;
                        string filePath = folderPath + fileName;

                        fileUpload.SaveAs(filePath);
                        imagePath = "~/Images/" + fileName;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Invalid image file format. Please upload .jpg, .jpeg, .png, or .gif files.');", true);
                        return;
                    }
                }

                if (productId == 0)
                {
                    DataRow dr = _dao.AddProduct(name, quantity, price, added_by, "1", imagePath);
                    if (dr["code"].ToString().Equals("0"))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess", "toastr.success('Product added successfully!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Failed to add product: " + dr["message"]?.ToString() + "');", true);
                    }
                }
                else
                {
                    DataRow dr = _dao.UpdateProduct(name, quantity, price, "1", imagePath, productId);
                    if (dr["code"].ToString().Equals("0"))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess", "toastr.success('Product edited successfully!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Failed to edit product: " + dr["message"]?.ToString() + "');", true);
                    }
                }

                ClearField();
                LoadSellerProducts();
            }
            catch (ThreadAbortException)
            {
                // Ignore ThreadAbortException (it might still occur in rare cases)
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", $"toastr.error('Error saving product: {ex.Message}');", true);
            }
        }

        protected void gvSellerProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteProduct")
                {
                    int productId = Convert.ToInt32(e.CommandArgument);
                    DeleteProduct(productId);
                }
                else if (e.CommandName == "EditProduct")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvSellerProducts.Rows[rowIndex];

                    txtProductID.Text = ((Label)row.FindControl("lblProductID")).Text;
                    txtProductName.Text = ((Label)row.FindControl("lblProductName")).Text;
                    txtQuantity.Text = ((Label)row.FindControl("lblQuantity")).Text;
                    txtPrice.Text = ((Label)row.FindControl("lblPrice")).Text;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", $"toastr.error('Row command error: {ex.Message}');", true);
            }
        }

        private void DeleteProduct(int productId)
        {
            try
            {
                DataRow dr = _dao.DeleteProduct(productId);
                if (dr["code"].ToString().Equals("0"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess", "toastr.success('Product deleted successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Failed to delete product: " + dr["message"]?.ToString() + "');", true);
                }
                LoadSellerProducts();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", $"toastr.error('Delete error: {ex.Message}');", true);
            }
        }

        protected void BindCategoryDropdown()
        {
            DataSet ds = _dao.FetchCategory();
            if(ds.Tables.Count > 0 && ds.Tables[1].Rows[0]["code"].ToString().Equals("0"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "swalBuy", $"Swal.fire('Added', 'Category fetched successfully!', 'success');", true);

                // Set the DataSource to the table containing category data (usually Table[0])
                ddlCategory.DataSource = ds.Tables[0]; // Assuming Table[0] has the category data
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();

                // Insert the default "Select Category" item
                ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Failed to fetch categories: " + ds.Tables[1].Rows[0]["message"]?.ToString() + "');", true);
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}