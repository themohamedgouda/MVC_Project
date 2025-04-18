using BusinessLogic.DataTransfereObjects.DepartmentDtos;
using DataAccess.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDTO ToDepartmentDTO(this Department department) 
        {
            return new DepartmentDTO()
            {
                DeptId = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description ?? "",
                DateOfCreation = DateOnly.FromDateTime(department.CreateOn)
            };
        }

        public static DepartmentDetailsDTO ToDepartmentDetailsDTO(this Department department)
        {
            return new DepartmentDetailsDTO()
            {

                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreateOn = DateOnly.FromDateTime(department.CreateOn),
                CreateBy = department.CreateBy,
                Id = department.Id,
                IsDeleted = department.IsDeleted,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn)
            };
        }

        public static Department ToEntity(this CreatedDepartmentDTO department)
        {
            return new Department()
            {

                Name = department.Name,
                Description = department.Description,
                Code = department.Code,
                CreateOn = department.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

        public static Department ToEntity(this UpdatedDepartmentDTO department)
        {
            return new Department()
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                Code = department.Code,
                CreateOn = department.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
    }
}
