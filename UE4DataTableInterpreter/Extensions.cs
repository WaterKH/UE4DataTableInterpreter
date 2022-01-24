using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace UE4DataTableInterpreter
{
    public static class Extensions
    {
        public static List<byte> ReadBytesFromFileStream(this FileStream reader, int length)
        {
            if (reader.Position + length > reader.Length)
                return null;

            var data = new List<byte>();

            for (int i = 0; i < length; ++i)
            {
                var t = reader.ReadByte();
                if (t == -1)
                    return null;
                data.Add((byte)t);
            }

            return data;
        }

        //public static byte[] CreateZipArchive(this Dictionary<string, List<byte>> dataTables, string randomSeed)
        //{
        //    var zipPath = @$".\Seeds\{randomSeed}\pakchunk99-randomizer-{randomSeed}.zip";


        //    // Create the ZIP Archive
        //    Directory.CreateDirectory(@$".\Seeds\{randomSeed}");

        //    if (File.Exists(zipPath))
        //        File.Delete(zipPath);

        //    using (var zipFile = new FileStream(zipPath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite))
        //    {
        //        using var archive = new ZipArchive(zipFile, ZipArchiveMode.Update);

        //        // Create the README file
        //        var readmeEntry = archive.CreateEntry("SpoilerLog.json");
        //        using var readmeWriter = new StreamWriter(readmeEntry.Open());

        //        readmeWriter.WriteLine("TODO");

        //        // Create the entry from the file path/ name, open the data in a memory stream and copy it to the entry
        //        foreach (var (filePathAndName, data) in dataTables)
        //        {
        //            var dataTableEntry = archive.CreateEntry(filePathAndName);
        //            using var memoryStream = new MemoryStream(data.ToArray());
        //            using var stream = dataTableEntry.Open();

        //            memoryStream.CopyTo(stream);
        //        }
        //    }

        //    using var reader = new FileStream(zipPath, FileMode.Open);
        //    using var fileDataStream = new MemoryStream();
        //    reader.CopyTo(fileDataStream);
            
        //    return fileDataStream.ToArray();
        //}
    }
}