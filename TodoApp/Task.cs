using System;

namespace TodoApp
{
    /// <summary>
    /// Task statuses
    /// </summary>
    public enum TaskStatus
    {
        Idle,
        Doing,
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

        /// <value>Task elapsed time</value>
        public TimeSpan ElapsedTime { get; private set; }

        /// <value>Last time the task has been toggled</value>
        private DateTime _lastTimeToggled;

        public Task(string name)
        {
            this.Name = name;
            this.Status = TaskStatus.Idle;
            this.ElapsedTime = TimeSpan.Zero;
            this._lastTimeToggled = DateTime.MinValue;
        }

        /// <summary>
        /// Mark task as done
        /// </summary>
        public void MarkDone()
        {
            if (this.Status == TaskStatus.Doing)
            {
                UpdateElapsedTime();
            }
            this.Status = TaskStatus.Done;
        }

        /// <summary>
        /// Mark task as doing
        /// </summary>
        public void MarkDoing()
        {
            this.Status = TaskStatus.Doing;
            this._lastTimeToggled = DateTime.Now;
        }

        /// <summary>
        /// Mark task as idle
        /// </summary>
        public void MarkIdle()
        {
            if (this.Status == TaskStatus.Doing)
            {
                UpdateElapsedTime();
            }
            this.Status = TaskStatus.Idle;
        }

        /// <summary>
        /// Update elapsed time
        /// </summary>
        private void UpdateElapsedTime()
        {
            if (this._lastTimeToggled != DateTime.MinValue)
            {
                this.ElapsedTime = this.ElapsedTime + (DateTime.Now - this._lastTimeToggled);
            }
        }
    }
}