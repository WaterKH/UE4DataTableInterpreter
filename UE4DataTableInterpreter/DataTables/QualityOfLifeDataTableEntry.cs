using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class QualityOfLifeDataTableEntry : IDataTable
    {
        public QualityOfLifeDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long qolName { get; set; }
        public long NameProperty_1 { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public long qolValue { get; set; }

        public long activeName { get; set; }
        public long BoolProperty { get; set; }
        public long Unk2 { get; set; } 
        public byte activeValue { get; set; }
        public byte Unk3 { get; set; }

        public long gameflowFlagName { get; set; }
        public long NameProperty_2 { get; set; }
        public List<byte> Unk4 { get; set; } // 9 bytes
        public long gameflowFlagValue { get; set; }

        public long intValueGameflagTriggerName { get; set; }
        public long IntProperty_1 { get; set; }
        public List<byte> Unk5 { get; set; } // 9 bytes
        public int intValueGameflagTriggerValue { get; set; }

        public long intValueGameflagSkipToName { get; set; }
        public long IntProperty_2 { get; set; }
        public List<byte> Unk6 { get; set; } // 9 bytes
        public int intValueGameflagSkipToValue { get; set; }

        public long None { get; set; }

        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.qolName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty_1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.qolValue = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.activeName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.activeValue = (byte)reader.ReadByte();
            this.Unk3 = (byte)reader.ReadByte();

            this.gameflowFlagName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty_2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(9);
            this.gameflowFlagValue = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.intValueGameflagTriggerName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.IntProperty_1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = reader.ReadBytesFromFileStream(9);
            this.intValueGameflagTriggerValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.intValueGameflagSkipToName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.IntProperty_2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = reader.ReadBytesFromFileStream(9);
            this.intValueGameflagSkipToValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.qolName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty_1));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.qolValue));
            
            data.AddRange(BitConverter.GetBytes(this.activeName));
            data.AddRange(BitConverter.GetBytes(this.BoolProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk2));
            data.Add(this.activeValue); 
            data.Add(this.Unk3);

            data.AddRange(BitConverter.GetBytes(this.gameflowFlagName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty_2));
            data.AddRange(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.gameflowFlagValue));

            data.AddRange(BitConverter.GetBytes(this.intValueGameflagTriggerName));
            data.AddRange(BitConverter.GetBytes(this.IntProperty_1));
            data.AddRange(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.intValueGameflagTriggerValue));

            data.AddRange(BitConverter.GetBytes(this.intValueGameflagSkipToName));
            data.AddRange(BitConverter.GetBytes(this.IntProperty_2));
            data.AddRange(this.Unk6);
            data.AddRange(BitConverter.GetBytes(this.intValueGameflagSkipToValue));

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}
