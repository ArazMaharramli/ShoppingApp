using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.ProductModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface ITagService
    {
        Task<Tag> GetByName(string name);
        Task<Tag> CreateAsync(string name);
    }
}
