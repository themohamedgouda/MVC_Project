using DataAccess.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Presentation.Utilities;
using Presentation.ViewModels.AuthViewModel;
using Presentation.ViewModels.ForgetPasswordViewModel;

namespace Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager , SignInManager<ApplicationUser> _signInManager) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
           if (!ModelState.IsValid) return View(viewModel);

            var User = new ApplicationUser()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,

            };
            var Result = _userManager.CreateAsync(User,viewModel.Password).Result;
            if (!Result.Succeeded)
                 return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            return View(viewModel);
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
            if (User is not null)
            {
               bool Flag = _userManager.CheckPasswordAsync(User, viewModel.Password).Result;
                if (Flag) 
                {
                   var Result =  _signInManager.PasswordSignInAsync(User, viewModel.Password, viewModel.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is not Allowed");
                    }
                     if (Result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is LockedOut");
                    }
                    if (Result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "InvalidLogin");
            }
                return View(viewModel);
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(nameof(ForgetPassword),viewModel);
            var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
            if (User != null)
            {
                var Email = new Email()
                {
                    To = viewModel.Email,
                    Subject = "Reset Password",
                    Body = "Reset Password To Do" // TODO
                };

                EmailSettings.SendEmail(Email);
                return RedirectToAction("CheckYouInboxAction");
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);
        }
        [HttpGet]
        public IActionResult CheckYouInboxAction()
        {
            return View();
        }
    }
}
