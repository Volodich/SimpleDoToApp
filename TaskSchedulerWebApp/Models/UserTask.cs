using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TaskSchedulerWebApp.Models
{
    public class UserTask
    {
        [Key]
        public int TaskId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public User UserIdFk { get; set; }
        public virtual TaskLevel TaskLevel { get; set; }
        public virtual TaskState TaskState { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime Time { get; set; }
    }
}
