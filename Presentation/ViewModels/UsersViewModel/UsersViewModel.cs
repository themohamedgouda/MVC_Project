namespace Presentation.ViewModels.UsersViewModel
{
    public class UsersViewModel 
    {
        public string Id { get; set; }
        public string FName { get; set; } = string.Empty;
        public string? LName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; } = new List<string>();


    }
}
