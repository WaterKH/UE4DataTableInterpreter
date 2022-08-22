using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.DataTables.Data;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class CompletionConditionsDataTableEntry : IDataTable
    {
        public CompletionConditionsDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long StructProperty1 { get; set; }
        public long ArrayProperty { get; set; }
        public long LengthOfAllData { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes

        public int ArraySize { get; set; }
        public long Unk2 { get; set; }

        // Start of All Data

        public long StructProperty2 { get; set; }
        public long LengthOfArrayData { get; set; }
        public long Unk3 { get; set; } // TresCompletionData?
        public List<byte> Unk4 { get; set; } // 17 bytes
        
        // Start of Array Data
        public List<CompletionConditionItem> CompletionConditionItems { get; set; }

        public long None { get; set; }

        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.StructProperty1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ArrayProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.LengthOfAllData = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);

            this.ArraySize = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.StructProperty2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.LengthOfArrayData = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(17);

            this.CompletionConditionItems = new List<CompletionConditionItem>();
            for (int i = 0; i < this.ArraySize; ++i)
            {
                var completionConditionItem = new CompletionConditionItem().Decompile(reader);

                this.CompletionConditionItems.Add(completionConditionItem);
            }

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.StructProperty1));
            data.AddRange(BitConverter.GetBytes(this.ArrayProperty));
            data.AddRange(BitConverter.GetBytes(this.LengthOfAllData));
            data.AddRange(this.Unk1);

            data.AddRange(BitConverter.GetBytes(this.ArraySize));
            data.AddRange(BitConverter.GetBytes(this.Unk2));

            data.AddRange(BitConverter.GetBytes(this.StructProperty2));
            data.AddRange(BitConverter.GetBytes(this.LengthOfArrayData));
            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(this.Unk4);

            foreach (var completionConditionItem in this.CompletionConditionItems)
            {
                data.AddRange(completionConditionItem.Recompile());
            }

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }

        public long Length()
        {
            var length = 0L;

            length += BitConverter.GetBytes(this.StructProperty2).Length;
            length += BitConverter.GetBytes(this.LengthOfArrayData).Length;
            length += BitConverter.GetBytes(this.Unk3).Length;
            length += this.Unk4.Count;

            foreach (var item in this.CompletionConditionItems)
            {
                length += item.Length();
            }

            length += BitConverter.GetBytes(this.None).Length;

            return length;
        }
    }
}