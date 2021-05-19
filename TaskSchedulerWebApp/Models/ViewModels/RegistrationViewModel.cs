using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;

namespace TaskSchedulerWebApp.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "Ваше имя")]
        public string FirstName { get; set; }
        
        [Display(Name = "Ваша фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [Display(Name = "Ваша электронная почта")]
        [UIHint("Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Не указан пароль")]
        [Display(Name = "Введите пароль")]
        [UIHint("Password")]
        public string Password { get; set; }

        [Display(Name = "Подтвердите введённый пароль")]
        [UIHint("Password")]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
