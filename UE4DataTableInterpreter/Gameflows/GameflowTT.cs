using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.Gameflows
{
    public class GameflowTT : IDataTable
    {
        public GameflowTT() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_KeyName { get; set; }
        public long NameProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public long KeyName { get; set; }

        public long m_Comment{ get; set; }
        public long StrProperty { get; set; }
        public long CommentLength { get; set; }
        public byte Unk2 { get; set; }
        public List<byte> Comment { get; set; }

        public long m_AP { get; set; }
        public long AP_IntProperty { get; set; }
        public List<byte> Unk3 { get; set; } // 9 bytes
        public int APValue { get; set; }
        
        public long m_AttackPlus { get; set; }
        public long Attack_IntProperty { get; set; }
        public List<byte> Unk4 { get; set; } // 9 bytes
        public int AttackValue { get; set; }

        public long m_MagicPlus { get; set; }
        public long Magic_IntProperty { get; set; }
        public List<byte> Unk5 { get; set; } // 9 bytes
        public int MagicValue { get; set; }

        public long m_DefensePlus { get; set; }
        public long Defense_IntProperty { get; set; }
        public List<byte> Unk6 { get; set; } // 9 bytes
        public int DefenseValue { get; set; }

        public long m_AttrResistPhysical { get; set; }
        public long PhysicalResist_IntProperty { get; set; }
        public List<byte> Unk7 { get; set; } // 9 bytes
        public int PhysicalResistValue { get; set; }

        public long m_AttrResistFire { get; set; }
        public long FireResist_IntProperty { get; set; }
        public List<byte> Unk8 { get; set; } // 9 bytes
        public int FireResistValue { get; set; }

        public long m_AttrResistBlizzard { get; set; }
        public long BlizzardResist_IntProperty { get; set; }
        public List<byte> Unk9 { get; set; } // 9 bytes
        public int BlizzardResistValue { get; set; }

        public long m_AttrResistThunder { get; set; }
        public long ThunderResist_IntProperty { get; set; }
        public List<byte> Unk10 { get; set; } // 9 bytes
        public int ThunderResistValue { get; set; }

        public long m_AttrResistWater { get; set; }
        public long WaterResist_IntProperty { get; set; }
        public List<byte> Unk11 { get; set; } // 9 bytes
        public int WaterResistValue { get; set; }

        public long m_AttrResistAero { get; set; }
        public long AeroResist_IntProperty { get; set; }
        public List<byte> Unk12 { get; set; } // 9 bytes
        public int AeroResistValue { get; set; }

        public long m_AttrResistDark { get; set; }
        public long DarkResist_IntProperty { get; set; }
        public List<byte> Unk13 { get; set; } // 9 bytes
        public int DarkResistValue { get; set; }

        public long m_AttrResistNoType { get; set; }
        public long NoTypeResist_IntProperty { get; set; }
        public List<byte> Unk14 { get; set; } // 9 bytes
        public int NoTypeResistValue { get; set; }

        public long m_AppendAbility { get; set; }
        public long ArrayProperty { get; set; }
        public long Unk15 { get; set; }
        public long EnumProperty { get; set; }
        public byte Unk16 { get; set; }
        public int AbilityCount { get; set; }
        public List<long> Abilities { get; set; } = new List<long>();

        public long m_EquipSubclass { get; set; }
        public long AssetObjectProperty { get; set; }
        public List<byte> Unk17 { get; set; } // 9 bytes
        public int EquipSubClassLength { get; set; }
        public List<byte> EquipSubClass { get; set; }

        public long None { get; set; }

        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_KeyName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.KeyName = BitConverter.ToInt32(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Comment = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.StrProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.CommentLength = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = (byte)reader.ReadByte();
            this.Comment = reader.ReadBytesFromFileStream((int)this.CommentLength);

            this.m_AP = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AP_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(9);
            this.APValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttackPlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Attack_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(9);
            this.AttackValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MagicPlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Magic_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = reader.ReadBytesFromFileStream(9);
            this.MagicValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_DefensePlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Defense_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = reader.ReadBytesFromFileStream(9);
            this.DefenseValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistPhysical = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.PhysicalResist_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk7 = reader.ReadBytesFromFileStream(9);
            this.PhysicalResistValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistFire = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.FireResist_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk8 = reader.ReadBytesFromFileStream(9);
            this.FireResistValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistBlizzard = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BlizzardResist_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk9 = reader.ReadBytesFromFileStream(9);
            this.BlizzardResistValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistThunder = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ThunderResist_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk10 = reader.ReadBytesFromFileStream(9);
            this.PhysicalResistValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistWater = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.WaterResist_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk11 = reader.ReadBytesFromFileStream(9);
            this.WaterResistValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistAero = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AeroResist_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk12 = reader.ReadBytesFromFileStream(9);
            this.AeroResistValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistDark = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DarkResist_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk13 = reader.ReadBytesFromFileStream(9);
            this.DarkResistValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistNoType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NoTypeResist_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk14 = reader.ReadBytesFromFileStream(9);
            this.NoTypeResistValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AppendAbility = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ArrayProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk15 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk16 = (byte)reader.ReadByte();
            this.AbilityCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            for (int i = 0; i < this.AbilityCount; ++i)
            {
                this.Abilities.Add(BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()));
            }

            this.m_EquipSubclass = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AssetObjectProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk17 = reader.ReadBytesFromFileStream(9);
            this.EquipSubClassLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.EquipSubClass = reader.ReadBytesFromFileStream(this.EquipSubClassLength);

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_KeyName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.KeyName));

            data.AddRange(BitConverter.GetBytes(this.m_Comment));
            data.AddRange(BitConverter.GetBytes(this.StrProperty));
            data.AddRange(BitConverter.GetBytes(this.CommentLength));
            data.Add(this.Unk2);
            data.AddRange(this.Comment);

            data.AddRange(BitConverter.GetBytes(this.m_AP));
            data.AddRange(BitConverter.GetBytes(this.AP_IntProperty));
            data.AddRange(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.APValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttackPlus));
            data.AddRange(BitConverter.GetBytes(this.Attack_IntProperty));
            data.AddRange(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.AttackValue));

            data.AddRange(BitConverter.GetBytes(this.m_MagicPlus));
            data.AddRange(BitConverter.GetBytes(this.Magic_IntProperty));
            data.AddRange(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.MagicValue));

            data.AddRange(BitConverter.GetBytes(this.m_DefensePlus));
            data.AddRange(BitConverter.GetBytes(this.Defense_IntProperty));
            data.AddRange(this.Unk6);
            data.AddRange(BitConverter.GetBytes(this.DefenseValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistPhysical));
            data.AddRange(BitConverter.GetBytes(this.PhysicalResist_IntProperty));
            data.AddRange(this.Unk7);
            data.AddRange(BitConverter.GetBytes(this.PhysicalResistValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistFire));
            data.AddRange(BitConverter.GetBytes(this.FireResist_IntProperty));
            data.AddRange(this.Unk8);
            data.AddRange(BitConverter.GetBytes(this.FireResistValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistBlizzard));
            data.AddRange(BitConverter.GetBytes(this.BlizzardResist_IntProperty));
            data.AddRange(this.Unk9);
            data.AddRange(BitConverter.GetBytes(this.BlizzardResistValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistThunder));
            data.AddRange(BitConverter.GetBytes(this.ThunderResist_IntProperty));
            data.AddRange(this.Unk10);
            data.AddRange(BitConverter.GetBytes(this.ThunderResistValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistWater));
            data.AddRange(BitConverter.GetBytes(this.WaterResist_IntProperty));
            data.AddRange(this.Unk11);
            data.AddRange(BitConverter.GetBytes(this.WaterResistValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistAero));
            data.AddRange(BitConverter.GetBytes(this.AeroResist_IntProperty));
            data.AddRange(this.Unk12);
            data.AddRange(BitConverter.GetBytes(this.AeroResistValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistDark));
            data.AddRange(BitConverter.GetBytes(this.DarkResist_IntProperty));
            data.AddRange(this.Unk13);
            data.AddRange(BitConverter.GetBytes(this.DarkResistValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistNoType));
            data.AddRange(BitConverter.GetBytes(this.NoTypeResist_IntProperty));
            data.AddRange(this.Unk14);
            data.AddRange(BitConverter.GetBytes(this.NoTypeResistValue));

            data.AddRange(BitConverter.GetBytes(this.m_AppendAbility));
            data.AddRange(BitConverter.GetBytes(this.ArrayProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk15));
            data.AddRange(BitConverter.GetBytes(this.EnumProperty));
            data.Add(this.Unk16);
            data.AddRange(BitConverter.GetBytes(this.AbilityCount));

            foreach (var ability in this.Abilities)
                data.AddRange(BitConverter.GetBytes(ability));

            data.AddRange(BitConverter.GetBytes(this.m_EquipSubclass));
            data.AddRange(BitConverter.GetBytes(this.AssetObjectProperty));
            data.AddRange(this.Unk17);
            data.AddRange(BitConverter.GetBytes(this.EquipSubClassLength));
            data.AddRange(this.EquipSubClass);

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}