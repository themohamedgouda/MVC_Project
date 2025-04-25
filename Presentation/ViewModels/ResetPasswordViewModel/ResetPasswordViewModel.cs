using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.ResetPasswordViewModel
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password), Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
