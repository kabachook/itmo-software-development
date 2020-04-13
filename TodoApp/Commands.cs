using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TodoApp
{
    class HelpCommand : ICommand
    {
        public string Help => "List available commands";
        public string Usage => "";
        private Dictionary<string, ICommand> _commands;
        public HelpCommand(Dictionary<string, ICommand> commands)
        {
            this._commands = commands;
        }
        public string Invoke(IList<string> args = default(List<string>))
        {
            var builder = new StringBuilder();
            foreach (var (name, cmd) in _commands)
            {
                builder.Append($"/{name} {cmd.Usage} — {cmd.Help}\n");
            }
            return builder.ToString();
        }
    }

    class NewTaskCommand : ICommand
    {
        public string Help => "New task";
        public string Usage => "name";
        private TodoList _todoList;
        public NewTaskCommand(TodoList todoList)
        {
            this._todoList = todoList;
        }
        public string Invoke(IList<string> args = default(List<string>))
        {
            _todoList.Add(new Task(args[0]));
            return "Added!";
        }
    }

    class RemoveTaskCommand : ICommand
    {
        public string Help => "Remove task";
        public string Usage => "index";
        private TodoList _todoList;
        public RemoveTaskCommand(TodoList todoList)
        {
            this._todoList = todoList;
        }
        public string Invoke(IList<string> args = default(List<string>))
        {
            int idx;
            if (!int.TryParse(args[0], out idx))
            {
                throw new ArgumentException("Index is not valid!");
            }

            _todoList.Remove(idx);
            return "Removed!";
        }
    }

    class ListTasksCommand : ICommand
    {
        public string Help => "List tasks";
        public string Usage => "";
        private TodoList _todoList;
        public ListTasksCommand(TodoList todoList)
        {
            this._todoList = todoList;
        }
        public string Invoke(IList<string> args = default(List<string>))
        {
            var builder = new StringBuilder();

            foreach (var (idx, task) in _todoList.TaskList.Select((Value, Index) => (Index, Value)))
            {
                builder.Append($"{idx}. {task.Name}, status: {task.Status}, elapsed: {task.ElapsedTime}\n");
            }

            return builder.ToString();
        }
    }

    class ToggleCommand : ICommand
    {
        public string Help => "Toggle task (idle/doing)";
        public string Usage => "index";

        private TodoList _todoList;

        public ToggleCommand(TodoList todoList)
        {
            this._todoList = todoList;
        }

        public string Invoke(IList<string> args = default(List<string>))
        {
            int idx;
            if (!int.TryParse(args[0], out idx))
            {
                throw new ArgumentException("Index is not valid!");
            }

            this._todoList.Toggle(idx);
            return "Toggled!";
        }

    }

    class DoneCommand : ICommand
    {
        public string Help => "Mark task done";
        public string Usage => "index";

        private TodoList _todoList;

        public DoneCommand(TodoList todoList)
        {
            this._todoList = todoList;
        }

        public string Invoke(IList<string> args = default(List<string>))
        {
            int idx;
            if (!int.TryParse(args[0], out idx))
            {
                throw new ArgumentException("Index is not valid!");
            }

            this._todoList.MarkDone(idx);
            return "ok";
        }
    }

    class IdleCommand : ICommand
    {
        public string Help => "Mark task idle";
        public string Usage => "index";

        private TodoList _todoList;

        public IdleCommand(TodoList todoList)
        {
            this._todoList = todoList;
        }

        public string Invoke(IList<string> args = default(List<string>))
        {
            int idx;
            if (!int.TryParse(args[0], out idx))
            {
                throw new ArgumentException("Index is not valid!");
            }

            this._todoList.MarkIdle(idx);
            return "ok";
        }
    }

    class ProgressCommand : ICommand {
        public static int CellsCount = 10;
        public static char FilledCell = '#';
        public static char EmptyCell = '_';
        public string Help => "See your progress";
        public string Usage => "";

        private TodoList _todoList;

        public ProgressCommand(TodoList todoList)
        {
            this._todoList = todoList;
        }

        public string Invoke(IList<string> args = default(List<string>))
        {
            var builder = new StringBuilder();

            int countDone = _todoList.TaskList.Count(t => t.Status == TaskStatus.Done);
            int countAll = _todoList.TaskList.Count();
            double percentDone = (double)countDone / (double)countAll;
            int filledCells = (int)Math.Round(percentDone*CellsCount);

            builder.Append("[");
            for (int i = 0; i < filledCells; i++){
                builder.Append(FilledCell);
            }
            for (int i = filledCells; i < CellsCount; i++){
                builder.Append(EmptyCell);
            }
            builder.Append("] ").Append($"{percentDone*100:.##}").Append(" %");

            return builder.ToString();
        }
    }

    class SaveCommand : ICommand {
        public string Help => "Save tasks to file";
        public string Usage => "filename";

        private TodoList _todoList;

        public SaveCommand(TodoList todoList)
        {
            this._todoList = todoList;
        }

        public string Invoke(IList<string> args = default(List<string>))
        {
            string json = _todoList.Serialize();
            using (var writer = new StreamWriter(args[0])){
                writer.Write(json);
            }
            return json;
        }
    }

    class LoadCommand : ICommand {
        public string Help => "Load tasks from file";
        public string Usage => "filename";

        private TodoList _todoList;

        public LoadCommand(TodoList todoList)
        {
            this._todoList = todoList;
        }

        public string Invoke(IList<string> args = default(List<string>))
        {
            string json;
            using (var reader = new StreamReader(args[0])){
                json = reader.ReadToEnd();
            }

            List<Task> tasks = JsonConvert.DeserializeObject<List<Task>>(json);
            _todoList.TaskList.AddRange(tasks);

            return "ok";
        }
    }
}