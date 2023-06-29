using FarmersMarketAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FarmersMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("selectProducts")]

        public Response selectProducts()
        {
            Response response = new Response();
            Application apl = new Application();
            response = apl.selectProducts();
            return response;
        } 

        [HttpPost]
        [Route("InsertProduct")]
        public async Task<Response> InsertProduct(Products products)
        {
            Application apl = new Application();
            Response response = await apl.InsertProduct(products); 
            return response;

        }

        [HttpPut]
        [Route("UpdateProduct")]
        public Task<Response> UpdateProduct(Products products)
        {
            Application apl = new Application();
            Task<Response> response = apl.UpdateProduct(products);
            return response;

        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public Task<Response> DeleteProduct(int id)
        {
            Application apl = new Application();
            Task<Response> response = apl.DeleteProduct(id);
            return response;

        }

        [HttpPost]
        [Route("InsertSales")]
        public async Task<Response> InsertSales(Sales sale)
        {
            Application apl = new Application();
            Response response = await apl.InsertSales(sale);
            return response;

        }

        [HttpPut]
        [Route("UpdateSales")]
        public Task<Response> UpdateSales(Sales sale)
        {
            Application apl = new Application();
            Task<Response> response = apl.UpdateSales(sale);
            return response;

        }
    }
}
