using BusinessLogic.DataTransfereObjects;
using BusinessLogic.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class DepartmentController(IDepartmentServices _departmentservices , 
        ILogger<DepartmentController> _logger , IWebHostEnvironment _environment) : Controller
    {

        public IActionResult Index()
        
        {
            var departments = _departmentservices.GetAllDepartments();
            return View(departments);
        }

        #region CreateDepartment
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDTO DepartmentDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _departmentservices.AddDepartment(DepartmentDTO);
                    if (Result > 0)
                    {
                       // return View(nameof(Index));
                       return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't be Created");
                      //  return View(DepartmentDTO) ;
                    }
                }
                catch(Exception Ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        //Dev
                        ModelState.AddModelError(string.Empty, $"{Ex.Message}");
                      //  return View(DepartmentDTO);
                    }
                    else
                    {
                        //Deployment
                        _logger.LogError(Ex.Message);
                       // return View(DepartmentDTO);
                    }

                }
            }
            //else
            //{
            //    return View(DepartmentDTO);

            //}
            return View(DepartmentDTO);
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
    }
}
