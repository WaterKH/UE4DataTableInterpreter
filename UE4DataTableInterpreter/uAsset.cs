using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UE4DataTableInterpreter
{
    public class uAsset
    {
        public ulong UnrealId { get; set; }
        public List<byte> Unk1 { get; set; }
        public int FileSize { get; set; }
        public int UnkLength { get; set; }
        public List<byte> Unk2 { get; set; } // Uses Unk Length?
        public int UnkLength2 { get; set; }
        public int UnkLength3 { get; set; } // AssetNameCount?
        public int UnkLength4 { get; set; }
        public long Unk3 { get; set; }
        public int Unk4 { get; set; }
        public int Unk5 { get; set; } // Has to do with AssetNameCount
        public int Unk6 { get; set; }
        public int SubSize1 { get; set; } // Has to do with AssetNameCount
        public int SubSize2 { get; set; } // Has to do with AssetNameCount
        public List<byte> Unk7 { get; set; } // Length 13 bytes?
        public int UnkLength5 { get; set; }
        public List<byte> Unk8 { get; set; } // Uses Unk Length 5?
        public byte Unk9 { get; set; }
        public int Unk10 { get; set; }
        public int Unk11 { get; set; }
        public int Unk12 { get; set; }
        public List<byte> Unk13 { get; set; } // 0x24 bytes?
        public int Unk14 { get; set; }
        public int Unk15 { get; set; }
        public int Unk16 { get; set; } // Has to do with AssetNameCount
        public int Unk17 { get; set; } // Has to do with AssetNameCount
        public List<byte> Unk18 { get; set; } // 16 bytes?
        public int Unk19 { get; set; } // Has to do with AssetNameCount


        public List<Asset> AssetStrings { get; set; }

        // TODO Rest of the data

        public List<byte> DuplicateData { get; set; } // 0xB0 length
        public int FinalLength { get; set; }
        public List<byte> DuplicateData2 { get; set; } // 0x54 length

        public uAsset Decompile(FileStream reader)
        {
            this.UnrealId = BitConverter.ToUInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.Unk1 = reader.ReadBytesFromFileStream(16);
            this.FileSize = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.UnkLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(this.UnkLength);

            this.UnkLength2 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.UnkLength3 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.UnkLength4 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.Unk3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.Unk4 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk5 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk6 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.SubSize1 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.SubSize2 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.Unk7 = reader.ReadBytesFromFileStream(13);

            var unkLengthTemp = reader.ReadBytesFromFileStream(4);
            unkLengthTemp.Reverse();
            this.UnkLength5 = BitConverter.ToInt32(unkLengthTemp.ToArray());
            this.Unk8 = reader.ReadBytesFromFileStream(0xE);//this.UnkLength5);

            this.Unk9 = (byte)reader.ReadByte();
            this.Unk10 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk11 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk12 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk13 = reader.ReadBytesFromFileStream(36);
            this.Unk14 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk15 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk16 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk17 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.Unk18 = reader.ReadBytesFromFileStream(16);
            this.Unk19 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.AssetStrings = new List<Asset>();

            var length = -1;
            for (int i = 0; i < this.Unk12; ++i)
            {
                length = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

                if (length != 1 && length >= -1 && length < 500)
                {
                    var asset = new Asset
                    {
                        Length = length,
                        AssetName = Encoding.UTF8.GetString(reader.ReadBytesFromFileStream(length).ToArray()),
                        Id = BitConverter.ToUInt32(reader.ReadBytesFromFileStream(4).ToArray())
                    };

                    this.AssetStrings.Add(asset);
                }
            }

            //reader.Position -= 4;
            this.DuplicateData = reader.ReadBytesFromFileStream(0xB0); // 0xB68 for ChrInit
            this.FinalLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray()); // 0x369D for ChrInit
            this.DuplicateData2 = reader.ReadBytesFromFileStream(0x54); // 0x118 for ChrInit

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.UnrealId));

            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.FileSize));

            data.AddRange(BitConverter.GetBytes(this.UnkLength));
            data.AddRange(this.Unk2);

            data.AddRange(BitConverter.GetBytes(this.UnkLength2));
            data.AddRange(BitConverter.GetBytes(this.UnkLength3));
            data.AddRange(BitConverter.GetBytes(this.UnkLength4));

            data.AddRange(BitConverter.GetBytes(this.Unk3));

            data.AddRange(BitConverter.GetBytes(this.Unk4));
            data.AddRange(BitConverter.GetBytes(this.Unk5));
            data.AddRange(BitConverter.GetBytes(this.Unk6));

            data.AddRange(BitConverter.GetBytes(this.SubSize1));
            data.AddRange(BitConverter.GetBytes(this.SubSize2));
            
            data.AddRange(this.Unk7);

            var reversedUnkLength5 = BitConverter.GetBytes(this.UnkLength5).ToList();
            reversedUnkLength5.Reverse();
            data.AddRange(reversedUnkLength5);
            data.AddRange(this.Unk8);

            data.Add(this.Unk9);
            data.AddRange(BitConverter.GetBytes(this.Unk10));
            data.AddRange(BitConverter.GetBytes(this.Unk11));
            data.AddRange(BitConverter.GetBytes(this.Unk12));
            data.AddRange(this.Unk13);
            data.AddRange(BitConverter.GetBytes(this.Unk14));
            data.AddRange(BitConverter.GetBytes(this.Unk15));
            data.AddRange(BitConverter.GetBytes(this.Unk16));
            data.AddRange(BitConverter.GetBytes(this.Unk17));
            data.AddRange(this.Unk18);
            data.AddRange(BitConverter.GetBytes(this.Unk19));

            foreach(var asset in this.AssetStrings)
            {
                data.AddRange(BitConverter.GetBytes(asset.Length));
                data.AddRange(Encoding.UTF8.GetBytes(asset.AssetName));
                data.AddRange(BitConverter.GetBytes(asset.Id));
            }

            data.AddRange(this.DuplicateData);
            data.AddRange(BitConverter.GetBytes(this.FinalLength));
            data.AddRange(this.DuplicateData2);

            return data;
        }
    }

    public class Asset
    {
        public int Length { get; set; }
        public string AssetName { get; set; }
        public uint Id { get; set; }
    }
}
