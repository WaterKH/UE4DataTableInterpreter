using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class FoodItemEffectDataTableEntry : IDataTable
    {
        public FoodItemEffectDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_Comment { get; set; }
        public long StrProperty { get; set; }
        public long CommentLength { get; set; }
        public byte Unk1 { get; set; }
        public List<byte> Comment { get; set; }

        public long m_MaxHPPlus { get; set; }
        public long MaxHPPlus_IntProperty { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public int MaxHPPlusValue { get; set; }

        public long m_MaxMPPlus { get; set; }
        public long MaxMPPlus_IntProperty { get; set; }
        public List<byte> Unk3 { get; set; } // 9 bytes
        public int MaxMPPlusValue { get; set; }

        public long m_AttackPlus { get; set; }
        public long AttackPlus_IntProperty { get; set; }
        public List<byte> Unk4 { get; set; } // 9 bytes
        public int AttackPlusValue { get; set; }

        public long m_MagicPlus { get; set; }
        public long MagicPlus_IntProperty { get; set; }
        public List<byte> Unk5 { get; set; } // 9 bytes
        public int MagicPlusValue { get; set; }

        public long m_DefensePlus { get; set; }
        public long DefensePlus_IntProperty { get; set; }
        public List<byte> Unk6 { get; set; } // 9 bytes
        public int DefensePlusValue { get; set; }

        public long m_FoodItemLevel { get; set; }
        public long FoodItemLevel_IntProperty { get; set; }
        public List<byte> Unk7 { get; set; } // 9 bytes
        public int FoodItemLevelValue { get; set; }

        public long m_bPlusFoodItem { get; set; }
        public long BoolProperty { get; set; }
        public List<byte> Unk8 { get; set; } // 8 bytes
        public byte PlusFoodItemValue { get; set; }
        public byte Unk9 { get; set; }

        public long None { get; set; }


        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Comment = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.StrProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.CommentLength = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = (byte)reader.ReadByte();
            this.Comment = reader.ReadBytesFromFileStream((int)this.CommentLength);

            this.m_MaxHPPlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MaxHPPlus_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.MaxHPPlusValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MaxMPPlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MaxMPPlus_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(9);
            this.MaxMPPlusValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttackPlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttackPlus_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(9);
            this.AttackPlusValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MagicPlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MagicPlus_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = reader.ReadBytesFromFileStream(9);
            this.MagicPlusValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_DefensePlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DefensePlus_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = reader.ReadBytesFromFileStream(9);
            this.DefensePlusValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_FoodItemLevel = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.FoodItemLevel_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk7 = reader.ReadBytesFromFileStream(9);
            this.FoodItemLevelValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_bPlusFoodItem = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk8 = reader.ReadBytesFromFileStream(8);
            this.PlusFoodItemValue = (byte)reader.ReadByte();
            this.Unk9 = (byte)reader.ReadByte();

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_Comment));
            data.AddRange(BitConverter.GetBytes(this.StrProperty));
            data.AddRange(BitConverter.GetBytes(this.CommentLength));
            data.Add(this.Unk1);
            data.AddRange(this.Comment);

            data.AddRange(BitConverter.GetBytes(this.m_MaxHPPlus));
            data.AddRange(BitConverter.GetBytes(this.MaxHPPlus_IntProperty));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.MaxHPPlusValue));

            data.AddRange(BitConverter.GetBytes(this.m_MaxMPPlus));
            data.AddRange(BitConverter.GetBytes(this.MaxMPPlus_IntProperty));
            data.AddRange(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.MaxMPPlusValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttackPlus));
            data.AddRange(BitConverter.GetBytes(this.AttackPlus_IntProperty));
            data.AddRange(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.AttackPlusValue));

            data.AddRange(BitConverter.GetBytes(this.m_MagicPlus));
            data.AddRange(BitConverter.GetBytes(this.MagicPlus_IntProperty));
            data.AddRange(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.MagicPlusValue));

            data.AddRange(BitConverter.GetBytes(this.m_DefensePlus));
            data.AddRange(BitConverter.GetBytes(this.DefensePlus_IntProperty));
            data.AddRange(this.Unk6);
            data.AddRange(BitConverter.GetBytes(this.DefensePlusValue));

            data.AddRange(BitConverter.GetBytes(this.m_FoodItemLevel));
            data.AddRange(BitConverter.GetBytes(this.FoodItemLevel_IntProperty));
            data.AddRange(this.Unk7);
            data.AddRange(BitConverter.GetBytes(this.FoodItemLevelValue));

            data.AddRange(BitConverter.GetBytes(this.m_bPlusFoodItem));
            data.AddRange(BitConverter.GetBytes(this.BoolProperty));
            data.AddRange(this.Unk8);
            data.Add(this.PlusFoodItemValue);
            data.Add(this.Unk9);

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}