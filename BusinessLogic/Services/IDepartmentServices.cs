using BusinessLogic.DataTransfereObjects;

namespace BusinessLogic.Services
{
    public interface IDepartmentServices
    {
        int AddDepartment(CreatedDepartmentDTO createdDepartmentDTO);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDTO> GetAllDepartments();
        DepartmentDetailsDTO GetDepartmentById(int id);
        int UpdateDepartment(UpdatedDepartmentDTO updatedDepartmentDTO);
    }
}