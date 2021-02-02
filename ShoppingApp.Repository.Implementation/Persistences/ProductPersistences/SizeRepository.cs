﻿using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Repository.Implementation.Repositories.ProductRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.ProductPersistences
{
    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        public SizeRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }

}
