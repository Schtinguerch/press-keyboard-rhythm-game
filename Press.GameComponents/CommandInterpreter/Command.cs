using System.Collections.Generic;

namespace Press.GameComponents
{
    public abstract class Command
    {
        public abstract string Name { get; set; }

        public abstract void Run(List<string> arguments);

        protected void SetAdditional(List<string> arguments)
        {
            if (arguments != null)
            {
                SubCommandName = (arguments.Count > 0) ? arguments[0] : "";
                SubCommandArguments = arguments.SubList(0);
                UnitedStringArgument = arguments.ConcatToString();
            }
        }

        protected string UnitedStringArgument;

        protected string SubCommandName;

        protected List<string> SubCommandArguments;
    }
}
