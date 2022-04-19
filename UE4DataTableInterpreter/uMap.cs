using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UE4DataTableInterpreter
{
    public class uMap
    {
        public ulong UnrealId { get; set; } // Potentially the ID
        public List<byte> EmptyData { get; set; } // 16 bytes of empty data
        public int FileSize { get; set; } // uMap Length
        public int RowNameLength { get; set; } // Usually 0x5
        public List<byte> RowName { get; set; } // Usually None\u0000
        public uint Unk1 { get; set; } // UNK1
        public int AssetNameCount { get; set; } // Number of Asset Strings
        public long HeaderSize { get; set; } // Usually 0xC1
        public int Unk2 { get; set; }
        public int ObjectCount { get; set; } // Number of Objects (Block size each: 0x68)
        public int ObjectStartAddress { get; set; } // I believe this is the Object Start Address
        public int SectionCount { get; set; } // Number of Sections (Block size each: 0x1C)
        public int SectionStartAddress { get; set; } // I believe this is the Section Start Address
        public int MainLength { get; set; } // Header length + AssetName length + sections length + objects length
        public List<byte> UnkData { get; set; } // Length 20 bytes
        public int Unk3 { get; set; } // UNK2
        public int Unk4 { get; set; } // UNK3
        public int Unk5 { get; set; } // UNK4
        public List<byte> EmptyData2 { get; set; } // 36 bytes of empty data
        public long Unk6 { get; set; } // UNK5
        public int uMapLengthMinusLastPortion1 { get; set; } // ALMOST uMap size excluding the last part
        public int uMapuExpLength { get; set; } // uMap + uExp Length (minus last 4 bytes of uExp)
        public List<byte> EmptyData3 { get; set; } // 12 bytes of empty data
        public int Unk7 { get; set; } // UNK6
        public int uMapLengthMinusLastPortion2 { get; set; } // uMap size excluding the last part

        public List<Asset> AssetStrings { get; set; }

        public List<uMapSection> Sections { get; set; }
        //public List<byte> SectionData { get; set; }
        public List<uMapObject> Objects { get; set; }


        public List<byte> RestOfData { get; set; }


        public uMap Decompile(FileStream reader)
        {
            this.UnrealId = BitConverter.ToUInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.EmptyData = reader.ReadBytesFromFileStream(16);
            this.FileSize = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.RowNameLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.RowName = reader.ReadBytesFromFileStream(this.RowNameLength);

            this.Unk1 = BitConverter.ToUInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.AssetNameCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.HeaderSize = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.Unk2 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.ObjectCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.ObjectStartAddress = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.SectionCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.SectionStartAddress = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.MainLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.UnkData = reader.ReadBytesFromFileStream(32);

            this.Unk3 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk4 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk5 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.EmptyData2 = reader.ReadBytesFromFileStream(36);

            this.Unk6 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.uMapLengthMinusLastPortion1 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.uMapuExpLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.EmptyData3 = reader.ReadBytesFromFileStream(12);
            this.Unk7 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.uMapLengthMinusLastPortion2 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());


            this.AssetStrings = new List<Asset>();
            for (int i = 0; i < this.AssetNameCount; ++i)
            {
                int length = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

                var tempLength = length == -8 ? 16 : length;
                var asset = new Asset
                {
                    Length = length,
                    AssetName = Encoding.UTF8.GetString(reader.ReadBytesFromFileStream(tempLength).ToArray()),
                    Id = BitConverter.ToUInt32(reader.ReadBytesFromFileStream(4).ToArray())
                };
                

                this.AssetStrings.Add(asset);
            }

            this.Sections = new List<uMapSection>();
            for (int i = 0; i < this.SectionCount; ++i)
            {
                var section = new uMapSection
                {
                    ScriptNameIndex = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()),
                    SectionNameIndex = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()),
                    Unk1 = BitConverter.ToUInt32(reader.ReadBytesFromFileStream(4).ToArray()),
                    Unk2 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray()),
                    SectionCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray())
                };

                this.Sections.Add(section);
            }

            //this.SectionData = reader.ReadBytesFromFileStream(this.ObjectStartAddress - (int)reader.Position);

            this.Objects = new List<uMapObject>();
            for (int i = 0; i < this.ObjectCount; ++i)
            {
                var uMapObject = new uMapObject
                {
                    Unk1 = BitConverter.ToUInt32(reader.ReadBytesFromFileStream(4).ToArray()),
                    Unk2 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray()),
                    Unk3 = BitConverter.ToUInt32(reader.ReadBytesFromFileStream(4).ToArray()),
                    Unk4 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray()),
                    Unk5 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray()),
                    Unk6 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray()),
                    Unk7 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray()),
                    uExpBlockLength = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()),
                    uExpPosition = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()),
                    UnkData = reader.ReadBytesFromFileStream(0x3C)
                };

                this.Objects.Add(uMapObject);
            }

            this.RestOfData = reader.ReadBytesFromFileStream((int)(reader.Length - reader.Position));

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.UnrealId));

            data.AddRange(this.EmptyData);
            data.AddRange(BitConverter.GetBytes(this.FileSize));

            data.AddRange(BitConverter.GetBytes(this.RowNameLength));
            data.AddRange(this.RowName);

            data.AddRange(BitConverter.GetBytes(this.Unk1));
            data.AddRange(BitConverter.GetBytes(this.AssetNameCount));
            data.AddRange(BitConverter.GetBytes(this.HeaderSize));

            data.AddRange(BitConverter.GetBytes(this.Unk2));

            data.AddRange(BitConverter.GetBytes(this.ObjectCount));
            data.AddRange(BitConverter.GetBytes(this.ObjectStartAddress));
            data.AddRange(BitConverter.GetBytes(this.SectionCount));
            data.AddRange(BitConverter.GetBytes(this.SectionStartAddress));
            data.AddRange(BitConverter.GetBytes(this.MainLength));

            data.AddRange(this.UnkData);

            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.Unk4));
            data.AddRange(BitConverter.GetBytes(this.Unk5));

            data.AddRange(this.EmptyData2);

            data.AddRange(BitConverter.GetBytes(this.Unk6));

            data.AddRange(BitConverter.GetBytes(this.uMapLengthMinusLastPortion1));
            data.AddRange(BitConverter.GetBytes(this.uMapuExpLength));

            data.AddRange(this.EmptyData3);
            data.AddRange(BitConverter.GetBytes(this.Unk7));

            data.AddRange(BitConverter.GetBytes(this.uMapLengthMinusLastPortion2));

            foreach (var asset in this.AssetStrings)
            {
                var tempAssetName = asset.Length == -8 ?
                       new byte[] { 0xB0, 0x65, 0x8F, 0x89, 0x54, 0x00, 0x72, 0x00, 0x61, 0x00, 0x63, 0x00, 0x6B, 0x00, 0x00, 0x00 } :
                       Encoding.UTF8.GetBytes(asset.AssetName);

                data.AddRange(BitConverter.GetBytes(asset.Length));
                data.AddRange(tempAssetName);
                data.AddRange(BitConverter.GetBytes(asset.Id));
            }

            foreach (var section in this.Sections)
            {
                data.AddRange(BitConverter.GetBytes(section.ScriptNameIndex));
                data.AddRange(BitConverter.GetBytes(section.SectionNameIndex));
                data.AddRange(BitConverter.GetBytes(section.Unk1));
                data.AddRange(BitConverter.GetBytes(section.Unk2));
                data.AddRange(BitConverter.GetBytes(section.SectionCount));
            }

            //data.AddRange(this.SectionData);

            foreach (var uMapObject in this.Objects)
            {
                data.AddRange(BitConverter.GetBytes(uMapObject.Unk1));
                data.AddRange(BitConverter.GetBytes(uMapObject.Unk2));
                data.AddRange(BitConverter.GetBytes(uMapObject.Unk3));
                data.AddRange(BitConverter.GetBytes(uMapObject.Unk4));
                data.AddRange(BitConverter.GetBytes(uMapObject.Unk5));
                data.AddRange(BitConverter.GetBytes(uMapObject.Unk6));
                data.AddRange(BitConverter.GetBytes(uMapObject.Unk7));
                data.AddRange(BitConverter.GetBytes(uMapObject.uExpBlockLength));
                data.AddRange(BitConverter.GetBytes(uMapObject.uExpPosition));

                data.AddRange(uMapObject.UnkData);
            }

            data.AddRange(this.RestOfData);

            return data;
        }
    }

    public class uMapSection
    {
        public long ScriptNameIndex { get; set; }
        public long SectionNameIndex { get; set; }
        public uint Unk1 { get; set; }
        public int Unk2 { get; set; }
        public int SectionCount { get; set; }
    }

    public class uMapObject
    {
        public uint Unk1 { get; set; }
        public int Unk2 { get; set; }
        public uint Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; }
        public int Unk6 { get; set; }
        public int Unk7 { get; set; }
        public long uExpBlockLength { get; set; }
        public long uExpPosition { get; set; }

        public List<byte> UnkData { get; set; } // 0x3C length of unknown data
    }
}
