using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class LuckyMarkDataTableEntry : IDataTable
    {
        public LuckyMarkDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long RewardIndex { get; set; }
        public long Reward_IntProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public int Reward { get; set; }

        public long MarkCount { get; set; }
        public long Mark_IntProperty { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public int Mark { get; set; }

        public long CompleteRewardFlag { get; set; }
        public long BoolProperty { get; set; }
        public List<byte> Unk3 { get; set; } // 8 bytes
        public byte Flag { get; set; }
        public byte ExtraUnk1 { get; set; }

        public long RewardTreasureName { get; set; }
        public long NameProperty { get; set; }
        public List<byte> Unk4 { get; set; } // 9 bytes
        public long Treasure { get; set; }

        public long None { get; set; }

        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.RewardIndex = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Reward_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.Reward = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.MarkCount = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Mark_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.Mark = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.CompleteRewardFlag = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(8);
            this.Flag = (byte)reader.ReadByte();
            this.ExtraUnk1 = (byte)reader.ReadByte();

            this.RewardTreasureName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(9);
            this.Treasure = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.RewardIndex));
            data.AddRange(BitConverter.GetBytes(this.Reward_IntProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.Reward));

            data.AddRange(BitConverter.GetBytes(this.MarkCount));
            data.AddRange(BitConverter.GetBytes(this.Mark_IntProperty));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.Mark));

            data.AddRange(BitConverter.GetBytes(this.CompleteRewardFlag));
            data.AddRange(BitConverter.GetBytes(this.BoolProperty));
            data.AddRange(this.Unk3);
            data.Add(this.Flag);
            data.Add(this.ExtraUnk1);

            data.AddRange(BitConverter.GetBytes(this.RewardTreasureName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty));
            data.AddRange(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.Treasure));

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}