namespace Presentation.ViewModels.DepartmentViwModel
{
    public class DepartmentEditViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateOnly DateOfCreation { get; set; } 


    }
}
