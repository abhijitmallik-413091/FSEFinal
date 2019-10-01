using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Models;
using TaskManager.DAC;

using TaskBCModel = TaskManager.Models.Task;
using ParentTaskBCModel = TaskManager.Models.ParentTask;
using TaskDACModel = TaskManager.DAC.Task;
using ParentTaskDACModel = TaskManager.DAC.ParentTask;

namespace TaskManager.BC
{
    public class TaskBC : IDisposable
    {
        #region Fields ....
        private TaskDBEntities _dbContext;
        #endregion

        #region Constructors ....
        public TaskBC()
        {
            _dbContext = new TaskDBEntities();
        }

        public TaskBC(TaskDBEntities context)
        {
            _dbContext = context;
        }
        #endregion

        #region Public Methods ....
        public List<TaskBCModel> RetrieveTaskByUserInput(string taskName, string parentTaskName, int? priorityFrom, int? priorityTo, DateTime startDate, DateTime endDate)
        {
            List<TaskBCModel> tasks = new List<TaskBCModel>();
            ParentTaskDACModel parentTask = null;
            IEnumerable<TaskBCModel> tasksForParent = null;
            var dbQuery = _dbContext.Tasks.Select(taskEntry => new TaskBCModel()
            {
                TaskId = taskEntry.Task_ID,
                Task_Name = taskEntry.Task_Name,
                Start_Date = taskEntry.Start_Date,
                End_Date = taskEntry.End_Date,
                Priority = taskEntry.Priority,
                Status = taskEntry.Status
            });

            if (!string.IsNullOrEmpty(parentTaskName.Trim('"')))
            {
                parentTask = GetParentTaskByName(parentTaskName);

                if (parentTask != null && parentTask.Parent_ID >= 0)
                {
                    tasksForParent = parentTask.Tasks.Select(taskEntry => new TaskBCModel()
                    {
                        TaskId = taskEntry.Task_ID,
                        Task_Name = taskEntry.Task_Name,
                        Start_Date = taskEntry.Start_Date,
                        End_Date = taskEntry.End_Date,
                        Priority = taskEntry.Priority,
                        Status = taskEntry.Status
                    });
                }
            }   
            
            if(tasksForParent != null)
            {
                tasks = ApplyFilters(tasksForParent, taskName, priorityFrom, priorityTo, startDate, endDate);
            }
            else
            {
                tasks = ApplyFilters(dbQuery, taskName, priorityFrom, priorityTo, startDate, endDate);
            }            

            if (parentTask != null && parentTask.Parent_ID >= 0)
            {
                tasks.ForEach(taskEntry => taskEntry.ParentTaskName = parentTask.Parent_Task_Name);
            }

            return tasks;
        }

        public List<ParentTaskBCModel> RetrieveParentTasks()
        {           
                return _dbContext.ParentTasks.Select(parentTaskEntry => new ParentTaskBCModel()
                {
                    ParentTaskId = parentTaskEntry.Parent_ID,
                    ParentTaskName = parentTaskEntry.Parent_Task_Name
                }).ToList();            
        }


        public int InsertTaskDetails(TaskBCModel task)
        {                
            TaskDACModel taskDetail = new TaskDACModel()
            {
                Task_Name = task.Task_Name,                        
                Start_Date = task.Start_Date,
                End_Date = task.End_Date,
                Priority = task.Priority,
                Status = task.Status
            };
            _dbContext.Tasks.Add(taskDetail); 
            
            if(!string.IsNullOrEmpty(task.ParentTaskName))
            {
                ParentTaskDACModel parentTask = GetParentTaskByName(task.ParentTaskName);

                if(parentTask != null && parentTask.Parent_ID >= 0)
                {
                    taskDetail.Parent_ID = parentTask.Parent_ID;
                }
                else
                {
                    TaskDACModel taskDACModel = GetTaskByName(task.ParentTaskName);

                    if(taskDACModel != null && taskDACModel.Task_ID >= 0)
                    {
                        parentTask = new ParentTaskDACModel
                        {
                            Parent_ID = taskDACModel.Task_ID,
                            Parent_Task_Name = taskDACModel.Task_Name
                        };
                        taskDetail.Parent_ID = taskDACModel.Task_ID;                       
                        _dbContext.ParentTasks.Add(parentTask);
                    }
                }
            }

            return _dbContext.SaveChanges();            
        }

        public int UpdateTaskDetails(TaskBCModel task)
        {
            TaskDACModel editDetails = GetTaskById(task.TaskId);
            // Modify existing records
            if (editDetails != null)
            {
                if (!string.IsNullOrEmpty(task.Task_Name) && string.Compare(task.Task_Name, editDetails.Task_Name, true) != 0)
                {
                    editDetails.Task_Name = task.Task_Name;
                    ParentTaskDACModel parentTask = GetParentTaskById(task.TaskId);
                    if(parentTask != null)
                    {
                        parentTask.Parent_Task_Name = task.Task_Name;
                    }
                }

                if (task.Start_Date.HasValue && task.Start_Date.Value > DateTime.MinValue && task.Start_Date.Value.CompareTo(editDetails.Start_Date.Value) != 0)
                {
                    editDetails.Start_Date = task.Start_Date;
                }

                if (task.End_Date.HasValue && task.End_Date.Value > DateTime.MinValue && task.End_Date.Value.CompareTo(editDetails.End_Date.Value) != 0)
                {
                    editDetails.End_Date = task.End_Date;
                }

                if (task.Status >= 0 && task.Status != editDetails.Status)
                {
                    editDetails.Status = task.Status;
                }

                if (task.Priority.HasValue && task.Priority.Value >= 0 && task.Priority.Value != editDetails.Priority.Value)
                {
                    editDetails.Priority = task.Priority;
                }
            }
            
            return _dbContext.SaveChanges();            
        }

        public int DeleteTaskDetails(TaskBCModel task)
        {
            TaskDACModel taskDACModel = GetTaskById(task.TaskId);
                
            if (taskDACModel != null)
            {                
                _dbContext.Tasks.Remove(taskDACModel);

                ParentTaskDACModel parentTask = GetParentTaskById(task.TaskId);

                if(parentTask != null)
                {
                    DeleteParentTask(parentTask);
                }
            }
            return _dbContext.SaveChanges();            
        }

        public void Dispose()
        {
            if(_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
        #endregion

        #region Private Methods ....
        private ParentTaskDACModel GetParentTaskByName(string parentTaskName)
        {
            return _dbContext.ParentTasks.Where(parentTaskEntry => string.Compare(parentTaskEntry.Parent_Task_Name, parentTaskName, true) == 0).FirstOrDefault();
        }

        private ParentTaskDACModel GetParentTaskById(int parentTaskId)
        {
            return _dbContext.ParentTasks.Where(parentTaskEntry => parentTaskEntry.Parent_ID == parentTaskId).FirstOrDefault();
        }

        private TaskDACModel GetTaskByName(string taskName)
        {
            return _dbContext.Tasks.Where(taskEntry => string.Compare(taskEntry.Task_Name, taskName, true) == 0).FirstOrDefault();
        }

        private TaskDACModel GetTaskById(int taskId)
        {
            return _dbContext.Tasks.Where(taskEntry => taskEntry.Task_ID == taskId).FirstOrDefault();
        }      
        
        private void DeleteParentTask(ParentTaskDACModel parentTask)
        {
            List<TaskDACModel> tasks = _dbContext.Tasks.Where(taskEntry => taskEntry.Parent_ID == parentTask.Parent_ID).ToList();
            _dbContext.ParentTasks.Remove(parentTask);

            tasks.ForEach(taskEntry => taskEntry.Parent_ID = null);
        }

        private List<TaskBCModel> ApplyFilters(IQueryable<TaskBCModel> dbQuery, string taskName, int? priorityFrom, int? priorityTo, DateTime startDate, DateTime endDate)
        {
            if (!string.IsNullOrEmpty(taskName.Trim('"')))
            {

                dbQuery = dbQuery.Where(taskEntry => string.Compare(taskEntry.Task_Name, taskName, true) == 0);
            }

            if (priorityFrom.HasValue && priorityFrom.Value >= 0)
            {
                dbQuery = dbQuery.Where(taskEntry => taskEntry.Priority >= priorityFrom.Value);
            }

            if (priorityTo.HasValue && priorityTo.Value >= 0)
            {
                dbQuery = dbQuery.Where(taskEntry => taskEntry.Priority <= priorityTo.Value);
            }

            if (startDate > DateTime.MinValue)
            {
                dbQuery = dbQuery.Where(taskEntry => taskEntry.Start_Date >= startDate);
            }

            if (endDate > DateTime.MinValue)
            {
                dbQuery = dbQuery.Where(taskEntry => taskEntry.End_Date <= endDate);
            }

            return dbQuery.ToList();
        }

        private List<TaskBCModel> ApplyFilters(IEnumerable<TaskBCModel> dbQuery, string taskName, int? priorityFrom, int? priorityTo, DateTime startDate, DateTime endDate)
        {
            if (!string.IsNullOrEmpty(taskName.Trim('"')))
            {
                dbQuery = dbQuery.Where(taskEntry => string.Compare(taskEntry.Task_Name, taskName, true) == 0);
            }

            if (priorityFrom.HasValue && priorityFrom.Value >= 0)
            {
                dbQuery = dbQuery.Where(taskEntry => taskEntry.Priority >= priorityFrom.Value);
            }

            if (priorityTo.HasValue && priorityTo.Value >= 0)
            {
                dbQuery = dbQuery.Where(taskEntry => taskEntry.Priority <= priorityTo.Value);
            }

            if (startDate > DateTime.MinValue)
            {
                dbQuery = dbQuery.Where(taskEntry => taskEntry.Start_Date >= startDate);
            }

            if (endDate > DateTime.MinValue)
            {
                dbQuery = dbQuery.Where(taskEntry => taskEntry.End_Date <= endDate);
            }

            return dbQuery.ToList();
        }
        #endregion
    }
}