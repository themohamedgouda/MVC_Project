using AutoMapper;
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
    public class EmployeeServices(IEmployeeRepository _employeeRepository , IMapper _mapper) : IEmployeeServices
    {
        public IEnumerable<EmployeeDTO> GetAllEmployees(bool WithTracking)
        {
            var Employees = _employeeRepository.GetAll();
            var EmployeesDto = _mapper.Map<IEnumerable<EmployeeDTO>>(Employees);
            return EmployeesDto;
        }
        public EmployeeDetailsDTO? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDTO>(employee);
        }
        public int CreateEmployee(CreatedEmployeeDTO createdEmployeeDTO)
        {
            var newEmployee = _mapper.Map<CreatedEmployeeDTO,Employee>(createdEmployeeDTO);
           ;
            return _employeeRepository.Add(newEmployee);
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true : false ;
            }  
        }
        public int UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO)
        {
            return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDTO,Employee>(updatedEmployeeDTO));
        }

    }
}
