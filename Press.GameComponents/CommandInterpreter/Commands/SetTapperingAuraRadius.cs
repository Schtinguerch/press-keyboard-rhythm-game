using System;
using System.Collections.Generic;
using Press.GameComponents.Properties;

namespace Press.GameComponents
{
    public class SetTapperingAuraRadius : Command
    {
        public override string Name { get; set; } = "auraRadius";

        public override void Run(List<string> arguments, object processingObject = null) =>
            Settings.Default.TapperingAuraRadius = Convert.ToDouble(arguments[0]);
    }
}
