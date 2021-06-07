using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Documents;
using Press.GameComponents;

namespace TappingRecorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool isRecording = false;

        private Stopwatch _stopwatch;

        private void StartRecordingButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!isRecording)
            {
                isRecording = true;
                StartRecordingButton.Content = "Stop recording";

                _stopwatch = Stopwatch.StartNew();
            }
            else
            {
                isRecording = false;
                StartRecordingButton.Content = "Start recording";

                _stopwatch.Stop();
            }
        }

        private double m = 0;
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!isRecording) return;

            m++;
            var polarAngle = Math.PI * 1.5 + Math.PI * 2 / 20 * m;
            var x = Math.Cos(polarAngle) * 25 + 50;
            var y = Math.Sin(polarAngle) * 25 + 50;

            int milliseconds = (int)_stopwatch.ElapsedMilliseconds;
            RecordingTextBox.Text +=
                milliseconds.ToTimeFormat() + " - " + ToDvorak(e.Key.ToString()) + " " + x.ToString("N") + ";" + y.ToString("N") + "\n";
        }

        private void ClearDataButton_OnClick(object sender, RoutedEventArgs e)
        {
            RecordingTextBox.Text = "";
        }

        private List<string> QWERTYkeys = new List<string>()
        {
            "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P",
            "A", "S", "D", "F", "G", "H", "J", "K", "L", "Oem1", "OemQuotes",
            "Z", "X", "C", "V", "B", "N", "M", "OemComma", "OemPeriod", "OemQuestion"
        };

        private List<string> SchtDvorakKeys = new List<string>()
        {
            ";", ",", ".", "P", "Y", "F", "G", "C", "R", "K",
            "A", "O", "E", "I", "U", "D", "H", "T", "N", "S", "Z",
            "'", "X", "Q", "V", "J", "L", "M", "W", "B", "?"
        };

        private string ToDvorak(string QWERTY)
        {
            for (int i = 0; i < QWERTYkeys.Count; i++)
                if (QWERTY == QWERTYkeys[i])
                    return SchtDvorakKeys[i];

            return "0";
        }
    }
}
