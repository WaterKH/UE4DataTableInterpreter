using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class LevelUpDataTableEntry : IDataTable
    {
        public LevelUpDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_Exp { get; set; }
        public long Exp_IntProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public int ExpValue { get; set; }

        public long m_AttackPower { get; set; }
        public long Attack_IntProperty { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public int AttackValue { get; set; }

        public long m_MagicPower { get; set; }
        public long Magic_IntProperty { get; set; }
        public List<byte> Unk3 { get; set; } // 9 bytes
        public int MagicValue { get; set; }

        public long m_DefensePower { get; set; }
        public long Defense_IntProperty { get; set; }
        public List<byte> Unk4 { get; set; } // 9 bytes
        public int DefenseValue { get; set; }

        public long m_AbilityPoint { get; set; }
        public long Ability_IntProperty { get; set; }
        public List<byte> Unk5 { get; set; } // 9 bytes
        public int AbilityPointValue { get; set; }

        public long m_AbilityKind1 { get; set; }
        public long Ability1_IntProperty { get; set; }
        public long Unk6 { get; set; }
        public long ETresAbilityKind_1 { get; set; }
        public byte Unk7 { get; set; }
        public long AbilityValue_1 { get; set; }

        public long m_AbilityKind2 { get; set; }
        public long Ability2_IntProperty { get; set; }
        public long Unk8 { get; set; }
        public long ETresAbilityKind_2 { get; set; }
        public byte Unk9 { get; set; }
        public long AbilityValue_2 { get; set; }

        public long m_AbilityKind3 { get; set; }
        public long Ability3_IntProperty { get; set; }
        public long Unk10 { get; set; }
        public long ETresAbilityKind_3 { get; set; }
        public byte Unk11 { get; set; }
        public long AbilityValue_3 { get; set; }

        public long None { get; set; }


        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Exp = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Exp_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.ExpValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttackPower = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Attack_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.AttackValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MagicPower = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Magic_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(9);
            this.MagicValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_DefensePower = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Defense_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(9);
            this.DefenseValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AbilityPoint = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Ability_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = reader.ReadBytesFromFileStream(9);
            this.AbilityPointValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AbilityKind1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Ability1_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ETresAbilityKind_1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk7 = (byte)reader.ReadByte();
            this.AbilityValue_1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_AbilityKind2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Ability2_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk8 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ETresAbilityKind_2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk9 = (byte)reader.ReadByte();
            this.AbilityValue_2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_AbilityKind3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Ability3_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk10 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ETresAbilityKind_3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk11 = (byte)reader.ReadByte();
            this.AbilityValue_3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_Exp));
            data.AddRange(BitConverter.GetBytes(this.Exp_IntProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.ExpValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttackPower));
            data.AddRange(BitConverter.GetBytes(this.Attack_IntProperty));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.AttackValue));

            data.AddRange(BitConverter.GetBytes(this.m_MagicPower));
            data.AddRange(BitConverter.GetBytes(this.Magic_IntProperty));
            data.AddRange(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.MagicValue));

            data.AddRange(BitConverter.GetBytes(this.m_DefensePower));
            data.AddRange(BitConverter.GetBytes(this.Defense_IntProperty));
            data.AddRange(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.DefenseValue));

            data.AddRange(BitConverter.GetBytes(this.m_AbilityPoint));
            data.AddRange(BitConverter.GetBytes(this.Ability_IntProperty));
            data.AddRange(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.AbilityPointValue));

            data.AddRange(BitConverter.GetBytes(this.m_AbilityKind1));
            data.AddRange(BitConverter.GetBytes(this.Ability1_IntProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk6));
            data.AddRange(BitConverter.GetBytes(this.ETresAbilityKind_1));
            data.Add(this.Unk7);
            data.AddRange(BitConverter.GetBytes(this.AbilityValue_1));

            data.AddRange(BitConverter.GetBytes(this.m_AbilityKind2));
            data.AddRange(BitConverter.GetBytes(this.Ability2_IntProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk8));
            data.AddRange(BitConverter.GetBytes(this.ETresAbilityKind_2));
            data.Add(this.Unk9);
            data.AddRange(BitConverter.GetBytes(this.AbilityValue_2));

            data.AddRange(BitConverter.GetBytes(this.m_AbilityKind3));
            data.AddRange(BitConverter.GetBytes(this.Ability3_IntProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk10));
            data.AddRange(BitConverter.GetBytes(this.ETresAbilityKind_3));
            data.Add(this.Unk11);
            data.AddRange(BitConverter.GetBytes(this.AbilityValue_3));

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}