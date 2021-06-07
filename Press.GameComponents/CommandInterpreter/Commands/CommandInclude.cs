using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Press.GameComponents
{
    class CommandInclude : Command
    {
        public override string Name { get; set; } = "include";

        public override void Run(List<string> arguments, object processingObject = null)
        {
            SetAdditional(arguments);

            if (UnitedStringArgument.LastChar() != 's')
                UnitedStringArgument = UnitedStringArgument.Substring(0, UnitedStringArgument.Length - 1);

            var includingCode = File.ReadAllText(UnitedStringArgument).PreprocessedMap();
            var processingCode = (TextBlock)processingObject;

            processingCode.Text = processingCode.Text.Replace($"{Name} {UnitedStringArgument}", includingCode);
        }
    }
}
