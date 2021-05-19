using System.ComponentModel.DataAnnotations;

namespace TaskSchedulerWebApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [UIHint("Password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
