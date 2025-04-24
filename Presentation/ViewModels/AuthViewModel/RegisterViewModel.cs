using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.AuthViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="FirstName Can't be null")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;   
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;   
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;    
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
        public bool IsAgree { get; set; }

    }
}
