using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class EventDataTableEntry : IDataTable
    {
        public EventDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long gameflowFlagName { get; set; }
        public long NameProperty_1 { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public long gameflowFlagValue { get; set; }

        public long intValueGameflagSourceName { get; set; }
        public long NameProperty_2 { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public long intValueGameflagSourceValue { get; set; }

        public long intValueName { get; set; }
        public long IntProperty_1 { get; set; }
        public List<byte> Unk3 { get; set; } // 9 bytes
        public int intValueValue { get; set; }

        public long originalItemName { get; set; }
        public long NameProperty_3 { get; set; }
        public List<byte> Unk4 { get; set; } // 9 bytes
        public long originalItemValue { get; set; }

        public long originalAmountName { get; set; }
        public long IntProperty_2 { get; set; }
        public List<byte> Unk5 { get; set; } // 9 bytes
        public int originalAmountValue { get; set; }

        public long randomizedItemName { get; set; }
        public long NameProperty_4 { get; set; }
        public List<byte> Unk6 { get; set; } // 9 bytes
        public long randomizedItemValue { get; set; }

        public long randomizedAmountName { get; set; }
        public long IntProperty_3 { get; set; }
        public List<byte> Unk7 { get; set; } // 9 bytes
        public int randomizedAmountValue { get; set; }

        public long None { get; set; }

        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.gameflowFlagName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty_1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.gameflowFlagValue = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.intValueGameflagSourceName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty_2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.intValueGameflagSourceValue = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.intValueName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.IntProperty_1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(9);
            this.intValueValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.originalItemName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty_3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(9);
            this.originalItemValue = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.originalAmountName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.IntProperty_2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = reader.ReadBytesFromFileStream(9);
            this.originalAmountValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.randomizedItemName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty_4 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = reader.ReadBytesFromFileStream(9);
            this.randomizedItemValue = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.randomizedAmountName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.IntProperty_3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk7 = reader.ReadBytesFromFileStream(9);
            this.randomizedAmountValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.gameflowFlagName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty_1));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.gameflowFlagValue));

            data.AddRange(BitConverter.GetBytes(this.intValueGameflagSourceName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty_2));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.intValueGameflagSourceValue));

            data.AddRange(BitConverter.GetBytes(this.intValueName));
            data.AddRange(BitConverter.GetBytes(this.IntProperty_1));
            data.AddRange(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.intValueValue));

            data.AddRange(BitConverter.GetBytes(this.originalItemName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty_3));
            data.AddRange(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.originalItemValue));

            data.AddRange(BitConverter.GetBytes(this.originalAmountName));
            data.AddRange(BitConverter.GetBytes(this.IntProperty_2));
            data.AddRange(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.originalAmountValue));

            data.AddRange(BitConverter.GetBytes(this.randomizedItemName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty_4));
            data.AddRange(this.Unk6);
            data.AddRange(BitConverter.GetBytes(this.randomizedItemValue));

            data.AddRange(BitConverter.GetBytes(this.randomizedAmountName));
            data.AddRange(BitConverter.GetBytes(this.IntProperty_3));
            data.AddRange(this.Unk7);
            data.AddRange(BitConverter.GetBytes(this.randomizedAmountValue));

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}
