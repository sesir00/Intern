using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Net;
using System.Security.Policy;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Demofirst.Dao; // Include the namespace where ProDao is defined

namespace Demofirst.Dao
{
    public partial class Users : System.Web.UI.Page
    {
        private readonly UserDao _dao = new UserDao(); // Initiate ProDao

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            try
            {
                DataTable dt = _dao.FetchUser(); // ✅ Fetch entire DataTable

                if (dt != null && dt.Rows.Count > 0)
                {
                    gvUsers.DataSource = dt;
                    gvUsers.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess", "toastr.success('Users loaded successfully!');", true);
                }
                else
                {
                    gvUsers.DataSource = null;
                    gvUsers.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastrInfo", "toastr.info('No users found.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", $"toastr.error('Error: {ex.Message}');", true);
            }
        }

        private void ClearField()
        {
            hfUserId.Value = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtType.Text = "";
            ddlIsActive.SelectedIndex = 0;
            btnSubmit.Text = "Add User"; // Reset button text
            formTitle.InnerText = "Add User"; // Reset form title

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {

            int id = string.IsNullOrEmpty(hfUserId.Value) ? 0 : Convert.ToInt32(hfUserId.Value);
            var name = txtName.Text;
            var email = txtEmail.Text;
            var phone = txtPhone.Text;
            var address = txtAddress.Text;
            var type = txtType.Text;
            var isActive = ddlIsActive.Text;
           
            if(id == 0)
            {
                var dr = _dao.AddUser(name, email, phone, address, type, isActive);
                //insert new user
                if (dr["code"].ToString().Equals("0")) // Success
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess", "toastr.success('User added successfully!');", true);
                }
                else // Failure
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Failed to add user. Please try again.');", true);
                }
            }
            else
            {
                var dr = _dao.EditUser(id, name, email, phone, address, type, isActive);
                //update existig user
                if (dr["code"].ToString().Equals("0")) // Success
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess", "toastr.success('User updated successfully!');", true);
                }
                else // Failure
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Failed to update user. Please try again.');", true);
                }
            }

            ClearField();
            LoadUsers();
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //get row index
            int rowIndex = e.NewEditIndex;
            GridViewRow row = gvUsers.Rows[rowIndex];

            // Extract data
            string id = gvUsers.DataKeys[rowIndex].Value.ToString();
            string name = row.Cells[1].Text;
            string email = row.Cells[2].Text;
            string phone = row.Cells[3].Text;
            string address = row.Cells[4].Text;
            string type = row.Cells[5].Text;
            string isActive = row.Cells[6].Text == "Active" ? "1" : "0";

            // Set values in the textboxes below
            hfUserId.Value = id;
            txtName.Text = name;
            txtEmail.Text = email;
            txtPhone.Text = phone;
            txtAddress.Text = address;
            txtType.Text = type;
            ddlIsActive.SelectedValue = isActive;

            //change the button text to Update User
            btnSubmit.Text = "Update user";
            formTitle.InnerText = "Update user";
        }

        //protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    //int id = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
        //    //GridViewRow row = gvUsers.Rows[e.RowIndex];

        //    //string name = (row.Cells[1].Controls[0] as TextBox).Text;
        //    //string email = (row.Cells[2].Controls[0] as TextBox).Text;
        //    //string phone = (row.Cells[3].Controls[0] as TextBox).Text;
        //    //string address = (row.Cells[4].Controls[0] as TextBox).Text;
        //    //string type = (row.Cells[5].Controls[0] as TextBox).Text;
        //    //string isActive = (row.Cells[6].Controls[0] as TextBox).Text;


        //    //var dr = _dao.EditUser(id, name, email, phone, address, type, isActive);
        //    //if (dr["code"].ToString().Equals("0")) // Success
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess", "toastr.success('User updated successfully!');", true);
        //    //}
        //    //else // Failure
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Failed to update user. Please try again.');", true);
        //    //}
        //    //gvUsers.EditIndex = -1;   // Exit edit mode
        //    //ClearField();
        //    //LoadUsers();









        //    //string query = "UPDATE Users SET Name=@Name, Email=@Email, Phone=@Phone, Address=@Address, Type=@Type, is_active=@IsActive WHERE Id=@Id";
        //    //SqlParameter[] parameters =
        //    //{
        //    //    new SqlParameter("@Id", id),
        //    //    new SqlParameter("@Name", name),
        //    //    new SqlParameter("@Email", email),
        //    //    new SqlParameter("@Phone", phone),
        //    //    new SqlParameter("@Address", address),
        //    //    new SqlParameter("@Type", type),
        //    //    new SqlParameter("@IsActive", isActive)
        //    //};
        //    //_dao.ExecuteProcedure(query, parameters);

        //    //gvUsers.EditIndex = -1;
        //    //LoadUsers();
        //}

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            LoadUsers();
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //    int id = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            //    //string query = "DELETE FROM Users WHERE Id=@Id";
            //    SqlParameter[] parameters =
            //    {
            //        new SqlParameter("@Id", id)
            //    };
            //    _dao.ExecuteProcedure("DeleteUser", parameters);

            int id = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            var dr = _dao.DeleteUser(id);
            if (dr["code"].ToString().Equals("0")) // Success
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrSuccess", "toastr.success('User deleted successfully!');", true);
            }
            else // Failure
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toastrError", "toastr.error('Failed to delete user. Please try again.');", true);
            }
            LoadUsers();
        }
    }
}
