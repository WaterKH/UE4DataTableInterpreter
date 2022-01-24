using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UE4DataTableInterpreter.DataTables.Data
{
    public class ChrInitRow
    {
        public ChrInitRow() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long StructProperty { get; set; }
        public long Unk1 { get; set; }
        public long TresChrInitEquip { get; set; }
        public long Unk2 { get; set; }
        public List<byte> Unk3 { get; set; } // 9 bytes

        public long m_Weapons { get; set; }
        public long Weapons_ArrayProperty { get; set; }
        public long Unk4 { get; set; }
        public long Weapons_EnumProperty { get; set; }
        public byte Unk5 { get; set; }
        public int WeaponCount { get; set; }
        public List<long> Weapons { get; set; } = new List<long>();

        public long m_EquipAbility { get; set; }
        public long EquipAbility_ArrayProperty { get; set; }
        public long Unk6 { get; set; }
        public long EquipAbility_EnumProperty { get; set; }
        public byte Unk7 { get; set; }
        public int EquipAbilityCount { get; set; }
        public List<long> EquipAbilities { get; set; } = new List<long>();

        public long m_HaveAbility { get; set; }
        public long HaveAbility_ArrayProperty { get; set; }
        public long Unk8 { get; set; }
        public long HaveAbility_EnumProperty { get; set; }
        public byte Unk9 { get; set; }
        public int HaveAbilityCount { get; set; }
        public List<long> HaveAbilities { get; set; } = new List<long>();

        public long m_CriticalEquipAbility { get; set; }
        public long CritEquip_ArrayProperty { get; set; }
        public long Unk10 { get; set; }
        public long CritEquip_EnumProperty { get; set; }
        public byte Unk11 { get; set; }
        public int CritEquipCount { get; set; }
        public List<long> CritEquipAbilities { get; set; } = new List<long>();

        public long m_CriticalHaveAbility { get; set; }
        public long CritHave_ArrayProperty { get; set; }
        public long Unk12 { get; set; }
        public long CritHave_EnumProperty { get; set; }
        public byte Unk13 { get; set; }
        public int CritHaveCount { get; set; }
        public List<long> CritHaveAbilities { get; set; } = new List<long>();

        public long m_BaseParamData { get; set; }
        public long BaseParam_ObjectProperty { get; set; }
        public List<byte> Unk14 { get; set; } // 9 bytes
        public uint BaseParamData { get; set; }

        public long m_CriticalUseAPUpNum { get; set; }
        public long IntProperty { get; set; }
        public List<byte> Unk15 { get; set; } // 9 bytes
        public uint CriticalUseApUpNum { get; set; }

        public long m_LevelData { get; set; }
        public long Level_ObjectProperty { get; set; }
        public List<byte> Unk16 { get; set; } // 9 bytes
        public uint LevelData { get; set; }

        public long m_FormAbilityAsset { get; set; }
        public long FormAbilityAsset_ObjectProperty { get; set; }
        public List<byte> Unk17 { get; set; } // 9 bytes
        public uint FormAbilityAsset { get; set; }

        public long None { get; set; }

        public ChrInitRow Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.StructProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.TresChrInitEquip = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(9);

            this.m_Weapons = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Weapons_ArrayProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Weapons_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = (byte)reader.ReadByte();
            this.WeaponCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            for (int i = 0; i < this.WeaponCount; ++i)
                this.Weapons.Add(BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()));

            this.m_EquipAbility = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.EquipAbility_ArrayProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.EquipAbility_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk7 = (byte)reader.ReadByte();
            this.EquipAbilityCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            for (int i = 0; i < this.EquipAbilityCount; ++i)
                this.EquipAbilities.Add(BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()));

            this.m_HaveAbility = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.HaveAbility_ArrayProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk8 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.HaveAbility_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk9 = (byte)reader.ReadByte();
            this.HaveAbilityCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            for (int i = 0; i < this.HaveAbilityCount; ++i)
                this.HaveAbilities.Add(BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()));

            this.m_CriticalEquipAbility = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.CritEquip_ArrayProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk10 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.CritEquip_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk11 = (byte)reader.ReadByte();
            this.CritEquipCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            for (int i = 0; i < this.CritEquipCount; ++i)
                this.CritEquipAbilities.Add(BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()));

            this.m_CriticalHaveAbility = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.CritHave_ArrayProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk12 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.CritHave_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk13 = (byte)reader.ReadByte();
            this.CritHaveCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            for (int i = 0; i < this.CritHaveCount; ++i)
                this.CritHaveAbilities.Add(BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()));

            this.m_BaseParamData = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BaseParam_ObjectProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk14 = reader.ReadBytesFromFileStream(9);
            this.BaseParamData = BitConverter.ToUInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_CriticalUseAPUpNum = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk15 = reader.ReadBytesFromFileStream(9);
            this.CriticalUseApUpNum = BitConverter.ToUInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_LevelData = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Level_ObjectProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk16 = reader.ReadBytesFromFileStream(9);
            this.LevelData = BitConverter.ToUInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_FormAbilityAsset = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.FormAbilityAsset_ObjectProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk17 = reader.ReadBytesFromFileStream(9);
            this.FormAbilityAsset = BitConverter.ToUInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            //this.RestOfData = reader.ReadBytesFromFileStream((int)(reader.Length - reader.Position) - 4);

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));
            data.AddRange(BitConverter.GetBytes(this.StructProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk1));
            data.AddRange(BitConverter.GetBytes(this.TresChrInitEquip));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.AddRange(this.Unk3);

            data.AddRange(BitConverter.GetBytes(this.m_Weapons));
            data.AddRange(BitConverter.GetBytes(this.Weapons_ArrayProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk4));
            data.AddRange(BitConverter.GetBytes(this.Weapons_EnumProperty));
            data.Add(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.WeaponCount));

            foreach (var weapon in this.Weapons)
                data.AddRange(BitConverter.GetBytes(weapon));

            data.AddRange(BitConverter.GetBytes(this.m_EquipAbility));
            data.AddRange(BitConverter.GetBytes(this.EquipAbility_ArrayProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk6));
            data.AddRange(BitConverter.GetBytes(this.EquipAbility_EnumProperty));
            data.Add(this.Unk7);
            data.AddRange(BitConverter.GetBytes(this.EquipAbilityCount));

            foreach (var ability in this.EquipAbilities)
                data.AddRange(BitConverter.GetBytes(ability));

            data.AddRange(BitConverter.GetBytes(this.m_HaveAbility));
            data.AddRange(BitConverter.GetBytes(this.HaveAbility_ArrayProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk8));
            data.AddRange(BitConverter.GetBytes(this.HaveAbility_EnumProperty));
            data.Add(this.Unk9);
            data.AddRange(BitConverter.GetBytes(this.HaveAbilityCount));

            foreach (var ability in this.HaveAbilities)
                data.AddRange(BitConverter.GetBytes(ability));

            data.AddRange(BitConverter.GetBytes(this.m_CriticalEquipAbility));
            data.AddRange(BitConverter.GetBytes(this.CritEquip_ArrayProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk10));
            data.AddRange(BitConverter.GetBytes(this.CritEquip_EnumProperty));
            data.Add(this.Unk11);
            data.AddRange(BitConverter.GetBytes(this.CritEquipCount));

            foreach (var ability in this.CritEquipAbilities)
                data.AddRange(BitConverter.GetBytes(ability));

            data.AddRange(BitConverter.GetBytes(this.m_CriticalHaveAbility));
            data.AddRange(BitConverter.GetBytes(this.CritHave_ArrayProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk12));
            data.AddRange(BitConverter.GetBytes(this.CritHave_EnumProperty));
            data.Add(this.Unk13);
            data.AddRange(BitConverter.GetBytes(this.CritHaveCount));

            foreach (var ability in this.CritHaveAbilities)
                data.AddRange(BitConverter.GetBytes(ability));

            data.AddRange(BitConverter.GetBytes(this.m_BaseParamData));
            data.AddRange(BitConverter.GetBytes(this.BaseParam_ObjectProperty));
            data.AddRange(this.Unk14);
            data.AddRange(BitConverter.GetBytes(this.BaseParamData));

            data.AddRange(BitConverter.GetBytes(this.m_CriticalUseAPUpNum));
            data.AddRange(BitConverter.GetBytes(this.IntProperty));
            data.AddRange(this.Unk15);
            data.AddRange(BitConverter.GetBytes(this.CriticalUseApUpNum));

            data.AddRange(BitConverter.GetBytes(this.m_LevelData));
            data.AddRange(BitConverter.GetBytes(this.Level_ObjectProperty));
            data.AddRange(this.Unk16);
            data.AddRange(BitConverter.GetBytes(this.LevelData));

            data.AddRange(BitConverter.GetBytes(this.m_FormAbilityAsset));
            data.AddRange(BitConverter.GetBytes(this.FormAbilityAsset_ObjectProperty));
            data.AddRange(this.Unk17);
            data.AddRange(BitConverter.GetBytes(this.FormAbilityAsset));

            data.AddRange(BitConverter.GetBytes(this.None));

            //data.AddRange(this.RestOfData);

            return data;
        }
    }
}