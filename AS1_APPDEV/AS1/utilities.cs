using AS1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AS1
{
    public static class utilities
    {
        public static class API
        {
            public static HttpClient conn()
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://localhost:7119/api/Product/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                return httpClient;
            }
        }
        public static class AS1
        { 
            public static async Task showAsync(DataGrid grid)
            {
                HttpClient httpClient = API.conn();
                var response = await httpClient.GetStringAsync("selectProducts");
                Response response_Json = JsonConvert.DeserializeObject<Response>(response);
                grid.ItemsSource = response_Json.products;
            }

            public static void copyToTextBox(Object ob, Products selected_row)
            {
                Admin objAdmin = new Admin();
                Sale objSales=new Sale();
                dynamic obj=null;
                if (objAdmin.GetType() == ob.GetType())
                {
                    obj = (Admin)ob;
                }
                else if (objSales.GetType() == ob.GetType())
                {
                    obj = (Sale)ob;
                }

                if (selected_row != null)
                {
                    obj.ProductId.Text =Convert.ToString(selected_row.Id);
                    obj.producName.Text = selected_row.Name;
                    obj.Amount.Text =Convert.ToString( selected_row.Amount);
                    obj.Price.Text = Convert.ToString(selected_row.Price);
                }

            }
        }
        public static class tools
        {
            public static Boolean numberValidation(TextCompositionEventArgs e)
            {

                e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);
                return e.Handled;
            }

        }
        public static  class sql
        {
            private static string getConnnectionString()
            {

                string connectionString = "Server=localhost;Database=FarmersMarket;Trusted_Connection=True; encrypt=false;";
                return connectionString;
            }
            private static SqlConnection con;
            private static SqlCommand cmd;

            private static  void establishConnection()
            {
                try
                {
                    con = new SqlConnection(getConnnectionString());
                    
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }

            //Threading for handling database operations
            public static async 
            //Threading for handling database operations
            Task
            Set(string query)
            {
                try
                {
                    establishConnection();
                    if (con.State != ConnectionState.Open)
                    {
                        await con.OpenAsync();
                    }
                    using (cmd = new SqlCommand(query, con))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    MessageBox.Show("Executed Successfully");
                    con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            public static DataTable Get(string query)
            {
                DataTable dt = new DataTable();
                try
                {
                    establishConnection();
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.Fill(dt);
                    con.Close();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return dt;
            }
        }
    }

}
