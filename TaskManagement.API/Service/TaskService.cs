using dbModel = TaskManagement.Domain.Model;

namespace TaskManagement.API.Services
{

    public class TaskService : ITaskService
    {
        private static List<dbModel.Task> tasks = new List<dbModel.Task>();
        public IEnumerable<dbModel.Task> GetAllTasks()
        {
            return tasks;
        }
        public dbModel.Task GetTaskById(int taskId)
        {
            return tasks.FirstOrDefault(x => x.TaskId == taskId);
        }

        public void AddTask(dbModel.Task task)
        {
            if (tasks.FirstOrDefault(x => x.Name == task.Name) == null)
            {
                task.TaskId = tasks.Count > 0 ? tasks.Max(x => x.TaskId) + 1 : 1;
                tasks.Add(task);
            }
            else
            {
                throw new ApplicationException("Task name already exist.");
            }
        }
        public void UpdateTask(dbModel.Task task)
        {
            var existingTask = tasks.FirstOrDefault(x => x.TaskId == task.TaskId);
            if (existingTask != null)
            {
                if (tasks.FirstOrDefault(x => x.Name == task.Name && x.TaskId != task.TaskId) == null)
                {
                    existingTask.Name = task.Name;
                    existingTask.Status = task.Status;
                    existingTask.Priority = task.Priority;
                }
                else
                {
                    throw new ApplicationException("Task name already exist.");
                }
            }
        }
        public void DeleteTask(int taskId)
        {
            if (tasks.FirstOrDefault(x => x.Status.ToLower() == "completed" && x.TaskId == taskId) != null)
            {
                tasks.RemoveAll(x => x.TaskId == taskId);
            }
            else { throw new ApplicationException("Only completed task can be deleted."); }
        }

        public void ClearList()
        {
            tasks.Clear();
        }
    }
}
