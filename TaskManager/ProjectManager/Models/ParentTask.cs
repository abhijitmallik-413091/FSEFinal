using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class ParentTask
    {
        public int ParentTaskId { get; set; }

        public string ParentTaskName { get; set; }
    }
}