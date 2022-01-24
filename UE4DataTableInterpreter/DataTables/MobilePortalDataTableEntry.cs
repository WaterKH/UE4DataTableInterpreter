using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class MobilePortalDataTableEntry : IDataTable
    {
        public MobilePortalDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        public long JiminyRenderTargetTextureName { get; set; }
        public long ObjectProperty_1 { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public int JiminyRenderTargetTextureValue { get; set; }

        public long JiminyActorName { get; set; }
        public long AssetObjectProperty_1 { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public int JiminyActorLength { get; set; }
        public List<byte> JiminyActorValue { get; set; }

        public long DictionaryDataName { get; set; }
        public long ObjectProperty_2 { get; set; }
        public List<byte> Unk3 { get; set; } // 9 bytes
        public int DictionaryDataValue { get; set; }

        public long KeywordGlossaryDataName { get; set; }
        public long ObjectProperty_3 { get; set; }
        public List<byte> Unk4 { get; set; } // 9 bytes
        public int KeywordGlossaryDataValue { get; set; }

        public long AnsemCodeDataName { get; set; }
        public long ObjectProperty_4 { get; set; }
        public List<byte> Unk5 { get; set; } // 9 bytes
        public int AnsemCodeDataValue { get; set; }

        public long StoryDataName { get; set; }
        public long ObjectProperty_5 { get; set; }
        public List<byte> Unk6 { get; set; } // 9 bytes
        public int StoryDataValue { get; set; }

        public long LSIGameDataName { get; set; }
        public long ObjectProperty_6 { get; set; }
        public List<byte> Unk7 { get; set; } // 9 bytes
        public int LSIGameDataValue { get; set; }

        public long SwfMovieAssetLSIButtonSetName { get; set; }
        public long AssetObjectProperty_2 { get; set; }
        public List<byte> Unk8 { get; set; } // 9 bytes
        public int SwfMovieAssetLSIButtonSetLength { get; set; }
        public List<byte> SwfMovieAssetLSIButtonSetValue { get; set; }

        public long LSIGamePlayRewardItemName { get; set; }
        public long NameProperty { get; set; }
        public List<byte> Unk9 { get; set; } // 9 bytes
        public int LSIGamePlayRewardItemValue { get; set; }
        public int LSIGamePlayRewardItemValue2 { get; set; }

        public long LSIMenuBGMName { get; set; }
        public long AssetObjectProperty_3 { get; set; }
        public List<byte> Unk10 { get; set; } // 9 bytes
        public int LSIMenuBGMLength { get; set; }
        public List<byte> LSIMenuBGMValue { get; set; }

        public SwfMovieAsset[] SwfMovieAssets { get; set; } = new SwfMovieAsset[12];


        public List<byte> RestOfData { get; set; }

        public class SwfMovieAsset
        {
            public long Name { get; set; }
            public long AssetObjectProperty { get; set; }
            public List<byte> Unk { get; set; }
            public int Length { get; set; }
            public List<byte> Value { get; set; }
        }

        public IDataTable Decompile(FileStream reader)
        {
            this.JiminyRenderTargetTextureName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ObjectProperty_1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.JiminyRenderTargetTextureValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.JiminyActorName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AssetObjectProperty_1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.JiminyActorLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.JiminyActorValue = reader.ReadBytesFromFileStream(this.JiminyActorLength);

            this.DictionaryDataName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ObjectProperty_2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(9);
            this.DictionaryDataValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.KeywordGlossaryDataName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ObjectProperty_3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(9);
            this.KeywordGlossaryDataValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.AnsemCodeDataName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ObjectProperty_4 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = reader.ReadBytesFromFileStream(9);
            this.AnsemCodeDataValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.StoryDataName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ObjectProperty_5 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = reader.ReadBytesFromFileStream(9);
            this.StoryDataValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.LSIGameDataName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ObjectProperty_6 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk7 = reader.ReadBytesFromFileStream(9);
            this.LSIGameDataValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.SwfMovieAssetLSIButtonSetName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AssetObjectProperty_2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk8 = reader.ReadBytesFromFileStream(9);
            this.SwfMovieAssetLSIButtonSetLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.SwfMovieAssetLSIButtonSetValue = reader.ReadBytesFromFileStream(this.SwfMovieAssetLSIButtonSetLength);

            this.LSIGamePlayRewardItemName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NameProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk9 = reader.ReadBytesFromFileStream(9);
            this.LSIGamePlayRewardItemValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.LSIGamePlayRewardItemValue2 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.LSIMenuBGMName = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AssetObjectProperty_3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk10 = reader.ReadBytesFromFileStream(9);
            this.LSIMenuBGMLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.LSIMenuBGMValue = reader.ReadBytesFromFileStream(this.LSIMenuBGMLength);

            for (int i = 0; i < this.SwfMovieAssets.Length; ++i)
            {
                this.SwfMovieAssets[i] = new SwfMovieAsset
                {
                    Name = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()),
                    AssetObjectProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray()),
                    Unk = reader.ReadBytesFromFileStream(9),
                    Length = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray())
                };
                this.SwfMovieAssets[i].Value = reader.ReadBytesFromFileStream(this.SwfMovieAssets[i].Length);
            }

            this.RestOfData = reader.ReadBytesFromFileStream((int)(reader.Length - reader.Position));

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.JiminyRenderTargetTextureName));
            data.AddRange(BitConverter.GetBytes(this.ObjectProperty_1));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.JiminyRenderTargetTextureValue));

            data.AddRange(BitConverter.GetBytes(this.JiminyActorName));
            data.AddRange(BitConverter.GetBytes(this.AssetObjectProperty_1));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.JiminyActorLength));
            data.AddRange(this.JiminyActorValue);

            data.AddRange(BitConverter.GetBytes(this.DictionaryDataName));
            data.AddRange(BitConverter.GetBytes(this.ObjectProperty_2));
            data.AddRange(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.DictionaryDataValue));

            data.AddRange(BitConverter.GetBytes(this.KeywordGlossaryDataName));
            data.AddRange(BitConverter.GetBytes(this.ObjectProperty_3));
            data.AddRange(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.KeywordGlossaryDataValue));

            data.AddRange(BitConverter.GetBytes(this.AnsemCodeDataName));
            data.AddRange(BitConverter.GetBytes(this.ObjectProperty_4));
            data.AddRange(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.AnsemCodeDataValue));

            data.AddRange(BitConverter.GetBytes(this.StoryDataName));
            data.AddRange(BitConverter.GetBytes(this.ObjectProperty_5));
            data.AddRange(this.Unk6);
            data.AddRange(BitConverter.GetBytes(this.StoryDataValue));

            data.AddRange(BitConverter.GetBytes(this.LSIGameDataName));
            data.AddRange(BitConverter.GetBytes(this.ObjectProperty_6));
            data.AddRange(this.Unk7);
            data.AddRange(BitConverter.GetBytes(this.LSIGameDataValue));

            data.AddRange(BitConverter.GetBytes(this.SwfMovieAssetLSIButtonSetName));
            data.AddRange(BitConverter.GetBytes(this.AssetObjectProperty_3));
            data.AddRange(this.Unk8);
            data.AddRange(BitConverter.GetBytes(this.SwfMovieAssetLSIButtonSetLength));
            data.AddRange(this.SwfMovieAssetLSIButtonSetValue);

            data.AddRange(BitConverter.GetBytes(this.LSIGamePlayRewardItemName));
            data.AddRange(BitConverter.GetBytes(this.NameProperty));
            data.AddRange(this.Unk9);
            data.AddRange(BitConverter.GetBytes(this.LSIGamePlayRewardItemValue));
            data.AddRange(BitConverter.GetBytes(this.LSIGamePlayRewardItemValue2));

            data.AddRange(BitConverter.GetBytes(this.LSIMenuBGMName));
            data.AddRange(BitConverter.GetBytes(this.AssetObjectProperty_3));
            data.AddRange(this.Unk10);
            data.AddRange(BitConverter.GetBytes(this.LSIMenuBGMLength));
            data.AddRange(this.LSIMenuBGMValue);

            foreach (var item in this.SwfMovieAssets)
            {
                data.AddRange(BitConverter.GetBytes(item.Name));
                data.AddRange(BitConverter.GetBytes(item.AssetObjectProperty));
                data.AddRange(item.Unk);
                data.AddRange(BitConverter.GetBytes(item.Length));
                data.AddRange(item.Value);
            }

            data.AddRange(this.RestOfData);

            return data;
        }
    }
}
