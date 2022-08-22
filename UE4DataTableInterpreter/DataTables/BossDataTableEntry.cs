using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.DataTables.Data;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class BossDataTableEntry : IDataTable
    {
        public BossDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_DisplayName { get; set; }
        public long StrProperty { get; set; }
        public long DisplayNameLength { get; set; }
        public byte Unk1 { get; set; }
        public List<byte> DisplayName { get; set; }

        public long m_OriginalName { get; set; }
        public long NameProperty_1 { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public long OriginalName { get; set; }

        public long m_RandomizedName { get; set; }
        public long NameProperty_2 { get; set; }
        public List<byte> Unk3 { get; set; } // 9 bytes
        public long RandomizedName { get; set; }

        public long m_SwapBack { get; set; }
        public long BoolProperty { get; set; }
        public List<byte> Unk4 { get; set; } // 8 bytes
        public byte SwapBack { get; set; }
        public byte Unk5 { get; set; }

        public long None { get; set; }

        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_DisplayName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.StrProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DisplayNameLength = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = (byte)reader.ReadByte();
            this.DisplayName = reader.ReadBytesFromFileStream((int)this.DisplayNameLength);

            this.m_OriginalName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty_1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.OriginalName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_RandomizedName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty_2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(9);
            this.RandomizedName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_SwapBack = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(8);
            this.SwapBack = (byte)reader.ReadByte();
            this.Unk5 = (byte)reader.ReadByte();

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_DisplayName));
            data.AddRange(BitConverter.GetBytes(this.StrProperty));
            data.AddRange(BitConverter.GetBytes(this.DisplayNameLength));
            data.Add(this.Unk1);
            data.AddRange(this.DisplayName);

            data.AddRange(BitConverter.GetBytes(this.m_OriginalName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty_1));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.OriginalName));

            data.AddRange(BitConverter.GetBytes(this.m_RandomizedName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty_2));
            data.AddRange(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.RandomizedName));

            data.AddRange(BitConverter.GetBytes(this.m_SwapBack));
            data.AddRange(BitConverter.GetBytes(this.BoolProperty));
            data.AddRange(this.Unk4);
            data.Add(this.SwapBack);
            data.Add(this.Unk5);

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}