using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Press.GameComponents.Properties;

namespace Press.GameComponents
{
    public class GameScene
    {
        public string SourceData { get; set; }

        private Canvas _canvas;

        private long _nextClickTime;

        private CircleCanvasArranger _canvasArranger;

        private Queue<string> _actions;

        private Queue<long> _clickTimes;

        private Stopwatch _timeStopWatch;

        private DispatcherTimer _timeTracker;

        private CommandInterpreter _interpreter;

        private Queue<string> GetActionsFromSourceData()
        {
            var actions = new Queue<string>();
            var actionsArray = SourceData.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var action in actionsArray)
                actions.Enqueue(action);

            return actions;
        }

        private void TrackerOnTick(object sender, EventArgs e)
        {
            if (_actions.Count == 0)
            {
                _timeStopWatch.Stop();
                _timeTracker.Stop();

                MessageBox.Show("The map is over");
                return;
            }

            if (_clickTimes.Count > 0)
            {
                if (_timeStopWatch.ElapsedMilliseconds - _clickTimes.Peek() > Settings.Default.ShowCircleTime / 6 + 100)
                    _clickTimes.Dequeue();
            }

            var command = _actions.Peek();
            if (command.Length < 3)
            {
                _actions.Dequeue();
                return;
            }

            if (!command.Contains(" - "))
            {
                _interpreter.Run(command);

                if (command.Contains("play"))
                {
                    (_canvas.Children[0] as MediaElement).Play();
                    _timeStopWatch = Stopwatch.StartNew();
                }

                _actions.Dequeue();
            }
            
            else
            {
                var tokens = command.Split(new[] {" - "}, StringSplitOptions.RemoveEmptyEntries);
                _nextClickTime = tokens[0].ToMilliseconds();

                if (_timeStopWatch.ElapsedMilliseconds >= (_nextClickTime - 1 * Settings.Default.ShowCircleTime))
                {
                    var circleData = tokens[1].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var key = circleData[0][0];
                    var point = circleData[1].Split(new[] {';'});

                    _clickTimes.Enqueue(_nextClickTime);
                    _canvasArranger.DrawCircle(Convert.ToDouble(point[0]), Convert.ToDouble(point[1]), key);
                    _actions.Dequeue();
                }
            }
        }

        public GameScene(Canvas canvas, string fileName)
        {
            if (!File.Exists(fileName))
                throw new ArgumentException("The file does not exist");

            if (canvas == null)
                throw new NullReferenceException("Null canvas");

            SourceData = File.ReadAllText(fileName).PreprocessedMap();
            _actions = GetActionsFromSourceData();
            _clickTimes = new Queue<long>();

            _canvas = canvas;
            _canvasArranger = new CircleCanvasArranger(_canvas);

            _interpreter = new CommandInterpreter(new List<Command>()
            {
                new CommandSet(),
                new CommandPlayMusic(),
            }, _canvas);

            _timeTracker = new DispatcherTimer();
            _timeTracker.Interval = TimeSpan.FromMilliseconds(1);

            _timeTracker.Tick += TrackerOnTick;
            _timeTracker.Start();
        }

        public string PointsFromTap(KeyEventArgs e)
        {
            if (_clickTimes.Count == 0) return "Empty queue";
            
            var tapTime = _timeStopWatch.ElapsedMilliseconds;
            var deltaTime = Math.Abs(tapTime - _clickTimes.Peek());
            
            var stringData = $"tap-time: {tapTime};  time: {_clickTimes.Peek()} => {deltaTime}";

            if (deltaTime < Settings.Default.ShowCircleTime / 10)
            {
                _clickTimes.Dequeue();
                stringData += " => 100 PTS";
                _canvasArranger.HighlightCorrectClick();
            }

            return stringData;
        }
    }
}
