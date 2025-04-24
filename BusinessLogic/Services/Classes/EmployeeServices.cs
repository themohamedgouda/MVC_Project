using AutoMapper;
using BusinessLogic.DataTransfereObjects.DepartmentDtos;
using BusinessLogic.DataTransfereObjects.EmployeeDtos;
using BusinessLogic.Services.AttachmentService;
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
    public class EmployeeServices(IUnitOfWork _unitOfWork, IMapper _mapper , IAttachmentService attachmentService) : IEmployeeServices
    {
        public IEnumerable<EmployeeDTO> GetAllEmployees(string? EmployeeSearchName, bool WithTracking = false)
        {
            IEnumerable<Employee> employees;
            if ( string.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            }
            var EmployeesDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDTO>>(employees);
            return EmployeesDto;
        }
        public EmployeeDetailsDTO? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDTO>(employee);
        }
        public int CreateEmployee(CreatedEmployeeDTO createdEmployeeDTO)
        {
            var newEmployee = _mapper.Map<CreatedEmployeeDTO,Employee>(createdEmployeeDTO);

            if (createdEmployeeDTO.Image is not null)
            {
                newEmployee.ImageName =  attachmentService.Upload(createdEmployeeDTO.Image, "Images");

            }


            _unitOfWork.EmployeeRepository.Add(newEmployee); // add Locally
            return _unitOfWork.SaveChanges();
        }
        public int UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO)
        {
            _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDTO, Employee>(updatedEmployeeDTO));
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
               return _unitOfWork.SaveChanges() > 0 ? true : false;
            }  
        }
   

    }
}
