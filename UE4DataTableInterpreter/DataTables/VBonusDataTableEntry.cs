using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.DataTables.Data;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class VBonusDataTableEntry : IDataTable
    {
        public VBonusDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_FlagName { get; set; }
        public long NameProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public long FlagValue { get; set; } // The actual VBonus Asset Name

        public long m_Comment { get; set; }
        public long StrProperty { get; set; }
        public long CommentLength { get; set; }
        public byte Unk2 { get; set; }
        public List<byte> Comment { get; set; }

        public long m_InitAbilityEquipOnCriticalMode { get; set; }
        public long EnumProperty { get; set; }
        public long Unk3 { get; set; }
        public long AbilityType { get; set; }
        public byte Unk4 { get; set; }
        public long ETresVictoryAbilityType { get; set; }

        public VBonusRow m_Sora1 { get; set; }
        public VBonusRow m_Sora2 { get; set; }
        public VBonusRow m_DONALD1 { get; set; }
        public VBonusRow m_GOOFY1 { get; set; }
        public VBonusRow m_HERCULES1 { get; set; }
        public VBonusRow m_WOODY1 { get; set; }
        public VBonusRow m_BUZZ1 { get; set; }
        public VBonusRow m_RAPUNZEL1 { get; set; }
        public VBonusRow m_FLYNN1 { get; set; }
        public VBonusRow m_SULLEY1 { get; set; }
        public VBonusRow m_MIKE1 { get; set; }
        public VBonusRow m_MARSHMALLOW1 { get; set; }
        public VBonusRow m_BAYMAX1 { get; set; }
        public VBonusRow m_JACK_SPARROW1 { get; set; }
        public long None { get; set; }


        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_FlagName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.FlagValue = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Comment = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.StrProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.CommentLength = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = (byte)reader.ReadByte();
            this.Comment = reader.ReadBytesFromFileStream((int)this.CommentLength);

            this.m_InitAbilityEquipOnCriticalMode = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AbilityType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = (byte)reader.ReadByte();
            this.ETresVictoryAbilityType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Sora1 = new VBonusRow().Decompile(reader);
            this.m_Sora2 = new VBonusRow().Decompile(reader);
            
            this.m_DONALD1 = new VBonusRow().Decompile(reader);
            this.m_GOOFY1 = new VBonusRow().Decompile(reader);

            this.m_HERCULES1 = new VBonusRow().Decompile(reader);
            this.m_WOODY1 = new VBonusRow().Decompile(reader);
            this.m_BUZZ1 = new VBonusRow().Decompile(reader);
            this.m_RAPUNZEL1 = new VBonusRow().Decompile(reader);
            this.m_FLYNN1 = new VBonusRow().Decompile(reader);
            this.m_SULLEY1 = new VBonusRow().Decompile(reader);
            this.m_MIKE1 = new VBonusRow().Decompile(reader);
            this.m_MARSHMALLOW1 = new VBonusRow().Decompile(reader);
            this.m_BAYMAX1 = new VBonusRow().Decompile(reader);
            this.m_JACK_SPARROW1 = new VBonusRow().Decompile(reader);
            
            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_FlagName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.FlagValue));

            data.AddRange(BitConverter.GetBytes(this.m_Comment));
            data.AddRange(BitConverter.GetBytes(this.StrProperty));
            data.AddRange(BitConverter.GetBytes(this.CommentLength));
            data.Add(this.Unk2);
            data.AddRange(this.Comment);

            data.AddRange(BitConverter.GetBytes(this.m_InitAbilityEquipOnCriticalMode));
            data.AddRange(BitConverter.GetBytes(this.EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.AbilityType));
            data.Add(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.ETresVictoryAbilityType));

            data.AddRange(this.m_Sora1.Recompile());
            data.AddRange(this.m_Sora2.Recompile());
            
            data.AddRange(this.m_DONALD1.Recompile());
            data.AddRange(this.m_GOOFY1.Recompile());

            data.AddRange(this.m_HERCULES1.Recompile());
            data.AddRange(this.m_WOODY1.Recompile()); 
            data.AddRange(this.m_BUZZ1.Recompile());
            data.AddRange(this.m_RAPUNZEL1.Recompile()); 
            data.AddRange(this.m_FLYNN1.Recompile());
            data.AddRange(this.m_SULLEY1.Recompile()); 
            data.AddRange(this.m_MIKE1.Recompile());
            data.AddRange(this.m_MARSHMALLOW1.Recompile());
            data.AddRange(this.m_BAYMAX1.Recompile());
            data.AddRange(this.m_JACK_SPARROW1.Recompile());

            data.AddRange(BitConverter.GetBytes(this.None));
            
            return data;
        }
    }
}