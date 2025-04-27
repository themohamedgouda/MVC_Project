using DataAccess.Models.IdentityModel;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Common;
using Presentation.Helper;
using Presentation.Utilities;
using Presentation.ViewModels.AuthViewModel;
using Presentation.ViewModels.ForgetPasswordViewModel;
using Presentation.ViewModels.ResetPasswordViewModel;

namespace Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager , SignInManager<ApplicationUser> _signInManager , IMailServices mailService) : Controller
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
                var _Token = _userManager.GeneratePasswordResetTokenAsync(User).Result;
                var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = viewModel.Email , Token = _Token },Request.Scheme);
                var Email = new Email()
                {
                    To = viewModel.Email,
                    Subject = "Reset Password",
                    Body = ResetPasswordLink // TODO
                };

               // EmailSettings.SendEmail(Email);
                mailService.Send(Email);    
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
        [HttpGet]
        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var Email = TempData["email"] as string ?? "";
            var Token = TempData["token"] as string ?? "";
            var User = _userManager.FindByEmailAsync(Email).Result;
            if (User != null) 
            {
                var Result = _userManager.ResetPasswordAsync(user: User, Token, viewModel.Password).Result;
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in Result.Errors)
                         ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(nameof(ResetPassword),viewModel);
        }
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
