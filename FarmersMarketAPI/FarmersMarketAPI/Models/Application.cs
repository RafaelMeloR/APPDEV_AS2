using FarmersMarketAPI.DataAccessLayer; 

namespace FarmersMarketAPI.Models
{
    public class Application
    {
        public Response selectProducts()
        {
            Response response = new Response();
            List<Products> products = dal.get.selectProducts();

            if (products.Count > 0)
            {

                response.statusCode = 200;
                response.statusMessage = "Data retrievable is successful";
                response.products = products;
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data retrievable";
            }
            return response;
        }

        public async Task<Response> InsertProduct(Products products)
        {
            Response response = new Response();
            bool state = await dal.set.InsertProduct(products.Name, products.Amount, products.Price);

            if (state)
            {
                response.statusCode = 200;
                response.statusMessage = "Data entry Successfully";
                response.product = products;
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data inserted";
                response.product = null;
            }

            return response;

        }

        public async Task<Response> UpdateProduct(Products product)
        {
            Response response = new Response();
            bool state = await dal.set.UpdateProduct(product.Id, product.Name, product.Amount, product.Price);

            if (state)
            {
                response.statusCode = 200;
                response.statusMessage = "Data updated Successfully";
                response.product = product;
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data updated";
                response.product = null;
            }

            return response;

        }

        public async Task<Response> DeleteProduct(int id)
        {
            Response response = new Response(); 
            bool state = await dal.set.DeleteProduct(id);

            if (state)
            {
                response.statusCode = 200;
                response.statusMessage = "Data deleted Successfully";
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data deleted";
            }


            return response;
        }

        public async Task<Response> InsertSales(Sales sale)
        {
            Response response = new Response();
            bool state = await dal.set.InsertSales(sale);

            if (state)
            {
                response.statusCode = 200;
                response.statusMessage = "Data entry Successfully";
                response.sale = sale;
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data inserted";
                response.sale = null;
            }

            return response;

        }

        public async Task<Response> UpdateSales(Sales sale)
        {
            Response response = new Response();
            bool state = await dal.set.UpdateSales( sale);

            if (state)
            {
                response.statusCode = 200;
                response.statusMessage = "Data updated Successfully";
                response.sale = sale;
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data updated";
                response.sale = null;
            }

            return response;

        }
    }
}
