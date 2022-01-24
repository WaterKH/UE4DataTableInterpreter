using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class WeaponEnhanceDataTableEntry : IDataTable
    {
        public WeaponEnhanceDataTableEntry() { }

        public int IW { get; set; }
        public int Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_FlagIndex { get; set; }
        public long Flag_IntProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public int FlagValue { get; set; }

        public long m_WeaponID { get; set; }
        public long Weapon_EnumProperty { get; set; }
        public long Unk2 { get; set; }
        public long ETresItemDefWeapon { get; set; }
        public byte Unk3 { get; set; }
        public long Weapon { get; set; }

        public long m_bInitAchieved { get; set; }
        public long Achieved_IntProperty { get; set; }
        public List<byte> Unk4 { get; set; } // 9 bytes
        public byte Achieved { get; set; }

        public long m_WeaponLevel { get; set; }
        public long Level_IntProperty { get; set; }
        public List<byte> Unk5 { get; set; } // 9 bytes
        public int WeaponLevel { get; set; }

        public long m_AttackPlus { get; set; }
        public long Attack_IntProperty { get; set; }
        public List<byte> Unk6 { get; set; } // 9 bytes
        public int AttackPlus { get; set; }

        public long m_MagicPlus { get; set; }
        public long Magic_IntProperty { get; set; }
        public List<byte> Unk7 { get; set; } // 9 bytes
        public int MagicPlus { get; set; }

        public long m_AppendAbility { get; set; }
        public long Ability_EnumProperty { get; set; }
        public long Unk8 { get; set; }
        public long ETresAbilityKind { get; set; }
        public byte Unk9 { get; set; }
        public long Ability { get; set; }

        #region Materials

        public long m_Material0 { get; set; }
        public long Mat0_IntProperty { get; set; }
        public List<byte> Unk10 { get; set; } // 9 bytes
        public long Material0 { get; set; }

        public long m_MatNum0 { get; set; }
        public long MatNum0_IntProperty { get; set; }
        public List<byte> Unk11 { get; set; } // 9 bytes
        public int MatNum0 { get; set; }

        public long m_Material1 { get; set; }
        public long Mat1_IntProperty { get; set; }
        public List<byte> Unk12 { get; set; } // 9 bytes
        public long Material1 { get; set; }

        public long m_MatNum1 { get; set; }
        public long MatNum1_IntProperty { get; set; }
        public List<byte> Unk13 { get; set; } // 9 bytes
        public int MatNum1 { get; set; }

        public long m_Material2 { get; set; }
        public long Mat2_IntProperty { get; set; }
        public List<byte> Unk14 { get; set; } // 9 bytes
        public long Material2 { get; set; }

        public long m_MatNum2 { get; set; }
        public long MatNum2_IntProperty { get; set; }
        public List<byte> Unk15 { get; set; } // 9 bytes
        public int MatNum2 { get; set; }

        public long m_Material3 { get; set; }
        public long Mat3_IntProperty { get; set; }
        public List<byte> Unk16 { get; set; } // 9 bytes
        public long Material3 { get; set; }

        public long m_MatNum3 { get; set; }
        public long MatNum3_IntProperty { get; set; }
        public List<byte> Unk17 { get; set; } // 9 bytes
        public int MatNum3 { get; set; }

        public long m_Material4 { get; set; }
        public long Mat4_IntProperty { get; set; }
        public List<byte> Unk18 { get; set; } // 9 bytes
        public long Material4 { get; set; }

        public long m_MatNum4 { get; set; }
        public long MatNum4_IntProperty { get; set; }
        public List<byte> Unk19 { get; set; } // 9 bytes
        public int MatNum4 { get; set; }

        public long m_Material5 { get; set; }
        public long Mat5_IntProperty { get; set; }
        public List<byte> Unk20 { get; set; } // 9 bytes
        public long Material5 { get; set; }

        public long m_MatNum5 { get; set; }
        public long MatNum5_IntProperty { get; set; }
        public List<byte> Unk21 { get; set; } // 9 bytes
        public int MatNum5 { get; set; }

        #endregion

        public long None { get; set; }
        
        public IDataTable Decompile(FileStream reader)
        {
            this.IW = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Id = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_FlagIndex = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Flag_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.FlagValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_WeaponID = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Weapon_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ETresItemDefWeapon = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = (byte)reader.ReadByte();
            this.Weapon = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_bInitAchieved = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Achieved_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(9);
            this.Achieved = (byte)reader.ReadByte();

            this.m_WeaponLevel = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Level_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = reader.ReadBytesFromFileStream(9);
            this.WeaponLevel = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttackPlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Attack_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = reader.ReadBytesFromFileStream(9);
            this.AttackPlus = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MagicPlus = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Magic_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk7 = reader.ReadBytesFromFileStream(9);
            this.MagicPlus = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AppendAbility = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Ability_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk8 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ETresAbilityKind = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk9 = (byte)reader.ReadByte();
            this.Ability = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Material0 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat0_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk10 = reader.ReadBytesFromFileStream(9);
            this.Material0 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum0 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum0_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk11 = reader.ReadBytesFromFileStream(9);
            this.MatNum0 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Material1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat1_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk12 = reader.ReadBytesFromFileStream(9);
            this.Material1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum1_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk13 = reader.ReadBytesFromFileStream(9);
            this.MatNum1 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Material2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat2_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk14 = reader.ReadBytesFromFileStream(9);
            this.Material2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum2_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk15 = reader.ReadBytesFromFileStream(9);
            this.MatNum2 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Material3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat3_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk16 = reader.ReadBytesFromFileStream(9);
            this.Material3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum3_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk17 = reader.ReadBytesFromFileStream(9);
            this.MatNum3 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Material4 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat4_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk18 = reader.ReadBytesFromFileStream(9);
            this.Material4 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum4 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum4_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk19 = reader.ReadBytesFromFileStream(9);
            this.MatNum4 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Material5 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat5_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk20 = reader.ReadBytesFromFileStream(9);
            this.Material5 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum5 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum5_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk21 = reader.ReadBytesFromFileStream(9);
            this.MatNum5 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.IW));
            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_FlagIndex));
            data.AddRange(BitConverter.GetBytes(this.Flag_IntProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.FlagValue));

            data.AddRange(BitConverter.GetBytes(this.m_WeaponID));
            data.AddRange(BitConverter.GetBytes(this.Weapon_EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.AddRange(BitConverter.GetBytes(this.ETresItemDefWeapon));
            data.Add(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.Weapon));

            data.AddRange(BitConverter.GetBytes(this.m_bInitAchieved));
            data.AddRange(BitConverter.GetBytes(this.Achieved_IntProperty));
            data.AddRange(this.Unk4);
            data.Add(this.Achieved);

            data.AddRange(BitConverter.GetBytes(this.m_WeaponLevel));
            data.AddRange(BitConverter.GetBytes(this.Level_IntProperty));
            data.AddRange(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.WeaponLevel));

            data.AddRange(BitConverter.GetBytes(this.m_AttackPlus));
            data.AddRange(BitConverter.GetBytes(this.Attack_IntProperty));
            data.AddRange(this.Unk6);
            data.AddRange(BitConverter.GetBytes(this.AttackPlus));

            data.AddRange(BitConverter.GetBytes(this.m_MagicPlus));
            data.AddRange(BitConverter.GetBytes(this.Magic_IntProperty));
            data.AddRange(this.Unk7);
            data.AddRange(BitConverter.GetBytes(this.MagicPlus));


            data.AddRange(BitConverter.GetBytes(this.m_AppendAbility));
            data.AddRange(BitConverter.GetBytes(this.Ability_EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk8));
            data.AddRange(BitConverter.GetBytes(this.ETresAbilityKind));
            data.Add(this.Unk9);
            data.AddRange(BitConverter.GetBytes(this.Ability));

            data.AddRange(BitConverter.GetBytes(this.m_Material0));
            data.AddRange(BitConverter.GetBytes(this.Mat0_IntProperty));
            data.AddRange(this.Unk10);
            data.AddRange(BitConverter.GetBytes(this.Material0));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum0));
            data.AddRange(BitConverter.GetBytes(this.MatNum0_IntProperty));
            data.AddRange(this.Unk11);
            data.AddRange(BitConverter.GetBytes(this.MatNum0));

            data.AddRange(BitConverter.GetBytes(this.m_Material1));
            data.AddRange(BitConverter.GetBytes(this.Mat1_IntProperty));
            data.AddRange(this.Unk12);
            data.AddRange(BitConverter.GetBytes(this.Material1));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum1));
            data.AddRange(BitConverter.GetBytes(this.MatNum1_IntProperty));
            data.AddRange(this.Unk13);
            data.AddRange(BitConverter.GetBytes(this.MatNum1));

            data.AddRange(BitConverter.GetBytes(this.m_Material0));
            data.AddRange(BitConverter.GetBytes(this.Mat0_IntProperty));
            data.AddRange(this.Unk10);
            data.AddRange(BitConverter.GetBytes(this.Material0));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum0));
            data.AddRange(BitConverter.GetBytes(this.MatNum0_IntProperty));
            data.AddRange(this.Unk11);
            data.AddRange(BitConverter.GetBytes(this.MatNum0));

            data.AddRange(BitConverter.GetBytes(this.m_Material1));
            data.AddRange(BitConverter.GetBytes(this.Mat1_IntProperty));
            data.AddRange(this.Unk12);
            data.AddRange(BitConverter.GetBytes(this.Material1));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum1));
            data.AddRange(BitConverter.GetBytes(this.MatNum1_IntProperty));
            data.AddRange(this.Unk13);
            data.AddRange(BitConverter.GetBytes(this.MatNum1));

            data.AddRange(BitConverter.GetBytes(this.m_Material2));
            data.AddRange(BitConverter.GetBytes(this.Mat2_IntProperty));
            data.AddRange(this.Unk14);
            data.AddRange(BitConverter.GetBytes(this.Material2));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum2));
            data.AddRange(BitConverter.GetBytes(this.MatNum2_IntProperty));
            data.AddRange(this.Unk15);
            data.AddRange(BitConverter.GetBytes(this.MatNum2));

            data.AddRange(BitConverter.GetBytes(this.m_Material3));
            data.AddRange(BitConverter.GetBytes(this.Mat3_IntProperty));
            data.AddRange(this.Unk16);
            data.AddRange(BitConverter.GetBytes(this.Material3));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum3));
            data.AddRange(BitConverter.GetBytes(this.MatNum3_IntProperty));
            data.AddRange(this.Unk17);
            data.AddRange(BitConverter.GetBytes(this.MatNum3));

            data.AddRange(BitConverter.GetBytes(this.m_Material4));
            data.AddRange(BitConverter.GetBytes(this.Mat4_IntProperty));
            data.AddRange(this.Unk18);
            data.AddRange(BitConverter.GetBytes(this.Material4));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum4));
            data.AddRange(BitConverter.GetBytes(this.MatNum4_IntProperty));
            data.AddRange(this.Unk19);
            data.AddRange(BitConverter.GetBytes(this.MatNum4));

            data.AddRange(BitConverter.GetBytes(this.m_Material5));
            data.AddRange(BitConverter.GetBytes(this.Mat5_IntProperty));
            data.AddRange(this.Unk20);
            data.AddRange(BitConverter.GetBytes(this.Material5));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum5));
            data.AddRange(BitConverter.GetBytes(this.MatNum5_IntProperty));
            data.AddRange(this.Unk21);
            data.AddRange(BitConverter.GetBytes(this.MatNum5));
            
            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}