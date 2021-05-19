using System;
using System.ComponentModel.DataAnnotations;

namespace TaskSchedulerWebApp.Models.ViewModels
{
    public class UserTasksViewModel : UserTask
    {
        [Required(ErrorMessage = "Название задачи не указано!")]
        [Display(Name = "Укажите название задачи")]
        public override string Name { get; set; }

        [Required(ErrorMessage = "Описание задачи не может быть пустым")]
        [Display(Name = "Опишите задачу")]
        public override string Description { get; set; }

        [Required(ErrorMessage = "Укажите срок выполнения задачи")]
        [Display(Name = "Дата")]
        [UIHint("Date")]
        public override DateTime Date { get; set; }

        [Required(ErrorMessage = "Укажите время выполнения задачи")]
        [Display(Name = "Время")]
        [UIHint("Time")]
        public override DateTime Time { get; set; }
    }
}
