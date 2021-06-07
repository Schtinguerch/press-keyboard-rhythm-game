using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Press.GameComponents
{
    class CommandDefine : Command
    {
        public override string Name { get; set; } = "define";

        public override void Run(List<string> arguments, object processingObject = null)
        {
            SetAdditional(arguments);

            var pattern = arguments[0];
            var replacement = SubCommandArguments.ConcatToString();

            var processingCode = (TextBlock)processingObject;

            processingCode.Text = processingCode.Text.RemoveFirstLines(1);
            processingCode.Text = processingCode.Text.Replace(pattern, replacement);

            Clipboard.SetText(processingCode.Text);
        }
    }
}
