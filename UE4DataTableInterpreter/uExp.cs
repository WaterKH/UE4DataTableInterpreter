using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UE4DataTableInterpreter.DataTables;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter
{
    public class uExp
    {
        public long Struct1 { get; set; }
        public long Property1 { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public int Unk2 { get; set; }
        
        public long Unk3 { get; set; }
        public int Id { get; set; }
        public int RowCount { get; set; }

        public Dictionary<string, IDataTable> DataTableEntries { get; set; } = new Dictionary<string, IDataTable>();

        public int UnkData { get; set; }

        private int magicCount = 10;
        public uExp Decompile<T>(FileStream reader, List<Asset> uAssetStrings) where T : IDataTable
        {
            this.Struct1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Property1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.Unk2 = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.Unk3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Id = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            this.RowCount = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());
            
            if (this.RowCount == 0)
                this.RowCount = 1;

            for (int i = 0; i < RowCount; ++i)
            {
                object temp = null;

                switch (typeof(T).Name)
                {
                    case "TreasureDataTableEntry":
                        temp = new TreasureDataTableEntry();
                        break;
                    case "VBonusDataTableEntry":
                        temp = new VBonusDataTableEntry();
                        break;
                    case "VBonusDataTableAltEntry":
                        temp = new VBonusDataTableAltEntry();
                        break;
                    case "LevelUpDataTableEntry":
                        temp = new LevelUpDataTableEntry();
                        break;
                    case "LevelUpDataTableAltEntry":
                        temp = new LevelUpDataTableAltEntry();
                        break;
                    case "EquipItemDataTableEntry":
                        temp = new EquipItemDataTableEntry();
                        break;
                    case "FullcourseAbilityDataTableEntry":
                        temp = new FullcourseAbilityDataTableEntry();
                        break;
                    case "LuckyMarkDataTableEntry":
                        temp = new LuckyMarkDataTableEntry();
                        break;
                    case "WeaponEnhanceDataTableEntry":
                        temp = new WeaponEnhanceDataTableEntry();
                        break;
                    case "EventDataTableEntry":
                        temp = new EventDataTableEntry();
                        break;
                    case "ChrInitDataTableEntry":
                        temp = new ChrInitDataTableEntry();
                        reader.Position = 0;
                        break;
                    case "MobilePortalDataTableEntry":
                        temp = new MobilePortalDataTableEntry();
                        reader.Position = 0;
                        break;
                    case "SynthesisItemDataTableEntry":
                        temp = new SynthesisItemDataTableEntry();
                        break;
                    case "QualityOfLifeDataTableEntry":
                        temp = new QualityOfLifeDataTableEntry();
                        break;
                    case "SecretReportInfoDataTableEntry":
                        temp = new SecretReportInfoDataTableEntry();
                        break;

                    case "FoodItemEffectDataTableEntry":
                        temp = new FoodItemEffectDataTableEntry();
                        break;

                    case "CharacterBaseDataTableEntry":
                        temp = new CharacterBaseDataTableEntry();
                        break;

                    case "CompletionConditionsDataTableEntry":
                        temp = new CompletionConditionsDataTableEntry();
                        break;
                    case "BossDataTableEntry":
                        temp = new BossDataTableEntry();
                        break;
                    case "PartyMemberDataTableEntry":
                        temp = new PartyMemberDataTableEntry();
                        break;
                    default:
                        break;
                }

                var dataTableEntry = ((IDataTable)temp).Decompile(reader);

                if (typeof(T).Name == "WeaponEnhanceDataTableEntry")
                    dataTableEntry.RowName = uAssetStrings[((WeaponEnhanceDataTableEntry)dataTableEntry).IW].AssetName.Replace("\0", $"_{dataTableEntry.IndexId - 1}");
                else if (typeof(T).Name == "ChrInitDataTableEntry")
                    dataTableEntry.RowName = uAssetStrings[((ChrInitDataTableEntry)dataTableEntry).m_PlayerSora.IndexId].AssetName.Replace("\0", "");
                else if (typeof(T).Name == "MobilePortalDataTableEntry")
                    dataTableEntry.RowName = uAssetStrings[^3].AssetName.Replace("\0", "");
                else if (typeof(T).Name == "SynthesisItemDataTableEntry")
                    dataTableEntry.RowName = uAssetStrings[((SynthesisItemDataTableEntry)dataTableEntry).IS].AssetName.Replace("\0", $"_{dataTableEntry.IndexId - 1}");
                else if (typeof(T).Name == "ShotlockDataTableEntry")
                    dataTableEntry.RowName = uAssetStrings[^1].AssetName.Replace("\0", "");
                else
                    dataTableEntry.RowName = uAssetStrings[dataTableEntry.IndexId].AssetName.Replace("\0", "");

                var key = dataTableEntry.RowName;
                if (dataTableEntry.RowName.ToLower() == "vbonus_debug_magic")
                {
                    key += $"_{magicCount}";
                    ++magicCount;
                }

                //this.AddToJsonData<T>(dataTableEntry, uAssetStrings);
                this.DataTableEntries.Add(key, dataTableEntry);
            }

            if (typeof(T).Name != "ChrInitDataTableEntry" && typeof(T).Name != "MobilePortalDataTableEntry")
                this.UnkData = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            return this;
        }

        public List<byte> Recompile<T>() where T : IDataTable
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Struct1));
            data.AddRange(BitConverter.GetBytes(this.Property1));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.Unk2));

            data.AddRange(BitConverter.GetBytes(this.Unk3));
            data.AddRange(BitConverter.GetBytes(this.Id));
            data.AddRange(BitConverter.GetBytes(this.RowCount));

            switch (typeof(T).Name)
            {
                case "TreasureDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((TreasureDataTableEntry)entry).Recompile());
                    break;
                case "VBonusDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((VBonusDataTableEntry)entry).Recompile());
                    break;
                case "VBonusDataTableAltEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((VBonusDataTableAltEntry)entry).Recompile());
                    break;
                case "LevelUpDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((LevelUpDataTableEntry)entry).Recompile());
                    break;
                case "LevelUpDataTableAltEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((LevelUpDataTableAltEntry)entry).Recompile());
                    break;
                case "EquipItemDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((EquipItemDataTableEntry)entry).Recompile());
                    break;
                case "FullcourseAbilityDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((FullcourseAbilityDataTableEntry)entry).Recompile());
                    break;
                case "LuckyMarkDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((LuckyMarkDataTableEntry)entry).Recompile());
                    break;
                case "WeaponEnhanceDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((WeaponEnhanceDataTableEntry)entry).Recompile());
                    break;
                case "EventDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((EventDataTableEntry)entry).Recompile());
                    break;
                case "ChrInitDataTableEntry":
                    data = new List<byte>();
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((ChrInitDataTableEntry)entry).Recompile());
                    break;
                case "MobilePortalDataTableEntry":
                    data = new List<byte>();
                    data.AddRange(((MobilePortalDataTableEntry)this.DataTableEntries.FirstOrDefault().Value).Recompile());
                    break;
                case "SynthesisItemDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((SynthesisItemDataTableEntry)entry).Recompile());
                    break;
                case "QualityOfLifeDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((QualityOfLifeDataTableEntry)entry).Recompile());
                    break;
                case "SecretReportInfoDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((SecretReportInfoDataTableEntry)entry).Recompile());
                    break;

                case "FoodItemEffectDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((FoodItemEffectDataTableEntry)entry).Recompile());
                    break;

                case "CharacterBaseDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((CharacterBaseDataTableEntry)entry).Recompile());
                    break;

                case "CompletionConditionsDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((CompletionConditionsDataTableEntry)entry).Recompile());
                    break;
                case "BossDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((BossDataTableEntry)entry).Recompile());
                    break;
                case "PartyMemberDataTableEntry":
                    foreach (var (rowName, entry) in this.DataTableEntries)
                        data.AddRange(((PartyMemberDataTableEntry)entry).Recompile());
                    break;
                default:
                    break;
            }

            if (typeof(T).Name != "ChrInitDataTableEntry" && typeof(T).Name != "MobilePortalDataTableEntry")
                data.AddRange(BitConverter.GetBytes(UnkData));

            return data;
        }

        public void AddToJsonData<T>(IDataTable dataTableEntry, List<Asset> uAssetStrings) where T : IDataTable
        {
            //if (typeof(T).Name == "EventDataTableEntry")
            //{
            //    Program.Data[Enums.DataTableEnum.Event].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //        { "RandomizedItem", uAssetStrings[(int)((EventDataTableEntry)dataTableEntry).randomizedItemValue].AssetName },
            //    });
            //}
            //else if (typeof(T).Name == "MobilePortalDataTableEntry")
            //{
            //    Program.Data[Enums.DataTableEnum.Event].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //        { "Reward", uAssetStrings[(int)((MobilePortalDataTableEntry)dataTableEntry).LSIGamePlayRewardItemValue].AssetName },
            //    });
            //}

            if (typeof(T).Name == "VBonusDataTableEntry" && dataTableEntry.RowName != "Vbonus_debug_Magic")
            {
                Program.Data[Enums.DataTableEnum.VBonus].Add(dataTableEntry.RowName, new Dictionary<string, string>{
                    { "Sora_Bonus1", uAssetStrings[(int)((VBonusDataTableEntry)dataTableEntry).m_Sora1.ETresVictoryBonusKind].AssetName },
                    { "Sora_Ability1", uAssetStrings[(int)((VBonusDataTableEntry)dataTableEntry).m_Sora1.ETresVictoryAbilityType].AssetName },
                    { "Sora_Bonus2", uAssetStrings[(int)((VBonusDataTableEntry)dataTableEntry).m_Sora2.ETresVictoryBonusKind].AssetName },
                    { "Sora_Ability2", uAssetStrings[(int)((VBonusDataTableEntry)dataTableEntry).m_Sora2.ETresVictoryAbilityType].AssetName },
                });
            }

            //if (typeof(T).Name == "TreasureDataTableEntry")
            //{
            //    if (dataTableEntry.RowName.Substring(0, 2) == "BT")
            //        Program.Data[Enums.DataTableEnum.TreasureBT].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //    else if (dataTableEntry.RowName.Substring(0, 2) == "BX")
            //        Program.Data[Enums.DataTableEnum.TreasureBX].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //    else if (dataTableEntry.RowName.Substring(0, 2) == "CA")
            //        Program.Data[Enums.DataTableEnum.TreasureCA].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //    else if (dataTableEntry.RowName.Substring(0, 2) == "EW")
            //        Program.Data[Enums.DataTableEnum.TreasureEW].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //    else if (dataTableEntry.RowName.Substring(0, 2) == "FZ")
            //        Program.Data[Enums.DataTableEnum.TreasureFZ].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //    else if (dataTableEntry.RowName.Substring(0, 2) == "HE")
            //        Program.Data[Enums.DataTableEnum.TreasureHE].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //    else if (dataTableEntry.RowName.Substring(0, 2) == "KG")
            //        Program.Data[Enums.DataTableEnum.TreasureKG].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //    else if (dataTableEntry.RowName.Substring(0, 2) == "MI")
            //        Program.Data[Enums.DataTableEnum.TreasureMI].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //    else if (dataTableEntry.RowName.Substring(0, 2) == "RA")
            //        Program.Data[Enums.DataTableEnum.TreasureRA].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //    else if (dataTableEntry.RowName.Substring(0, 2) == "TS")
            //        Program.Data[Enums.DataTableEnum.TreasureTS].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //    else if (dataTableEntry.RowName.Substring(0, 2) == "TT")
            //        Program.Data[Enums.DataTableEnum.TreasureTT].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "Treasure", uAssetStrings[(int)((TreasureDataTableEntry)dataTableEntry).Treasure].AssetName }
            //        });
            //}
            //else if (typeof(T).Name == "VBonusDataTableEntry")
            //{
            //    Program.Data[Enums.DataTableEnum.VBonus].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //        { "Sora_Bonus1", uAssetStrings[(int)((VBonusDataTableEntry)dataTableEntry).m_Sora1.ETresVictoryBonusKind].AssetName },
            //        { "Sora_Ability1", uAssetStrings[(int)((VBonusDataTableEntry)dataTableEntry).m_Sora1.ETresVictoryAbilityType].AssetName },
            //        { "Sora_Bonus2", uAssetStrings[(int)((VBonusDataTableEntry)dataTableEntry).m_Sora2.ETresVictoryBonusKind].AssetName },
            //        { "Sora_Ability2", uAssetStrings[(int)((VBonusDataTableEntry)dataTableEntry).m_Sora2.ETresVictoryAbilityType].AssetName },
            //    });
            //}
            //else if (typeof(T).Name == "LevelUpDataTableEntry")
            //{
            //    if (int.Parse(dataTableEntry.RowName) > 0 && int.Parse(dataTableEntry.RowName) <= 50)
            //    {
            //        Program.Data[Enums.DataTableEnum.LevelUp].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //            { "TypeA", uAssetStrings[(int)((LevelUpDataTableEntry)dataTableEntry).AbilityValue_1].AssetName },
            //            { "TypeB", uAssetStrings[(int)((LevelUpDataTableEntry)dataTableEntry).AbilityValue_2].AssetName },
            //            { "TypeC", uAssetStrings[(int)((LevelUpDataTableEntry)dataTableEntry).AbilityValue_3].AssetName },
            //        });
            //    }
            //}
            //else if (typeof(T).Name == "EquipItemDataTableEntry")
            //{
            //    var tempDict = new Dictionary<string, string>();
            //    for (int i = 0; i < ((EquipItemDataTableEntry)dataTableEntry).AbilityCount; ++i)
            //        tempDict.Add($"Ability{i}", uAssetStrings[(int)((EquipItemDataTableEntry)dataTableEntry).Abilities[i]].AssetName);

            //    Program.Data[Enums.DataTableEnum.EquipItem].Add(dataTableEntry.RowName, tempDict);
            //}
            //else if (typeof(T).Name == "FullcourseAbilityDataTableEntry")
            //{
            //    Program.Data[Enums.DataTableEnum.FullcourseAbility].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //        { "Ability", uAssetStrings[(int)((FullcourseAbilityDataTableEntry)dataTableEntry).Ability].AssetName },
            //    });
            //}
            //else if (typeof(T).Name == "LuckyMarkDataTableEntry")
            //{
            //    Program.Data[Enums.DataTableEnum.LuckyMark].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //        { "Reward", uAssetStrings[(int)((LuckyMarkDataTableEntry)dataTableEntry).Treasure].AssetName },
            //    });
            //}
            //else if (typeof(T).Name == "WeaponEnhanceDataTableEntry")
            //{
            //    Program.Data[Enums.DataTableEnum.WeaponEnhance].Add(dataTableEntry.RowName, new Dictionary<string, string>{
            //        { "Ability", uAssetStrings[(int)((WeaponEnhanceDataTableEntry)dataTableEntry).Ability].AssetName },
            //    });
            //}
            //else if (typeof(T).Name == "ChrInitDataTableEntry")
            //{
            //    if (dataTableEntry.RowName == "m_PlayerSora")
            //    {
            //        var chrInitDataTableEntry = (ChrInitDataTableEntry)dataTableEntry;
            //        var tempDict = new Dictionary<string, string>();

            //        for (int i = 0; i < chrInitDataTableEntry.EquipAbilityCount; ++i)
            //            tempDict.Add($"EquipAbility{i}", uAssetStrings[(int)chrInitDataTableEntry.EquipAbilities[i]].AssetName);

            //        for (int i = 0; i < chrInitDataTableEntry.CritEquipCount; ++i)
            //            tempDict.Add($"CritEquipAbility{i}", uAssetStrings[(int)chrInitDataTableEntry.CritEquipAbilities[i]].AssetName);

            //        tempDict.Add("Weapon", uAssetStrings[(int)chrInitDataTableEntry.Weapons[0]].AssetName);
            //        tempDict.Add("HaveAbility", uAssetStrings[(int)chrInitDataTableEntry.HaveAbilities[0]].AssetName);
            //        tempDict.Add("CritHaveAbility", uAssetStrings[(int)chrInitDataTableEntry.CritHaveAbilities[0]].AssetName);

            //        Program.Data[Enums.DataTableEnum.ChrInit].Add(dataTableEntry.RowName, tempDict);
            //    }
            //}
        }
    }
}