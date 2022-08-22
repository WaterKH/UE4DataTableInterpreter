using System;
using System.Collections.Generic;
using System.IO;

namespace UE4DataTableInterpreter.DataTables.Data
{
    public class CompletionConditionItem
    {
        public long CompletionConditionName { get; set; }
        public long StrProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public int CompletionConditionNameLength { get; set; }
        public List<byte> CompletionConditionValue { get; set; }

        public long CompletionConditionAmountName { get; set; }
        public long IntProperty { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public int CompletionConditionAmountValue { get; set; }

        public long None { get; set; }

        public CompletionConditionItem Decompile(FileStream reader)
        {
            this.CompletionConditionName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.StrProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.CompletionConditionNameLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.CompletionConditionValue = reader.ReadBytesFromFileStream((int)this.CompletionConditionNameLength);

            this.CompletionConditionAmountName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.CompletionConditionAmountValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.CompletionConditionName));
            data.AddRange(BitConverter.GetBytes(this.StrProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.CompletionConditionNameLength));
            data.AddRange(this.CompletionConditionValue);

            data.AddRange(BitConverter.GetBytes(this.CompletionConditionAmountName));
            data.AddRange(BitConverter.GetBytes(this.IntProperty));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.CompletionConditionAmountValue));

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }

        public long Length()
        {
            return this.Recompile().Count;
        }
    }
}