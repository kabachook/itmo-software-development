using System;
using System.Threading;
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

        [Fact]
        public void MarkDoing_ElapsedTime(){
            int sleepPeriod = 1000;
            int eps = (int)(sleepPeriod * 0.1f);
            this.task.MarkDoing();
            Thread.Sleep(1000);
            this.task.MarkIdle();
            Assert.InRange(this.task.ElapsedTime, TimeSpan.FromMilliseconds(sleepPeriod - eps), TimeSpan.FromMilliseconds(sleepPeriod + eps));
        }
    }
}
