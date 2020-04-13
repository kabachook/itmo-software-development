using System;

namespace TodoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var console = new InteractiveConsole();
            console.Start();
        }
    }
}
