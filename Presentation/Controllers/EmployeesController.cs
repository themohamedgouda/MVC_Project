using BusinessLogic.DataTransfereObjects.DepartmentDtos;
using BusinessLogic.DataTransfereObjects.EmployeeDtos;
using BusinessLogic.Services.Classes;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.DepartmentViwModel;

namespace Presentation.Controllers
{
    public class EmployeesController(IEmployeeServices _employeeServices,IWebHostEnvironment _environment , ILogger<EmployeesController> _logger) : Controller
    {
        public IActionResult Index()
            {
            var Employees = _employeeServices.GetAllEmployees();
            return View(Employees);
        }
        #region CreateEmployee
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedEmployeeDTO EmployeeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _employeeServices.CreateEmployee(EmployeeDTO);
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't be Created");
                    }
                }
                catch (Exception Ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, $"{Ex.Message}");
                    }
                    else
                    {
                        _logger.LogError(Ex.Message);
                    }

                }
            }
      
            return View(EmployeeDTO);
        }
        #endregion
        #region Detials of Employee
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _employeeServices.GetEmployeeById(id.Value);
            if (department == null) return NotFound();
            return View(department);
        }
        #endregion
        #region Edit Employee
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeServices.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeDTO = new UpdatedEmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber,
                Gender= Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            };
            return View(employeeDTO);

        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? Id, UpdatedEmployeeDTO employeeDTO)
        {
            if (!Id.HasValue || Id != employeeDTO.Id) return BadRequest();

            if (!ModelState.IsValid) return View(employeeDTO);

            try
            {
                var Result = _employeeServices.UpdateEmployee(employeeDTO);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not updated");
                    return View(employeeDTO);
                }
            }
            catch (Exception ex)
            {
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, $"{ex.Message}");
                        return View(employeeDTO);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                    }
                }
            }

        }
        #endregion
        #region Delete Employee
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Result = _employeeServices.DeleteEmployee(id);
                if (Result)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError(string.Empty, "Employee Is not deleted");
                // return View();
                return RedirectToAction(nameof(Delete), new { id });
            }
            catch (Exception Ex)
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
