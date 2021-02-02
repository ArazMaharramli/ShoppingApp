using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.OrderModels;
using ShoppingApp.Repository.Implementation.Repositories.OrderRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.OrderPersistences
{
    public class OrderItemNoteRepository : Repository<OrderItemNote>, IOrderItemNoteRepository
    {
        public OrderItemNoteRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
