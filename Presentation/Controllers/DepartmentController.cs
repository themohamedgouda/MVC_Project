using BusinessLogic.DataTransfereObjects;
using BusinessLogic.DataTransfereObjects.DepartmentDtos;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
using Presentation.ViewModels.DepartmentViwModel;
using System.Linq.Expressions;

namespace Presentation.Controllers
{
    [Authorize]
    public class DepartmentController(IDepartmentServices _departmentservices , 
        ILogger<DepartmentController> _logger , IWebHostEnvironment _environment) : Controller
    {

        public IActionResult Index()
        
        {
            //ViewBag.Message = new DepartmentDTO() { Name = "TestViewBag"};
            ////ViewData["Message"] = "Hello From View Data";
            //ViewData["Message"] = new DepartmentDTO() { Name = "TestViewData" };
            ////ViewBag.Message = "Hello From View Bag";
            var departments = _departmentservices.GetAllDepartments();
            return View(departments);
        }

        #region CreateDepartment
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //[ValidateAntiForgeryToken] // action fillter
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDTO { 
                        Code = departmentViewModel.Code,
                        DateOfCreation = departmentViewModel.DateOfCreation,
                        Description = departmentViewModel.Description,
                        Name = departmentViewModel.Name                     
                    };
                    int Result = _departmentservices.AddDepartment(departmentDto);
                    var Message = string.Empty;
                    if (Result > 0)
                    {
                        Message = $"Department {departmentViewModel.Name} is Created Successfully";
                    }
                    else
                    {
                        Message = $"Department {departmentViewModel.Name} Cant not be Created ";
                    }
                    TempData["Message"] = Message;
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception Ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, $"{Ex.Message}");
                    
                    else
                        _logger.LogError(Ex.Message);
                }
            }
            return View(model: departmentViewModel);

        }
        #endregion
        #region Details of Department
        [HttpGet]
            public IActionResult Details(int? id) 
            {
                if (!id.HasValue)
                    return BadRequest();
                var department = _departmentservices.GetDepartmentById(id.Value);
                if (department == null) return NotFound();
                return View(department);
            }

        #endregion
        #region Edit Department
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var department = _departmentservices.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentViewModel
            { 
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.CreateOn,
            };
            return View(departmentViewModel);

        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int Id , DepartmentViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdatedDepartment = new UpdatedDepartmentDTO
                    {
                        Id = Id,
                        Code = ViewModel.Code,
                        Name = ViewModel.Name,
                        Description = ViewModel.Description,
                        DateOfCreation = ViewModel.DateOfCreation,
                    };
                    int Result = _departmentservices.UpdateDepartment(UpdatedDepartment);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is not updated");
                    }
                }
                catch (Exception ex)
                {
                    {
                        if (_environment.IsDevelopment())
                        {
                            //Dev
                            ModelState.AddModelError(string.Empty, $"{ex.Message}");
                        }
                        else
                        {
                            //Deployment
                            _logger.LogError(ex.Message);
                            return View("ErrorView", ex);
                        }
                    }
                }
            }
            return View(ViewModel);
        }

        #endregion
        #region Delete Department
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(!id.HasValue)  return BadRequest();
            var department = _departmentservices.GetDepartmentById(id.Value);
            if (department != null) return View(department);
            return NotFound();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Result = _departmentservices.DeleteDepartment(id);
                if (Result)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError(string.Empty, "Department Is not deleted");
                // return View();
                return RedirectToAction(nameof(Delete), new { id });
            }
            catch(Exception Ex)
            {
                if (_environment.IsDevelopment())
                {
                    //Dev
                    ModelState.AddModelError(string.Empty, $"{Ex.Message}");
                    return RedirectToAction(nameof(Index));
                    //  return View(DepartmentDTO);
                }
                else
                {
                    //Deployment
                    _logger.LogError(Ex.Message);
                    return View("ErrorView", Ex);

                    // return View(DepartmentDTO);
                }
            }
        }
        #endregion
    }
}
