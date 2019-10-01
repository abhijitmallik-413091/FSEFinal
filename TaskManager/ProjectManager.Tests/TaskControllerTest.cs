using NUnit.Framework;
using TaskManager.Controllers;
using TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Test
{
    [TestFixture]
    public class TaskControllerTest
    {
        [Test]
        public void TestRetrieveTasks_Success()
        {
            var context = new MockTaskDBEntities();
            InitializeData(context);
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveTaskByUserInput("Task1", null, null, null, DateTime.MinValue, DateTime.MinValue) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<TaskManager.Models.Task>),result.Data);
        }

        [Test]
        public void TestRetrieveParentTasks_Success()
        {
            var context = new MockTaskDBEntities();
            InitializeData(context);
            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.RetrieveParentTasks() as JSendResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<TaskManager.Models.ParentTask>),result.Data);
            Assert.AreEqual((result.Data as List<ParentTask>).Count, 1);
        }

        [Test]
        public void TestInsertTasks_Success()
        {
            var context = new MockTaskDBEntities();            
            
            var task = new TaskManager.Models.Task()
            {
                Task_Name = "Task4",
                Parent_ID = 1,                
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0,                
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.InsertTaskDetails(task) as JSendResponse;
                        Assert.IsNotNull(result);            
        }

        [Test]
        public void TestUpdateTasks_Success()
        {
            var context = new MockTaskDBEntities();
            InitializeData(context);
            var testTask = new Models.Task()
            {
                TaskId = 1,
                Task_Name = "task11",                                               
                End_Date = DateTime.Now.AddDays(5),
                Priority = 28,                              
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.UpdateTaskDetails(testTask) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Tasks.Local[0]).Priority, 28);
        }

        [Test]
        public void TestDeleteProjects_Success()
        {
            var context = new MockTaskDBEntities();
            InitializeData(context);

            var testTask = new Models.Task()
            {
                TaskId = 1,            
            };

            var controller = new TaskController(new BC.TaskBC(context));
            var result = controller.DeleteTaskDetails(testTask) as JSendResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)result.Data, 2);
        }     
        
        [Test]       
        public void TestInsertTask_NullTaskObject()
        {
            var context = new MockTaskDBEntities();
            var controller = new TaskController(new BC.TaskBC(context));           
            Assert.That(() => controller.InsertTaskDetails(null),
              Throws.TypeOf<ArgumentNullException>());
        }

        [Test]        
        public void TestInsertTask_NegativeTaskParentId()
        {
            var context = new MockTaskDBEntities();
            TaskManager.Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));           
            Assert.That(() => controller.InsertTaskDetails(task),
             Throws.TypeOf<ArithmeticException>());
        }        

        [Test]       
        public void TestInsertTask_NegativeTaskId()
        {
            var context = new MockTaskDBEntities();
            TaskManager.Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));           
            Assert.That(() => controller.InsertTaskDetails(task),
                Throws.TypeOf<ArithmeticException>());
        }

        [Test]        
        public void TestUpdateTask_NullTaskObject()
        {
            var context = new MockTaskDBEntities();
            var controller = new TaskController(new BC.TaskBC(context));            
            Assert.That(() => controller.UpdateTaskDetails(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]       
        public void TestUpdateTask_NegativeTaskParentId()
        {
            var context = new MockTaskDBEntities();
            TaskManager.Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));          
            Assert.That(() => controller.UpdateTaskDetails(task),
               Throws.TypeOf<ArithmeticException>());
        }        

        [Test]        
        public void TestUpdateTask_NegativeTaskId()
        {
            var context = new MockTaskDBEntities();
            TaskManager.Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));           
            Assert.That(() => controller.UpdateTaskDetails(task),
                 Throws.TypeOf<ArithmeticException>());
        }
        
        [Test]       
        public void TestDeleteTask_NullTaskObject()
        {
            var context = new MockTaskDBEntities();
            var controller = new TaskController(new BC.TaskBC(context));           
            Assert.That(() => controller.DeleteTaskDetails(null),
                Throws.TypeOf<ArgumentNullException>());
        }
        
        [Test]       
        public void TestDeleteTask_NegativeTaskParentId()
        {
            var context = new MockTaskDBEntities();
            TaskManager.Models.Task task = new Models.Task();
            task.Parent_ID = -234;
            var controller = new TaskController(new BC.TaskBC(context));           
            Assert.That(() => controller.DeleteTaskDetails(task),
                Throws.TypeOf<ArithmeticException>());
        }
      
        [Test]        
        public void TestDeleteTask_NegativeTaskId()
        {
            var context = new MockTaskDBEntities();
            TaskManager.Models.Task task = new Models.Task();
            task.TaskId = -234;
            var controller = new TaskController(new BC.TaskBC(context));          
            Assert.That(() => controller.DeleteTaskDetails(task),
                                  Throws.TypeOf<ArithmeticException>());
        }

        private void InitializeData(MockTaskDBEntities context)
        {
            context.Tasks.Add(new DAC.Task()
            {              
                Task_ID = 1,
                Task_Name = "Task1",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(2),
                Priority = 10,
                Status = 0                
            });

            context.Tasks.Add(new DAC.Task()
            {
                Task_ID = 2,
                Task_Name = "Task2",
                Start_Date = DateTime.Now.AddDays(1),
                End_Date = DateTime.Now.AddDays(3),
                Priority = 5,
                Status = 0,
                Parent_ID = 1
            });

            context.ParentTasks.Add(new DAC.ParentTask()
            {
                Parent_ID = 1,
                Parent_Task_Name = "Task1"
            });
        }
    }
}
