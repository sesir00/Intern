using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demofirst
{
	public partial class List : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			txtName.Text = "Hello";
        }
	}
}