namespace FarmersMarketAPI.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public string statusMessage { get; set; }

        public Products product { get; set; }
        public List<Products> products { get; set; }
        public Sales sale { get; set; }
        public List<Sales> sales { get; set; }
    }
}
