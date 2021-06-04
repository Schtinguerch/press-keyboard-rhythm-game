using System;
using System.Collections.Generic;
using System.Linq;

namespace Press.GameComponents
{
    public class CommandInterpreter
    {
        private List<Command> _commands;

        public List<Command> Commands { get => _commands; }

        public CommandInterpreter() => 
            _commands = new List<Command>();

        public CommandInterpreter(List<Command> commands) =>
            _commands = commands;

        public CommandInterpreter(Command[] commands) =>
            _commands = commands.ToList();

        public void Run(string commandName, string[] arguments) =>
            Run(commandName, arguments.ToList());

        public void Run(string commandName, List<string> arguments)
        {
            foreach (var command in _commands)
                if (commandName == command.Name)
                {
                    command.Run(arguments);
                    break;
                }
        }
    }
}
