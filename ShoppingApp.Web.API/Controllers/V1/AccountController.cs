using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.AuthServices.JwtTokenServices;
using ShoppingApp.Web.API.Contracts.RequestModels.V1;
using ShoppingApp.Web.API.ApiRoutes.v1;
using ShoppingApp.Web.API.Contracts.ResponseModels.V1;
using ShoppingApp.Web.API.Filters;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels;
using AutoMapper;
using System;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;

namespace ShoppingApp.Web.API.Controllers
{
    // response modeller ve request modelleri uygun folderlerde yaratmaq ve burda istifade etmek lazimdi 
    [ApiController]
    [ApiKeyAuth]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;
        private readonly IUserIdentityService _userIdentityService;

        public AccountController(
                                 //IFacebookAuthService facebookAuthService,
                                 //                     IGoogleAuthService googleAuthService,
                                 IJwtTokenService jwtTokenService,
                                 IMediator mediator,
                                 IMapper mapper, IUserIdentityService userIdentityService)
        {
            _jwtTokenService = jwtTokenService;
            _mediatr = mediator;
            _mapper = mapper;
            _userIdentityService = userIdentityService;
        }

        [HttpPost(APIRoutes.Account.Login)]
        [AllowAnonymous]
        [Produces("application/json")]
        //[ProducesResponseType(200, Type = typeof(AuthResponceModel))]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var loginCommand = new LoginCommand(email: model.Email, password: model.Password);
            var response = await _mediatr.Send(loginCommand);
            if (!response.HasError)
            {
                var jwtId = Guid.NewGuid().ToString();
                var refreshTokenResult = await _userIdentityService.CreateRefreshTokenAsync(response.User.Id, jwtId);
                if (!refreshTokenResult.HasError)
                {
                    var claims = await _userIdentityService.GetClaimsAsync(response.User);
                    var roles = await _userIdentityService.GetRolesAsync(response.User);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwt = _jwtTokenService.CreateNewJwtToken(response.User, claims, jwtId);
                    return Ok(new RefreshAndJwtTokenResponseModel
                    {
                        RefreshToken = refreshTokenResult.RefreshToken,
                        JwtToken = jwt.JwtToken,
                        JwtExpirationDate = jwt.ExpirationDate,
                        Provider = "ShoppingApp"
                    });
                }
                if (refreshTokenResult.Error.Type == Utils.Enums.ErrorType.Model)
                {
                    return Unauthorized(refreshTokenResult.Error);
                }
                else
                {
                    return Unauthorized(new ErrorResponseModel
                    {

                        ErrorMessage = "Something went wrong!"
                    });
                }
            }
            var errors = response.Errors.Where(x => x.Type == Utils.Enums.ErrorType.Model).Select(x => new ErrorResponseModel
            {
                ErrorMessage = x.Message
            }).ToList();
            ErrorListResponseModel errorModel = new ErrorListResponseModel
            {
                Errors = errors.Count > 0 ? errors : new List<ErrorResponseModel> {
                    new ErrorResponseModel
                    {
                        ErrorMessage = "Something went wrong"
                    }
                }

            };

            return Unauthorized(errors);

        }

        [HttpPost(APIRoutes.Account.LoginWithFacebook)]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(401, Type = typeof(ErrorResponseModel))]
        // response modeller ve request modelleri uygun folderlerde yaratmaq ve burda istifade etmek lazimdi 
        public async Task<IActionResult> LoginWithFacebook([FromBody] LoginWithExternalProviderRequestModel model)
        {

            var command = new LoginWithFacebookCommand(token: model.Token, userType: Utils.Enums.UserType.Customer);
            var response = await _mediatr.Send(command);

            if (!response.HasError)
            {
                var jwtId = Guid.NewGuid().ToString();
                var refreshTokenResult = await _userIdentityService.CreateRefreshTokenAsync(response.User.Id, jwtId);
                if (!refreshTokenResult.HasError)
                {
                    var claims = await _userIdentityService.GetClaimsAsync(response.User);
                    var roles = await _userIdentityService.GetRolesAsync(response.User);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwt = _jwtTokenService.CreateNewJwtToken(response.User, claims, jwtId);
                    return Ok(new RefreshAndJwtTokenResponseModel
                    {
                        RefreshToken = refreshTokenResult.RefreshToken,
                        JwtToken = jwt.JwtToken,
                        JwtExpirationDate = jwt.ExpirationDate,
                        Provider = "ShoppingApp"
                    });
                }
                if (refreshTokenResult.Error.Type == Utils.Enums.ErrorType.Model)
                {
                    return Unauthorized(refreshTokenResult.Error);
                }
                else
                {
                    return Unauthorized(new ErrorResponseModel
                    {

                        ErrorMessage = "Something went wrong!"
                    });
                }
            }
            var errors = response.Errors.Where(x => x.Type == Utils.Enums.ErrorType.Model).Select(x => new ErrorResponseModel
            {
                ErrorMessage = x.Message
            }).ToList();
            ErrorListResponseModel errorModel = new ErrorListResponseModel
            {
                Errors = errors.Count > 0 ? errors : new List<ErrorResponseModel> {
                    new ErrorResponseModel
                    {
                        ErrorMessage = "Something went wrong"
                    }
                }

            };

            return Unauthorized(errors);
        }


        [HttpPost(APIRoutes.Account.LoginWithGoogle)]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(401, Type = typeof(ErrorResponseModel))]
        // response modeller ve request modelleri uygun folderlerde yaratmaq ve burda istifade etmek lazimdi 
        public async Task<IActionResult> LoginWithGoogle([FromBody] LoginWithExternalProviderRequestModel model)
        {
            var command = new LoginWithGoogleCommand(token: model.Token, userType: Utils.Enums.UserType.Customer);
            var response = await _mediatr.Send(command);

            if (!response.HasError)
            {
                var jwtId = Guid.NewGuid().ToString();
                var refreshTokenResult = await _userIdentityService.CreateRefreshTokenAsync(response.User.Id, jwtId);
                if (!refreshTokenResult.HasError)
                {
                    var claims = await _userIdentityService.GetClaimsAsync(response.User);
                    var roles = await _userIdentityService.GetRolesAsync(response.User);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwt = _jwtTokenService.CreateNewJwtToken(response.User, claims, jwtId);
                    return Ok(new RefreshAndJwtTokenResponseModel
                    {
                        RefreshToken = refreshTokenResult.RefreshToken,
                        JwtToken = jwt.JwtToken,
                        JwtExpirationDate = jwt.ExpirationDate,
                        Provider = "ShoppingApp"
                    });
                }
                if (refreshTokenResult.Error.Type == Utils.Enums.ErrorType.Model)
                {
                    return Unauthorized(refreshTokenResult.Error);
                }
                else
                {
                    return Unauthorized(new ErrorResponseModel
                    {

                        ErrorMessage = "Something went wrong!"
                    });
                }
            }
            var errors = response.Errors.Where(x => x.Type == Utils.Enums.ErrorType.Model).Select(x => new ErrorResponseModel
            {
                ErrorMessage = x.Message
            }).ToList();
            ErrorListResponseModel errorModel = new ErrorListResponseModel
            {
                Errors = errors.Count > 0 ? errors : new List<ErrorResponseModel> {
                    new ErrorResponseModel
                    {
                        ErrorMessage = "Something went wrong"
                    }
                }

            };

            return Unauthorized(errors);
        }



        [HttpPost(APIRoutes.Account.RefreshToken)]
        [AllowAnonymous]
        [Produces("application/json")]//register request model
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel model)
        {
            //var jwtTokenValidatioon = _jwtTokenService.GetClaimsPrincipalFromToken(model.JwtToken);
            //var refreshTokenCommand = new RefreshTokenCommand(refreshToken: model.RefreshToken, jwt: model.JwtToken);
            //var response = await _mediatr.Send(refreshTokenCommand);
            try
            {
                var oldJwtId = _jwtTokenService.GetTokenId(model.JwtToken);
                if (string.IsNullOrEmpty(oldJwtId))
                {
                    return Unauthorized(new ErrorResponseModel { ErrorMessage = "Invalid Token" });
                }

                var userId = _jwtTokenService.GetUserId(model.JwtToken);

                var user = await _userIdentityService.FindByIdAsync(userId);

                if (user.LockoutEnabled)
                {
                    return Unauthorized(new ErrorResponseModel { ErrorMessage = "You are locked out" });
                }

                var newJwtId = Guid.NewGuid().ToString();
                var newRefreshToken = await _userIdentityService.UpdateRefreshTokenAsync(model.RefreshToken, oldJwtId, newJwtId);

                if (newRefreshToken.HasError)
                {
                    return BadRequest(new ErrorResponseModel { ErrorMessage = "Could not create new refresh token" });
                }


                var claims = await _userIdentityService.GetClaimsAsync(user);
                var roles = await _userIdentityService.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var newJwt = _jwtTokenService.CreateNewJwtToken(user: user, userClaims: claims, tokenId: newJwtId);

                return Ok(new RefreshAndJwtTokenResponseModel
                {
                    JwtToken = newJwt.JwtToken,
                    RefreshToken = newRefreshToken.RefreshToken,
                    JwtExpirationDate = newJwt.ExpirationDate,
                    Provider = "ShoppingApp"
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new ErrorResponseModel { ErrorMessage = "Something went wrong" });

            }

        }

        [HttpPost(APIRoutes.Account.Register)]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
        {
            var registerCommand = new RegisterCommand(
                firstName: model.FirstName,
                lastName: model.LastName,
                email: model.Email,
                password: model.Password,
                userType: Utils.Enums.UserType.Customer);

            var response = await _mediatr.Send(registerCommand);

            if (!response.HasError)
            {
                var jwtId = Guid.NewGuid().ToString();
                var refreshTokenResult = await _userIdentityService.CreateRefreshTokenAsync(response.User.Id, jwtId);
                if (!refreshTokenResult.HasError)
                {
                    var claims = await _userIdentityService.GetClaimsAsync(response.User);
                    var roles = await _userIdentityService.GetRolesAsync(response.User);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwt = _jwtTokenService.CreateNewJwtToken(response.User, claims, jwtId);
                    return Ok(new RefreshAndJwtTokenResponseModel
                    {
                        RefreshToken = refreshTokenResult.RefreshToken,
                        JwtToken = jwt.JwtToken,
                        JwtExpirationDate = jwt.ExpirationDate,
                        Provider = "ShoppingApp"
                    });
                }
                if (refreshTokenResult.Error.Type == Utils.Enums.ErrorType.Model)
                {
                    return Unauthorized(refreshTokenResult.Error);
                }
                else
                {
                    return Unauthorized(new ErrorResponseModel
                    {

                        ErrorMessage = "Something went wrong!"
                    });
                }
            }
            var errors = response.Errors.Where(x => x.Type == Utils.Enums.ErrorType.Model).Select(x => new ErrorResponseModel
            {
                ErrorMessage = x.Message
            }).ToList();
            ErrorListResponseModel errorModel = new ErrorListResponseModel
            {
                Errors = errors.Count > 0 ? errors : new List<ErrorResponseModel> {
                    new ErrorResponseModel
                    {
                        ErrorMessage = "Something went wrong"
                    }
                }

            };

            return Unauthorized(errors);
        }
    }
}
