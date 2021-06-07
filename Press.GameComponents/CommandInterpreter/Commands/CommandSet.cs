using System;
using System.Collections.Generic;

namespace Press.GameComponents
{
    class CommandSet : Command
    {
        public override string Name { get; set; } = "set";

        public override void Run(List<string> arguments, object processingObject = null)
        {
            SetAdditional(arguments);
            if (arguments.Count < 2) return;

            var commands = new List<Command>()
            {
                new SetShowTime(),
                new SetCircleRadius(),
                new SetTapperingAuraRadius(),
            };

            var subInterpreter = new CommandInterpreter(commands);
            subInterpreter.Run(SubCommandName, SubCommandArguments);
        }
    }
}
