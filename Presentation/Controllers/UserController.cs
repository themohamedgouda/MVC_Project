using BusinessLogic.DataTransfereObjects.EmployeeDtos;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.IdentityModel;
using DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presentation.ViewModels.EmployeeViwModel;
using Presentation.ViewModels.UsersViewModel;

namespace Presentation.Controllers
{
    public class UserController(UserManager<ApplicationUser> userManager , IWebHostEnvironment environment) : Controller
    {
        #region Index
        public async Task<IActionResult> Index(string SearchValue)
        {
            var UserQuery = userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(SearchValue))
            {
                UserQuery = UserQuery.Where(x => x.Email.ToLower().Contains(SearchValue.ToLower()));
            }
            foreach (var user in UserQuery)
            {
                Console.WriteLine($"User: {user.Email}, FirstName: {user.FirstName}, LastName: {user.LastName}");
            }
            var UserList = await UserQuery.Select(U => new UsersViewModel
            {
                Id = U.Id,
                Email = U.Email,
                FName = U.FirstName,
                LName = U.LastName,
                // Roles = U.role


            }).ToListAsync();
            foreach (var user in UserList)
            {
                var appUser = await userManager.FindByIdAsync(user.Id);
                if (appUser != null)
                {
                    var roles = await userManager.GetRolesAsync(appUser);
                    user.Roles = roles?.ToList() ?? new List<string>();
                }
            }

            return View(UserList);
        }
        #endregion
        #region Detials of User
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return BadRequest();
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var UserViewModel = new UsersViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FName = user.FirstName,
                LName = user.LastName,
                Roles = await userManager.GetRolesAsync(user)
            };
            return View(UserViewModel);
        }
        #endregion
        #region Edit User
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is  null) return BadRequest();
            var user = await userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            var UserViewModel = new UsersViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FName = user.FirstName,
                LName = user.LastName,
                Roles = await userManager.GetRolesAsync(user)
            };
            return View(UserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit( string? Id, UsersViewModel UsersViewModel)
        {
            if (!ModelState.IsValid) return View(UsersViewModel);
            var message = string.Empty;
            try
            {
                var user = await userManager.FindByIdAsync(Id);
                if (user is null) return NotFound();
                user.FirstName = UsersViewModel.FName;
                user.LastName = UsersViewModel.LName;
                user.Email = UsersViewModel.Email;
               var Results = await userManager.UpdateAsync(user);
                if (Results.Succeeded)
                    RedirectToAction("Index");
                else
                    message = "User Can not be updated";
            }
            catch (Exception ex)
            {
                 message =  environment.IsDevelopment() ?  ex.Message : "User Can not be updated";
            }
            ModelState.AddModelError(string.Empty , message);
            return View(UsersViewModel);


        }
        #endregion
        #region Delete User
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id  is null) return BadRequest();
            var user = await userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            return View(new UsersViewModel()
            {
                Email = user.Email,
                FName = user.FirstName,
                LName = user.LastName,
                Id = id
            });
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string? id)
        {
            if (id is null) return BadRequest();
            var user = await userManager.FindByIdAsync(id);
            var Message = string.Empty;
            try
            {
                if (user is not null)
                {
                    await userManager.DeleteAsync(user);
                    return RedirectToAction("Index");
                }

                Message = "An Error Happend When You Delete the User";
            }
            catch(Exception exception)
            {
                Message = environment.IsDevelopment() ? exception.Message : "An Error Happend When You Delete the User";
            }
            ModelState.AddModelError(string.Empty , Message);
            return View(nameof(Index));
        }
        #endregion
    }
}
