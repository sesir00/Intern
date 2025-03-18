using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Demofirst.Dao
{
	public class LoginDao: ProDao
	{
		public DataRow RegisterUser(string username, string email, string password, string role)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("EXEC proc_Register @flag='i', @Username={0}, @Email={1}, @Password={2}, @Role={3} ", FilterString(username),FilterString(email), FilterString(password), FilterString(role));
			return ExecuteDataRow(sb.ToString());
		}

		public DataRow LoginUser(string username, string pws)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("EXEC proc_Register @flag='f', @Username={0}, @Password={1}", FilterString(username), FilterString(pws) );
			return ExecuteDataRow(sb.ToString());
        }




	}
}