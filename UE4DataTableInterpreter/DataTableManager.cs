using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using UE4DataTableInterpreter.DataTables;
using UE4DataTableInterpreter.Enums;

namespace UE4DataTableInterpreter
{
    public class DataTableManager
    {
        /// <summary>
        /// Randomizes items in the available pools.
        /// </summary>
        /// <param name="randomSeed">The random seed generated along with the values.</param>
        /// <param name="randomizedValues">This dictionary has the following pattern: Key: DataTableEnum - Value: { Key: - Value: { Key: EntryIndex - Value: {Key: ValueToChangeName - Value: RandomizedValue } }.</param>
        public Dictionary<string, List<byte>> RandomizeDataTables(Dictionary<DataTableEnum, Dictionary<string, Dictionary<string, string>>> randomizedValues)
        {
            var recompiledFiles = new Dictionary<string, List<byte>>();

            using var streamReader = new StreamReader("uAssetIds.json");
            var uAssetIds = JsonSerializer.Deserialize<Dictionary<string, uint>>(streamReader.ReadToEnd());

            foreach (var (dataTableEnum, entries) in randomizedValues)
            {   
                if (dataTableEnum == DataTableEnum.Shotlock)
                    continue;

                // Decompile uAsset
                var uAsset = new uAsset();
                var path = "";


                // Only for VBonus for now
                var uAssetAlt = new uAsset();
                var subPath = "";

                switch (dataTableEnum)
                {
                    case DataTableEnum.ChrInit:
                        path = @"Content/Load/Common/TresChrInitData";
                        break;
                    case DataTableEnum.EquipItem:
                        path = @"Content/Load/Tres/TresEquipItemData";
                        break;
                    case DataTableEnum.FullcourseAbility:
                        path = @"Content/Load/Tres/TresFullcourseAbilityList";
                        break;
                    case DataTableEnum.LevelUp:
                        path = @"Content/DataTable/Player/LevelUp/p_ex001_LevelUpData";
                        subPath = @"Content/DataTable/Player/LevelUp/p_ex001_LevelUpLookupData";
                        break;
                    case DataTableEnum.LuckyMark:
                        path = @"Content/Load/Tres/TresLuckyMarkMilestoneRewardData";
                        break;
                    case DataTableEnum.VBonus:
                        path = @"Content/Load/Tres/TresVBonusData";
                        subPath = @"Content/Load/Tres/TresVBonusTableAlt";
                        break;
                    case DataTableEnum.WeaponEnhance:
                        path = @"Content/Load/Tres/ItemSynthesis/TresItemWeaponEnhanceData";
                        break;
                    case DataTableEnum.Event:
                        path = @"Content/Load/Tres/TresEventData";
                        subPath = @"Content/Game/UI/Data/MobilePortal/MobilePortalDataAsset";
                        break;
                    case DataTableEnum.SynthesisItem:
                        path = @"Content/Load/Tres/ItemSynthesis/TresItemSynthesisData";
                        break;


                    case DataTableEnum.TreasureBT:
                        path = @"Content/Load/Tres/TresTreasureDataBT";
                        break;
                    case DataTableEnum.TreasureBX:
                        path = @"Content/Load/Tres/TresTreasureDataBX";
                        break;
                    case DataTableEnum.TreasureCA:
                        path = @"Content/Load/Tres/TresTreasureDataCA";
                        break;
                    case DataTableEnum.TreasureEW:
                        path = @"Content/Load/Tres/TresTreasureDataEW";
                        break;
                    case DataTableEnum.TreasureFZ:
                        path = @"Content/Load/Tres/TresTreasureDataFZ";
                        break;
                    case DataTableEnum.TreasureHE:
                        path = @"Content/Load/Tres/TresTreasureDataHE";
                        break;
                    case DataTableEnum.TreasureKG:
                        path = @"Content/Load/Tres/TresTreasureDataKG";
                        break;
                    case DataTableEnum.TreasureMI:
                        path = @"Content/Load/Tres/TresTreasureDataMI";
                        break;
                    case DataTableEnum.TreasureRA:
                        path = @"Content/Load/Tres/TresTreasureDataRA";
                        break;
                    case DataTableEnum.TreasureTS:
                        path = @"Content/Load/Tres/TresTreasureDataTS";
                        break;
                    case DataTableEnum.TreasureTT:
                        path = @"Content/Load/Tres/TresTreasureDataTT";
                        break;
                    default:
                        break;
                }

                using var reader = File.OpenRead($"{path}.uasset");

                // modifies the existing uAsset (+ returns itself, but we won't need that here)
                uAsset.Decompile(reader);

                if (dataTableEnum == DataTableEnum.ChrInit)
                {
                    reader.Position -= (0xB0 + 0x4 + 0x54);
                    uAsset.DuplicateData = reader.ReadBytesFromFileStream(0xB68); // 0xB68 for ChrInit
                    uAsset.FinalLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray()); // 0x4 for ChrInit
                    uAsset.DuplicateData2 = reader.ReadBytesFromFileStream(0x118); // 0x118 for ChrInit
                }
                else if (dataTableEnum == DataTableEnum.VBonus || dataTableEnum == DataTableEnum.LevelUp || dataTableEnum == DataTableEnum.Event)
                {
                    using var subReader = File.OpenRead($"{subPath}.uasset");

                    uAssetAlt.Decompile(subReader);

                    if (dataTableEnum == DataTableEnum.Event)
                    {
                        subReader.Position -= (0xB0 + 0x4 + 0x54);
                        uAssetAlt.DuplicateData = subReader.ReadBytesFromFileStream(0x1C8); // 0x1C8 for MobilePortal
                        uAssetAlt.FinalLength = BitConverter.ToInt32(subReader.ReadBytesFromFileStream(4).ToArray()); // 0x4 for MobilePortal
                        uAssetAlt.DuplicateData2 = subReader.ReadBytesFromFileStream(0x68); // 0x118 for MobilePortal
                    }

                    subReader.Flush();
                    subReader.Close();
                }

                reader.Flush();
                reader.Close();

                // Decompile uExp
                var uExp = new uExp();

                // Only for VBonus for now
                var uExpAlt = new uExp();

                using var readerExp = File.OpenRead($"{path}.uexp");

                if (dataTableEnum == DataTableEnum.VBonus)
                {
                    using var subExpReader = File.OpenRead($"{subPath}.uexp");

                    uExpAlt.Decompile<VBonusDataTableAltEntry>(subExpReader, uAssetAlt.AssetStrings);
                }
                else if (dataTableEnum == DataTableEnum.LevelUp)
                {
                    using var subExpReader = File.OpenRead($"{subPath}.uexp");

                    uExpAlt.Decompile<LevelUpDataTableAltEntry>(subExpReader, uAssetAlt.AssetStrings);
                }
                else if (dataTableEnum == DataTableEnum.Event)
                {
                    using var subExpReader = File.OpenRead($"{subPath}.uexp");

                    uExpAlt.Decompile<MobilePortalDataTableEntry>(subExpReader, uAssetAlt.AssetStrings);
                }

                // modifies the existing uExp (+ returns itself, but we won't need that here)
                switch (dataTableEnum)
                {
                    case DataTableEnum.ChrInit:
                        uExp.Decompile<ChrInitDataTableEntry>(readerExp, uAsset.AssetStrings); 
                        break;
                    case DataTableEnum.EquipItem:
                        uExp.Decompile<EquipItemDataTableEntry>(readerExp, uAsset.AssetStrings); 
                        break;
                    case DataTableEnum.FullcourseAbility:
                        uExp.Decompile<FullcourseAbilityDataTableEntry>(readerExp, uAsset.AssetStrings);
                        break;
                    case DataTableEnum.LevelUp:
                        uExp.Decompile<LevelUpDataTableEntry>(readerExp, uAsset.AssetStrings);
                        break;
                    case DataTableEnum.LuckyMark:
                        uExp.Decompile<LuckyMarkDataTableEntry>(readerExp, uAsset.AssetStrings);
                        break;
                    case DataTableEnum.VBonus:
                        uExp.Decompile<VBonusDataTableEntry>(readerExp, uAsset.AssetStrings);
                        break;
                    case DataTableEnum.WeaponEnhance:
                        uExp.Decompile<WeaponEnhanceDataTableEntry>(readerExp, uAsset.AssetStrings);
                        break;
                    case DataTableEnum.Event:
                        uExp.Decompile<EventDataTableEntry>(readerExp, uAsset.AssetStrings);
                        break;
                    case DataTableEnum.SynthesisItem:
                        uExp.Decompile<SynthesisItemDataTableEntry>(readerExp, uAsset.AssetStrings);
                        break;


                    case DataTableEnum.TreasureBT:
                    case DataTableEnum.TreasureBX:
                    case DataTableEnum.TreasureCA:
                    case DataTableEnum.TreasureEW:
                    case DataTableEnum.TreasureFZ:
                    case DataTableEnum.TreasureHE:
                    case DataTableEnum.TreasureKG:
                    case DataTableEnum.TreasureMI:
                    case DataTableEnum.TreasureRA:
                    case DataTableEnum.TreasureTS:
                    case DataTableEnum.TreasureTT:
                        uExp.Decompile<TreasureDataTableEntry>(readerExp, uAsset.AssetStrings);
                        break;
                    default:
                        break;
                }


                readerExp.Flush();
                readerExp.Close();

                // Change items according to the randomizedValues
                foreach (var (entryIndex, items) in entries)
                {
                    if (entryIndex.Contains("GIVESORA"))
                        continue;

                    var assetIndex = -1;

                    foreach(var (keyName, randomizedValue) in items)
                    {
                        // Look up Index if it exists
                        if (uAsset.AssetStrings.Exists(x => x.AssetName == randomizedValue))
                        {
                            var asset = uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == randomizedValue);
                            assetIndex = uAsset.AssetStrings.IndexOf(asset);
                        }
                        else
                        {
                            uAsset.AssetStrings.Add(new Asset { AssetName = randomizedValue, Length = randomizedValue.Length, Id = uAssetIds[randomizedValue] });
                            assetIndex = uAsset.AssetStrings.Count - 1;

                            // Update All Apparent Lengths in uAsset
                            uAsset.FileSize += randomizedValue.Length + 8;
                            uAsset.UnkLength3 += 1;
                            uAsset.Unk5 += randomizedValue.Length + 8;
                            uAsset.SubSize1 += randomizedValue.Length + 8;
                            uAsset.SubSize2 += randomizedValue.Length + 8;
                            uAsset.Unk16 += randomizedValue.Length + 8;
                            uAsset.Unk17 += randomizedValue.Length + 8;
                            uAsset.Unk19 += randomizedValue.Length + 8;
                            uAsset.FinalLength += randomizedValue.Length + 8;
                        }


                        // Update uExp
                        switch (dataTableEnum)
                        {
                            case DataTableEnum.ChrInit:
                                if (keyName.Contains("EquipAbility") && !keyName.Contains("Crit"))
                                {
                                    var equipAbilityIndex = int.Parse(keyName.Replace("EquipAbility", ""));
                                    ((ChrInitDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_PlayerSora.EquipAbilities[equipAbilityIndex] = assetIndex;
                                }
                                else if (keyName.Contains("CritEquipAbility"))
                                {
                                    var equipAbilityIndex = int.Parse(keyName.Replace("CritEquipAbility", ""));
                                    ((ChrInitDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_PlayerSora.CritEquipAbilities[equipAbilityIndex] = assetIndex;
                                }
                                else if (keyName == "Weapon")
                                {
                                    ((ChrInitDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_PlayerSora.Weapons[0] = assetIndex;
                                }
                                else if (keyName == "HaveAbility" && !keyName.Contains("Crit"))
                                {
                                    ((ChrInitDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_PlayerSora.HaveAbilities[0] = assetIndex;
                                }
                                else if (keyName == "CritHaveAbility")
                                {
                                    ((ChrInitDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_PlayerSora.CritHaveAbilities[0] = assetIndex;
                                }
                                break;
                            case DataTableEnum.EquipItem:
                                if (keyName.Contains("Ability"))
                                {
                                    var abilityIndex = int.Parse(keyName.Replace("Ability", ""));
                                    ((EquipItemDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Abilities[abilityIndex] = assetIndex;
                                }
                                break;
                            case DataTableEnum.FullcourseAbility:
                                ((FullcourseAbilityDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Ability = assetIndex;
                                break;
                            case DataTableEnum.LevelUp:
                                if (keyName == "TypeA")
                                {
                                    if (randomizedValue.Split("::")[0] == "ETresAbilityKind")
                                    {
                                        ((LevelUpDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).AbilityValue_1 = assetIndex;
                                    }
                                    else
                                    {
                                        var assetAltIndex = -1;

                                        // Look up Index if it exists
                                        if (uAssetAlt.AssetStrings.Exists(x => x.AssetName == randomizedValue))
                                        {
                                            var assetAlt = uAssetAlt.AssetStrings.FirstOrDefault(x => x.AssetName == randomizedValue);
                                            assetAltIndex = uAssetAlt.AssetStrings.IndexOf(assetAlt);
                                        }
                                        else
                                        {
                                            uAssetAlt.AssetStrings.Add(new Asset { AssetName = randomizedValue, Length = randomizedValue.Length, Id = uAssetIds[randomizedValue] });
                                            assetAltIndex = uAssetAlt.AssetStrings.Count - 1;

                                            // Update All Apparent Lengths in uAsset
                                            uAssetAlt.FileSize += randomizedValue.Length + 8;
                                            uAssetAlt.UnkLength3 += 1;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.FinalLength += randomizedValue.Length + 8;
                                        }

                                        var none = uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == "ETresAbilityKind::NONE\u0000");
                                        var noneIndex = uAsset.AssetStrings.IndexOf(none);
                                        ((LevelUpDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).AbilityValue_1 = noneIndex;
                                        ((LevelUpDataTableAltEntry)uExpAlt.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Ability1Value = assetAltIndex;
                                    }
                                }
                                else if (keyName == "TypeB")
                                {
                                    if (randomizedValue.Split("::")[0] == "ETresAbilityKind")
                                    {
                                        ((LevelUpDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).AbilityValue_2 = assetIndex;
                                    }
                                    else
                                    {
                                        var assetAltIndex = -1;

                                        // Look up Index if it exists
                                        if (uAssetAlt.AssetStrings.Exists(x => x.AssetName == randomizedValue))
                                        {
                                            var assetAlt = uAssetAlt.AssetStrings.FirstOrDefault(x => x.AssetName == randomizedValue);
                                            assetAltIndex = uAssetAlt.AssetStrings.IndexOf(assetAlt);
                                        }
                                        else
                                        {
                                            uAssetAlt.AssetStrings.Add(new Asset { AssetName = randomizedValue, Length = randomizedValue.Length, Id = uAssetIds[randomizedValue] });
                                            assetAltIndex = uAssetAlt.AssetStrings.Count - 1;

                                            // Update All Apparent Lengths in uAsset
                                            uAssetAlt.FileSize += randomizedValue.Length + 8;
                                            uAssetAlt.UnkLength3 += 1;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.FinalLength += randomizedValue.Length + 8;
                                        }

                                        var none = uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == "ETresAbilityKind::NONE\u0000");
                                        var noneIndex = uAsset.AssetStrings.IndexOf(none);
                                        ((LevelUpDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).AbilityValue_2 = noneIndex;
                                        ((LevelUpDataTableAltEntry)uExpAlt.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Ability2Value = assetAltIndex;
                                    }
                                }
                                else if (keyName == "TypeC")
                                {
                                    if (randomizedValue.Split("::")[0] == "ETresAbilityKind")
                                    {
                                        ((LevelUpDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).AbilityValue_3 = assetIndex;
                                    }
                                    else
                                    {
                                        var assetAltIndex = -1;

                                        // Look up Index if it exists
                                        if (uAssetAlt.AssetStrings.Exists(x => x.AssetName == randomizedValue))
                                        {
                                            var assetAlt = uAssetAlt.AssetStrings.FirstOrDefault(x => x.AssetName == randomizedValue);
                                            assetAltIndex = uAssetAlt.AssetStrings.IndexOf(assetAlt);
                                        }
                                        else
                                        {
                                            uAssetAlt.AssetStrings.Add(new Asset { AssetName = randomizedValue, Length = randomizedValue.Length, Id = uAssetIds[randomizedValue] });
                                            assetAltIndex = uAssetAlt.AssetStrings.Count - 1;

                                            // Update All Apparent Lengths in uAsset
                                            uAssetAlt.FileSize += randomizedValue.Length + 8;
                                            uAssetAlt.UnkLength3 += 1;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.FinalLength += randomizedValue.Length + 8;
                                        }

                                        var none = uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == "ETresAbilityKind::NONE\u0000");
                                        var noneIndex = uAsset.AssetStrings.IndexOf(none);
                                        ((LevelUpDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).AbilityValue_3 = noneIndex;
                                        ((LevelUpDataTableAltEntry)uExpAlt.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Ability3Value = assetAltIndex;
                                    }
                                }
                                break;
                            case DataTableEnum.LuckyMark:
                                ((LuckyMarkDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Treasure = assetIndex;
                                break;
                            case DataTableEnum.VBonus:
                                if (keyName == "Sora_Bonus1")
                                {
                                    if (randomizedValue.Split("::")[0] == "ETresVictoryBonusKind")
                                    {
                                        ((VBonusDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_Sora1.ETresVictoryBonusKind = assetIndex;
                                    }
                                    else
                                    {
                                        var assetAltIndex = -1;

                                        // Look up Index if it exists
                                        if (uAssetAlt.AssetStrings.Exists(x => x.AssetName == randomizedValue))
                                        {
                                            var assetAlt = uAssetAlt.AssetStrings.FirstOrDefault(x => x.AssetName == randomizedValue);
                                            assetAltIndex = uAssetAlt.AssetStrings.IndexOf(assetAlt);
                                        }
                                        else
                                        {
                                            uAssetAlt.AssetStrings.Add(new Asset { AssetName = randomizedValue, Length = randomizedValue.Length, Id = uAssetIds[randomizedValue] });
                                            assetAltIndex = uAssetAlt.AssetStrings.Count - 1;

                                            // Update All Apparent Lengths in uAsset
                                            uAssetAlt.FileSize += randomizedValue.Length + 8;
                                            uAssetAlt.UnkLength3 += 1;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.FinalLength += randomizedValue.Length + 8;
                                        }

                                        var none = uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == "ETresVictoryBonusKind::NONE\u0000");
                                        var noneIndex = uAsset.AssetStrings.IndexOf(none);
                                        ((VBonusDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_Sora1.ETresVictoryBonusKind = noneIndex;
                                        ((VBonusDataTableAltEntry)uExpAlt.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Bonus1Value = assetAltIndex;
                                    }
                                }
                                else if (keyName == "Sora_Bonus2")
                                {
                                    if (randomizedValue.Split("::")[0] == "ETresVictoryBonusKind")
                                    {
                                        ((VBonusDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_Sora2.ETresVictoryBonusKind = assetIndex;
                                    }
                                    else
                                    {
                                        var assetAltIndex = -1;

                                        // Look up Index if it exists
                                        if (uAssetAlt.AssetStrings.Exists(x => x.AssetName == randomizedValue))
                                        {
                                            var assetAlt = uAssetAlt.AssetStrings.FirstOrDefault(x => x.AssetName == randomizedValue);
                                            assetAltIndex = uAssetAlt.AssetStrings.IndexOf(assetAlt);
                                        }
                                        else
                                        {
                                            uAssetAlt.AssetStrings.Add(new Asset { AssetName = randomizedValue, Length = randomizedValue.Length, Id = uAssetIds[randomizedValue] });
                                            assetAltIndex = uAssetAlt.AssetStrings.Count - 1;

                                            // Update All Apparent Lengths in uAsset
                                            uAssetAlt.FileSize += randomizedValue.Length + 8;
                                            uAssetAlt.UnkLength3 += 1;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.FinalLength += randomizedValue.Length + 8;
                                        }

                                        var none = uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == "ETresVictoryBonusKind::NONE\u0000");
                                        var noneIndex = uAsset.AssetStrings.IndexOf(none);
                                        ((VBonusDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_Sora2.ETresVictoryBonusKind = noneIndex;
                                        ((VBonusDataTableAltEntry)uExpAlt.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Bonus2Value = assetAltIndex;
                                    }
                                }
                                else if (keyName == "Sora_Ability1")
                                {
                                    if (randomizedValue.Split("::")[0] == "ETresAbilityKind")
                                    {
                                        ((VBonusDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_Sora1.ETresVictoryAbilityType = assetIndex;
                                    }
                                    else
                                    {
                                        var assetAltIndex = -1;

                                        // Look up Index if it exists
                                        if (uAssetAlt.AssetStrings.Exists(x => x.AssetName == randomizedValue))
                                        {
                                            var assetAlt = uAssetAlt.AssetStrings.FirstOrDefault(x => x.AssetName == randomizedValue);
                                            assetAltIndex = uAssetAlt.AssetStrings.IndexOf(assetAlt);
                                        }
                                        else
                                        {
                                            uAssetAlt.AssetStrings.Add(new Asset { AssetName = randomizedValue, Length = randomizedValue.Length, Id = uAssetIds[randomizedValue] });
                                            assetAltIndex = uAssetAlt.AssetStrings.Count - 1;

                                            // Update All Apparent Lengths in uAsset
                                            uAssetAlt.FileSize += randomizedValue.Length + 8;
                                            uAssetAlt.UnkLength3 += 1;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.FinalLength += randomizedValue.Length + 8;
                                        }

                                        var none = uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == "ETresAbilityKind::NONE\u0000");
                                        var noneIndex = uAsset.AssetStrings.IndexOf(none);
                                        ((VBonusDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_Sora1.ETresVictoryAbilityType = noneIndex;
                                        ((VBonusDataTableAltEntry)uExpAlt.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Ability1Value = assetAltIndex;
                                    }
                                }
                                else if (keyName == "Sora_Ability2")
                                {
                                    if (randomizedValue.Split("::")[0] == "ETresAbilityKind")
                                    {
                                        ((VBonusDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_Sora2.ETresVictoryAbilityType = assetIndex;
                                    }
                                    else
                                    {
                                        var assetAltIndex = -1;

                                        // Look up Index if it exists
                                        if (uAssetAlt.AssetStrings.Exists(x => x.AssetName == randomizedValue))
                                        {
                                            var assetAlt = uAssetAlt.AssetStrings.FirstOrDefault(x => x.AssetName == randomizedValue);
                                            assetAltIndex = uAssetAlt.AssetStrings.IndexOf(assetAlt);
                                        }
                                        else
                                        {
                                            uAssetAlt.AssetStrings.Add(new Asset { AssetName = randomizedValue, Length = randomizedValue.Length, Id = uAssetIds[randomizedValue] });
                                            assetAltIndex = uAssetAlt.AssetStrings.Count - 1;

                                            // Update All Apparent Lengths in uAsset
                                            uAssetAlt.FileSize += randomizedValue.Length + 8;
                                            uAssetAlt.UnkLength3 += 1;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.FinalLength += randomizedValue.Length + 8;
                                        }

                                        var none = uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == "ETresAbilityKind::NONE\u0000");
                                        var noneIndex = uAsset.AssetStrings.IndexOf(none);
                                        ((VBonusDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).m_Sora2.ETresVictoryAbilityType = noneIndex;
                                        ((VBonusDataTableAltEntry)uExpAlt.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Ability2Value = assetAltIndex;
                                    }
                                }
                                break;
                            case DataTableEnum.WeaponEnhance:
                                ((WeaponEnhanceDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Ability = assetIndex;
                                break;
                            case DataTableEnum.Event:
                                if (keyName == "RandomizedItem")
                                {
                                    ((EventDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).randomizedItemValue = assetIndex;
                                }
                                else if (keyName == "Reward")
                                {
                                    var assetAltIndex = -1;

                                    // Look up Index if it exists
                                    if (uAssetAlt.AssetStrings.Exists(x => x.AssetName == randomizedValue))
                                    {
                                        var assetAlt = uAssetAlt.AssetStrings.FirstOrDefault(x => x.AssetName == randomizedValue);
                                        assetAltIndex = uAssetAlt.AssetStrings.IndexOf(assetAlt);
                                    }
                                    else
                                    {
                                        uAssetAlt.AssetStrings.Add(new Asset { AssetName = randomizedValue, Length = randomizedValue.Length, Id = uAssetIds[randomizedValue] });
                                        assetAltIndex = uAssetAlt.AssetStrings.Count - 1;

                                        // Update All Apparent Lengths in uAsset
                                        uAssetAlt.FileSize += randomizedValue.Length + 8;
                                        uAssetAlt.UnkLength3 += 1;
                                        uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                        uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                        uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                        uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                        uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                        uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                        uAssetAlt.FinalLength += randomizedValue.Length + 8;
                                    }

                                    ((MobilePortalDataTableEntry)uExpAlt.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).LSIGamePlayRewardItemValue = assetAltIndex;
                                }
                                break;
                            case DataTableEnum.SynthesisItem:
                                ((SynthesisItemDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Reward = assetIndex;
                                break;

                            case DataTableEnum.TreasureBT:
                            case DataTableEnum.TreasureBX:
                            case DataTableEnum.TreasureCA:
                            case DataTableEnum.TreasureEW:
                            case DataTableEnum.TreasureFZ:
                            case DataTableEnum.TreasureHE:
                            case DataTableEnum.TreasureKG:
                            case DataTableEnum.TreasureMI:
                            case DataTableEnum.TreasureRA:
                            case DataTableEnum.TreasureTS:
                            case DataTableEnum.TreasureTT:
                                ((TreasureDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).Treasure = assetIndex;
                                break;
                            default:
                                break;
                        }
                    }
                }


                // Recompile uAsset
                var uAssetFileBytes = uAsset.Recompile();


                // Recompile uExp
                var uExpFileBytes = new List<byte>();

                switch (dataTableEnum)
                {
                    case DataTableEnum.ChrInit:
                        uExpFileBytes = uExp.Recompile<ChrInitDataTableEntry>();
                        break;
                    case DataTableEnum.EquipItem:
                        uExpFileBytes = uExp.Recompile<EquipItemDataTableEntry>();
                        break;
                    case DataTableEnum.FullcourseAbility:
                        uExpFileBytes = uExp.Recompile<FullcourseAbilityDataTableEntry>();
                        break;
                    case DataTableEnum.LevelUp:
                        uExpFileBytes = uExp.Recompile<LevelUpDataTableEntry>();
                        break;
                    case DataTableEnum.LuckyMark:
                        uExpFileBytes = uExp.Recompile<LuckyMarkDataTableEntry>();
                        break;
                    case DataTableEnum.VBonus:
                        uExpFileBytes = uExp.Recompile<VBonusDataTableEntry>();
                        break;
                    case DataTableEnum.WeaponEnhance:
                        uExpFileBytes = uExp.Recompile<WeaponEnhanceDataTableEntry>();
                        break;
                    case DataTableEnum.Event:
                        uExpFileBytes = uExp.Recompile<EventDataTableEntry>();
                        break;
                    case DataTableEnum.SynthesisItem:
                        uExpFileBytes = uExp.Recompile<SynthesisItemDataTableEntry>();
                        break;


                    case DataTableEnum.TreasureBT:
                    case DataTableEnum.TreasureBX:
                    case DataTableEnum.TreasureCA:
                    case DataTableEnum.TreasureEW:
                    case DataTableEnum.TreasureFZ:
                    case DataTableEnum.TreasureHE:
                    case DataTableEnum.TreasureKG:
                    case DataTableEnum.TreasureMI:
                    case DataTableEnum.TreasureRA:
                    case DataTableEnum.TreasureTS:
                    case DataTableEnum.TreasureTT:
                        uExpFileBytes = uExp.Recompile<TreasureDataTableEntry>();
                        break;
                    default:
                        break;
                }

                // Add Recompiled uAsset + uExp to recompiledFiles
                recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);

                // Add Recompiled ALT uAsset + uExp to recompiledFiles
                if (dataTableEnum == DataTableEnum.VBonus)
                {
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uasset", uAssetAlt.Recompile());
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uexp", uExpAlt.Recompile<VBonusDataTableAltEntry>());
                }
                else if (dataTableEnum == DataTableEnum.LevelUp)
                {
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uasset", uAssetAlt.Recompile());
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uexp", uExpAlt.Recompile<LevelUpDataTableAltEntry>());
                }
                else if (dataTableEnum == DataTableEnum.Event)
                {
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uasset", uAssetAlt.Recompile());
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uexp", uExpAlt.Recompile<MobilePortalDataTableEntry>());
                }
            }

            // TODO Shotlocks
            if (randomizedValues.ContainsKey(DataTableEnum.Shotlock))
            {
                foreach (var (replaceShotlock, values) in randomizedValues[DataTableEnum.Shotlock])
                {
                    var newShotlock = values.FirstOrDefault().Key;

                    // Replace shotlock file name with new name (for both uasset + uexp),
                    // Then find the string inside of the uassets and update to new 
                    
                    // Decompile uAsset
                    var uAsset = new uAsset();
                    var path = $"Content/Blueprints/Player/p_ex001/{replaceShotlock}";
                    var newPath = $"Content/Blueprints/Player/p_ex001/{newShotlock}";

                    using var reader = File.OpenRead($"{path}_ProjSet.uasset");

                    // modifies the existing uAsset (+ returns itself, but we won't need that here)
                    uAsset.Decompile(reader);

                    reader.Position -= (0xB0 + 0x4 + 0x54);
                    uAsset.DuplicateData = reader.ReadBytesFromFileStream(0x1); // 0xB68 for Shotlock
                    uAsset.FinalLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray()); // 0x4 for Shotlock
                    uAsset.DuplicateData2 = reader.ReadBytesFromFileStream((int)(reader.Length - reader.Position)); // 0x118 for Shotlock

                    uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == $"{replaceShotlock}_ProjSet\u0000").AssetName = newShotlock;

                    // Recompile uAsset
                    var uAssetFileBytes = uAsset.Recompile();


                    // Read uExp into Byte Stream
                    List<byte> uExpFileBytes;
                    using var memoryStream = new MemoryStream();

                    using var uexp_reader = File.OpenRead($"{path}_ProjSet.uexp");
                    
                    uexp_reader.CopyTo(memoryStream);
                    uExpFileBytes = memoryStream.ToArray().ToList();
                    
                    // Add Recompiled uAsset + uExp to recompiledFiles
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{newPath}_ProjSet.uasset", uAssetFileBytes);
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{newPath}_ProjSet.uexp", uExpFileBytes);

                    // Decompile uAsset

                    using var readerEquip = File.OpenRead($"{path}_Equip.uasset");

                    // modifies the existing uAsset (+ returns itself, but we won't need that here)
                    uAsset = new uAsset();
                    uAsset.Decompile(readerEquip);

                    readerEquip.Position -= (0xB0 + 0x4 + 0x54);
                    uAsset.DuplicateData = readerEquip.ReadBytesFromFileStream(0x1); // 0xB68 for Shotlock
                    uAsset.FinalLength = BitConverter.ToInt32(readerEquip.ReadBytesFromFileStream(4).ToArray()); // 0x4 for Shotlock
                    uAsset.DuplicateData2 = readerEquip.ReadBytesFromFileStream((int)(readerEquip.Length - readerEquip.Position)); // 0x118 for Shotlock

                    uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == $"{replaceShotlock}_ProjSet\u0000").AssetName = newShotlock;

                    // Recompile uAsset
                    var uAssetEquipFileBytes = uAsset.Recompile();


                    // Read uExp into Byte Stream
                    List<byte> uExpEquipFileBytes;
                    using var memoryEquipStream = new MemoryStream();

                    using var uexpEquipReader = File.OpenRead($"{path}_Equip.uexp");

                    uexpEquipReader.CopyTo(memoryEquipStream);
                    uExpEquipFileBytes = memoryEquipStream.ToArray().ToList();

                    // Add Recompiled uAsset + uExp to recompiledFiles
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{newPath}_Equip.uasset", uAssetEquipFileBytes);
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{newPath}_Equip.uexp", uExpEquipFileBytes);
                }
            }


            // Create ZIP Archive and send back
            return recompiledFiles;
            //return recompiledFiles.CreateZipArchive(randomSeed); // TODO Remember to delete this after downloaded
        }


        public Dictionary<string, List<byte>> GenerateHintDataTable(Dictionary<string, List<string>> hints)
        {
            var recompiledFiles = new Dictionary<string, List<byte>>();

            // Decompile uAsset
            var uAsset = new uAsset();
            var path = @"Content/Load/Tres/SecretReportInfoTable";
            
            using var reader = File.OpenRead($"{path}.uasset");

            // modifies the existing uAsset (+ returns itself, but we won't need that here)
            uAsset.Decompile(reader);

            reader.Flush();
            reader.Close();

            // Decompile uExp
            var uExp = new uExp();

            using var readerExp = File.OpenRead($"{path}.uexp");
            
            uExp.Decompile<SecretReportInfoDataTableEntry>(readerExp, uAsset.AssetStrings);

            readerExp.Flush();
            readerExp.Close();

            foreach (var (report, hintTexts) in hints)
            {
                var concattedHints = string.Join("  -  ", hintTexts) + "\u0000";

                ((SecretReportInfoDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == report).Value).ReportText = Encoding.ASCII.GetBytes(concattedHints).ToList();
                ((SecretReportInfoDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == report).Value).ReportTextLength = concattedHints.Length;
            }

            // Recompile uAsset
            var uAssetFileBytes = uAsset.Recompile();


            // Recompile uExp
            var uExpFileBytes = uExp.Recompile<SecretReportInfoDataTableEntry>();

            // Add Recompiled uAsset + uExp to recompiledFiles
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);

            return recompiledFiles;
        }

        public Dictionary<string, List<byte>> GenerateQualityOfLifeDataTable(Dictionary<string, bool> qol)
        {
            var recompiledFiles = new Dictionary<string, List<byte>>();

            // Decompile uAsset
            var uAsset = new uAsset();
            var path = @"Content/Load/Tres/QualityOfLifeTable";

            using var reader = File.OpenRead($"{path}.uasset");

            // modifies the existing uAsset (+ returns itself, but we won't need that here)
            uAsset.Decompile(reader);

            reader.Flush();
            reader.Close();

            // Decompile uExp
            var uExp = new uExp();

            using var readerExp = File.OpenRead($"{path}.uexp");

            uExp.Decompile<QualityOfLifeDataTableEntry>(readerExp, uAsset.AssetStrings);

            readerExp.Flush();
            readerExp.Close();

            foreach (var (qolName, active) in qol)
            {
                ((QualityOfLifeDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == qolName).Value).activeValue = Convert.ToByte(active);
            }

            // Recompile uAsset
            var uAssetFileBytes = uAsset.Recompile();


            // Recompile uExp
            var uExpFileBytes = uExp.Recompile<QualityOfLifeDataTableEntry>();

            // Add Recompiled uAsset + uExp to recompiledFiles
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);

            return recompiledFiles;
        }
    }
}