using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            string query = "SELECT * FROM Users";
            DataTable dt = _dao.ExecuteDataTable(query);
            gvUsers.DataSource = dt;
            gvUsers.DataBind();

            //// Trigger a client-side page reload
            //ScriptManager.RegisterStartupScript(this, GetType(), "reloadPage", "location.reload();", true);
        }
        private void ClearField()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtType.Text = "";
            ddlIsActive.Text = "";
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            var dr = _dao.AddUser();
            if (dr["code"].ToString().Equals(0))
            {
                //success
            }
            else
            {
                //failed
            }
                ClearField();
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            LoadUsers();
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvUsers.Rows[e.RowIndex];

            string name = (row.Cells[1].Controls[0] as TextBox).Text;
            string email = (row.Cells[2].Controls[0] as TextBox).Text;
            string phone = (row.Cells[3].Controls[0] as TextBox).Text;
            string address = (row.Cells[4].Controls[0] as TextBox).Text;
            string type = (row.Cells[5].Controls[0] as TextBox).Text;
            string isActive = (row.Cells[6].Controls[0] as TextBox).Text;

            string query = "UPDATE Users SET Name=@Name, Email=@Email, Phone=@Phone, Address=@Address, Type=@Type, is_active=@IsActive WHERE Id=@Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", name),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Address", address),
                new SqlParameter("@Type", type),
                new SqlParameter("@IsActive", isActive)
            };
            _dao.ExecuteProcedure(query, parameters);

            gvUsers.EditIndex = -1;
            LoadUsers();
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            LoadUsers();
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            //string query = "DELETE FROM Users WHERE Id=@Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", id)
            };
            _dao.ExecuteProcedure("DeleteUser", parameters);
            LoadUsers();
        }
    }
}
