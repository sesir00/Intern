using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace Demofirst.Dao
{
    public class UserDao : ProDao
    {
        public DataRow AddUser(string name, string email, string phone, string address, string type, string isActive)
        {
            StringBuilder sb = new StringBuilder();
			sb.AppendFormat("EXEC proc_ProductMaster @flag='i', @Name={0}, @Email={1}, @Phone={2} , @Address={3} , @Type={4} , @IsActive={5} ", FilterString(name), FilterString(email), FilterString(phone), FilterString(address), FilterString(type), FilterString(isActive));

			return ExecuteDataRow(sb.ToString());
		}
        public DataTable FetchUser()
        {
            
            StringBuilder sb = new StringBuilder();
			sb.AppendFormat("EXEC proc_ProductMaster @flag = 'f' ");

			return ExecuteDataTable(sb.ToString());
        }
        public DataRow DeleteUser(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_ProductMaster @flag='d', @Id='{0}' ", id );

            return ExecuteDataRow(sb.ToString());

        }
        public DataRow EditUser(string name, string email, string phone, string address, string type, string isActive)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_ProductMaster @flag='e', @Name={0}, @Email={1}, @Phone={2} , @Address={3} , @Type={4} , @IsActive={5} ", FilterString(name), FilterString(email), FilterString(phone), FilterString(address), FilterString(type), FilterString(isActive));

            return ExecuteDataRow(sb.ToString());
        }
	}
}