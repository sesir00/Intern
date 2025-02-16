using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Demofirst.Dao
{
    public class ProDao
    {
        private SqlConnection _connection;
        public ProDao()
        {
            Init();
        }

        private void Init()
        {
            _connection = new SqlConnection(GetConnectionString());
        }

        private void OpenConnection()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
            _connection.Open();
        }

        private void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
                this._connection.Close();
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["connectionString"].ToString();
        }

        private int GetCommandTimeOut()
        {
            int cto = 0;
            try
            {
                int.TryParse("30", out cto);
                if (cto == 0)
                    cto = 30;
            }
            catch (Exception ex)
            {
                cto = 30;
            }
            return cto;
        }

        public DataSet ExecuteDatasetV2(string sql)
        {
            var ds = new DataSet();
            var cmd = new SqlCommand(sql, _connection);
            cmd.CommandTimeout = GetCommandTimeOut();
            SqlDataAdapter da;
            try
            {
                OpenConnection();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                da = null;
                cmd.Dispose();
                CloseConnection();
            }
            return ds;
        }

        public DataSet ExecuteDataset(string sql)
        {
            var ds = new DataSet();
            var cmd = new SqlCommand(sql, _connection);
            cmd.CommandTimeout = GetCommandTimeOut();
            SqlDataAdapter da;
            try
            {
                OpenConnection();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                da = null;
                cmd.Dispose();
                CloseConnection();
            }
            return ds;
        }

        public DataTable ExecuteDataTable(string sql)
        {
            using (var ds = ExecuteDataset(sql))
            {
                if (ds == null || ds.Tables.Count == 0)
                    return null;

                return ds.Tables[0];
            }
        }

        public DataRow ExecuteDataRow(string sql)
        {
            using (var ds = ExecuteDataset(sql))
            {
                if (ds == null || ds.Tables.Count == 0)
                    return null;

                if (ds.Tables[0].Rows.Count == 0)
                    return null;

                return ds.Tables[0].Rows[0];
            }
        }

        public String GetSingleResult(string sql)
        {
            try
            {
                var ds = ExecuteDataset(sql);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return "";

                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public string RemoveDecimal(double amt)
        {
            return Math.Floor(amt).ToString();
        }

        public String FilterStringNative(string strVal)
        {
            var str = FilterQuoteNative(strVal);

            if (str.ToLower() != "null")
                str = "'" + str + "'";

            return str;
        }

        public String FilterString(string strVal)
        {
            var str = FilterQuote(strVal);

            if (str.ToLower() != "null")
                str = "'" + str + "'";

            return str;
        }

        private string Encode(string strVal)
        {
            var sb = new StringBuilder(HttpUtility.HtmlEncode(HttpUtility.JavaScriptStringEncode(strVal)));
            // Selectively allow  <b> and <i>
            sb.Replace("&lt;b&gt;", "<b>");
            sb.Replace("&lt;/b&gt;", "");
            sb.Replace("&lt;i&gt;", "<i>");
            sb.Replace("&lt;/i&gt;", "");
            return sb.ToString();
        }

        public String FilterQuoteNative(string strVal)
        {
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = "";
            }
            var str = Encode(strVal.Trim());

            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(";", "");
                //str = str.Replace(",", "");
                str = str.Replace("--", "");
                str = str.Replace("'", "");

                str = str.Replace("/*", "");
                str = str.Replace("*/", "");

                str = str.Replace(" select ", "");
                str = str.Replace(" insert ", "");
                str = str.Replace(" update ", "");
                str = str.Replace(" delete ", "");

                str = str.Replace(" drop ", "");
                str = str.Replace(" truncate ", "");
                str = str.Replace(" create ", "");

                str = str.Replace(" begin ", "");
                str = str.Replace(" end ", "");
                str = str.Replace(" char(", "");

                str = str.Replace(" exec ", "");
                str = str.Replace(" xp_cmd ", "");

                str = str.Replace("<script", "");
            }
            else
            {
                str = "null";
            }
            return str;
        }

        public String FilterQuote(string strVal)
        {
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = "";
            }

            var str = strVal.Trim();

            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(";", "");
                //str = str.Replace(",", "");
                str = str.Replace("--", "");
                str = str.Replace("'", "");

                str = str.Replace("/*", "");
                str = str.Replace("*/", "");

                str = str.Replace(" select ", "");
                str = str.Replace(" insert ", "");
                str = str.Replace(" update ", "");
                str = str.Replace(" delete ", "");

                str = str.Replace(" drop ", "");
                str = str.Replace(" truncate ", "");
                str = str.Replace(" create ", "");

                str = str.Replace(" begin ", "");
                str = str.Replace(" end ", "");
                str = str.Replace(" char(", "");

                str = str.Replace(" exec ", "");
                str = str.Replace(" xp_cmd ", "");
                str = str.Replace("svg/onload", "");

                str = str.Replace("<script", "");
            }
            else
            {
                str = "null";
            }
            return str;
        }



        #region ParseReportResult2

        private bool HasError(DataTable dt)
        {
            if (dt.Columns.IndexOf("ErrorMsg") < 0)
            {
                return false;
            }
            return true;
        }

        public string GetRowData(ref DataRow dr, string fieldName, string defValue)
        {
            if (dr == null) return defValue;

            if (dr.Table.Columns.IndexOf(fieldName) < 0)
            {
                return defValue;
            }

            return dr[fieldName].ToString();
        }

        public string GetRowData(ref DataRow dr, string fieldName)
        {
            return GetRowData(ref dr, fieldName, "");
        }

        private bool ParseBool(string data)
        {
            bool tmpData;
            bool.TryParse(data, out tmpData);
            return tmpData;
        }

        private int ParseInt(string data)
        {
            int tmpData;
            int.TryParse(data, out tmpData);
            return tmpData;
        }

        #endregion ParseReportResult2

        public string AutoSelect(string str1, string str2)
        {
            if (str1.ToLower() == str2.ToLower())
                return "selected=\"selected\"";

            return "";
        }

        public DataTable getTable(string sql)
        {
            return ExecuteDataTable(sql);
        }

        public void ExecuteProcedure(string spName, SqlParameter[] param)
        {
            using (SqlConnection _connection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    _connection.Open();
                    SqlCommand command = new SqlCommand(spName, _connection);
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter p in param)
                    {
                        command.Parameters.Add(p);
                    }
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string DataTableToText(ref DataTable dt, string delemeter, Boolean includeColHeader)
        {
            var sb = new StringBuilder();
            var del = "";
            var rowcnt = 0;
            if (includeColHeader)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    sb.Append(del);
                    sb.Append(col.ColumnName);
                    del = delemeter;
                }
                rowcnt++;
            }

            foreach (DataRow row in dt.Rows)
            {
                if (rowcnt > 0)
                {
                    sb.AppendLine();
                }
                del = "";
                foreach (DataColumn col in dt.Columns)
                {
                    sb.Append(del);
                    sb.Append((row[col.ColumnName].ToString().Replace(",", "")));
                    del = delemeter;
                }
                rowcnt++;
            }
            return sb.ToString();
        }
    }
}