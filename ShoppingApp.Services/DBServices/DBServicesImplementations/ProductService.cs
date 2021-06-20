using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Repositories;

namespace ShoppingApp.Services.DBServices.DBServicesImplementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            if ((await IsSlugAvailableAsync(product.UniqueSlug, product.GlobalId)))
            {
                _unitOfWork.Products.Add(product);
                await _unitOfWork.SaveChangesAsync();
                return product;
            }
            return null;
        }

        public async Task<bool> IsSlugAvailableAsync(string slug, string productId = null)
        {
            return (await _unitOfWork.Products.GetAsync(x => x.UniqueSlug != slug.ToLower().Trim() && x.GlobalId != productId)) == null;
        }
    }
}
