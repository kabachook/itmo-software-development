using System;
using System.Collections.Generic;

namespace TodoApp
{
    /// <summary>
    /// Interface for InteractiveConsole command
    /// </summary>
    public interface ICommand
    {
        /// <value>
        /// Help string, shown in /help menu
        /// </value>
        public string Help { get; }

        /// <value>
        /// Usage string, shown in /help menu
        /// </value>
        public string Usage { get; }

        /// <summary>
        /// Method which will be called on command invokation
        /// </summary>
        /// <param name="args">List of arguments</param>
        /// <returns>String to print to console</returns>
        public string Invoke(IList<string> args = default(List<string>));
    }
}