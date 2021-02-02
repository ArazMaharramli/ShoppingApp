using System;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.PaymentModels;
using ShoppingApp.Repository.Implementation.Repositories.PaymentRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.PaymentPersistences
{
    public class PaymentOptionRepository : Repository<PaymentOption>, IPaymentOptionRepository
    {
        public PaymentOptionRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
