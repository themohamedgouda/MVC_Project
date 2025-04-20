using BusinessLogic.DataTransfereObjects.DepartmentDtos;
using BusinessLogic.DataTransfereObjects.EmployeeDtos;
using BusinessLogic.Services.Classes;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.DepartmentViwModel;
using Presentation.ViewModels.EmployeeViwModel;

namespace Presentation.Controllers
{
    public class EmployeesController(IEmployeeServices _employeeServices,IWebHostEnvironment _environment , ILogger<EmployeesController> _logger ) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
            {
            var Employees = _employeeServices.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }
        #region CreateEmployee
        [HttpGet]
        public IActionResult Create()
        {
             return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel EmployeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDTO()
                    {
                        Address = EmployeeViewModel.Address,
                        Age = EmployeeViewModel.Age,
                        DepartmentId = EmployeeViewModel.DepartmentId,
                        Email = EmployeeViewModel.Email,
                        EmployeeType = EmployeeViewModel.EmployeeType,
                        Gender = EmployeeViewModel.Gender,
                        HiringDate = EmployeeViewModel.HiringDate,
                        IsActive = EmployeeViewModel.IsActive,
                        Name = EmployeeViewModel.Name,
                        PhoneNumber = EmployeeViewModel.PhoneNumber,
                        Salary = EmployeeViewModel.Salary, 
                    };
                    int Result = _employeeServices.CreateEmployee(employeeDto);
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
      
            return View(EmployeeViewModel);
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
            var employeeViewModel = new EmployeeViewModel
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber,
                Gender= Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId
                
            };
            return View(employeeViewModel);

        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? Id, EmployeeViewModel employeeViewModel)
        {
            if (!Id.HasValue ) return BadRequest();

            if (!ModelState.IsValid) return View(employeeViewModel);

            try
            {
                var employeeDto = new UpdatedEmployeeDTO()
                {
                    DepartmentId = employeeViewModel.DepartmentId,
                    Age = employeeViewModel.Age,
                    Email = employeeViewModel.Email,
                    IsActive = employeeViewModel.IsActive,
                    Salary = employeeViewModel.Salary,
                    Address = employeeViewModel.Address,
                    HiringDate= employeeViewModel.HiringDate,
                    PhoneNumber= employeeViewModel.PhoneNumber,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Gender = employeeViewModel.Gender,
                    Id = Id.Value,
                    Name = employeeViewModel.Name

                };
                var Result = _employeeServices.UpdateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee is not updated");
                    return View(employeeViewModel);
                }
            }
            catch (Exception ex)
            {
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, $"{ex.Message}");
                        return View(employeeViewModel);
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
