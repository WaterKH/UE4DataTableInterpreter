using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class SecretReportInfoDataTableEntry : IDataTable
    {
        public SecretReportInfoDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_ItemName { get; set; }
        public long NameProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public long ItemName { get; set; }


        public long m_ReportText { get; set; }
        public long StrProperty { get; set; }
        public List<byte> Unk2 { get; set; }
        public int ReportTextLength { get; set; }
        public List<byte> ReportText { get; set; }

        public long None { get; set; }

        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_ItemName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.ItemName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_ReportText = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.StrProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.ReportTextLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.ReportText = reader.ReadBytesFromFileStream((int)this.ReportTextLength);

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_ItemName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.ItemName));

            data.AddRange(BitConverter.GetBytes(this.m_ReportText));
            data.AddRange(BitConverter.GetBytes(this.StrProperty));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.ReportTextLength));
            data.AddRange(ReportText);

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}