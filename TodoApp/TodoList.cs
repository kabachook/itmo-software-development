using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TodoApp
{
    /// <summary>
    /// Class which represents task storage == task list
    /// </summary>
    public class TodoList
    {
        public List<Task> TaskList { get; private set; }


        public TodoList(IEnumerable<Task> tasks = default(List<Task>))
        {
            tasks = tasks ?? new List<Task>();
            this.TaskList = tasks.ToList();
        }

        /// <summary>
        /// Add task to list
        /// </summary>
        /// <param name="task">Task to add</param>
        public void Add(Task task)
        {
            TaskList.Add(task);
        }

        /// <summary>
        /// Mark task as done
        /// </summary>
        /// <param name="idx">Index of task in list</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when index is not in the list
        /// </exception>
        public void MarkDone(int idx)
        {
            if (idx >= TaskList.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(idx), idx, "Index out of range");
            }
            TaskList[idx].MarkDone();
        }

        /// <summary>
        /// Mark task as doing
        /// </summary>
        /// <param name="idx">Index of task in list</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when index is not in the list
        /// </exception>
        public void MarkDoing(int idx)
        {
            if (idx >= TaskList.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(idx), idx, "Index out of range");
            }
            TaskList[idx].MarkDoing();
        }

        /// <summary>
        /// Mark task as idle
        /// </summary>
        /// <param name="idx">Index of task in list</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when index is not in the list
        /// </exception>
        public void MarkIdle(int idx)
        {
            if (idx >= TaskList.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(idx), idx, "Index out of range");
            }
            TaskList[idx].MarkIdle();
        }

        /// <summary>
        /// Toggle task status (done/idle)
        /// </summary>
        /// <param name="idx">Index of task in list</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when index is not in the list
        /// </exception>
        public void Toggle(int idx)
        {
            if (idx >= TaskList.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(idx), idx, "Index out of range");
            }

            var task = TaskList[idx];

            switch (task.Status)
            {
                
                case TaskStatus.Idle:
                    task.MarkDoing();
                    break;
                case TaskStatus.Doing:
                    task.MarkIdle();
                    break;
            }

        }

        /// <summary>
        /// Remove task from list
        /// </summary>
        /// <param name="idx">Index of task in list</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown when index is not in the list
        /// </exception>
        public void Remove(int idx)
        {
            if (idx >= TaskList.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(idx), idx, "Index out of range");
            }
            TaskList.RemoveAt(idx);
        }

        public string Serialize(){
            return JsonConvert.SerializeObject(this.TaskList, Formatting.Indented);
        }
    }
}