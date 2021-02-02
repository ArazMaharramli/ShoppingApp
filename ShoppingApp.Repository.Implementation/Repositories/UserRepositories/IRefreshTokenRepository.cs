using System;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Repository.Repositories;

namespace ShoppingApp.Repository.Implementation.Repositories.UserRepositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {

    }
}
