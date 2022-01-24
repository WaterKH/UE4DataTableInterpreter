using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class LevelUpDataTableAltEntry : IDataTable
    {
        public LevelUpDataTableAltEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_Ability1 { get; set; }
        public long Ability1NameProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public int Ability1Length { get;}
        public long Ability1Value { get; set; } // The actual VBonus Asset Name

        public long m_Ability2 { get; set; }
        public long Ability2NameProperty { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public long Ability2Value { get; set; } // The actual VBonus Asset Name

        public long m_Ability3 { get; set; }
        public long Ability3NameProperty { get; set; }
        public List<byte> Unk3 { get; set; } // 9 bytes
        public long Ability3Value { get; set; } // The actual VBonus Asset Name

        public long None { get; set; }


        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Ability1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Ability1NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.Ability1Value = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Ability2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Ability2NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.Ability2Value = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Ability3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Ability3NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(9);
            this.Ability3Value = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_Ability1));
            data.AddRange(BitConverter.GetBytes(this.Ability1NameProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.Ability1Value));

            data.AddRange(BitConverter.GetBytes(this.m_Ability2));
            data.AddRange(BitConverter.GetBytes(this.Ability2NameProperty));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.Ability2Value));

            data.AddRange(BitConverter.GetBytes(this.m_Ability3));
            data.AddRange(BitConverter.GetBytes(this.Ability3NameProperty));
            data.AddRange(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.Ability3Value));

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}