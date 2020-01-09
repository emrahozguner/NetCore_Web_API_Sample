using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Domain.Services.Communication;

namespace API.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync(int? categoryId);
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(int id);
    }
}