﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UE4DataTableInterpreter.DataTables;
using UE4DataTableInterpreter.DataTables.Data;
using UE4DataTableInterpreter.Enums;
using UE4DataTableInterpreter.Interfaces;
using UE4DataTableInterpreter.Models;

namespace UE4DataTableInterpreter
{
    public class DataTableManager
    {
        /// <summary>
        /// Randomizes items in the available pools.
        /// </summary>
        /// <param name="randomSeed">The random seed generated along with the values.</param>
        /// <param name="randomizedValues">This dictionary has the following pattern: Key: DataTableEnum - Value: { Key: - Value: { Key: EntryIndex - Value: {Key: ValueToChangeName - Value: RandomizedValue } }.</param>
        public async Task<Dictionary<string, List<byte>>> RandomizeDataTables(Dictionary<DataTableEnum, Dictionary<string, Dictionary<string, string>>> randomizedValues, bool easyUpgradeKeyblades = false)
        {
            var recompiledFiles = new Dictionary<string, List<byte>>();

            using var streamReader = new StreamReader("uAssetIds.json");
            var uAssetIds = JsonSerializer.Deserialize<Dictionary<string, uint>>(streamReader.ReadToEnd());

            foreach (var (dataTableEnum, entries) in randomizedValues)
            {
                // Processing for edge case situations like Stat Boosts, EXP Multiplier, etc.
                if (dataTableEnum == DataTableEnum.Shotlock || dataTableEnum == DataTableEnum.LevelUpStat || dataTableEnum == DataTableEnum.EquipItemStat || dataTableEnum == DataTableEnum.WeaponEnhanceStat)
                {
                    continue;
                }
                else if (dataTableEnum == DataTableEnum.EXP)
                {
                    this.ProcessEXPTables(ref recompiledFiles, entries["EXP"]["Multiplier"]);
                    continue;
                }
                else if (dataTableEnum == DataTableEnum.BaseCharStat)
                {
                    this.ProcessBaseCharacterStats(ref recompiledFiles, entries["Sora"]);
                    continue;
                }
                else if (dataTableEnum == DataTableEnum.FoodItemEffectStat)
                {
                    this.ProcessFoodItemEffectStats(ref recompiledFiles, entries);
                    continue;
                }

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
                    reader.Position -= (0xA8 + 0x8 + 0x8 + 0x50);
                    uAsset.DuplicateData = reader.ReadBytesFromFileStream(0xB60); // 0xB68 for ChrInit
                    uAsset.uExpLength = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
                    uAsset.uAssetLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(8).ToArray()); // 0x4 for ChrInit
                    uAsset.DuplicateData2 = reader.ReadBytesFromFileStream(0x114); // 0x118 for ChrInit
                }
                else if (dataTableEnum == DataTableEnum.VBonus || dataTableEnum == DataTableEnum.LevelUp || dataTableEnum == DataTableEnum.Event)
                {
                    using var subReader = File.OpenRead($"{subPath}.uasset");

                    uAssetAlt.Decompile(subReader);

                    if (dataTableEnum == DataTableEnum.Event)
                    {
                        subReader.Position -= (0xA8 + 0x8 + 0x8 + 0x50);
                        uAssetAlt.DuplicateData = subReader.ReadBytesFromFileStream(0x1C0); // 0x1C0 for Event
                        uAssetAlt.uExpLength = BitConverter.ToInt64(subReader.ReadBytesFromFileStream(8).ToArray());
                        uAssetAlt.uAssetLength = BitConverter.ToInt32(subReader.ReadBytesFromFileStream(8).ToArray());
                        uAssetAlt.DuplicateData2 = subReader.ReadBytesFromFileStream(0x64); // 0x64 for Event
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
                            uAsset.Unk12 = uAsset.UnkLength3;
                            uAsset.Unk5 += randomizedValue.Length + 8;
                            uAsset.SubSize1 += randomizedValue.Length + 8;
                            uAsset.SubSize2 += randomizedValue.Length + 8;
                            uAsset.Unk16 += randomizedValue.Length + 8;
                            uAsset.Unk17 += randomizedValue.Length + 8;
                            uAsset.Unk19 += randomizedValue.Length + 8;
                            uAsset.uAssetLength += randomizedValue.Length + 8;
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
                                            uAssetAlt.Unk12 = uAssetAlt.UnkLength3;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.uAssetLength += randomizedValue.Length + 8;
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
                                            uAssetAlt.Unk12 = uAssetAlt.UnkLength3;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.uAssetLength += randomizedValue.Length + 8;
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
                                            uAssetAlt.Unk12 = uAssetAlt.UnkLength3;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.uAssetLength += randomizedValue.Length + 8;
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
                                            uAssetAlt.Unk12 = uAssetAlt.UnkLength3;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.uAssetLength += randomizedValue.Length + 8;
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
                                            uAssetAlt.Unk12 = uAssetAlt.UnkLength3;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.uAssetLength += randomizedValue.Length + 8;
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
                                            uAssetAlt.Unk12 = uAssetAlt.UnkLength3;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.uAssetLength += randomizedValue.Length + 8;
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
                                            uAssetAlt.Unk12 = uAssetAlt.UnkLength3;
                                            uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                            uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                            uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                            uAssetAlt.uAssetLength += randomizedValue.Length + 8;
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

                                if (easyUpgradeKeyblades)
                                {
                                    ((WeaponEnhanceDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).MatNum0 = 0;
                                    ((WeaponEnhanceDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).MatNum1 = 0;
                                    ((WeaponEnhanceDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).MatNum2 = 0;
                                    ((WeaponEnhanceDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).MatNum3 = 0;
                                    ((WeaponEnhanceDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).MatNum4 = 0;
                                    ((WeaponEnhanceDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == entryIndex).Value).MatNum5 = 0;
                                }

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
                                        uAssetAlt.Unk12 = uAssetAlt.UnkLength3;
                                        uAssetAlt.Unk5 += randomizedValue.Length + 8;
                                        uAssetAlt.SubSize1 += randomizedValue.Length + 8;
                                        uAssetAlt.SubSize2 += randomizedValue.Length + 8;
                                        uAssetAlt.Unk16 += randomizedValue.Length + 8;
                                        uAssetAlt.Unk17 += randomizedValue.Length + 8;
                                        uAssetAlt.Unk19 += randomizedValue.Length + 8;
                                        uAssetAlt.uAssetLength += randomizedValue.Length + 8;
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


                // Update existing tables
                switch (dataTableEnum)
                {
                    case DataTableEnum.LevelUp:
                        if (randomizedValues.ContainsKey(DataTableEnum.LevelUpStat))
                            this.ProcessLevelUpStats(randomizedValues[DataTableEnum.LevelUpStat], uExp.DataTableEntries);

                        break;
                    case DataTableEnum.WeaponEnhance:
                        if (randomizedValues.ContainsKey(DataTableEnum.WeaponEnhanceStat))
                            this.ProcessWeaponEnhanceStats(randomizedValues[DataTableEnum.WeaponEnhanceStat], uExp.DataTableEntries);

                        break;
                    case DataTableEnum.EquipItem:
                        if (randomizedValues.ContainsKey(DataTableEnum.EquipItemStat))
                            this.ProcessEquipItemStats(randomizedValues[DataTableEnum.EquipItemStat], uExp.DataTableEntries);

                        break;

                    default:
                        break;
                }

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


                // Recompile uAsset with uExp length
                uAsset.uExpLength = uExpFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
                uAsset.Unk17 = (int)(uAsset.uExpLength + uAsset.uAssetLength);

                var uAssetFileBytes = uAsset.Recompile();

                // Add Recompiled uAsset + uExp to recompiledFiles
                recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);

                // Add Recompiled ALT uAsset + uExp to recompiledFiles
                if (dataTableEnum == DataTableEnum.VBonus)
                {
                    var uExpAltFileBytes = uExpAlt.Recompile<VBonusDataTableAltEntry>();

                    uAssetAlt.uExpLength = uExpAltFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
                    uAssetAlt.Unk17 = (int)(uAssetAlt.uExpLength + uAssetAlt.uAssetLength);

                    var uAssetAltFileBytes = uAssetAlt.Recompile();

                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uasset", uAssetAltFileBytes);
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uexp", uExpAltFileBytes);
                }
                else if (dataTableEnum == DataTableEnum.LevelUp)
                {
                    var uExpAltFileBytes = uExpAlt.Recompile<LevelUpDataTableAltEntry>();

                    uAssetAlt.uExpLength = uExpAltFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
                    uAssetAlt.Unk17 = (int)(uAssetAlt.uExpLength + uAssetAlt.uAssetLength);

                    var uAssetAltFileBytes = uAssetAlt.Recompile();

                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uasset", uAssetAltFileBytes);
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uexp", uExpAltFileBytes);
                }
                else if (dataTableEnum == DataTableEnum.Event)
                {
                    var uExpAltFileBytes = uExpAlt.Recompile<MobilePortalDataTableEntry>();

                    uAssetAlt.uExpLength = uExpAltFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
                    uAssetAlt.Unk17 = (int)(uAssetAlt.uExpLength + uAssetAlt.uAssetLength);

                    var uAssetAltFileBytes = uAssetAlt.Recompile();

                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uasset", uAssetAltFileBytes);
                    recompiledFiles.Add($@"KINGDOM HEARTS III/{subPath}.uexp", uExpAltFileBytes);
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

                    // TODO Redo this
                    reader.Position -= (0xA8 + 0x8 + 0x8 + 0x50);
                    uAsset.DuplicateData = reader.ReadBytesFromFileStream(0x1); // 0xB68 for Shotlock
                    uAsset.uExpLength = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
                    uAsset.uAssetLength = BitConverter.ToInt32(reader.ReadBytesFromFileStream(8).ToArray()); // 0x4 for ChrInit
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
                    
                    // TODO Redo this
                    readerEquip.Position -= (0xA8 + 0x8 + 0x8 + 0x50);
                    uAsset.DuplicateData = readerEquip.ReadBytesFromFileStream(0x1); // 0xB68 for Shotlock
                    uAsset.uExpLength = BitConverter.ToInt64(readerEquip.ReadBytesFromFileStream(8).ToArray());
                    uAsset.uAssetLength = BitConverter.ToInt32(readerEquip.ReadBytesFromFileStream(8).ToArray()); // 0x4 for ChrInit
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

        public void ProcessEXPTables(ref Dictionary<string, List<byte>> recompiledFiles, string expMultiplier)
        {
            var convertedEXPMultiplier = float.Parse(expMultiplier);

            var enemyTablePaths = Directory.GetFiles(@"Content/DataTable/Enemy/Base");

            foreach (var enemyTablePath in enemyTablePaths.Select(x => x.Split('.')[0].Replace("\\", "/")).Distinct())
            {
                // For now, just skip this since we have the duplicate table issue on unzip
                if (enemyTablePath.Contains("ex021"))
                    continue;

                // Decompile uAsset
                var uAsset = new uAsset();


                // Only for VBonus for now
                var uAssetAlt = new uAsset();


                using var reader = File.OpenRead($"{enemyTablePath}.uasset");

                // modifies the existing uAsset (+ returns itself, but we won't need that here)
                uAsset.Decompile(reader);

                reader.Flush();
                reader.Close();

                // Decompile uExp
                var uExp = new uExp();

                using var readerExp = File.OpenRead($"{enemyTablePath}.uexp");


                // modifies the existing uExp (+ returns itself, but we won't need that here)
                uExp.Decompile<CharacterBaseDataTableEntry>(readerExp, uAsset.AssetStrings);

                readerExp.Flush();
                readerExp.Close();

                // Change EXP Multiplier according to the passed in multiplier
                uExp.DataTableEntries.ToList().ForEach(x => ((CharacterBaseDataTableEntry)x.Value).ExpRateValue *= convertedEXPMultiplier);
                
                
                // Recompile uExp
                var uExpFileBytes = new List<byte>();

                uExpFileBytes = uExp.Recompile<CharacterBaseDataTableEntry>();

                // Recompile uAsset with uExp length
                uAsset.uExpLength = uExpFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
                uAsset.Unk17 = (int)(uAsset.uExpLength + uAsset.uAssetLength);

                var uAssetFileBytes = uAsset.Recompile();

                // Add Recompiled uAsset + uExp to recompiledFiles
                recompiledFiles.Add($@"KINGDOM HEARTS III/{enemyTablePath}.uasset", uAssetFileBytes);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{enemyTablePath}.uexp", uExpFileBytes);
            }
        }

        public void ProcessBaseCharacterStats(ref Dictionary<string, List<byte>> recompiledFiles, Dictionary<string, string> randomizedValues)
        {
            var path = @"Content/DataTable/Player/Base/p_ex001_BaseParamData";

            // Decompile uAsset
            var uAsset = new uAsset();


            using var reader = File.OpenRead($"{path}.uasset");

            // modifies the existing uAsset (+ returns itself, but we won't need that here)
            uAsset.Decompile(reader);

            reader.Flush();
            reader.Close();

            // Decompile uExp
            var uExp = new uExp();

            using var readerExp = File.OpenRead($"{path}.uexp");


            // modifies the existing uExp (+ returns itself, but we won't need that here)
            uExp.Decompile<CharacterBaseDataTableEntry>(readerExp, uAsset.AssetStrings);

            readerExp.Flush();
            readerExp.Close();

            foreach (var (name, randomizedValue) in randomizedValues)
            {
                if (name == "MaxHitPoint")
                    ((CharacterBaseDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).MaxHitPointValue = int.Parse(randomizedValue);
                else if (name == "MaxMagicPoint")
                    ((CharacterBaseDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).MaxMagicPointValue = int.Parse(randomizedValue);
                else if (name == "MaxFocusPoint")
                    ((CharacterBaseDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).MaxFocusPointValue = int.Parse(randomizedValue);
                else if (name == "AttackPower")
                    ((CharacterBaseDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).AttackPowerValue = int.Parse(randomizedValue);
                else if (name == "MagicPower")
                    ((CharacterBaseDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).MagicPowerValue = int.Parse(randomizedValue);
                else if (name == "DefensePower")
                    ((CharacterBaseDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).DefensePowerValue = int.Parse(randomizedValue);
                else if (name == "AbilityPoint")
                    ((CharacterBaseDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).AbilityPointValue = int.Parse(randomizedValue);
            }
            

            // Recompile uExp
            var uExpFileBytes = new List<byte>();

            uExpFileBytes = uExp.Recompile<CharacterBaseDataTableEntry>();

            // Recompile uAsset with uExp length
            uAsset.uExpLength = uExpFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
            uAsset.Unk17 = (int)(uAsset.uExpLength + uAsset.uAssetLength);

            var uAssetFileBytes = uAsset.Recompile();

            // Add Recompiled uAsset + uExp to recompiledFiles
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);
        }

        public void ProcessFoodItemEffectStats(ref Dictionary<string, List<byte>> recompiledFiles, Dictionary<string, Dictionary<string, string>> randomizedValues)
        {
            var path = @"Content/Load/Tres/TresFoodItemEffectData";

            // Decompile uAsset
            var uAsset = new uAsset();


            using var reader = File.OpenRead($"{path}.uasset");

            // modifies the existing uAsset (+ returns itself, but we won't need that here)
            uAsset.Decompile(reader);

            reader.Flush();
            reader.Close();

            // Decompile uExp
            var uExp = new uExp();

            using var readerExp = File.OpenRead($"{path}.uexp");


            // modifies the existing uExp (+ returns itself, but we won't need that here)
            uExp.Decompile<FoodItemEffectDataTableEntry>(readerExp, uAsset.AssetStrings);

            readerExp.Flush();
            readerExp.Close();

            foreach (var (food, randomizedOptions) in randomizedValues)
            {
                foreach (var (name, randomizedValue) in randomizedOptions)
                {
                    if (name == "MaxHPPlus")
                        ((FoodItemEffectDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == food).Value).MaxHPPlusValue = int.Parse(randomizedValue);
                    else if (name == "MaxMPPlus")
                        ((FoodItemEffectDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == food).Value).MaxMPPlusValue = int.Parse(randomizedValue);
                    else if (name == "AttackPlus")
                        ((FoodItemEffectDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == food).Value).AttackPlusValue = int.Parse(randomizedValue);
                    else if (name == "MagicPlus")
                        ((FoodItemEffectDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == food).Value).MagicPlusValue = int.Parse(randomizedValue);
                    else if (name == "DefensePlus")
                        ((FoodItemEffectDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == food).Value).DefensePlusValue = int.Parse(randomizedValue);
                }
            }


            // Recompile uExp
            var uExpFileBytes = new List<byte>();

            uExpFileBytes = uExp.Recompile<FoodItemEffectDataTableEntry>();

            // Recompile uAsset with uExp length
            uAsset.uExpLength = uExpFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
            uAsset.Unk17 = (int)(uAsset.uExpLength + uAsset.uAssetLength);

            var uAssetFileBytes = uAsset.Recompile();

            // Add Recompiled uAsset + uExp to recompiledFiles
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);
        }

        public void ProcessLevelUpStats(Dictionary<string, Dictionary<string, string>> randomizedValues, Dictionary<string, IDataTable> entries)
        {
            foreach (var (level, randomizedOptions) in randomizedValues)
            {
                foreach (var (name, randomizedValue) in randomizedOptions)
                {
                    if (name == "AttackPower")
                        ((LevelUpDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == level).Value).AttackValue = int.Parse(randomizedValue);
                    else if (name == "MagicPower")
                        ((LevelUpDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == level).Value).MagicValue = int.Parse(randomizedValue);
                    else if (name == "DefensePower")
                        ((LevelUpDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == level).Value).DefenseValue = int.Parse(randomizedValue);
                    else if (name == "AbilityPoint")
                        ((LevelUpDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == level).Value).AbilityPointValue = int.Parse(randomizedValue);
                }
            }
        }

        public void ProcessWeaponEnhanceStats(Dictionary<string, Dictionary<string, string>> randomizedValues, Dictionary<string, IDataTable> entries)
        {
            foreach (var (equipment, randomizedOptions) in randomizedValues)
            {
                foreach (var (name, randomizedValue) in randomizedOptions)
                {
                    if (name == "AttackPlus")
                        ((WeaponEnhanceDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).AttackPlus = int.Parse(randomizedValue);
                    else if (name == "MagicPlus")
                        ((WeaponEnhanceDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).MagicPlus = int.Parse(randomizedValue);
                }
            }
        }

        public void ProcessEquipItemStats(Dictionary<string, Dictionary<string, string>> randomizedValues, Dictionary<string, IDataTable> entries)
        {
            foreach (var (equipment, randomizedOptions) in randomizedValues)
            {
                foreach (var (name, randomizedValue) in randomizedOptions)
                {
                    if (name == "AP")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).APValue = int.Parse(randomizedValue);
                    else if (name == "AttackPlus")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).AttackValue = int.Parse(randomizedValue);
                    else if (name == "MagicPlus")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).MagicValue = int.Parse(randomizedValue);
                    else if (name == "DefensePlus")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).DefenseValue = int.Parse(randomizedValue);
                    else if (name == "AttrResistPhysical")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).PhysicalResistValue = int.Parse(randomizedValue);
                    else if (name == "AttrResistFire")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).FireResistValue = int.Parse(randomizedValue);
                    else if (name == "AttrResistBlizzard")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).BlizzardResistValue = int.Parse(randomizedValue);
                    else if (name == "AttrResistThunder")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).ThunderResistValue = int.Parse(randomizedValue);
                    else if (name == "AttrResistWater")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).WaterResistValue = int.Parse(randomizedValue);
                    else if (name == "AttrResistAero")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).AeroResistValue = int.Parse(randomizedValue);
                    else if (name == "AttrResistDark")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).DarkResistValue = int.Parse(randomizedValue);
                    else if (name == "AttrResistNoType")
                        ((EquipItemDataTableEntry)entries.FirstOrDefault(x => x.Value.RowName == equipment).Value).NoTypeResistValue = int.Parse(randomizedValue);
                }
            }
        }



        public async Task<Dictionary<string, List<byte>>> GenerateHintDataTable(Dictionary<string, List<string>> hints)
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
                hintTexts.RemoveAll(x => string.IsNullOrEmpty(x));
                var concattedHints = string.Join("  -  ", hintTexts) + "\u0000";

                ((SecretReportInfoDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == report).Value).ReportText = Encoding.ASCII.GetBytes(concattedHints).ToList();
                ((SecretReportInfoDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == report).Value).ReportTextLength = concattedHints.Length;
            }


            // Recompile uExp
            var uExpFileBytes = uExp.Recompile<SecretReportInfoDataTableEntry>();


            // Recompile uAsset
            uAsset.uExpLength = uExpFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
            uAsset.Unk17 = (int)(uAsset.uExpLength + uAsset.uAssetLength);

            var uAssetFileBytes = uAsset.Recompile();


            // Add Recompiled uAsset + uExp to recompiledFiles
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);

            return recompiledFiles;
        }

        public async Task<Dictionary<string, List<byte>>> GenerateQualityOfLifeDataTable(Dictionary<string, bool> qol)
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


            // Recompile uExp
            var uExpFileBytes = uExp.Recompile<QualityOfLifeDataTableEntry>();


            // Recompile uAsset
            uAsset.uExpLength = uExpFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
            uAsset.Unk17 = (int)(uAsset.uExpLength + uAsset.uAssetLength);

            var uAssetFileBytes = uAsset.Recompile();


            // Add Recompiled uAsset + uExp to recompiledFiles
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);

            
            // Add Boss QoL
            if (qol.ContainsKey("BOSS_001") && qol["BOSS_001"]) // Easier Mini-UFO
            {
                var ufoPath = @"Content/Blueprints/Gimmick/ts/g_ts_UFO/g_ts_UFO";

                var ufoFiles = this.LoadAssetExpFiles(ufoPath);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{ufoPath}.uasset", ufoFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{ufoPath}.uexp", ufoFiles.Item2);
            }

            if (qol.ContainsKey("BOSS_002") && qol["BOSS_002"]) // Faster Raging Vulture
            {
                var vulturePath = @"Content/DataTable/Enemy/Base/e_ex021_BaseDataTable";

                var vultureFiles = this.LoadAssetExpFiles(vulturePath);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{vulturePath}.uasset", vultureFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{vulturePath}.uexp", vultureFiles.Item2);
            }

            if (qol.ContainsKey("BOSS_003") && qol["BOSS_003"]) // Skip Dark Baymax Phase 1
            {
                var baymaxPath = @"Content/Cutscene/Localization/ja/bx/cutscene_bx02_bx562";

                var baymaxFiles = this.LoadAssetExpFiles(baymaxPath, true);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{baymaxPath}.umap", baymaxFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{baymaxPath}.uexp", baymaxFiles.Item2);
            }

            if (qol.ContainsKey("BOSS_004") && qol["BOSS_004"]) // Lich Skip
            {
                var lichPath = @"Content/Maps/ew/umap/ew_28/ew_28_ENV";

                var lichFiles = this.LoadAssetExpFiles(lichPath, true);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{lichPath}.umap", lichFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{lichPath}.uexp", lichFiles.Item2);
            }


            // Add Event QoL
            if (qol.ContainsKey("EVENT_001") && qol["EVENT_001"]) // Frozen Chase Skip
            {
                var frozenChasePath = @"Content/Levels/fz/fz_03/umap/fz_03_gimmick_Avalanche";

                var frozenChaseFiles = this.LoadAssetExpFiles(frozenChasePath, true);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{frozenChasePath}.umap", frozenChaseFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{frozenChasePath}.uexp", frozenChaseFiles.Item2);
            }

            if (qol.ContainsKey("EVENT_002") && qol["EVENT_002"]) // Faster Crab Collection
            {
                var crabPath = @"Content/DataTable/Gimmick/Prize/g_ca_KaniDiveGim_PrizeData";

                var crabFiles = this.LoadAssetExpFiles(crabPath);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{crabPath}.uasset", crabFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{crabPath}.uexp", crabFiles.Item2);
            }

            if (qol.ContainsKey("EVENT_003") && qol["EVENT_003"]) // Big Hero 6 Rescue Skip
            {
                var bigSixPath = @"Content/Maps/ew/umap/ew_01/QoL_BigSix";

                var bigSixFiles = this.LoadAssetExpFiles(bigSixPath, true);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{bigSixPath}.umap", bigSixFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{bigSixPath}.uexp", bigSixFiles.Item2);
            }

            if (qol.ContainsKey("EVENT_004") && qol["EVENT_004"]) // Faster Sora Collection
            {
                var soraCollectionPath = @"Content/Maps/ew/umap/ew_02/QoL_SoraCollection";

                var soraCollectionFiles = this.LoadAssetExpFiles(soraCollectionPath, true);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{soraCollectionPath}.umap", soraCollectionFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{soraCollectionPath}.uexp", soraCollectionFiles.Item2);
            }

            if (qol.ContainsKey("EVENT_005") && qol["EVENT_005"]) // Union X Skip
            {
                var unionPath = @"Content/Cutscene/Localization/ja/kg/cutscene_kg01_kg852b";

                var unionFiles = this.LoadAssetExpFiles(unionPath, true);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{unionPath}.umap", unionFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{unionPath}.uexp", unionFiles.Item2);
            }

            if (qol.ContainsKey("EVENT_006") && qol["EVENT_006"]) // Guardians of Light Skip
            {
                var guardiansPath = @"Content/Cutscene/Localization/ja/kg_DLC/cutscene_kg_06_kg974";

                var guardiansFiles = this.LoadAssetExpFiles(guardiansPath, true);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{guardiansPath}.umap", guardiansFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{guardiansPath}.uexp", guardiansFiles.Item2);
            }


            // Add Item QoL
            if (qol.ContainsKey("ITEM_003") && qol["ITEM_003"]) // All Maps
            {
                var mapsPath = @"Content/Maps/ew/umap/ew_01/QoL_Maps";

                var mapsFiles = this.LoadAssetExpFiles(mapsPath, true);

                recompiledFiles.Add($@"KINGDOM HEARTS III/{mapsPath}.umap", mapsFiles.Item1);
                recompiledFiles.Add($@"KINGDOM HEARTS III/{mapsPath}.uexp", mapsFiles.Item2);
            }

            if (qol.ContainsKey("ITEM_004") && qol["ITEM_004"]) // Easily Upgraded Keyblades
            {
                // Handled above in the Options Randomizer
            }


            return recompiledFiles;
        }

        public async Task<Dictionary<string, List<byte>>> GenerateCompletionConditionsDataTable(Dictionary<string, int> conditions)
        {
            var recompiledFiles = new Dictionary<string, List<byte>>();

            // Decompile uAsset
            var uAsset = new uAsset();
            var path = @"Content/Load/Tres/TresCompletionData";

            using var reader = File.OpenRead($"{path}.uasset");

            // modifies the existing uAsset (+ returns itself, but we won't need that here)
            uAsset.Decompile(reader);

            reader.Flush();
            reader.Close();

            // Decompile uExp
            var uExp = new uExp();

            using var readerExp = File.OpenRead($"{path}.uexp");

            uExp.Decompile<CompletionConditionsDataTableEntry>(readerExp, uAsset.AssetStrings);

            readerExp.Flush();
            readerExp.Close();

            var completionConditionList = new List<CompletionConditionItem>();
            var arrayLength = 0L;
            
            var template = ((CompletionConditionsDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).CompletionConditionItems.FirstOrDefault();
            
            foreach (var (name, amount) in conditions)
            {
                var completionCondition = new CompletionConditionItem
                {
                    CompletionConditionName = template.CompletionConditionName,
                    StrProperty = template.StrProperty,
                    Unk1 = template.Unk1,
                    CompletionConditionNameLength = name.Length,
                    CompletionConditionValue = Encoding.UTF8.GetBytes(name).ToList(),

                    CompletionConditionAmountName = template.CompletionConditionAmountName,
                    IntProperty = template.IntProperty,
                    Unk2 = template.Unk2,
                    CompletionConditionAmountValue = amount,

                    None = template.None
                };

                completionConditionList.Add(completionCondition);
                arrayLength += completionCondition.Length();
            }

            ((CompletionConditionsDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).CompletionConditionItems = completionConditionList;
            ((CompletionConditionsDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).ArraySize = completionConditionList.Count;
            ((CompletionConditionsDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).LengthOfAllData = ((CompletionConditionsDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).Length();
            ((CompletionConditionsDataTableEntry)uExp.DataTableEntries.FirstOrDefault().Value).LengthOfArrayData = arrayLength;


            // Recompile uExp
            var uExpFileBytes = uExp.Recompile<CompletionConditionsDataTableEntry>();


            // Recompile uAsset
            uAsset.uExpLength = uExpFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
            uAsset.Unk17 = (int)(uAsset.uExpLength + uAsset.uAssetLength);

            var uAssetFileBytes = uAsset.Recompile();


            // Add Recompiled uAsset + uExp to recompiledFiles
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);

            return recompiledFiles;
        }


        public async Task<Dictionary<string, List<byte>>> GeneratePandorasPowerKeyblade(string seed = "")
        {
            var recompiledFiles = new Dictionary<string, List<byte>>();

            // Decompile uAsset
            var uAsset = new uAsset();
            var path = @"Content/Character/wep/w_so120/mdl/mat0/w_so120_mat2_Inst";

            using var reader = File.OpenRead($"{path}.uasset");

            // modifies the existing uAsset (+ returns itself, but we won't need that here)
            var uAssetFileBytes = new List<byte>();
            uAssetFileBytes.AddRange(reader.ReadBytesFromFileStream((int)reader.Length));

            reader.Flush();
            reader.Close();

            // Decompile uExp
            var uExp = new uExp();

            using var readerExp = File.OpenRead($"{path}.uexp");

            // Decompile uExp
            var uExpFileBytes = new List<byte>();
            uExpFileBytes.AddRange(readerExp.ReadBytesFromFileStream((int)readerExp.Length));

            readerExp.Flush();
            readerExp.Close();

            // Generate RGB Random Values
            var hash = seed.StringToSeed();
            var random = new Random((int)hash);

            var allowedValues = new List<float> { 0.00f, 0.05f, 0.10f, 0.15f, 0.20f, 0.25f, 0.30f, 0.35f, 0.40f, 0.45f, 0.50f, 
                                                    0.55f, 0.60f, 0.65f, 0.70f, 0.75f, 0.80f, 0.85f, 0.90f, 0.95f, 1.00f };

            int randRIndex = (random.Next() % allowedValues.Count);
            var rValue = BitConverter.GetBytes(allowedValues[randRIndex]);
            for (int i = 0; i < 4; ++i)
            {
                var index = 0xCD0 - (4 - i);
                uExpFileBytes[index] = rValue[i];
            }

            int randGIndex = (random.Next() % allowedValues.Count);
            var gValue = BitConverter.GetBytes(allowedValues[randGIndex]);
            for (int i = 0; i < 4; ++i)
            {
                var index = 0xCD4 - (4 - i);
                uExpFileBytes[index] = gValue[i];
            }

            int randBIndex = (random.Next() % allowedValues.Count);
            var bValue = BitConverter.GetBytes(allowedValues[randBIndex]);
            for (int i = 0; i < 4; ++i)
            {
                var index = 0xCD8 - (4 - i);
                uExpFileBytes[index] = bValue[i];
            }


            // Add Recompiled uAsset + uExp to recompiledFiles
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);

            return recompiledFiles;
        }

        private Tuple<List<byte>, List<byte>> LoadAssetExpFiles(string path, bool umapUse = false)
        {
            // Load uAsset
            List<byte> uAssetFileBytes;
            using var uAssetMemoryStream = new MemoryStream();

            var extension = umapUse ? "umap" : "uasset";
            using var reader = File.OpenRead($"{path}.{extension}");

            reader.CopyTo(uAssetMemoryStream);
            uAssetFileBytes = uAssetMemoryStream.ToArray().ToList();


            // Load uExp
            List<byte> uExpFileBytes;
            using var uExpMemoryStream = new MemoryStream();

            using var readerExp = File.OpenRead($"{path}.uexp");

            readerExp.CopyTo(uExpMemoryStream);
            uExpFileBytes = uExpMemoryStream.ToArray().ToList();


            return new Tuple<List<byte>, List<byte>>(uAssetFileBytes, uExpFileBytes);
        }


        /// <summary>
        /// Randomizes the enemy file path in the Levels folder. JSON is in reverse order of entities spawned in, so we can skip
        /// figuring out the correct offset for each entity.
        /// </summary>
        /// <param name="randomizedEnemies"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<byte>>> RandomizeEnemies(Dictionary<string, Enemy> randomizedEnemies, string seed = "", bool enemyChaos = false)
        {
            // TODO Update this logic to account for multiple bosses per file (or enemies)
            // I think the best approach would be to make a dictionary of the uMap instead of the list of bytes, this way if it exists, we can just update the uMap
            var recompiledFiles = new Dictionary<string, List<byte>>();

            var trackedEnemies = new Dictionary<string, uMap>();

            var hash = seed.StringToSeed();
            var random = new Random((int)hash);

            var enemyKeys = randomizedEnemies.Keys.ToList();

            foreach (var (enemyName, enemyData) in randomizedEnemies)
            {
                // Get uMap
                uMap uMap = null;
                if (trackedEnemies.ContainsKey(enemyData.FilePath))
                {
                    uMap = trackedEnemies[enemyData.FilePath];
                }
                else
                {
                    // Decompile uMap
                    uMap = new uMap();


                    using var reader = File.OpenRead($"{enemyData.FilePath}.umap");
                    uMap.uMapTotalLength = (int)reader.Length;

                    // modifies the existing uAsset (+ returns itself, but we won't need that here)
                    uMap.Decompile(reader);

                    reader.Flush();
                    reader.Close();
                }

                
                // Update uExp with correct length and enemy name
                foreach (var addressString in enemyData.Addresses)
                {
                    var enemyPath = enemyData.EnemyPath;

                    if (enemyChaos)
                    {
                        var randomEnemy = randomizedEnemies[enemyKeys[random.Next(0, enemyKeys.Count)]];

                        enemyPath = randomEnemy.EnemyPath;
                    }

                    try
                    {
                        // Get uExp
                        List<byte> uExpFileBytes;
                        if (recompiledFiles.ContainsKey($@"KINGDOM HEARTS III/{enemyData.FilePath}.uexp"))
                        {
                            uExpFileBytes = recompiledFiles[$@"KINGDOM HEARTS III/{enemyData.FilePath}.uexp"];
                        }
                        else
                        {
                            // Decompile uExp
                            uExpFileBytes = new List<byte>();

                            using var readerExp = File.OpenRead($"{enemyData.FilePath}.uexp");


                            // Since each level will have a unique uExp, it makes more sense to use the Address and change 
                            // the previous 4 bytes (the length, if necessary) and the actual string itself

                            // modifies the existing uExp (+ returns itself, but we won't need that here)
                            //uExp.Decompile<FoodItemEffectDataTableEntry>(readerExp, uMap.AssetStrings);
                            uExpFileBytes.AddRange(readerExp.ReadBytesFromFileStream((int)readerExp.Length));

                            readerExp.Flush();
                            readerExp.Close();
                        }

                        var address = Convert.ToInt32(addressString, 16);

                        var originalLength = BitConverter.ToInt32(uExpFileBytes.GetRange((int)(address - 4), 4).ToArray());
                        var nameBytes = Encoding.UTF8.GetBytes(enemyPath);

                        var lengthBytes = BitConverter.GetBytes(enemyPath.Length);
                        for (int i = 0; i < lengthBytes.Length; ++i)
                        {
                            var index = (int)(address - (4 - i));
                            uExpFileBytes[index] = lengthBytes[i];
                        }

                        var updateduExpFileBytes = new List<byte>();

                        var take = uExpFileBytes.Take((int)address);
                        var skip = uExpFileBytes.Skip((int)(address + originalLength));

                        updateduExpFileBytes.AddRange(take);
                        updateduExpFileBytes.AddRange(nameBytes);
                        updateduExpFileBytes.AddRange(skip);


                        // Add the offsets and length to uMap
                        var found = false;
                        var updated = false;
                        var offset = (enemyPath.Length - originalLength);

                        foreach (var uMapObject in uMap.Objects)
                        {
                            // If our address is within the current object's starting position and ending position, we found it
                            found = ((uMap.uMapTotalLength + address) >= uMapObject.uExpPosition && (uMap.uMapTotalLength + address) <= (uMapObject.uExpPosition + uMapObject.uExpBlockLength));

                            if (found || updated)
                            {
                                if (updated)
                                {
                                    uMapObject.uExpPosition += offset;
                                }
                                else
                                {
                                    uMapObject.uExpBlockLength += offset;

                                    updated = true;
                                }
                            }
                        }


                        // Recompile uMap with uExp length
                        uMap.uMapuExpLength += offset;


                        // Add Recompiled uMap + uExp to recompiledFiles
                        if (!trackedEnemies.ContainsKey(enemyData.FilePath))
                            trackedEnemies.Add(enemyData.FilePath, null);

                        trackedEnemies[enemyData.FilePath] = uMap;


                        if (!recompiledFiles.ContainsKey($@"KINGDOM HEARTS III/{enemyData.FilePath}.uexp"))
                            recompiledFiles.Add($@"KINGDOM HEARTS III/{enemyData.FilePath}.uexp", new List<byte>());

                        recompiledFiles[$@"KINGDOM HEARTS III/{enemyData.FilePath}.uexp"] = updateduExpFileBytes;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }


            foreach (var (filePath, uMap) in trackedEnemies)
            {
                recompiledFiles.Add($@"KINGDOM HEARTS III/{filePath}.umap", uMap.Recompile());
            }

            return recompiledFiles;
        }

        public async Task<Dictionary<string, List<byte>>> RandomizeBosses(Dictionary<string, string> randomizedBosses)
        {
            // I think the best approach would be to make a dictionary of the uMap instead of the list of bytes, this way if it exists, we can just update the uMap
            var recompiledFiles = new Dictionary<string, List<byte>>();


            // Decompile uAsset
            var uAsset = new uAsset();
            var path = @"Content/Load/Tres/TresBossData";

            using var reader = File.OpenRead($"{path}.uasset");

            // modifies the existing uAsset (+ returns itself, but we won't need that here)
            uAsset.Decompile(reader);

            reader.Flush();
            reader.Close();

            // Decompile uExp
            var uExp = new uExp();

            using var readerExp = File.OpenRead($"{path}.uexp");

            uExp.Decompile<BossDataTableEntry>(readerExp, uAsset.AssetStrings);

            readerExp.Flush();
            readerExp.Close();

            foreach (var (name, actorId) in randomizedBosses)
            {
                var asset = uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == actorId);
                var assetIndex = uAsset.AssetStrings.IndexOf(asset);

                ((BossDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == name).Value).RandomizedName = assetIndex;
            }


            // Recompile uExp
            var uExpFileBytes = uExp.Recompile<BossDataTableEntry>();


            // Recompile uAsset
            uAsset.uExpLength = uExpFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
            uAsset.Unk17 = (int)(uAsset.uExpLength + uAsset.uAssetLength);

            var uAssetFileBytes = uAsset.Recompile();


            // Add Recompiled uAsset + uExp to recompiledFiles
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);

            return recompiledFiles;
        }

        public async Task<Dictionary<string, List<byte>>> RandomizePartyMembers(Dictionary<string, string> randomizedPartyMembers, bool defaultDonaldGoofy = false, bool partyChaos = false)
        {
            // TODO Update this logic to account for multiple bosses per file (or enemies)
            // I think the best approach would be to make a dictionary of the uMap instead of the list of bytes, this way if it exists, we can just update the uMap
            var recompiledFiles = new Dictionary<string, List<byte>>();


            // Decompile uAsset
            var uAsset = new uAsset();
            var path = @"Content/Load/Tres/TresPartyData";

            using var reader = File.OpenRead($"{path}.uasset");

            // modifies the existing uAsset (+ returns itself, but we won't need that here)
            uAsset.Decompile(reader);

            reader.Flush();
            reader.Close();

            // Decompile uExp
            var uExp = new uExp();

            using var readerExp = File.OpenRead($"{path}.uexp");

            uExp.Decompile<PartyMemberDataTableEntry>(readerExp, uAsset.AssetStrings);

            readerExp.Flush();
            readerExp.Close();

            foreach (var (name, actorId) in randomizedPartyMembers)
            {
                if (defaultDonaldGoofy)
                {
                    var id = name.Split('_')[1];
                    // Donald and Goofy Variants
                    if (id == "001" || id == "002" || id == "022" || id == "023" || id == "024" || id == "025" || id == "026" || id == "027")
                        continue;
                }
                
                if (partyChaos) 
                    ((PartyMemberDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == name).Value).RandomizeConstantly = Convert.ToByte(partyChaos);
                else
                {
                    var asset = uAsset.AssetStrings.FirstOrDefault(x => x.AssetName == actorId);
                    var assetIndex = uAsset.AssetStrings.IndexOf(asset);

                    ((PartyMemberDataTableEntry)uExp.DataTableEntries.FirstOrDefault(x => x.Value.RowName == name).Value).RandomizedName = assetIndex;
                }
            }


            // Recompile uExp
            var uExpFileBytes = uExp.Recompile<PartyMemberDataTableEntry>();


            // Recompile uAsset
            uAsset.uExpLength = uExpFileBytes.Count - 4; // Remove 4 bytes for the ID at the end?
            uAsset.Unk17 = (int)(uAsset.uExpLength + uAsset.uAssetLength);

            var uAssetFileBytes = uAsset.Recompile();


            // Add Recompiled uAsset + uExp to recompiledFiles
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uasset", uAssetFileBytes);
            recompiledFiles.Add($@"KINGDOM HEARTS III/{path}.uexp", uExpFileBytes);

            return recompiledFiles;
        }
    }
}