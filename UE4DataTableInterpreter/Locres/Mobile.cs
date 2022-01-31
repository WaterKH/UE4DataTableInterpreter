using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;
using System.Linq;
using System.Text;

namespace UE4DataTableInterpreter.DataTables
{
    public class Mobile
    {
        // Key: Name - Value: <Length, Address Start>
        public Dictionary<string, Tuple<int, int>> ReportInformation = new Dictionary<string, Tuple<int, int>>
        {
            { "Report 1", new Tuple<int, int>(680, 0x1125c) }, { "Report 2", new Tuple<int, int>(993, 0x117b0) }, { "Report 3", new Tuple<int, int>(1168, 0x11f76) }, { "Report 4", new Tuple<int, int>(1078, 0x1289a) },
            { "Report 5", new Tuple<int, int>(1453, 0x1310a) }, { "Report 6", new Tuple<int, int>(971, 0x13c68) }, { "Report 7", new Tuple<int, int>(1307, 0x14402) }, { "Report 8", new Tuple<int, int>(1218, 0x14e3c) },
            { "Report 9", new Tuple<int, int>(1537, 0x157c4) }, { "Report 10", new Tuple<int, int>(1108, 0x163ca) }, { "Report 11", new Tuple<int, int>(1140, 0x16c76) }, { "Report 12", new Tuple<int, int>(1089, 0x17562) },
            { "Report 13", new Tuple<int, int>(1290, 0x17de8) }
        };

        public Mobile() { }

        public List<byte> Process(List<string> reports)
        {
            var path = @"Content\Locres\kh3_mobile";
            using var mobile = File.OpenRead($"{path}.locres");
            using var mobileMemory = new MemoryStream();
            mobile.CopyTo(mobileMemory);

            var modifiableMobileMemory = mobileMemory.ToArray();

            foreach (var reportInfo in this.ReportInformation)
            {
                var subSetOfReports = reports.Take(4).ToList();

                if (subSetOfReports.Count == 0)
                    break;

                reports.RemoveRange(0, subSetOfReports.Count);

                var newReport = string.Join("<br><br>", subSetOfReports).PadRight(reportInfo.Value.Item1, '\0');
                var encodedReport = Encoding.Unicode.GetBytes(newReport);

                var x = 0;
                for (int i = reportInfo.Value.Item2; i < (reportInfo.Value.Item2 + encodedReport.Length); ++i)
                {
                    modifiableMobileMemory[i] = encodedReport[x];

                    x += 1;
                }
            }

            return modifiableMobileMemory.ToList();
        }
    }
}