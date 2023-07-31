using TaskManagement.API.Services;

namespace TaskManagement.API.Factory
{
    public static class TaskServiceFactory
    {
        public static ITaskService CreateTaskService() { 
         return new TaskService();
        }

    }
}
