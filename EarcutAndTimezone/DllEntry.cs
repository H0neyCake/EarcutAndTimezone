using Newtonsoft.Json.Linq;
using RGiesecke.DllExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EarcutAndTimezone;
namespace EarcutAndTimezone
{
    public class DllEntry
    {
        [DllExport("_RVExtensionVersion@8", CallingConvention = CallingConvention.Winapi)]
        public static void RvExtensionVersion(StringBuilder output, int outputSize)
        {
            output.Append("EarcutAndTimezone v1.0");
        }

        //[DllExport("RVExtension", CallingConvention = CallingConvention.Winapi)]

        [DllExport("_RVExtension@12", CallingConvention = CallingConvention.Winapi)]
        public static void RvExtension(StringBuilder output, int outputSize,
           [MarshalAs(UnmanagedType.LPStr)] string function)
        {
            switch (function)
            {
                case "hello":
                    output.Append("Hola, TimeEarcutters");
                    break;
                case "GetTimezoneName":
                    output.Append(GetTimezoneName());
                    break;
                case "GetTimezoneOffset":
                    output.Append(GetTimezoneOffset());
                    break;
            }
        }

        //[DllExport("RVExtensionArgs", CallingConvention = CallingConvention.Winapi)]

        [DllExport("_RVExtensionArgs@20", CallingConvention = CallingConvention.Winapi)]
        public static int RvExtensionArgs(StringBuilder output, int outputSize,
           [MarshalAs(UnmanagedType.LPStr)] string function,
           [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] args, int argCount)
        {
            if (function == "GetEarcut")
            {
                string fnc = args[0];
                output.Append(GetEarcut(fnc));
            }
            return 0;
        }
        public static string GetEarcut(string path)
        {
            //var path = "[[[10,0],[0,50],[60,60],[70,10]]]";

            var polylines = JArray.Parse(path);

            var data = new List<double>();
            var holeIndices = new List<int>();

            foreach (var polyline in polylines)
            {
                if (data.Any())
                {
                    holeIndices.Add(data.Count / 2);
                }

                foreach (var point in polyline)
                {
                    data.Add((double)point[0]);
                    data.Add((double)point[1]);
                }
            }
            var tg = Earcut.Tessellate(data, holeIndices);

            int[] points = tg.ToArray();
            string pointArr = "[";
            pointArr += string.Join(", ", points);
            pointArr += "]";
            Console.WriteLine(pointArr);
            return pointArr;            
        }

        public static string GetTimezoneName()
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
             string dn = localZone.DisplayName;
            return dn;
        }
        public static string GetTimezoneOffset()
        {
            string offset = (TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow)).ToString();
            return offset;
        }
    }

}
