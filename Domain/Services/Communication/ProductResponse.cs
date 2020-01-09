using API.Domain.Models;

namespace API.Domain.Services.Communication
{
    public class ProductResponse : BaseResponse
    {
        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            Product = product;
        }

        public ProductResponse(Product product) : this(true, string.Empty, product)
        {
        }

        public ProductResponse(string message) : this(false, message, null)
        {
        }

        public Product Product { get; }
    }
}