using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.DataTables.Data;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class VBonusDataTableAltEntry : IDataTable
    {
        public VBonusDataTableAltEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_FlagName { get; set; }
        public long FlagNameProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public long FlagValue { get; set; } // The actual VBonus Asset Name

        public long m_BonusSora1 { get; set; }
        public long Bonus1NameProperty { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public long Bonus1Value { get; set; } // The actual VBonus Asset Name

        public long m_AbilitySora1 { get; set; }
        public long Ability1NameProperty { get; set; }
        public List<byte> Unk3 { get; set; } // 9 bytes
        public long Ability1Value { get; set; } // The actual VBonus Asset Name

        public long m_BonusSora2 { get; set; }
        public long Bonus2NameProperty { get; set; }
        public List<byte> Unk4 { get; set; } // 9 bytes
        public long Bonus2Value { get; set; } // The actual VBonus Asset Name

        public long m_AbilitySora2 { get; set; }
        public long Ability2NameProperty { get; set; }
        public List<byte> Unk5 { get; set; } // 9 bytes
        public long Ability2Value { get; set; } // The actual VBonus Asset Name

        public long None { get; set; }


        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_FlagName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.FlagNameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.FlagValue = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_BonusSora1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Bonus1NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.Bonus1Value = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_AbilitySora1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Ability1NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(9);
            this.Ability1Value = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_BonusSora2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Bonus2NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(9);
            this.Bonus2Value = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_AbilitySora2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Ability2NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = reader.ReadBytesFromFileStream(9);
            this.Ability2Value = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_FlagName));
            data.AddRange(BitConverter.GetBytes(this.FlagNameProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.FlagValue));

            data.AddRange(BitConverter.GetBytes(this.m_BonusSora1));
            data.AddRange(BitConverter.GetBytes(this.Bonus1NameProperty));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.Bonus1Value));

            data.AddRange(BitConverter.GetBytes(this.m_AbilitySora1));
            data.AddRange(BitConverter.GetBytes(this.Ability1NameProperty));
            data.AddRange(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.Ability1Value));

            data.AddRange(BitConverter.GetBytes(this.m_BonusSora2));
            data.AddRange(BitConverter.GetBytes(this.Bonus2NameProperty));
            data.AddRange(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.Bonus2Value));

            data.AddRange(BitConverter.GetBytes(this.m_AbilitySora2));
            data.AddRange(BitConverter.GetBytes(this.Ability2NameProperty));
            data.AddRange(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.Ability2Value));

            data.AddRange(BitConverter.GetBytes(this.None));
            
            return data;
        }
    }
}