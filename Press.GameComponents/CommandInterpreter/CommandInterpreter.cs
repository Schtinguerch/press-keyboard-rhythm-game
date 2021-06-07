using System;
using System.Collections.Generic;
using System.Linq;

namespace Press.GameComponents
{
    public class CommandInterpreter
    {
        private List<Command> _commands;

        private object _processingObject;

        public List<Command> Commands { get => _commands; }

        public CommandInterpreter(object processingObject = null)
        {
            _commands = new List<Command>();
            _processingObject = processingObject;
        }


        public CommandInterpreter(List<Command> commands, object processingObject = null)
        {
            _commands = commands;
            _processingObject = processingObject;
        }

        public CommandInterpreter(Command[] commands, object processingObject = null)
        {
            _commands = commands.ToList();
            _processingObject = processingObject;
        }
            

        public void Run(string commandName, string[] arguments) =>
            Run(commandName, arguments.ToList(), _processingObject);

        public void Run(string expression)
        {
            var tokens = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            Run(tokens[0], tokens.SubList(0), _processingObject);
        }

        public void Run(string commandName, List<string> arguments, object processingObject = null)
        {
            foreach (var command in _commands)
                if (commandName == command.Name)
                {
                    command.Run(arguments, processingObject);
                    break;
                }
        }
    }
}
