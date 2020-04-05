using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TodoApp
{
    /// <summary>
    /// Interactive console for TodoApp
    /// </summary>
    public class InteractiveConsole
    {
        private bool _exit = false;
        private TodoList _todoList = new TodoList(new List<Task>());
        private Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();


        public InteractiveConsole()
        {
            // Register all commands
            RegisterCommand("list", new ListTasksCommand(_todoList));
            RegisterCommand("new", new NewTaskCommand(_todoList));
            RegisterCommand("remove", new RemoveTaskCommand(_todoList));
            RegisterCommand("idle", new IdleCommand(_todoList));
            RegisterCommand("done", new DoneCommand(_todoList));
            RegisterCommand("toggle", new ToggleCommand(_todoList));
            RegisterCommand("help", new HelpCommand(_commands));

            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) {
                _exit = true;
                // Environment.Exit(0);
            };
        }

        /// <summary>
        /// Print object to console
        /// </summary>
        /// <param name="data">Object to print</param>
        private void Print(object data)
        {
            Console.WriteLine(data);
        }

        /// <summary>
        /// Print error
        /// </summary>
        /// <param name="data">Error object</param>
        private void PrintError(object data)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ERROR");
            Console.ResetColor();
            Console.Write("] ");
            Print(data);
        }

        /// <summary>
        /// Print prompt
        /// </summary>
        private void PrintPrompt(){
            Console.Write("> ");
        }

        /// <summary>
        /// Register a command in console
        /// </summary>
        /// <param name="name">Name of command</param>
        /// <param name="cmd">Command instance</param>
        private void RegisterCommand(string name, ICommand cmd)
        {
            _commands.Add(name, cmd);
        }

        /// <summary>
        /// Start interactive console
        /// </summary>
        public void Start()
        {
            Print("Welcome to ToDo app! Type /help to get program info.");
            PrintPrompt();
            while (!_exit)
            {
                try
                {
                    var line = Console.ReadLine();
                    if (line == null) continue;
                    var args = line.Split(" ");
                    if (args.Length < 1 || !args[0].StartsWith('/')) continue;

                    var cmd = args[0].Substring(1);

                    if (_commands.ContainsKey(cmd))
                    {
                        Print(_commands[cmd].Invoke(args.Skip(1).ToList()));
                    }
                    else
                    {
                        PrintError("Command does not exists");
                    }
                }
                catch (Exception e)
                {
                    PrintError(e.Message);
                }
                PrintPrompt();
            }
        }
    }
}