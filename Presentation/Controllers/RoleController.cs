using DataAccess.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModels.RoleViewModel;
using Presentation.ViewModels.UsersViewModel;
using System;

namespace Presentation.Controllers
{
    public class RoleController(RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment) : Controller
    {
      
        #region Index
        public async Task<IActionResult> Index(string SearchValue)
        {
            var RolesQuery = roleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(SearchValue))
            {
                RolesQuery = RolesQuery.Where(x => x.Name.ToLower().Contains(SearchValue.ToLower()));
            }
           
            var RolesList = await RolesQuery.Select(U => new RoleViewModel
            {
                Id = U.Id,
                Name = U.Name
            }).ToListAsync();
   

            return View(RolesList);
        }
        #endregion
        #region Ceate Role
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                await roleManager.CreateAsync(new IdentityRole()
                { 
                    Name = roleViewModel.Name
                });
                return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }
        #endregion
        #region Detials of Role
        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return BadRequest();
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            var RoleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
                
            };
            return View(RoleViewModel);
        }
        #endregion
        #region Edit Role
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null) return BadRequest();
            var role = await roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            var RoleViewModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(RoleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string? Id, RoleViewModel RoleViewModel)
        {
            if (!ModelState.IsValid) return View(RoleViewModel);
            var message = string.Empty;
            try
            {
                var role = await roleManager.FindByIdAsync(Id);
                if (role is null) return NotFound();
                role.Name = RoleViewModel.Name;              
                var Results = await roleManager.UpdateAsync(role);
                if (Results.Succeeded)
                    RedirectToAction("Index");
                else
                    message = "User Can not be updated";
            }
            catch (Exception ex)
            {
                message = environment.IsDevelopment() ? ex.Message : "User Can not be updated";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(RoleViewModel);


        }
        #endregion
        #region Delete Role
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null) return BadRequest();
            var role = await roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();
            return View(new RoleViewModel()
            {
                Name = role.Name,
                Id = role.Id,   
            });
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string? id)
        {
            if (id is null) return BadRequest();
            var role = await roleManager.FindByIdAsync(id);
            var Message = string.Empty;
            try
            {
                if (role is not null)
                {
                    await roleManager.DeleteAsync(role);
                    return RedirectToAction("Index");
                }

                Message = "An Error Happend When You Delete the User";
            }
            catch (Exception exception)
            {
                Message = environment.IsDevelopment() ? exception.Message : "An Error Happend When You Delete the User";
            }
            ModelState.AddModelError(string.Empty, Message);
            return View(nameof(Index));
        }
        #endregion
    }
}
