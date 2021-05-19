using System.ComponentModel.DataAnnotations;

namespace TaskSchedulerWebApp.Models
{
    public class TaskState
    {
        [Key]
        public int StateId { get; set; }
        public string Name { get; set; }
    }
}
