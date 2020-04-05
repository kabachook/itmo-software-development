using System;
using Xunit;

namespace TodoApp.Tests
{
    public class TodoListTests
    {
        public class EmptyTodoList
        {
            private readonly TodoList _todoList;
            public EmptyTodoList()
            {
                this._todoList = new TodoList();
            }

            [Fact]
            public void New_IsEmpty()
            {
                Assert.Empty(_todoList.TaskList);
            }

            [Fact]
            public void Add_IsSingle()
            {
                this._todoList.Add(new Task("test"));
                Assert.Single(_todoList.TaskList);
            }

            [Fact]
            public void MarkDone_Throws()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _todoList.MarkDone(0));
            }

            [Fact]
            public void MarkIdle_Throws()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _todoList.MarkIdle(0));
            }

            [Fact]
            public void Remove_Throws()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _todoList.Remove(0));
            }

            [Fact]
            public void Toggle_Throws()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _todoList.Toggle(0));
            }
        }

        public class SingleTodoList {
            private readonly TodoList _todoList;

            public SingleTodoList()
            {
                this._todoList = new TodoList();
                this._todoList.Add(new Task("test"));
            }

            [Fact]
            public void MarkDone_StatusIsDone(){
                _todoList.MarkDone(0);
                Assert.Equal(TaskStatus.Done, _todoList.TaskList[0].Status);
            }
            
            [Fact]
            public void Toggle_StatusIsDone(){
                _todoList.MarkDone(0);
                Assert.Equal(TaskStatus.Done, _todoList.TaskList[0].Status);
            }

            [Fact]
            public void Remove_Empty(){
                _todoList.Remove(0);
                Assert.Empty(_todoList.TaskList);
            }
        }
    }
}
