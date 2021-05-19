using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskSchedulerWebApp.Models.Repositories.Abstract;

namespace TaskSchedulerWebApp.Models.Repositories.EF
{
    public class EfUserTasksRepositories : IUserTaskRepositories
    {
        private ProcrastinatorContext _context;
        public EfUserTasksRepositories(ProcrastinatorContext context)
        {
            _context = context;
        }
        public async Task<List<UserTask>> GetUserTasksAsync(string username)
        {
            return await _context.UserTasks
                .Where(ut => ut.UserIdFk.UserName == username)
                .Include(ut => ut.TaskState)
                .Include(ut => ut.TaskLevel)
                .ToListAsync();
        }

        public async Task AddTaskAsync(string username, UserTask task)
        {
            var user = await _context.Users.FirstAsync(u=>u.UserName == username);
            var taskLevelContext = await _context.TaskLevels
                .FirstAsync(tl=>tl.Name == task.TaskLevel.Name);
            var taskStateContext = await _context.TaskStates
                .FirstAsync(ts => ts.Name == task.TaskState.Name);

            _context.UserTasks.Add(new UserTask()
            {
                Name = task.Name,
                Description = task.Description,
                UserIdFk = user,
                TaskLevel = taskLevelContext,
                TaskState =  taskStateContext,
                Date = task.Date.Date,
                Time = task.Time
            });

            await _context.SaveChangesAsync();
        }

        public async Task<UserTask> GetTaskByIdAsync(int id)
        {
            return await _context.UserTasks
                .Include(ut => ut.TaskState)
                .Include(ut => ut.TaskLevel)
                .FirstAsync(ut => ut.TaskId == id);
        }

        public async Task UpdateTaskInformationAsync(UserTask task)
        {
            var uTask = await GetTaskByIdAsync(task.TaskId);

            uTask.Name = task.Name;
            uTask.Description = task.Description;
            uTask.TaskLevel =  await _context.TaskLevels.FirstAsync(tl=>tl.Name == task.TaskLevel.Name);
            uTask.TaskState = await _context.TaskStates.FirstAsync(ts => ts.Name == task.TaskState.Name);
            uTask.Time = task.Time;
            uTask.Date = task.Date;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveTaskAsync(int id)
        {
            var uTask = await GetTaskByIdAsync(id);
            _context.UserTasks.Remove(uTask);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserTaskExists(int id)
        {
            return await _context.UserTasks.AnyAsync(e => e.TaskId == id);
        }

        public async Task<string[]> GetTaskLevelsName()
        {
            return await _context.TaskLevels.Select(tl => tl.Name).ToArrayAsync();
        }

        public async Task<string[]> GetTaskStatesName()
        {
            return await _context.TaskStates.Select(ts => ts.Name).ToArrayAsync();
        }
    }
}
