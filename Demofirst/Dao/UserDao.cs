using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Demofirst.Dao
{
	public class UserDao : ProDao
	{
		public DataRow AddUser()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("EXEC InsertContact @flag='i', @Name='{0}', @Email='{1}', @Message='{2}'", FilterString(name), email, message);

			return ExecuteDataRow(sb.ToString());
		}
	}
}