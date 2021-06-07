using System;
using System.Collections.Generic;
using Press.GameComponents.Properties;

namespace Press.GameComponents
{
    public class SetCircleRadius : Command
    {
        public override string Name { get; set; } = "circleRadius";

        public override void Run(List<string> arguments, object processingObject = null) =>
            Settings.Default.CircleRadius = Convert.ToDouble(arguments[0]);
    }
}
