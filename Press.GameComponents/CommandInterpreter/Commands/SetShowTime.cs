using System;
using System.Collections.Generic;
using Press.GameComponents.Properties;

namespace Press.GameComponents
{
    public class SetShowTime : Command
    {
        public override string Name { get; set; } = "showTime";

        public override void Run(List<string> arguments, object processingObject = null) =>
            Settings.Default.ShowCircleTime = Convert.ToInt32(arguments[0]);
    }
}
