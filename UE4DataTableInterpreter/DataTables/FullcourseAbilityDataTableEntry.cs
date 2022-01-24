using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class FullcourseAbilityDataTableEntry : IDataTable
    {
        public FullcourseAbilityDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_AbilityPlus { get; set; }
        public long EnumProperty { get; set; }
        public long ETresAbilityKind { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public long Ability { get; set; }

        public long m_Level{ get; set; }
        public long IntProperty { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public int Level { get; set; }

        public long None { get; set; }

        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_AbilityPlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ETresAbilityKind = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.Ability = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Level = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.Level = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_AbilityPlus));
            data.AddRange(BitConverter.GetBytes(this.EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.ETresAbilityKind));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.Ability));

            data.AddRange(BitConverter.GetBytes(this.m_Level));
            data.AddRange(BitConverter.GetBytes(this.IntProperty));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.Level));

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}