using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class SynthesisItemDataTableEntry : IDataTable
    {
        public SynthesisItemDataTableEntry() { }

        public int IS { get; set; }
        public int Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long m_FlagIndex { get; set; }
        public long Flag_IntProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public int FlagValue { get; set; }

        public long m_RewardItem { get; set; }
        public long Reward_NameProperty { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public long Reward { get; set; }

        public long m_Condition { get; set; }
        public long Condition_EnumProperty { get; set; }
        public long Unk3 { get; set; }
        public long ETresItemSynthesisCondition { get; set; }
        public byte Unk4 { get; set; }
        public long Condition { get; set; }

        public long m_ConditionName { get; set; }
        public long Condition_NameProperty { get; set; }
        public List<byte> Unk5 { get; set; } // 9 bytes
        public long ConditionName { get; set; }

        #region Materials

        public long m_Material0 { get; set; }
        public long Mat0_IntProperty { get; set; }
        public List<byte> Unk6 { get; set; } // 9 bytes
        public long Material0 { get; set; }

        public long m_MatNum0 { get; set; }
        public long MatNum0_IntProperty { get; set; }
        public List<byte> Unk7 { get; set; } // 9 bytes
        public int MatNum0 { get; set; }

        public long m_Material1 { get; set; }
        public long Mat1_IntProperty { get; set; }
        public List<byte> Unk8 { get; set; } // 9 bytes
        public long Material1 { get; set; }

        public long m_MatNum1 { get; set; }
        public long MatNum1_IntProperty { get; set; }
        public List<byte> Unk9 { get; set; } // 9 bytes
        public int MatNum1 { get; set; }

        public long m_Material2 { get; set; }
        public long Mat2_IntProperty { get; set; }
        public List<byte> Unk10 { get; set; } // 9 bytes
        public long Material2 { get; set; }

        public long m_MatNum2 { get; set; }
        public long MatNum2_IntProperty { get; set; }
        public List<byte> Unk11 { get; set; } // 9 bytes
        public int MatNum2 { get; set; }

        public long m_Material3 { get; set; }
        public long Mat3_IntProperty { get; set; }
        public List<byte> Unk12 { get; set; } // 9 bytes
        public long Material3 { get; set; }

        public long m_MatNum3 { get; set; }
        public long MatNum3_IntProperty { get; set; }
        public List<byte> Unk13 { get; set; } // 9 bytes
        public int MatNum3 { get; set; }

        public long m_Material4 { get; set; }
        public long Mat4_IntProperty { get; set; }
        public List<byte> Unk14 { get; set; } // 9 bytes
        public long Material4 { get; set; }

        public long m_MatNum4 { get; set; }
        public long MatNum4_IntProperty { get; set; }
        public List<byte> Unk15 { get; set; } // 9 bytes
        public int MatNum4 { get; set; }

        public long m_Material5 { get; set; }
        public long Mat5_IntProperty { get; set; }
        public List<byte> Unk16 { get; set; } // 9 bytes
        public long Material5 { get; set; }

        public long m_MatNum5 { get; set; }
        public long MatNum5_IntProperty { get; set; }
        public List<byte> Unk17 { get; set; } // 9 bytes
        public int MatNum5 { get; set; }

        #endregion

        public long None { get; set; }
        
        public IDataTable Decompile(FileStream reader)
        {
            this.IS = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Id = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_FlagIndex = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Flag_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.FlagValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_RewardItem = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Reward_NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.Reward = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_Condition = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Condition_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ETresItemSynthesisCondition = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = (byte)reader.ReadByte();
            this.Condition = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_ConditionName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Condition_NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = reader.ReadBytesFromFileStream(9);
            this.ConditionName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());


            this.m_Material0 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat0_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = reader.ReadBytesFromFileStream(9);
            this.Material0 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum0 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum0_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk7 = reader.ReadBytesFromFileStream(9);
            this.MatNum0 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Material1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat1_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk8 = reader.ReadBytesFromFileStream(9);
            this.Material1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum1_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk9 = reader.ReadBytesFromFileStream(9);
            this.MatNum1 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Material2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat2_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk10 = reader.ReadBytesFromFileStream(9);
            this.Material2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum2_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk11 = reader.ReadBytesFromFileStream(9);
            this.MatNum2 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Material3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat3_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk12 = reader.ReadBytesFromFileStream(9);
            this.Material3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum3_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk13 = reader.ReadBytesFromFileStream(9);
            this.MatNum3 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Material4 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat4_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk14 = reader.ReadBytesFromFileStream(9);
            this.Material4 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum4 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum4_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk15 = reader.ReadBytesFromFileStream(9);
            this.MatNum4 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_Material5 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mat5_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk16 = reader.ReadBytesFromFileStream(9);
            this.Material5 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MatNum5 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MatNum5_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk17 = reader.ReadBytesFromFileStream(9);
            this.MatNum5 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.IS));
            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_FlagIndex));
            data.AddRange(BitConverter.GetBytes(this.Flag_IntProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.FlagValue));

            data.AddRange(BitConverter.GetBytes(this.m_RewardItem));
            data.AddRange(BitConverter.GetBytes(this.Reward_NameProperty));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.Reward));

            data.AddRange(BitConverter.GetBytes(this.m_Condition));
            data.AddRange(BitConverter.GetBytes(this.Condition_EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.ETresItemSynthesisCondition));
            data.Add(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.Condition));

            data.AddRange(BitConverter.GetBytes(this.m_ConditionName));
            data.AddRange(BitConverter.GetBytes(this.Condition_NameProperty));
            data.AddRange(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.ConditionName));


            data.AddRange(BitConverter.GetBytes(this.m_Material0));
            data.AddRange(BitConverter.GetBytes(this.Mat0_IntProperty));
            data.AddRange(this.Unk6);
            data.AddRange(BitConverter.GetBytes(this.Material0));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum0));
            data.AddRange(BitConverter.GetBytes(this.MatNum0_IntProperty));
            data.AddRange(this.Unk7);
            data.AddRange(BitConverter.GetBytes(this.MatNum0));

            data.AddRange(BitConverter.GetBytes(this.m_Material1));
            data.AddRange(BitConverter.GetBytes(this.Mat1_IntProperty));
            data.AddRange(this.Unk8);
            data.AddRange(BitConverter.GetBytes(this.Material1));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum1));
            data.AddRange(BitConverter.GetBytes(this.MatNum1_IntProperty));
            data.AddRange(this.Unk9);
            data.AddRange(BitConverter.GetBytes(this.MatNum1));

            data.AddRange(BitConverter.GetBytes(this.m_Material2));
            data.AddRange(BitConverter.GetBytes(this.Mat2_IntProperty));
            data.AddRange(this.Unk10);
            data.AddRange(BitConverter.GetBytes(this.Material2));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum2));
            data.AddRange(BitConverter.GetBytes(this.MatNum2_IntProperty));
            data.AddRange(this.Unk11);
            data.AddRange(BitConverter.GetBytes(this.MatNum2));

            data.AddRange(BitConverter.GetBytes(this.m_Material3));
            data.AddRange(BitConverter.GetBytes(this.Mat3_IntProperty));
            data.AddRange(this.Unk12);
            data.AddRange(BitConverter.GetBytes(this.Material3));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum3));
            data.AddRange(BitConverter.GetBytes(this.MatNum3_IntProperty));
            data.AddRange(this.Unk13);
            data.AddRange(BitConverter.GetBytes(this.MatNum3));

            data.AddRange(BitConverter.GetBytes(this.m_Material4));
            data.AddRange(BitConverter.GetBytes(this.Mat4_IntProperty));
            data.AddRange(this.Unk14);
            data.AddRange(BitConverter.GetBytes(this.Material4));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum4));
            data.AddRange(BitConverter.GetBytes(this.MatNum4_IntProperty));
            data.AddRange(this.Unk15);
            data.AddRange(BitConverter.GetBytes(this.MatNum4));

            data.AddRange(BitConverter.GetBytes(this.m_Material5));
            data.AddRange(BitConverter.GetBytes(this.Mat5_IntProperty));
            data.AddRange(this.Unk16);
            data.AddRange(BitConverter.GetBytes(this.Material5));

            data.AddRange(BitConverter.GetBytes(this.m_MatNum5));
            data.AddRange(BitConverter.GetBytes(this.MatNum5_IntProperty));
            data.AddRange(this.Unk17);
            data.AddRange(BitConverter.GetBytes(this.MatNum5));
            
            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}