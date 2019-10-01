using TaskManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public int? Parent_ID { get; set; }    
        public string Task_Name { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public int? Priority { get; set; }
        public int Status { get; set; }        
        public string ParentTaskName { get; set; }
    }
}