using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransfereObjects.DepartmentDtos
{
    public class DepartmentDetailsDTO
    {
        // other ways to mapping
        // ctor mapping
        // auto mapper
        // Manual mapping
        // Exstinsion methoods
        //public DepartmentDetailsDTO(Department department)
        //{
        //    Code = department.Code;
        //    Name = department.Name;
        //    Description = department.Description;
        //    CreateOn = DateOnly.FromDateTime(department.CreateOn);
        //    CreateBy = department.CreateBy;
        //    Id = department.Id;
        //    IsDeleted = department.IsDeleted;
        //    LastModifiedBy = department.LastModifiedBy;
        //    LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn);
        //}
        public int Id { get; set; } // PK
        public int CreateBy { get; set; } // UserID
        public DateOnly CreateOn { get; set; }
        public int LastModifiedBy { get; set; }  // UserID
        public DateOnly LastModifiedOn { get; set; } // Automatic Calculated
        public bool IsDeleted { get; set; }  // Soft delete
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }

    }
}
