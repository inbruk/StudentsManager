using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsManager.BusinessLogicLayer
{
    public static class StringTools
    {
        public static String GetPartBetweenDefinedStringMarkers(String source, String begMarker, String endMarker)
        {
            int begMarkerLength = begMarker.Length;
            int endMarkerLength = endMarker.Length;

            int beginIndex = source.IndexOf(begMarker) + begMarkerLength;

            String result = source.Remove(0, beginIndex);

            int endIndex = result.IndexOf(endMarker);

            result = result.Remove(endIndex);

            return result;
        }

        public static String SetPartBetweenDefinedStringMarkers(String source, String begMarker, String endMarker, String value)
        {
            StringBuilder resultSB = new StringBuilder();

            int begMarkerLength = begMarker.Length;
            int endMarkerLength = endMarker.Length;
            int beginIndex = source.IndexOf(begMarker) + begMarkerLength;

            String tempStr = source.Remove(beginIndex + endMarkerLength - 1);

            resultSB.Append(tempStr);
            resultSB.Append(value);

            String endPartStr = source.Remove(0, beginIndex + endMarkerLength - 1);

            int endIndex = endPartStr.IndexOf(endMarker);

            tempStr = endPartStr.Remove(0, endIndex);
            resultSB.Append(tempStr);

            String result = resultSB.ToString();

            return result;
        }
    }
}
