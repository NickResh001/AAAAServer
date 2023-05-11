using System.ComponentModel.DataAnnotations;

namespace ASP_busstation.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Запомнить?")]
        [Required]
        public bool RememberMe { get; set; }
    }
}
