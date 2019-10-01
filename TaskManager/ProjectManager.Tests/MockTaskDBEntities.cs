using System.Data.Entity;

namespace TaskManager.Test
{
    class MockTaskDBEntities : DAC.TaskDBEntities
    {
        private DbSet<DAC.Task> _tasks;
        private DbSet<DAC.ParentTask> _parentTasks;

        public MockTaskDBEntities()
        {
            _tasks = new TestDbSet<DAC.Task>();
            _parentTasks = new TestDbSet<DAC.ParentTask>();
        }

        public override DbSet<DAC.Task> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        }

        public override DbSet<DAC.ParentTask> ParentTasks
        {
            get
            {
                return _parentTasks;
            }
            set
            {
                _parentTasks = value;
            }
        }
    }
}