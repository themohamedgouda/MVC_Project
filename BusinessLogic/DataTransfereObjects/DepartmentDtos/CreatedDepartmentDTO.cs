using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransfereObjects.DepartmentDtos
{
    public class CreatedDepartmentDTO
    {
        // [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; } = string.Empty;
        // [Required]
        //[Range(0,900)]
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateOnly DateOfCreation { get; set; }
    }
}
