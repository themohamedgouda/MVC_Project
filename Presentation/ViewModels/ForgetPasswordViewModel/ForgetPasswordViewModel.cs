using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.ForgetPasswordViewModel
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email Is Required")]
        public string Email { get; set; } = string.Empty;
    }
}
