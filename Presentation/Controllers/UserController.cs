using DataAccess.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presentation.ViewModels.UsersViewModel;

namespace Presentation.Controllers
{
    public class UserController(UserManager<ApplicationUser> userManager) : Controller
    {
        public async Task<IActionResult> Index(string SearchValue)
        {
            var UserQuery = userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(SearchValue))
            {
                UserQuery=UserQuery.Where(x => x.Email.ToLower().Contains(SearchValue.ToLower()));
            }
            foreach (var user in UserQuery)
            {
                Console.WriteLine($"User: {user.Email}, FirstName: {user.FirstName}, LastName: {user.LastName}");
            }
            var UserList = await UserQuery.Select(U =>new UsersViewModel
            {
                Id= U.Id,
                Email= U.Email,
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
    }
}
