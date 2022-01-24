using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class TreasureDataTableEntry : IDataTable
    {
        public TreasureDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_TreasureName { get; set; }
        public long NameProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public long Treasure { get; set; } // The actual Treasure Asset Name

        public long m_WorldAreaCode { get; set; }
        public long ByteProperty { get; set; }
        public long Unk2 { get; set; }
        public long ETresWorldAreaCode { get; set; }
        public byte Unk3 { get; set; }
        public long WorldId { get; set; }

        public long m_bUnused { get; set; }
        public long BoolProperty { get; set; }
        public long Unk4 { get; set; }
        public byte IsUnused { get; set; } // True or False
        public byte Unk5 { get; set; }

        public long m_UIPriority { get; set; }
        public long IntProperty { get; set; }
        public List<byte> Unk6 { get; set; } // 9 bytes
        public int Priority { get; set; }

        public long m_Comment { get; set; }
        public long StrProperty { get; set; }
        public long CommentLength { get; set; }
        public List<byte> Unk7 { get; set; } // 5 bytes
        public List<byte> Comment { get; set; }
        public int Unk8{ get; set; }

        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_TreasureName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.Treasure = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_WorldAreaCode = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ByteProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ETresWorldAreaCode = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = (byte)reader.ReadByte();
            this.WorldId = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_bUnused = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.IsUnused = (byte)reader.ReadByte();
            this.Unk5 = (byte)reader.ReadByte();

            this.m_UIPriority = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = reader.ReadBytesFromFileStream(9);
            this.Priority = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Comment = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.StrProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.CommentLength = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk7 = reader.ReadBytesFromFileStream(5);
            this.Comment = reader.ReadBytesFromFileStream((int)this.CommentLength);
            this.Unk8 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_TreasureName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.Treasure));

            data.AddRange(BitConverter.GetBytes(this.m_WorldAreaCode));
            data.AddRange(BitConverter.GetBytes(this.ByteProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.AddRange(BitConverter.GetBytes(this.ETresWorldAreaCode));
            data.Add(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.WorldId));

            data.AddRange(BitConverter.GetBytes(this.m_bUnused));
            data.AddRange(BitConverter.GetBytes(this.BoolProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk4));
            data.Add(this.IsUnused);
            data.Add(this.Unk5);

            data.AddRange(BitConverter.GetBytes(this.m_UIPriority));
            data.AddRange(BitConverter.GetBytes(this.IntProperty));
            data.AddRange(this.Unk6);
            data.AddRange(BitConverter.GetBytes(this.Priority));

            data.AddRange(BitConverter.GetBytes(this.m_Comment));
            data.AddRange(BitConverter.GetBytes(this.StrProperty));
            data.AddRange(BitConverter.GetBytes(this.CommentLength));
            data.AddRange(this.Unk7);
            data.AddRange(Comment);
            data.AddRange(BitConverter.GetBytes(this.Unk8));

            return data;
        }
    }
}