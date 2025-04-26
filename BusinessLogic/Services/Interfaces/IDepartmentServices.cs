using BusinessLogic.DataTransfereObjects.DepartmentDtos;

namespace BusinessLogic.Services.Interfaces
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