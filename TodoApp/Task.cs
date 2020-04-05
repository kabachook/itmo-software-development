using System;

namespace TodoApp
{
    /// <summary>
    /// Task statuses
    /// </summary>
    public enum TaskStatus
    {
        Idle,
        Done
    }
    
    /// <summary>
    /// A class which represents a task
    /// </summary>
    public class Task
    {
        /// <value>Task name</value>
        public string Name { get; private set; }

        /// <value>Task status</value>
        public TaskStatus Status { get; private set; }

        public Task(string name)
        {
            this.Name = name;
            this.Status = TaskStatus.Idle;
        }

        private Task(string name, TaskStatus status){
            Name = name;
            Status = status;
        }

        /// <summary>
        /// Mark task as done
        /// </summary>
        public void MarkDone(){
            this.Status = TaskStatus.Done;
        }

        /// <summary>
        /// Mark task as idle
        /// </summary>
        public void MarkIdle(){
            this.Status = TaskStatus.Idle;
        }
    }
}