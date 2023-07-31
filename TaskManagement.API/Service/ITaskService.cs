 using dbModel = TaskManagement.Domain.Model;
namespace TaskManagement.API.Services
{
    public interface ITaskService
    {
        IEnumerable<dbModel.Task> GetAllTasks();
        dbModel.Task GetTaskById(int taskId);

        void AddTask(dbModel.Task task);
        void UpdateTask(dbModel.Task task);
        void DeleteTask(int taskId);

        void ClearList();


    }
}
