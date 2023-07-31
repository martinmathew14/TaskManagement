using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dobModel = TaskManagement.Domain.Model;
using TaskManagement.API.Services;
using TaskManagement.API.Factory;
using TaskManagement.Domain.Model;

namespace TaskManagement.API.Test
{
    [TestClass]
    public class TaskServiceTest
    {
        private readonly ITaskService _taskService;
        public TaskServiceTest()
        {
            _taskService = TaskServiceFactory.CreateTaskService();
        }
       
        [TestMethod]
        public void GetAllTasks_ReturnAll()
        {
            //Arrange
            _taskService.ClearList();
            var taskItem1 = new dobModel.Task() { Name= "task1", Priority=1, Status= "not started" , TaskId=0};
            var taskItem2 = new dobModel.Task() { Name = "task2", Priority = 1, Status = "not started", TaskId = 0 };
            _taskService.AddTask(taskItem1);
            _taskService.AddTask(taskItem2);

            // Act
            var result = _taskService.GetAllTasks();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(taskItem1));
            Assert.IsTrue(result.Contains(taskItem2));
        }

        [TestMethod]
        public void GetTaskById_ReturnSelectedTask()
        {
            //Arrange
            _taskService.ClearList();
            var taskItem1 = new dobModel.Task() { Name = "task1", Priority = 1, Status = "not started", TaskId = 0 };
            var taskItem2 = new dobModel.Task() { Name = "task2", Priority = 1, Status = "not started", TaskId = 0 };
            _taskService.AddTask(taskItem1);
            _taskService.AddTask(taskItem2);

            // Act
            var result = _taskService.GetTaskById(2);

            // Assert
            Assert.IsTrue(result!=null);
            Assert.AreEqual(result.Name, taskItem2.Name);
            Assert.AreEqual(result.Status, taskItem2.Status);
        }

        [TestMethod]
        public void AddTask_WithSameNameFail()
        {
            try
            {

                //Arrange
                _taskService.ClearList();
                var taskItem1 = new dobModel.Task() { Name = "task1", Priority = 1, Status = "not started", TaskId = 0 };
                var taskItem2 = new dobModel.Task() { Name = "task1", Priority = 1, Status = "not started", TaskId = 0 };


                // Act
                _taskService.AddTask(taskItem1);
                _taskService.AddTask(taskItem2);

                // Assert
                Assert.Fail("Task Name Already Exists");
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual(ex.Message, "Task name already exist.");
            }

        }

        [TestMethod]
        public void UpdateTask_Sucess()
        {


            //Arrange
            _taskService.ClearList();
            var taskItem1 = new dobModel.Task() { Name = "task1", Priority = 1, Status = "not started", TaskId = 0 };
            var taskItem2 = new dobModel.Task() { Name = "task2", Priority = 1, Status = "not started", TaskId = 0 };


                // Act
                _taskService.AddTask(taskItem1);
                _taskService.AddTask(taskItem2);
                //Update 2 task with First Task Name
                var taskUpdateItem2 = new dobModel.Task() { Name = "task3", Priority = 1, Status = "not started", TaskId = 2 };
                _taskService.UpdateTask(taskUpdateItem2);

                var result = _taskService.GetTaskById(2);
                // Assert

                // Assert
                Assert.IsTrue(result != null);
                Assert.AreEqual(result.Name, taskUpdateItem2.Name);
                Assert.AreEqual(result.Status, taskUpdateItem2.Status);


        }

        [TestMethod]
        public void UpdateTask_WithSameNameFail()
        {
            try
            {
                //Arrange
                _taskService.ClearList();
                var taskItem1 = new dobModel.Task() { Name = "task1", Priority = 1, Status = "not started", TaskId = 0 };
                var taskItem2 = new dobModel.Task() { Name = "task2", Priority = 1, Status = "not started", TaskId = 0 };


                // Act
                _taskService.AddTask(taskItem1);
                _taskService.AddTask(taskItem2);
                //Update 2 task with First Task Name
                var taskUpdateItem2=  new dobModel.Task() { Name = "task1", Priority = 1, Status = "not started", TaskId = 2 };
                _taskService.UpdateTask(taskUpdateItem2);
                // Assert
                Assert.Fail("Task Name Already Exists");
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual(ex.Message, "Task name already exist.");
            }

        }

        [TestMethod]
        public void DeleteTask_Sucess()
        {
            //Arrange
            _taskService.ClearList();
            var taskItem1 = new dobModel.Task() { Name = "task1", Priority = 1, Status = "not started", TaskId = 0 };
            var taskItem2 = new dobModel.Task() { Name = "task2", Priority = 1, Status = "completed", TaskId = 0 };


            // Act
            _taskService.AddTask(taskItem1);
            _taskService.AddTask(taskItem2);
            _taskService.DeleteTask(2);
            // Assert
            var result = _taskService.GetAllTasks();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(taskItem1));
            Assert.IsFalse(result.Contains(taskItem2));
        }


        [TestMethod]
        public void DeleteTask_NotCompletedFail()
        {
            try
            {
                //Arrange
                _taskService.ClearList();
                var taskItem1 = new dobModel.Task() { Name = "task1", Priority = 1, Status = "not started", TaskId = 0 };
                var taskItem2 = new dobModel.Task() { Name = "task2", Priority = 1, Status = "not started", TaskId = 0 };


                // Act
                _taskService.AddTask(taskItem1);
                _taskService.AddTask(taskItem2);
                _taskService.DeleteTask(2);
                // Assert
                Assert.Fail("Only completed task can be deleted");
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual(ex.Message, "Only completed task can be deleted.");
            }

        }

    }
}
