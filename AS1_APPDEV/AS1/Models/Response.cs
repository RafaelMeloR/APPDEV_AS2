using System.Collections.Generic;

namespace AS1.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public string statusMessage { get; set; }

        public Products product { get; set; }
        public List<Products> products { get; set; }
    }
}
