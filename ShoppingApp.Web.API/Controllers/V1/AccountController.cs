using System.Threading.Tasks;
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
using ShoppingApp.Web.API.ExtentionMethods;

namespace ShoppingApp.Web.API.Controllers
{
    // response modeller ve request modelleri uygun folderlerde yaratmaq ve burda istifade etmek lazimdi 
    [ApiController]
    [ApiKeyAuth]
    public class AccountController : Controller
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;
        private readonly IUserIdentityService _userIdentityService;

        public AccountController(
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
        [Produces("application/json")]
        //[ProducesResponseType(200, Type = typeof(AuthResponceModel))]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var loginCommand = new LoginCommand(email: model.Email, password: model.Password);
            var response = await _mediatr.Send(loginCommand);
            if (!response.HasError)
            {

                var refreshTokenResult = await _userIdentityService.CreateRefreshTokenAsync(response.User.Id);
                if (!refreshTokenResult.HasError)
                {
                    var claims = await _userIdentityService.GetAllRolesAndClaimsAsync(response.User);
                    var jwt = _jwtTokenService.CreateNewJwtToken(response.User, claims, refreshTokenResult.JwtId);
                    return Ok(new RefreshAndJwtTokenResponseModel
                    {
                        RefreshToken = refreshTokenResult.RefreshToken,
                        JwtToken = jwt.JwtToken,
                        JwtExpirationDate = jwt.ExpirationDate,
                        Provider = "ShoppingApp"
                    });
                }
                if (refreshTokenResult.ErrorType == Utils.Enums.ErrorType.Model)
                {
                    return Unauthorized(refreshTokenResult.Errors);
                }
                else
                {
                    return Unauthorized(new ErrorResponseModel
                    {

                        ErrorMessage = "Something went wrong!"
                    });
                }
            }
            if (response.ErrorType == Utils.Enums.ErrorType.Model)
            {
                return Unauthorized(response.Errors);
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost(APIRoutes.Account.LoginWithFacebook)]
        [Produces("application/json")]
        [ProducesResponseType(401, Type = typeof(ErrorResponseModel))]
        // response modeller ve request modelleri uygun folderlerde yaratmaq ve burda istifade etmek lazimdi 
        public async Task<IActionResult> LoginWithFacebook([FromBody] LoginWithExternalProviderRequestModel model)
        {

            var command = new LoginWithFacebookCommand(token: model.Token, userType: Utils.Enums.UserType.Customer);
            var response = await _mediatr.Send(command);

            if (!response.HasError)
            {
                var refreshTokenResult = await _userIdentityService.CreateRefreshTokenAsync(response.User.Id);
                if (!refreshTokenResult.HasError)
                {
                    var claims = await _userIdentityService.GetAllRolesAndClaimsAsync(response.User);

                    var jwt = _jwtTokenService.CreateNewJwtToken(response.User, claims, refreshTokenResult.JwtId);
                    return Ok(new RefreshAndJwtTokenResponseModel
                    {
                        RefreshToken = refreshTokenResult.RefreshToken,
                        JwtToken = jwt.JwtToken,
                        JwtExpirationDate = jwt.ExpirationDate,
                        Provider = "ShoppingApp"
                    });
                }
                if (refreshTokenResult.ErrorType == Utils.Enums.ErrorType.Model)
                {
                    return Unauthorized(refreshTokenResult.Errors);
                }
                else
                {
                    return Unauthorized(new ErrorResponseModel
                    {

                        ErrorMessage = "Something went wrong!"
                    });
                }
            }

            if (response.ErrorType == Utils.Enums.ErrorType.Model)
            {
                return Unauthorized(response.Errors);
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost(APIRoutes.Account.LoginWithGoogle)]
        [Produces("application/json")]
        [ProducesResponseType(401, Type = typeof(ErrorResponseModel))]
        // response modeller ve request modelleri uygun folderlerde yaratmaq ve burda istifade etmek lazimdi 
        public async Task<IActionResult> LoginWithGoogle([FromBody] LoginWithExternalProviderRequestModel model)
        {
            var command = new LoginWithGoogleCommand(token: model.Token, userType: Utils.Enums.UserType.Customer);
            var response = await _mediatr.Send(command);

            if (!response.HasError)
            {
                var refreshTokenResult = await _userIdentityService.CreateRefreshTokenAsync(response.User.Id);
                if (!refreshTokenResult.HasError)
                {
                    var claims = await _userIdentityService.GetAllRolesAndClaimsAsync(response.User);

                    var jwt = _jwtTokenService.CreateNewJwtToken(response.User, claims, refreshTokenResult.JwtId);
                    return Ok(new RefreshAndJwtTokenResponseModel
                    {
                        RefreshToken = refreshTokenResult.RefreshToken,
                        JwtToken = jwt.JwtToken,
                        JwtExpirationDate = jwt.ExpirationDate,
                        Provider = "ShoppingApp"
                    });
                }
                if (refreshTokenResult.ErrorType == Utils.Enums.ErrorType.Model)
                {
                    return Unauthorized(refreshTokenResult.Errors);
                }
                else
                {
                    return Unauthorized(new ErrorResponseModel
                    {

                        ErrorMessage = "Something went wrong!"
                    });
                }
            }

            if (response.ErrorType == Utils.Enums.ErrorType.Model)
            {
                return Unauthorized(response.Errors);
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost(APIRoutes.Account.RefreshToken)]
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
                var newRefreshToken = await _userIdentityService.UpdateRefreshTokenAsync(model.RefreshToken, oldJwtId);

                if (newRefreshToken.HasError)
                {
                    return BadRequest(new ErrorResponseModel { ErrorMessage = "Could not create new refresh token" });
                }

                var claims = await _userIdentityService.GetAllRolesAndClaimsAsync(user);

                var newJwt = _jwtTokenService.CreateNewJwtToken(user: user, userClaims: claims, tokenId: newRefreshToken.JwtId);

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
                var refreshTokenResult = await _userIdentityService.CreateRefreshTokenAsync(response.User.Id);
                if (!refreshTokenResult.HasError)
                {
                    var claims = await _userIdentityService.GetAllRolesAndClaimsAsync(response.User);

                    var jwt = _jwtTokenService.CreateNewJwtToken(response.User, claims, jwtId);
                    return Ok(new RefreshAndJwtTokenResponseModel
                    {
                        RefreshToken = refreshTokenResult.RefreshToken,
                        JwtToken = jwt.JwtToken,
                        JwtExpirationDate = jwt.ExpirationDate,
                        Provider = "ShoppingApp"
                    });
                }
                if (refreshTokenResult.ErrorType == Utils.Enums.ErrorType.Model)
                {
                    return Unauthorized(refreshTokenResult.Errors);
                }
                else
                {
                    return Unauthorized(new ErrorResponseModel
                    {

                        ErrorMessage = "Something went wrong!"
                    });
                }
            }
            if (response.ErrorType == Utils.Enums.ErrorType.Model)
            {
                return Unauthorized(response.Errors);
            }
            else
            {
                return Unauthorized();
            }

        }

        [HttpPost(APIRoutes.Account.ForgotPassword)]
        [Produces("Application/json")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequestModel model)
        {
            var command = new ForgotPasswordCommand(email: model.Email);
            var response = await _mediatr.Send(command);
            if (!response.HasError)
            {
                return Ok(new ForgorPasswordResponseModel
                {
                    Message = response.Message
                });
            }
            return BadRequest();
        }


        [HttpPost(APIRoutes.Account.ResetPassword)]
        [Produces("Application/json")]
        public async Task<IActionResult> ResetPasswordAsync([FromQuery] string userId, [FromBody] ResetPasswordRequestModel model)
        {
            var command = new ResetPasswordCommand(email: model.Email, code: model.Code, password: model.Password, userId: userId);
            var response = await _mediatr.Send(command);
            if (!response.HasError)
            {
                return Ok(new ForgorPasswordResponseModel
                {
                    Message = response.Message
                });
            }
            return BadRequest();
        }

        [HttpPost(APIRoutes.Account.Logout)]
        public async Task<IActionResult> Logout()
        {
            var jwt = HttpContext.GetJwt();
            if (!(jwt is null))
            {
                var jwtId = _jwtTokenService.GetTokenId(jwt);
                if (!(jwtId is null))
                {
                    var result = await _userIdentityService.InvalidateRefreshTokenAsync(jwtId);
                    if (!result.HasError)
                    {
                        return Ok();
                    }
                }
                return BadRequest();
            }
            return BadRequest();

        }
    }
}
