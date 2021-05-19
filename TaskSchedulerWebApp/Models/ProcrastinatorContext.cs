using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskSchedulerWebApp.Models
{
    public class ProcrastinatorContext: IdentityDbContext<User>
    {
        public ProcrastinatorContext(DbContextOptions<ProcrastinatorContext> options) : base(options) {}
        
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskState> TaskStates { get; set; }
        public DbSet<TaskLevel> TaskLevels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TaskLevel>().HasData(new TaskLevel[]
            {
                new TaskLevel() {LevelId = 1, Name = "Когда-нибудь"},
                new TaskLevel() {LevelId = 2, Name = "Не срочно"},
                new TaskLevel() {LevelId = 3, Name = "Срочно"},
                new TaskLevel() {LevelId = 4, Name = "Вопрос жизни и смерти"}
            });

            builder.Entity<TaskState>().HasData(new TaskState[]
            {
               new TaskState() {StateId = 1, Name = "Создана"},
               new TaskState() {StateId = 2, Name = "В процессе"},
               new TaskState() {StateId = 3, Name = "Завершена"},
               new TaskState() {StateId = 4, Name = "Уделена"},
            });

            base.OnModelCreating(builder);
        }
    }
}
