using System;
using System.Windows;
using System.Windows.Controls;
using Press.GameComponents.Properties;

namespace Press.GameComponents.Controls.TappingCircles
{
    public partial class TappingCircle : UserControl
    {
        public TappingCircle()
        {
            InitializeComponent();

            var showTimeSpan = TimeSpan.FromMilliseconds(Settings.Default.ShowCircleTime);
            var showDuration = new Duration(showTimeSpan);
            TapperingHeightAnimation.Duration = TapperingWidthAnimation.Duration = showDuration;

            CircleHideAnimation.BeginTime =
                MainCircleHideAnimation.BeginTime = TextHideAnimation.BeginTime = showTimeSpan;

            var hideDuration = new Duration(TimeSpan.FromMilliseconds(Settings.Default.ShowCircleTime / 6d));

            CircleHideAnimation.Duration = MainCircleHideAnimation.Duration = TextHideAnimation.Duration =
                MainCircleShowAnimation.Duration = TapperingCircleShowAnimation.Duration = hideDuration;

            var cirleDiameter = Settings.Default.CircleRadius * 2d;
            var auraDiameter = Settings.Default.TapperingAuraRadius * 2d;

            TapperingWidthAnimation.From = TapperingHeightAnimation.From = auraDiameter;

            MainCircle.Width = MainCircle.Height = cirleDiameter;
            TapperingWidthAnimation.To = TapperingHeightAnimation.To = cirleDiameter;

            CircleText.FontSize = cirleDiameter / 2d;
        }

        public char CircleKey
        {
            get => CircleText.Text[0];
            set => CircleText.Text = value.ToString();
        }
    }
}
