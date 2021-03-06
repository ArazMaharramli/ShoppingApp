﻿using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Repository.Implementation.Repositories.UserRepositories;
using ShoppingApp.Repository.Persistences;

namespace ShoppingApp.Repository.Implementation.Persistences.UserPersistences
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ShoppingAppDbContext context) : base(context)
        {

        }

        public ShoppingAppDbContext ShoppingAppDbContext => Context as ShoppingAppDbContext;
    }
}
