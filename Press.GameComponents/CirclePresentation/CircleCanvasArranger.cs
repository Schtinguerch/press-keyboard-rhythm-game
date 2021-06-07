using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using Press.GameComponents.Controls.TappingCircles;
using Press.GameComponents.Properties;

namespace Press.GameComponents
{
    public class CircleCanvasArranger
    {
        private Canvas _canvas;

        private Queue<TappingCircle> _circles = new Queue<TappingCircle>();

        public CircleCanvasArranger(Canvas canvas) =>
            _canvas = canvas;

        public void DrawCircle(double relX, double relY, char key)
        {
            var circle = new TappingCircle();
            circle.CircleKey = key;

            Canvas.SetLeft(circle, _canvas.ActualWidth * relX / 100d - 600d);
            Canvas.SetTop(circle, _canvas.ActualHeight * relY / 100d - 600d);

            _canvas.Children.Add(circle);
            _circles.Enqueue(circle);

            var lifeTimer = new DispatcherTimer();

            lifeTimer.Interval = TimeSpan.FromMilliseconds(Settings.Default.ShowCircleTime * 8d / 6d);
            lifeTimer.Tick += (s, e) =>
            {
                 DequeueCircle();
                 lifeTimer.Stop();
            };

            lifeTimer.Start();
        }

        public void HighlightCorrectClick()
        {
            _circles.Peek().MainCircle.Fill = new SolidColorBrush(Colors.GreenYellow);
            _circles.Peek().TapperingCircle.Fill = new SolidColorBrush(Colors.GreenYellow);
            (_circles.Peek().TapperingCircle.Effect as DropShadowEffect).Color = Colors.GreenYellow;
        }

        private void DequeueCircle()
        {
            if (_circles.Count == 0) return;

            _circles.Dequeue();
            if (_circles.Count == 0)
            {
                var mediaElement = _canvas.Children[0];
                var textTextBox = _canvas.Children[1];
                
                _canvas.Children.Clear();
                GC.Collect();

                _canvas.Children.Add(mediaElement);
                _canvas.Children.Add(textTextBox);
            }
        }
    }
}
