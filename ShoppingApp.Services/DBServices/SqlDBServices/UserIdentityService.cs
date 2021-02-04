using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Domain.Data;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Persistence;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.SqlDBServices
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserIdentityService(DbContextOptions<ShoppingAppDbContext> contextOptions, UserManager<User> userManager)
        {

            _unitOfWork = new UnitOfWork(new ShoppingAppDbContext(contextOptions));
            _userManager = userManager;
        }

        public Task<User> FindByIdAsync(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public async Task<RefreshTokenResponseModel> CreateRefreshTokenAsync(string userId, string jwtId)
        {
            try
            {
                var refreshTokenInDb = await _unitOfWork.RefreshTokens.GetAsync(x => x.JwtId == jwtId);
                if (refreshTokenInDb != null)
                {
                    return new RefreshTokenResponseModel
                    {
                        HasError = true,
                        Error = new InternalErrorModel
                        {
                            Type = Utils.Enums.ErrorType.Model,
                            Message = "This JWT is in use. Try an other one."
                        }
                    };
                }

                var newRefreshToken = new RefreshToken
                {
                    JwtId = jwtId,
                    Token = Guid.NewGuid().ToString("N"),
                    ExpireDate = DateTime.Now.AddMonths(6),
                    Status = Utils.Enums.RefreshTokenStatus.Active,
                    UserId = userId
                };

                _unitOfWork.RefreshTokens.Add(newRefreshToken);
                await _unitOfWork.SaveChangesAsync();

                return new RefreshTokenResponseModel
                {
                    RefreshToken = newRefreshToken.Token,
                    ExpireDate = newRefreshToken.ExpireDate,
                    HasError = false
                };
            }
            catch (Exception ex)
            {
                return new RefreshTokenResponseModel
                {
                    HasError = true,
                    Error = new InternalErrorModel
                    {
                        Type = Utils.Enums.ErrorType.Exception,
                        Message = ex.InnerException.Message
                    }
                };
            }
        }

        public async Task<RefreshTokenResponseModel> UpdateRefreshTokenAsync(string oldRefreshToken, string oldJwtId, string newJwtId)
        {
            var refreshTokenInDb = await _unitOfWork.RefreshTokens.GetAsync(x => x.JwtId == oldJwtId);
            if (refreshTokenInDb == null)
            {
                return new RefreshTokenResponseModel
                {
                    HasError = true,
                    Error = new InternalErrorModel
                    {
                        Type = Utils.Enums.ErrorType.Model,
                        Message = "Not Found"
                    }
                };
            }
            if (refreshTokenInDb.Token != oldRefreshToken)
            {
                return new RefreshTokenResponseModel
                {
                    HasError = true,
                    Error = new InternalErrorModel
                    {
                        Type = Utils.Enums.ErrorType.Model,
                        Message = "RefreshToken does not match. Please enter valid one"
                    }
                };
            }
            if (refreshTokenInDb.Status != Utils.Enums.RefreshTokenStatus.Active)
            {
                return new RefreshTokenResponseModel
                {
                    HasError = true,
                    Error = new InternalErrorModel
                    {
                        Type = Utils.Enums.ErrorType.Model,
                        Message = "RefreshToken lifetime is expired. Please log in again."
                    }
                };
            }

            refreshTokenInDb.Status = Utils.Enums.RefreshTokenStatus.Used;
            _unitOfWork.RefreshTokens.Update(refreshTokenInDb);

            try
            {
                var newRefreshToken = new RefreshToken
                {
                    JwtId = newJwtId,
                    Token = Guid.NewGuid().ToString("N"),
                    ExpireDate = DateTime.Now.AddMonths(6),
                    Status = Utils.Enums.RefreshTokenStatus.Active,
                    UserId = refreshTokenInDb.UserId
                };

                _unitOfWork.RefreshTokens.Add(newRefreshToken);
                await _unitOfWork.SaveChangesAsync();

                return new RefreshTokenResponseModel
                {
                    RefreshToken = newRefreshToken.Token,
                    ExpireDate = newRefreshToken.ExpireDate,
                    HasError = false
                };
            }
            catch (Exception ex)
            {
                return new RefreshTokenResponseModel
                {
                    HasError = true,

                    Error = new InternalErrorModel
                    {
                        Type = Utils.Enums.ErrorType.Exception,
                        Message = ex.Message
                    }
                };
            }

        }

        public Task<IList<Claim>> GetClaimsAsync(User user)
        {
            return _userManager.GetClaimsAsync(user);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo loginInfo)
        {
            return _userManager.AddLoginAsync(user, loginInfo);
        }

        public Task<IdentityResult> CreateAsync(User user, string password = null)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<User> FindByLoginAsync(string providerName, string key)
        {
            return _userManager.FindByLoginAsync(providerName, key);
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public Task<bool> IsEmailConfirmedAsync(User user)
        {
            return _userManager.IsEmailConfirmedAsync(user);
        }
    }
}
