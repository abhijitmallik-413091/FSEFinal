using TaskManager.Models;
using TaskManager.BC;
using System.Web.Http;
using TaskManager.ActionFilters;
using System.Collections.Generic;
using System;

namespace TaskManager.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TaskController : ApiController
    {
        private TaskBC _taskBC;

        public TaskController()
        {
            _taskBC = new TaskBC();
        }

        public TaskController(TaskBC taskBc)
        {
            _taskBC = taskBc;
        }

        [HttpGet]
        //[Route("{taskName=taskName}/{parentTaskName=parentTaskName}/{priorityFrom=priorityFrom}/{priorityTo=priorityTo}/{startDate=startDate}/{endDate=endDate}")]
        [Route("{taskName?}/{parentTaskName?}/{priorityFrom?}/{priorityTo?}/{startDate?}/{endDate?}")]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]        
        public JSendResponse RetrieveTaskByUserInput(string taskName="", string parentTaskName="", int? priorityFrom=null, int? priorityTo=null, DateTime? startDate=null, DateTime? endDate=null)        
        {
            if (string.IsNullOrEmpty(taskName) && string.IsNullOrEmpty(parentTaskName) && priorityFrom == null && priorityTo == null && 
                (startDate == null || startDate == DateTime.MinValue) && (endDate == null || endDate == DateTime.MinValue))
            {
                throw new ArithmeticException("Please enter proper input to search task details");
            }

            DateTime startDateParam = DateTime.MinValue;
            if(startDate.HasValue)
            {
                startDateParam = startDate.Value;
            }

            DateTime endDateParam = DateTime.MinValue;
            if(endDate.HasValue)
            {
                endDateParam = endDate.Value;
            }
            List<Task> tasks = _taskBC.RetrieveTaskByUserInput(taskName, parentTaskName, priorityFrom, priorityTo, startDateParam, endDateParam);

            return new JSendResponse()
            {
                Data = tasks
            };
        }

        [HttpGet]
        [Route("parent")]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        public JSendResponse RetrieveParentTasks()
        {
            List<ParentTask> ParentTasks = _taskBC.RetrieveParentTasks();

            return new JSendResponse()
            {
                Data = ParentTasks
            };
        }

        [HttpPost]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        [Route("add")]
        public JSendResponse InsertTaskDetails(Task task)
        {
            ValidateTaskInput(task, false);
                        
            return new JSendResponse()
            {
                Data = _taskBC.InsertTaskDetails(task)
            };
        }

        [HttpPost]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        [Route("update")]
        public JSendResponse UpdateTaskDetails(Task task)
        {
            ValidateTaskInput(task);

            return new JSendResponse()
            {
                Data = _taskBC.UpdateTaskDetails(task)
            };
        }

        [HttpPost]
        [ProjectManagerLogFilter]
        [ProjectManagerExceptionFilter]
        [Route("delete")]
        public JSendResponse DeleteTaskDetails(Task task)
        {
            ValidateTaskInput(task);

            return new JSendResponse()
            {
                Data = _taskBC.DeleteTaskDetails(task)
            };
        }

        protected override void Dispose(bool disposing)
        {
            if(!disposing && _taskBC != null)
            {
                base.Dispose(disposing);
                _taskBC.Dispose();
                disposing = true;
            }
        }

        private void ValidateTaskInput(Task task, bool taskIdValidationRequired = true)
        {
            if (task == null)
            {
                throw new ArgumentNullException("Task object is null");
            }

            if (task.Parent_ID.HasValue && task.Parent_ID.Value < 0)
            {
                throw new ArithmeticException("Parent Id of task cannot be negative");
            }

            if (taskIdValidationRequired && task.TaskId < 0)
            {
                throw new ArithmeticException("Task id cannot be negative");
            }
        }
    }
}