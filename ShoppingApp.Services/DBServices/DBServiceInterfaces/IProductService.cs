using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface IProductService
    {
        Task<bool> IsSlugAvailableAsync(string slug, string productId = null);
        Task<Product> CreateProductAsync(Product product);
    }
}
