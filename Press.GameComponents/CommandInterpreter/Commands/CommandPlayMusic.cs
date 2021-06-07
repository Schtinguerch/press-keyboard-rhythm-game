using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Press.GameComponents.Properties;

namespace Press.GameComponents
{
    class CommandPlayMusic : Command
    {
        public override string Name { get; set; } = "play";

        public override void Run(List<string> arguments, object processingObject = null)
        {
            SetAdditional(arguments);
            var canvas = processingObject as Canvas;

            (canvas.Children[0] as MediaElement).LoadedBehavior = MediaState.Manual;

            (canvas.Children[0] as MediaElement).Source = new Uri(UnitedStringArgument, UriKind.Absolute);
            (canvas.Children[0] as MediaElement).Volume = Settings.Default.MusicVolume;
        }
    }
}
