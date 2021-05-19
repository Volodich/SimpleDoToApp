using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskSchedulerWebApp.Models.Repositories.Abstract
{
    public interface IUserTaskRepositories
    {
        Task<List<UserTask>> GetUserTasksAsync(string username);
        Task AddTaskAsync(string username, UserTask task);
        Task<UserTask> GetTaskByIdAsync(int id);
        Task UpdateTaskInformationAsync(UserTask task);
        Task RemoveTaskAsync(int id);
        Task<bool> UserTaskExists(int id);
        Task<string[]> GetTaskLevelsName();
        Task<string[]> GetTaskStatesName();

    }
}
