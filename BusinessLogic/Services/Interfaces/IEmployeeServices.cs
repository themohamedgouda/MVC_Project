using BusinessLogic.DataTransfereObjects.DepartmentDtos;
using BusinessLogic.DataTransfereObjects.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeeDTO> GetAllEmployees(bool WithTracking = false);
        EmployeeDetailsDTO? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDTO CreatedEmployeeDTO);
        int UpdateEmployee(UpdatedEmployeeDTO UpdatedEmployeeDTO);
        bool DeleteEmployee(int id);
    }
}
