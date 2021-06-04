using System;
using System.Collections.Generic;

namespace Press.GameComponents
{
    public static class TypesExtentions
    {
        public static List<string> SubList(this List<string> baseList, int eraseIndex)
        {
            baseList.RemoveAt(eraseIndex);
            return baseList;
        }

        public static string ConcatToString(this List<string> baseList)
        {
            if ((baseList == null) || baseList.Count == 0) return "";

            var stringValue = baseList[0];
            for (int i = 1; i < baseList.Count; i++)
                stringValue += " " + baseList[i];

            return stringValue;
        }
    }
}
