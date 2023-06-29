using FarmersMarketAPI.Models;
using System.Data;

namespace FarmersMarketAPI.DataAccessLayer
{
    public static class dal
    {
        public static class get
        {
            public static List<Products> selectProducts()
            {
                List <Products> list= new List<Products>();
                DataTable dt = utilities.sql.Get("Select * from Products");
                foreach (DataRow row in dt.Rows)
                {
                    Products products = new Products();
                    products.Id = (int)row[0];
                    products.Name = (string)row[1];
                    products.Amount = Convert.ToDecimal( row[2]);
                    products.Price = Convert.ToDecimal(row[3]);

                    list.Add(products);
                }

                return list;
            }
        }
        public static class set
        {
            public static async Task<bool> InsertProduct(string name, decimal amount, decimal price)
            {
                return  await utilities.sql.Set("INSERT INTO [dbo].[Products] VALUES ('" + name + "'," + amount + "," + price + ")");
            }

            public static async Task<bool> UpdateProduct(int id, string name, decimal amount, decimal price)
            {
                return await utilities.sql.Set("Update [dbo].[Products] Set [name] = '" + name + "',[Amount] ='" + amount + "',[price] ='" + price + "' Where [Id]=" + id + "");
            }

            public static async Task<bool> DeleteProduct(int id)
            {
                return await utilities.sql.Set("Delete From[dbo].[Products] Where[Id]="+id+"");
            }


            public static async Task<bool> InsertSales(Sales sale)
            {
                return await utilities.sql.Set("INSERT INTO [dbo].[sales] VALUES ('" + sale.Date + "','" + sale.Product + "'," +sale.Quantity + "," + sale.Price + "," + sale.Total + ")");
            }

            public static async Task<bool> UpdateSales(Sales sale)
            {
                return await utilities.sql.Set("update [dbo].[Products] set Amount=(select Amount from [dbo].[Products] where id=" + sale.Id + ")-(" + (sale.Quantity + ") where id=" + sale.Id + ""));
            }

        }
    }
}
