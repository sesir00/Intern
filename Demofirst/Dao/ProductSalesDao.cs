using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Demofirst.Dao
{
	public class ProductSalesDao: ProDao
	{
        public DataRow AddProduct(string productName, int quantity, double price, string added_by, string isActive, string ImagePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='i', @ProductName={0}, @Quantity={1}, @Price={2} , @AddedBy={3}, @IsActive={4}, @ImagePath={5} ", FilterString(productName), quantity, price, FilterString(added_by), isActive, FilterString(ImagePath));

            return ExecuteDataRow(sb.ToString());
        }

        public DataSet FetchProduct()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='f' ");

            return ExecuteDataset(sb.ToString());
        }

        public DataRow UpdateProduct(string product_name, int quantity, double price, string isActive, string ImagePath, int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='u', @ProductName={0}, @Quantity={1}, @Price={2} , @IsActive={3}, @ImagePath={4}, @Id={5}", FilterString(product_name), quantity, price, FilterString(isActive), FilterString(ImagePath), id);

            return ExecuteDataRow(sb.ToString());
        }

        public DataRow DeleteProduct(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='d', @Id={0} ", id);

            return ExecuteDataRow(sb.ToString());
        }

        public DataRow AddSales(string buyer_name, int quantity_purchased, double price, int product_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='is', @BuyerName={0}, @QuantityPurchased={1}, @Price={2} , @ProductID={3} ", FilterString(buyer_name), quantity_purchased, price, product_id);

            return ExecuteDataRow(sb.ToString());
        }

        public DataSet FetchSales()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='fs' ");

            return ExecuteDataset(sb.ToString());
        }

        public DataSet CartFetch(string added_by)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='cf', @AddedBy={0} ", added_by);

            return ExecuteDataset(sb.ToString());
        }

        public DataRow CartDelete(int productId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='cd', @ProductID={0}", productId);

            return ExecuteDataRow(sb.ToString());
        }
        public DataSet FetchCategory()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='fc'");

            return ExecuteDataset(sb.ToString());
        }

        public DataRow AddtoCart(int productId, int quantity, double price, string added_by)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='atc', @ProductID={0}, @Quantity={1}, @Price={2}, @AddedBy={3} ", productId, quantity, price, added_by);

            return ExecuteDataRow(sb.ToString());
        }
        public DataRow UpdateCartQuantity(int productId, int change, string added_by)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='ucq',  @ProductID={0}, @Change={1}, @AddedBy={2} ", productId, change, FilterString(added_by));

            return ExecuteDataRow(sb.ToString());
        }
        public DataSet AddtoCheckout(string added_by)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='che', @AddedBy={0}", FilterString(added_by));

            return ExecuteDataset(sb.ToString());
;       }
        public DataRow CheckoutProceed(string buyer_name, int quantity_purchased, double price, int product_id, string added_by)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EXEC proc_Product @flag='cpr', @BuyerName={0}, @QuantityPurchased={1}, @Price={2}, @ProductID={3}, @AddedBy={4} ", FilterString(buyer_name), quantity_purchased, price, product_id, FilterString(added_by));

            return ExecuteDataRow(sb.ToString());
        }
    }
}

    