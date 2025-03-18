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
	public partial class List : System.Web.UI.Page
	{
		private readonly ProductSalesDao _dao = new ProductSalesDao();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadSalesData();
			}
        }

		private void LoadSalesData()
		{
			try
			{
                DataSet ds = _dao.FetchSales();
                if (ds.Tables.Count > 0 && ds.Tables[1].Rows[0]["code"].ToString().Equals("0"))
                {
                    rptSales.DataSource = ds.Tables[0];
                    rptSales.DataBind();

                    // ✅ Show Success Alert
                    ScriptManager.RegisterStartupScript(this, GetType(), "success",
                        "Swal.fire({ icon: 'success', title: 'Success!', text: 'Sales data loaded successfully!' });", true);
                }
                else
                {
                    // ❌ Show No Data Alert
                    ScriptManager.RegisterStartupScript(this, GetType(), "nodata",
                        "Swal.fire({ icon: 'info', title: 'No Data', text: 'No sales records found.' });", true);
                }
            }
            catch(Exception ex)
            {
                // ❌ Show Error Alert
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"Swal.fire({{ icon: 'error', title: 'Error!', text: '{ex.Message}' }});", true);
            }
			
		}

    }
}