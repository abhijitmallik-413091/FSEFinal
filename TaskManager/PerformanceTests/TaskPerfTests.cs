using NUnit.Framework;
using NBench;
using TaskManager.Controllers;
using System;

namespace PerformanceTests
{
    [TestFixture]
    public class TaskPerfTests
    {
        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput,
        TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void PerformanceTestRetrieveTasks()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            // Act on Test  
            var response = controller.RetrieveTaskByUserInput("", "", 1, 15, null, null);
            // Assert the result  
            Assert.IsTrue(response != null);

        }

        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput,
        TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void PerformanceTestCreateTasks()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            // Act on Test  
            TaskManager.Models.Task testTask = new TaskManager.Models.Task()
            {
                Task_Name = "Task10",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(4),
                Priority = 1                
            };
            var response = controller.InsertTaskDetails(testTask);
            // Assert the result  
            Assert.IsTrue(response != null);
        }
    }
}
