using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.CQRS.Models.CommandModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Web.UI.ViewModels.Account;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserIdentityService _userIdentityService;

        public AccountController(
            IMediator mediator,
            SignInManager<User> signInManager,
            IUserIdentityService userIdentityService
            )
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userIdentityService = userIdentityService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var command = new LoginCommand(email: model.Email, password: model.Password);
            var response = await _mediator.Send(command);
            if (!response.HasError)
            {
                await _signInManager.SignInAsync(response.User, true);
                return RedirectToAction("Index", "Home");
            }
            if (response.ErrorType == Utils.Enums.ErrorType.Model)
            {
                foreach (var item in response.Errors)
                {
                    ModelState.AddModelError("", item.Message);
                }
                return View(model);
            }
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var command = new RegisterCommand(
                firstName: model.FirstName,
                lastName: model.LastName,
                email: model.Email,
                password: model.Password,
                userType: UserType.Customer);

            var response = await _mediator.Send(command);
            if (!response.HasError)
            {
                await _signInManager.SignInAsync(response.User, true);
                return RedirectToAction("Index", "Home");
            }
            if (response.ErrorType == ErrorType.Model)
            {
                foreach (var item in response.Errors)
                {
                    ModelState.AddModelError("", item.Message);
                }
                return View(model);
            }
            return View();
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var command = new ForgotPasswordCommand(email: model.Email);
            var response = await _mediator.Send(command);

            if (!response.HasError)
            {
                return View("ForgotPasswordConfirmation");
            }

            if (response.ErrorType == ErrorType.Model)
            {
                foreach (var item in response.Errors)
                {
                    ModelState.AddModelError("", item.Message);
                }
                return View(model);
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId, string code)
        {

            var model = new ResetPasswordViewModel
            {
                UserId = userId,
                Code = code,
                Email = await _userIdentityService.GetEmail(userId)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var command = new ResetPasswordCommand(
                email: model.Email,
                password: model.Password,
                code: model.Code,
                userId: model.UserId);
            var response = await _mediator.Send(command);

            if (!response.HasError)
            {
                var loginCommand = new LoginCommand(
                   email: model.Email,
                   password: model.Password
                   );
                var loginResponse = await _mediator.Send(loginCommand);

                if (!loginResponse.HasError)
                {
                    await _signInManager.SignInAsync(loginResponse.User, true);
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Login", "Account");
            }

            if (response.ErrorType == ErrorType.Model)
            {
                foreach (var item in response.Errors)
                {
                    ModelState.AddModelError("", item.Message);
                }
                return View(model);
            }
            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var confirmed = await _userIdentityService.ConfirmEmailAsync(userId, code);

            if (confirmed)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            return RedirectToAction("Error", "Home", new { area = "" });
        }

    }
}
