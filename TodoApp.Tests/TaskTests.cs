using System;
using Xunit;

namespace TodoApp.Tests
{
    public class TaskTests
    {
        private Task task;
        public TaskTests(){
            this.task = new Task("test");
        }

        [Fact]
        public void New_StatusIsIdle()
        {
            Assert.Equal(TaskStatus.Idle, this.task.Status);
        }

        [Fact]
        public void MarkDone_StatusIsDone(){
            this.task.MarkDone();
            Assert.Equal(TaskStatus.Done, this.task.Status);
        }

        [Fact]
        public void MarkIdle_StatusIsIdle(){
            this.task.MarkDone();
            this.task.MarkIdle();
            Assert.Equal(TaskStatus.Idle, this.task.Status);
        }
    }
}
