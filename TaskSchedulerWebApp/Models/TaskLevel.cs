using System.ComponentModel.DataAnnotations;

namespace TaskSchedulerWebApp.Models
{
    public class TaskLevel
    {
        [Key]
        public int LevelId { get; set; }
        public string Name { get; set; }

    }
}
