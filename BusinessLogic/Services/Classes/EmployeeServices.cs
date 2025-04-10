using BusinessLogic.DataTransfereObjects.DepartmentDtos;
using BusinessLogic.DataTransfereObjects.EmployeeDtos;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models.EmployeeModel;
using DataAccess.Models.Shared.Enums;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Classes
{
    public class EmployeeServices(IEmployeeRepository _employeeRepository) : IEmployeeServices
    {
        public IEnumerable<EmployeeDTO> GetAllEmployees(bool WithTracking)
        {
            var Employees = _employeeRepository.GetAll();
            var EmployeesDto = Employees.Select(emp => new EmployeeDTO
            {
                Id = emp.Id,
                Name = emp.Name,
                Age = emp.Age,
                Email = emp.Email,
                EmployeeType = emp.EmployeeType.ToString(),
                Gender = emp.Gender.ToString(),
                IsActive = emp.IsActive,
                Salary = emp.Salary
            });
            return EmployeesDto;
        }
        public EmployeeDetailsDTO GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return null!;
            else return new EmployeeDetailsDTO
            {
                Id= employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Salary= employee.Salary,
                Address = employee.Address,
                CreatedBy = 1,
                CreatedOn = employee.CreatedOn,
                LastModifiedBy = employee.LastModifiedBy,
                EmployeeType= employee.EmployeeType.ToString(),
                LastModifiedOn = employee.LastModifiedOn,
                Gender= employee.Gender.ToString(),
                IsActive= employee.IsActive,
                PhoneNumber= employee.PhoneNumber,
                HiringDate= DateOnly.FromDateTime(employee.HiringDate),
            };
        }
        public int CreateEmployee(CreatedEmployeeDTO createdEmployeeDTO)
        {
            var newEmployee = new Employee
            {
                Name = createdEmployeeDTO.Name,
                Age = createdEmployeeDTO.Age,
                Email = createdEmployeeDTO.Email,
                Address = createdEmployeeDTO.Address,
                HiringDate = createdEmployeeDTO.HiringDate.ToDateTime(TimeOnly.MinValue),
                IsActive = createdEmployeeDTO.IsActive,
                PhoneNumber = createdEmployeeDTO.PhoneNumber,
                Salary = createdEmployeeDTO.Salary,
                CreatedOn = DateTime.Now
            };
           ;
            return _employeeRepository.Add(newEmployee);
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;

            _employeeRepository.Remove(employee);
            return true;
        }
        public int UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO)
        {
            var employee = _employeeRepository.GetById(updatedEmployeeDTO.Id);
            if (employee is null) return 0;

            employee.Name = updatedEmployeeDTO.Name;
            employee.Email = updatedEmployeeDTO.Email;
            employee.Address = updatedEmployeeDTO.Address;
       
            employee.HiringDate = updatedEmployeeDTO.HiringDate.ToDateTime(TimeOnly.MinValue);
            employee.IsActive = updatedEmployeeDTO.IsActive;
            employee.PhoneNumber = updatedEmployeeDTO.PhoneNumber;
            employee.Salary = updatedEmployeeDTO.Salary;
            employee.LastModifiedBy = 1;
            employee.LastModifiedOn = DateTime.Now;
            
            return _employeeRepository.Update(employee);
        }

    }
}
