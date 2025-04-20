using BusinessLogic.DataTransfereObjects.DepartmentDtos;
using BusinessLogic.Factories;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Models.EmployeeModel;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Classes
{
    public class DepartmentServices(IUnitOfWork _unitOfWork ) : IDepartmentServices
    {
        #region Old_Ctor
        //private readonly IDepartmentRepository _departmentRepository;

        //public DepartmentServices(IDepartmentRepository departmentRepository)
        //{
        //    this._departmentRepository = departmentRepository;
        //}
        #endregion

        // Get  All Department
        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(D => D.ToDepartmentDTO());
            #region Making by anotherway
            //    var departmentsToReturn = departments.Select(D => new DepartmentDTO()
            //    {
            //        DeptId = D.Id,
            //        Code = D.Code,
            //        Name = D.Name,
            //        Description = D.Description ?? "",
            //        DateOfCreation = DateOnly.FromDateTime(D.CreateOn)
            //    });
            //    return departmentsToReturn;
            //
            #endregion
        }
        // Get Dept By Id
        public DepartmentDetailsDTO GetDepartmentById(int id)
        {
            var departmentdetails = _unitOfWork.DepartmentRepository.GetById(id);
            return departmentdetails is null ? null! : departmentdetails.ToDepartmentDetailsDTO();
            #region Making by another way
            //if (department == null) return null;
            //else
            //{
            //    var departmentDetails = new DepartmentDetailsDTO()
            //    {
            //        Code = department.Code,
            //        Name = department.Name,
            //        Description = department.Description,
            //        CreateOn = DateOnly.FromDateTime(department.CreateOn),
            //        CreateBy = department.CreateBy,
            //        Id = department.Id,
            //        IsDeleted = department.IsDeleted,
            //        LastModifiedBy = department.LastModifiedBy,
            //        LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn)
            //    };
            //    return departmentDetails; 
            #endregion
            #region OtherWay
            //return department is null ? null : new DepartmentDetailsDTO(department)
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    Description = department.Description,
            //    CreateOn = DateOnly.FromDateTime(department.CreateOn),
            //    CreateBy = department.CreateBy,
            //    Id = id,
            //    IsDeleted = department.IsDeleted,
            //    LastModifiedBy = department.LastModifiedBy,
            //    LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn)
            //}; 
            #endregion
        }
        // Create New department 
        public int AddDepartment(CreatedDepartmentDTO createdDepartmentDTO)
        {
            var department = createdDepartmentDTO.ToEntity();
            _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }
        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDTO updatedDepartmentDTO)
        {
            var department = updatedDepartmentDTO.ToEntity();
             _unitOfWork.DepartmentRepository.Update(department);
            return _unitOfWork.SaveChanges();

        }
        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var dept = _unitOfWork.DepartmentRepository.GetById(id);
            if (dept is null) return false;
            else
            {
                dept.IsDeleted = true;
                _unitOfWork.DepartmentRepository.Remove(dept);
                return _unitOfWork.SaveChanges() > 0;
            }
        }
    }
}
