using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using UE4DataTableInterpreter.DataTables;
using UE4DataTableInterpreter.Enums;
using UE4DataTableInterpreter.Gameflows;
using UE4DataTableInterpreter.Models;

namespace UE4DataTableInterpreter
{
    public class Program
    {
        public static Dictionary<DataTableEnum, Dictionary<string, Dictionary<string, string>>> Data = new Dictionary<DataTableEnum, Dictionary<string, Dictionary<string, string>>>()
        {
            //{ DataTableEnum.ChrInit, new Dictionary<string, Dictionary<string, string>> { } },
            { DataTableEnum.EquipItem, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.Event, new Dictionary<string, Dictionary<string, string>> {
            //    { "EVENT_001", new Dictionary<string, string> {
            //        { "RandomizedItem", "NAVI_MAP_CA01\u0000" }
            //    } }
            //} },
            //{ DataTableEnum.FullcourseAbility, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.LevelUp, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.LuckyMark, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureBT, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureBX, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureCA, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureEW, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureFZ, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureHE, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureKG, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureMI, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureRA, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureTS, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.TreasureTT, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.VBonus, new Dictionary<string, Dictionary<string, string>> {
            //    { "Vbonus_001", new Dictionary<string, string> {
            //        {  "Sora_Bonus1", "PRT_ITEM43\u0000" },
            //        {  "Sora_Ability1", "PRT_ITEM42\u0000" },
            //        {  "Sora_Bonus2", "PRT_ITEM41\u0000" },
            //        { "Sora_Ability2", "PRT_ITEM40\u0000" }
            //    } }
            //} },
            //{ DataTableEnum.WeaponEnhance, new Dictionary<string, Dictionary<string, string>> { } },
            //{ DataTableEnum.Shotlock, new Dictionary<string, Dictionary<string, string>> {
            //    { "w_so110", new Dictionary<string, string> {
            //            { "w_so120", "EMPTY ON PURPOSE" }
            //        }
            //    } }
            //}
            //{ DataTableEnum.FoodItemEffectStat, new Dictionary<string, Dictionary<string, string>> {
            //    { "FOOD_ITEM01", new Dictionary<string, string> {
            //        { "MaxHPPlus", "30" }, { "MaxMPPlus", "30" }, { "AttackPlus", "30" }, { "MagicPlus", "30" }, { "DefensePlus", "30" }
            //    } }
            //} },
            //{ DataTableEnum.EXP, new Dictionary<string, Dictionary<string, string>> {
            //    { "EXP", new Dictionary<string, string> {
            //        { "Multiplier", "10" }
            //    } }
            //} }
            //{ DataTableEnum.BaseCharStat, new Dictionary<string, Dictionary<string, string>> {
            //    { "Sora", new Dictionary<string, string> {
            //        { "MaxHitPoint", "1000" }, { "MaxMagicPoint", "1000" }, { "MaxFocusPoint", "100" }, { "AttackPower", "1000" },
            //        { "MagicPower", "1000" }, { "DefensePower", "1000" }, { "AbilityPoint", "2" }
            //    } }
            //} },
            //{ DataTableEnum.LevelUpStat, new Dictionary<string, Dictionary<string, string>> {
            //    { "1", new Dictionary<string, string> {
            //        { "AttackPower", "1000" }, { "MagicPower", "1000" }, { "DefensePower", "1000" }, { "AbilityPoint", "100" }
            //    } },
            //    { "2", new Dictionary<string, string> {
            //        { "AttackPower", "2000" }, { "MagicPower", "2000" }, { "DefensePower", "2000" }, { "AbilityPoint", "200" }
            //    } }
            //} }
            //{ DataTableEnum.WeaponEnhanceStat, new Dictionary<string, Dictionary<string, string>> {
            //    { "IW_0", new Dictionary<string, string> {
            //        { "AttackPlus", "10" }, { "MagicPlus", "6" }
            //    } },
            //    { "IW_1", new Dictionary<string, string> {
            //        { "AttackPlus", "10" }, { "MagicPlus", "10" }
            //    } }
            //} }
            { DataTableEnum.EquipItemStat, new Dictionary<string, Dictionary<string, string>> {
                { "I03001", new Dictionary<string, string> {
                    { "AP", "10" }, { "AttackPlus", "3" }, { "MagicPlus", "100" }, { "DefensePlus", "10" },
                    { "AttrResistPhysical", "20" }, { "AttrResistFire", "15" }, { "AttrResistBlizzard", "30" },
                    { "AttrResistThunder", "20" }, { "AttrResistWater", "15" }, { "AttrResistAero", "30" },
                    { "AttrResistDark", "20" }, { "AttrResistNoType", "15" }
                } }
            } },
        };

        public static Dictionary<string, bool> QualityOfLifeData = new Dictionary<string, bool>
        {
            { "BOSS_001", true }, { "BOSS_002", false }, { "BOSS_003", true }, { "BOSS_004", true },

            { "EVENT_001", true }, { "EVENT_002", false }, { "EVENT_003", true }, { "EVENT_004", true },
            { "EVENT_005", true }, { "EVENT_006", false }, { "EVENT_007", true },

            { "ITEM_001", true }, { "ITEM_002", false }, { "ITEM_003", true }, { "ITEM_004", true }
        };

        public static Dictionary<string, List<string>> HintData = new Dictionary<string, List<string>>
        {
            { "SecretReport02", new List<string> { "This is a test", "This is another test", "Yet Another Test", "Final Test" } }, { "SecretReport03", new List<string> { "TTest3" } }, { "SecretReport04", new List<string> { "TTest4" } }, { "SecretReport05", new List<string> { "TTest5" } },
            { "SecretReport06", new List<string> { "TTest6" } }, { "SecretReport07", new List<string> { "TTest7" } }, { "SecretReport08", new List<string> { "TTest8" } }, { "SecretReport09", new List<string> { "TTest9" } },
            { "SecretReport10", new List<string> { "TTest10" } }, { "SecretReport11", new List<string> { "TTest11" } }, { "SecretReport12", new List<string> { "TTest12" } }, { "SecretReport13", new List<string> { "TTest13" } },
            { "SecretReport14", new List<string> { "TTest14" } }
        };

        public static Dictionary<string, Enemy> RandomizedBosses = new Dictionary<string, Enemy> {
            { "Steel Titan", new Enemy { FilePath = @"Content/Levels/bx/bx_01/umap/bx_01_enemy_03", Address = 0x2FF5, EnemyPath = "/Game/Blueprints/Enemy/e_ex781/e_ex781_Pawn.e_ex781_Pawn_C\u0000" } }
            //{ "Demon Tide", new Enemy { FilePath = @"Content/Levels/tt/tt_01/umap/tt_01_enemy_02", Address = 0x5291, EnemyPath = "/Game/Blueprints/Enemy/e_ex816/e_ex816_Pawn.e_ex816_Pawn_C\u0000" } }
        };

        public static Dictionary<string, long> uAssetIds = new Dictionary<string, long>();

        public static void Main(string[] args)
        {
            var dataTableManager = new DataTableManager();

            //var tempQoL = dataTableManager.GenerateQualityOfLifeDataTable(QualityOfLifeData);

            //foreach (var te in tempQoL)
            //{
            //    File.WriteAllBytes(te.Key.Split('/')[^1], te.Value.ToArray());
            //}

            //var tempHint = dataTableManager.GenerateHintDataTable(HintData);

            //foreach (var te in tempHint)
            //{
            //    File.WriteAllBytes(te.Key.Split('/')[^1], te.Value.ToArray());
            //}

            var bosses = dataTableManager.RandomizeBossDataTables(RandomizedBosses);

            foreach (var file in bosses)
            {
                File.WriteAllBytes(file.Key.Split('/')[^1], file.Value.ToArray());
            }

            var filesToWrite = dataTableManager.RandomizeDataTables(Data);

            foreach (var file in filesToWrite)
            {
                File.WriteAllBytes(file.Key.Split('/')[^1], file.Value.ToArray());
            }

            var tempList = new List<string> { "Pole Spin is on Sora's Level Ups [5 (TypeC) - 10 (TypeB) - 8 (TypeC)]", "Oblivion is in Toy Box in Large Chest 1", "Guard is in The Keyblade Graveyard (Terra-Xehanort & Vanitas Boss) on Shooting Star on Level 8", "Proof of Fantasy is on Lucky Emblem Milestone 5",
                                              "There is 1 Check in Arendelle.", "There is 1 Check in Toy Box.", "There is 1 Check in Unreality.", "There is 1 Check in The Caribbean." };

            var mobile = new Mobile();
            var t = mobile.Process(tempList);

            File.WriteAllBytes("mobile_test.locres", t.ToArray());

            using var writer = new StreamWriter("uAssetIds_v2.json");
            
            string[] filePaths = Directory.GetFiles(@"D:/WaterKH/Repositories/UE4DataTableInterpreter/UE4DataTableInterpreter/Content/", "*.uasset", SearchOption.AllDirectories);
            foreach (var path in filePaths)
            {
                using var reader = new FileStream(path, FileMode.Open);

                var uAsset = new uAsset();
                uAsset.Decompile(reader);

                // Add to DataToWrite
                foreach (var asset in uAsset.AssetStrings)
                {
                    if (!uAssetIds.ContainsKey(asset.AssetName))
                        uAssetIds.Add(asset.AssetName, asset.Id);
                }
            }


            var temp = JsonSerializer.Serialize(uAssetIds);

            writer.WriteLine(temp);
            writer.Flush();



            

            //Console.WriteLine(umap.AssetStrings.IndexOf(umap.AssetStrings.FirstOrDefault(x => x.AssetName.Contains("BP_TresConvertItemIDtoKeyNameWeapon"))));

            //Console.WriteLine(umap.AssetStrings[0xc].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x40].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x18].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x04].AssetName);
            
            //Console.WriteLine(umap.AssetStrings[0x01].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x24].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x38f].AssetName);
            //Console.WriteLine(umap.AssetStrings[0xf].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x394].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x04].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x32e].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x38f].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x379].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x38d].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x39e].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x37a].AssetName);
            //Console.WriteLine(umap.AssetStrings[0x37c].AssetName);
            //using var uexpReader = new FileStream(@"D:\WaterKH\Repositories\UE4DataTableInterpreter\UE4DataTableInterpreter\Content\GameFlows\tt_01_gameflow.uexp", FileMode.Open);


            //var uexp = new uExp();
            //uexp.Decompile<GameflowTT>(reader, umap.AssetStrings);

            dataTableManager = new DataTableManager();
            var randomizedData = new Dictionary<DataTableEnum, Dictionary<string, Dictionary<string, string>>>
            {
                { DataTableEnum.ChrInit, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.EquipItem, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.Event, new Dictionary<string, Dictionary<string, string>> {
                    { "TresUIMobilePortalDataAsset", new Dictionary<string, string> { { "Reward", "WEP_GOOFY_01\u0000" } } } } },
                { DataTableEnum.FullcourseAbility, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.LevelUp, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.LuckyMark, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureBT, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureBX, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureCA, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureEW, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureFZ, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureHE, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureKG, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureMI, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureRA, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureTS, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.TreasureTT, new Dictionary<string, Dictionary<string, string>> { } },
                { DataTableEnum.VBonus, new Dictionary<string, Dictionary<string, string>> {
                { "Vbonus_001", new Dictionary<string, string> {
                    {  "Sora_Bonus1", "ETresVictoryBonusKind::HP_UP10\u0000" },
                    {  "Sora_Ability1", "PRT_ITEM42\u0000" },
                    {  "Sora_Bonus2", "ETresVictoryBonusKind::NONE\u0000" },
                    { "Sora_Ability2", "ETresAbilityKind::FLASH_STEP\u0000" }
                } }
            } },
                { DataTableEnum.WeaponEnhance, new Dictionary<string, Dictionary<string, string>> { } }
            };

            #region Decompiling

            dataTableManager.RandomizeDataTables(randomizedData);
            Console.WriteLine("Test");
            //foreach (var (key, value) in Data)
            //{
            //    foreach (var (rowName, value2) in value)
            //    {
            //        writer.WriteLine(rowName);
            //    }
            //}
            

            //var treasures = new Dictionary<string, DataTable>
            //{
            //    { "TresTreasureDataBT", new DataTable() },
            //    { "TresTreasureDataBX", new DataTable() },
            //    { "TresTreasureDataCA", new DataTable() },
            //    //{ "TresTreasureDataDW", new TreasureDataTableEntry() },
            //    { "TresTreasureDataEW", new DataTable() },
            //    { "TresTreasureDataFZ", new DataTable() },
            //    { "TresTreasureDataHE", new DataTable() },
            //    { "TresTreasureDataKG", new DataTable() },
            //    { "TresTreasureDataMI", new DataTable() },
            //    { "TresTreasureDataRA", new DataTable() },
            //    { "TresTreasureDataTS", new DataTable() },
            //    { "TresTreasureDataTT", new DataTable() }
            //};

            //foreach (var treasure in treasures)
            //{
            //    var uAsset = new uAsset();

            //    using var reader = File.OpenRead($"{treasure.Key}.uasset");

            //    treasure.Value.uAsset = uAsset.Decompile(reader);

            //    //for (int i = 0; i < treasure.Value.uAsset.AssetStrings.Count; ++i)
            //    //{
            //    //    Console.WriteLine($"{i} ({i.ToString("X")}): {treasure.Value.uAsset.AssetStrings[i].AssetName}");
            //    //}

            //    Console.WriteLine();

            //    var uExp = new uExp();

            //    using var readerExp = File.OpenRead($"{treasure.Key}.uexp");

            //    treasure.Value.uExp = uExp.Decompile<TreasureDataTableEntry>(readerExp);

            //    foreach (TreasureDataTableEntry treasureEntry in treasure.Value.uExp.DataTableEntries)
            //    {
            //        Console.WriteLine($"TreasureBox: {treasure.Value.uAsset.AssetStrings[(int)treasureEntry.Id].AssetName} - TreasureName: {treasure.Value.uAsset.AssetStrings[(int)treasureEntry.Treasure].AssetName}");
            //    }

            //    Console.WriteLine();
            //}

            //var vbonuses = new Dictionary<string, DataTable>
            //{
            //    { "TresVBonusData", new DataTable() }
            //};

            //foreach (var vbonus in vbonuses)
            //{
            //    var uAsset = new uAsset();

            //    using var reader = File.OpenRead($"{vbonus.Key}.uasset");

            //    vbonus.Value.uAsset = uAsset.Decompile(reader);

            //    for (int i = 0; i < vbonus.Value.uAsset.AssetStrings.Count; ++i)
            //    {
            //        Console.WriteLine($"{i} ({i.ToString("X")}): {vbonus.Value.uAsset.AssetStrings[i].AssetName}");
            //    }

            //    Console.WriteLine();

            //    var uExp = new uExp();

            //    using var readerExp = File.OpenRead($"{vbonus.Key}.uexp");

            //    vbonus.Value.uExp = uExp.Decompile<VBonusDataTableEntry>(readerExp);

            //    foreach (VBonusDataTableEntry vbonusEntry in vbonus.Value.uExp.DataTableEntries)
            //    {
            //        Console.WriteLine($"VBonus: {vbonus.Value.uAsset.AssetStrings[(int)vbonusEntry.Id].AssetName} - Abilities: {vbonus.Value.uAsset.AssetStrings[(int)vbonusEntry.m_Sora1.ETresVictoryAbilityType].AssetName}");
            //    }

            //    Console.WriteLine();
            //}

            //var levelUpData = new Dictionary<string, DataTable>
            //{
            //    { "p_ex001_LevelUpData", new DataTable() }
            //};

            //foreach (var levelUp in levelUpData)
            //{
            //    var uAsset = new uAsset();

            //    using var reader = File.OpenRead($"{levelUp.Key}.uasset");

            //    levelUp.Value.uAsset = uAsset.Decompile(reader);

            //    for (int i = 0; i < levelUp.Value.uAsset.AssetStrings.Count; ++i)
            //    {
            //        Console.WriteLine($"{i} ({i.ToString("X")}): {levelUp.Value.uAsset.AssetStrings[i].AssetName}");
            //    }

            //    Console.WriteLine();

            //    var uExp = new uExp();

            //    using var readerExp = File.OpenRead($"{levelUp.Key}.uexp");

            //    levelUp.Value.uExp = uExp.Decompile<LevelUpDataTableEntry>(readerExp);

            //    foreach (LevelUpDataTableEntry levelUpEntry in levelUp.Value.uExp.DataTableEntries)
            //    {
            //        Console.WriteLine($"LevelUp: {levelUp.Value.uAsset.AssetStrings[(int)levelUpEntry.Id].AssetName} - LevelUp Ability: {levelUp.Value.uAsset.AssetStrings[(int)levelUpEntry.AbilityValue_1].AssetName}");
            //    }

            //    Console.WriteLine();
            //}

            //var equipItemData = new Dictionary<string, DataTable>
            //{
            //    { "TresEquipItemData", new DataTable() }
            //};

            //foreach (var equipItem in equipItemData)
            //{
            //    var uAsset = new uAsset();

            //    using var reader = File.OpenRead($"{equipItem.Key}.uasset");

            //    equipItem.Value.uAsset = uAsset.Decompile(reader);

            //    for (int i = 0; i < equipItem.Value.uAsset.AssetStrings.Count; ++i)
            //    {
            //        Console.WriteLine($"{i} ({i.ToString("X")}): {equipItem.Value.uAsset.AssetStrings[i].AssetName}");
            //    }

            //    Console.WriteLine();

            //    var uExp = new uExp();

            //    using var readerExp = File.OpenRead($"{equipItem.Key}.uexp");

            //    equipItem.Value.uExp = uExp.Decompile<EquipItemDataTableEntry>(readerExp);

            //    foreach (EquipItemDataTableEntry equipItemEntry in equipItem.Value.uExp.DataTableEntries)
            //    {
            //        Console.WriteLine($"EquipItem: {equipItem.Value.uAsset.AssetStrings[(int)equipItemEntry.Id].AssetName} - EquipItem Name: {equipItem.Value.uAsset.AssetStrings[(int)equipItemEntry.KeyName].AssetName}");
            //    }

            //    Console.WriteLine();
            //}

            //var fullcourseAbilityData = new Dictionary<string, DataTable>
            //{
            //    { "TresFullcourseAbilityList", new DataTable() }
            //};

            //foreach (var fullcourseAbility in fullcourseAbilityData)
            //{
            //    var uAsset = new uAsset();

            //    using var reader = File.OpenRead($"{fullcourseAbility.Key}.uasset");

            //    fullcourseAbility.Value.uAsset = uAsset.Decompile(reader);

            //    for (int i = 0; i < fullcourseAbility.Value.uAsset.AssetStrings.Count; ++i)
            //    {
            //        Console.WriteLine($"{i} ({i.ToString("X")}): {fullcourseAbility.Value.uAsset.AssetStrings[i].AssetName}");
            //    }

            //    Console.WriteLine();

            //    var uExp = new uExp();

            //    using var readerExp = File.OpenRead($"{fullcourseAbility.Key}.uexp");

            //    fullcourseAbility.Value.uExp = uExp.Decompile<FullcourseAbilityDataTableEntry>(readerExp);

            //    foreach (FullcourseAbilityDataTableEntry abilityEntry in fullcourseAbility.Value.uExp.DataTableEntries)
            //    {
            //        Console.WriteLine($"FullcourseAbility: {fullcourseAbility.Value.uAsset.AssetStrings[(int)abilityEntry.Id].AssetName} - FullcourseAbility Name: {fullcourseAbility.Value.uAsset.AssetStrings[(int)abilityEntry.Ability].AssetName}");
            //    }

            //    Console.WriteLine();
            //}

            //var luckyMarkData = new Dictionary<string, DataTable>
            //{
            //    { "TresLuckyMarkMilestoneRewardData", new DataTable() }
            //};

            //foreach (var luckyMark in luckyMarkData)
            //{
            //    var uAsset = new uAsset();

            //    using var reader = File.OpenRead($"{luckyMark.Key}.uasset");

            //    luckyMark.Value.uAsset = uAsset.Decompile(reader);

            //    for (int i = 0; i < luckyMark.Value.uAsset.AssetStrings.Count; ++i)
            //    {
            //        Console.WriteLine($"{i} ({i.ToString("X")}): {luckyMark.Value.uAsset.AssetStrings[i].AssetName}");
            //    }

            //    Console.WriteLine();

            //    var uExp = new uExp();

            //    using var readerExp = File.OpenRead($"{luckyMark.Key}.uexp");

            //    luckyMark.Value.uExp = uExp.Decompile<LuckyMarkDataTableEntry>(readerExp);

            //    foreach (LuckyMarkDataTableEntry luckyMarkEntry in luckyMark.Value.uExp.DataTableEntries)
            //    {
            //        Console.WriteLine($"LuckyMark: {luckyMark.Value.uAsset.AssetStrings[(int)luckyMarkEntry.Id].AssetName} - LuckyMark Name: {luckyMark.Value.uAsset.AssetStrings[(int)luckyMarkEntry.Treasure].AssetName}");
            //    }

            //    Console.WriteLine();
            //}

            //var weaponEnhanceData = new Dictionary<string, DataTable>
            //{
            //    { "TresItemWeaponEnhanceData", new DataTable() }
            //};

            //foreach (var weapon in weaponEnhanceData)
            //{
            //    var uAsset = new uAsset();

            //    using var reader = File.OpenRead($"{weapon.Key}.uasset");

            //    weapon.Value.uAsset = uAsset.Decompile(reader);

            //    for (int i = 0; i < weapon.Value.uAsset.AssetStrings.Count; ++i)
            //    {
            //        Console.WriteLine($"{i} ({i.ToString("X")}): {weapon.Value.uAsset.AssetStrings[i].AssetName}");
            //    }

            //    Console.WriteLine();

            //    var uExp = new uExp();

            //    using var readerExp = File.OpenRead($"{weapon.Key}.uexp");

            //    weapon.Value.uExp = uExp.Decompile<WeaponEnhanceDataTableEntry>(readerExp);

            //    foreach (WeaponEnhanceDataTableEntry weaponEntry in weapon.Value.uExp.DataTableEntries)
            //    {
            //        Console.WriteLine($"WeaponEnhance: {weapon.Value.uAsset.AssetStrings[(int)weaponEntry.IW].AssetName}_{weaponEntry.Id} - WeaponEnhance Name: {weapon.Value.uAsset.AssetStrings[(int)weaponEntry.Weapon].AssetName}");
            //    }

            //    Console.WriteLine();
            //}

            //var initData = new Dictionary<string, DataTable>
            //{
            //    { "TresChrInitData", new DataTable() }
            //};

            //foreach (var init in initData)
            //{
            //    var uAsset = new uAsset();

            //    using var reader = File.OpenRead($"{init.Key}.uasset");

            //    init.Value.uAsset = uAsset.Decompile(reader);

            //    for (int i = 0; i < init.Value.uAsset.AssetStrings.Count; ++i)
            //    {
            //        Console.WriteLine($"{i} ({i.ToString("X")}): {init.Value.uAsset.AssetStrings[i].AssetName}");
            //    }

            //    Console.WriteLine();

            //    var uExp = new uExp();

            //    using var readerExp = File.OpenRead($"{init.Key}.uexp");

            //    init.Value.uExp = uExp.Decompile<ChrInitDataTableEntry>(readerExp);

            //    foreach (ChrInitDataTableEntry initEntry in init.Value.uExp.DataTableEntries)
            //    {
            //        Console.WriteLine($"Init: {init.Value.uAsset.AssetStrings[(int)initEntry.Id].AssetName} - Init Name: {init.Value.uAsset.AssetStrings[(int)initEntry.Weapons[0]].AssetName}");
            //    }

            //    Console.WriteLine();
            //}

            #endregion Decompiling

            #region Changing + Recompiling



            #endregion Changing + Recompiling
        }
    }
}