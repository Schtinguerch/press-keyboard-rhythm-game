using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Press.GameComponents
{
    public static class TypesExtentions
    {
        public static List<string> SubList(this List<string> baseList, int eraseIndex)
        {
            var clone = baseList.Clone();
            clone.RemoveAt(eraseIndex);
            return clone;
        }

        public static List<string> Clone(this List<string> baseList)
        { 
            var clone = new List<string>();
            foreach (var item in baseList)
                clone.Add(item);

            return clone;
        }

        public static string ConcatToString(this List<string> baseList)
        {
            if ((baseList == null) || baseList.Count == 0) return "";

            var stringValue = baseList[0];
            for (int i = 1; i < baseList.Count; i++)
                stringValue += " " + baseList[i];

            return stringValue;
        }

        public static string RemoveFirstLines(this string text, int linesCount)
        {
            var lines = Regex.Split(text, "\r\n|\r|\n").Skip(linesCount);
            return string.Join(Environment.NewLine, lines.ToArray());
        }

        public static char LastChar(this string text) =>
            text[text.Length - 1];

        public static string PreprocessedMap(this string baseMap)
        {
            var preprocessed = new TextBlock
            {
                Text = Regex.Replace(baseMap, "\n+", "\n")
            };

            var preprocessorCommands = new List<Command>()
            {
                new CommandDefine(),
            };

            var preprocessor = new CommandInterpreter(preprocessorCommands, preprocessed);
            var lines = preprocessed.Text.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
                preprocessor.Run(line);

            preprocessor.Commands.Add(new CommandInclude());
            lines = preprocessed.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
                preprocessor.Run(line);

            return preprocessed.Text;
        }

        public static List<long> ToLong(this List<string> list)
        {
            var listLong = new List<long>();

            foreach (var item in list)
                listLong.Add(Convert.ToInt64(item));

            return listLong;
        }

        public static long ToMilliseconds(this string time)
        {
            var values = time.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries).ToList().ToLong();

            var multipliers = new List<long>() { 3600000, 60000, 1000, 1 };
            var maxValues = new List<long>() { long.MaxValue, 60, 60, 1000 };

            long milliseconds = 0;

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] >= maxValues[i])
                    throw new ArgumentOutOfRangeException("Invalid time format");

                milliseconds += values[i] * multipliers[i];
            }

            return milliseconds;
        }

        public static string ToTimeFormat(this int milliseconds)
        {
            int seconds = milliseconds / 1000;
            int minutes = seconds / 60;
            seconds %= 60;
            milliseconds %= 1000;

            var time = "";
            
            int hours = minutes / 60;
            minutes %= 60;

            time += $"{hours}:";

            string
                mAddZero = (minutes < 10) ? "0" : "",
                sAddZero = (seconds < 10) ? "0" : "",
                msAddZero = (milliseconds < 10) ? "00" : (milliseconds < 100) ? "0" : "";

            time += $"{mAddZero}{minutes}:{sAddZero}{seconds}:{msAddZero}{milliseconds}";

            return time;
        }

    }
}
