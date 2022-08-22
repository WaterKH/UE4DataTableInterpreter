using System;
using System.Collections.Generic;
using System.IO;
using UE4DataTableInterpreter.Interfaces;

namespace UE4DataTableInterpreter.DataTables
{
    public class CharacterBaseDataTableEntry : IDataTable
    {
        public CharacterBaseDataTableEntry() { }

        public long Id { get; set; }
        public int IndexId { get { return (int)Id; } set { } }
        public string RowName { get; set; }

        #region Props

        public long m_MaxHitPoint { get; set; }
        public long MaxHitPoint_IntProperty { get; set; }
        public List<byte> Unk1 { get; set; } // 9 bytes
        public int MaxHitPointValue { get; set; }

        public long m_MaxHPRate { get; set; }
        public long MaxHPRate_FloatProperty { get; set; }
        public List<byte> Unk2 { get; set; } // 9 bytes
        public float MaxHPRateValue { get; set; }

        public long m_MaxMagicPoint { get; set; }
        public long MaxMagicPoint_IntProperty { get; set; }
        public List<byte> Unk3 { get; set; } // 9 bytes
        public int MaxMagicPointValue { get; set; }

        public long m_MaxFocusPoint { get; set; }
        public long MaxFocusPoint_IntProperty { get; set; }
        public List<byte> Unk4 { get; set; } // 9 bytes
        public int MaxFocusPointValue { get; set; }

        public long m_AttackPower { get; set; }
        public long AttackPower_IntProperty { get; set; }
        public List<byte> Unk5 { get; set; } // 9 bytes
        public int AttackPowerValue { get; set; }

        public long m_MagicPower { get; set; }
        public long MagicPower_IntProperty { get; set; }
        public List<byte> Unk6 { get; set; } // 9 bytes
        public int MagicPowerValue { get; set; }

        public long m_DefensePower { get; set; }
        public long DefensePower_IntProperty { get; set; }
        public List<byte> Unk7 { get; set; } // 9 bytes
        public int DefensePowerValue { get; set; }

        public long m_AbilityPoint { get; set; }
        public long AbilityPoint_IntProperty { get; set; }
        public List<byte> Unk8 { get; set; } // 9 bytes
        public int AbilityPointValue { get; set; }

        public long m_ExpRate { get; set; }
        public long ExpRate_FloatProperty { get; set; }
        public List<byte> Unk9 { get; set; } // 9 bytes
        public float ExpRateValue { get; set; }

        public long m_BodyPushPower { get; set; }
        public long BodyPushPower_EnumProperty { get; set; }
        public long Unk10 { get; set; }
        public long BodyPushPowerLevel { get; set; }
        public byte Unk11 { get; set; }
        public long ETresBodyPushPowerLevel { get; set; }

        public long m_BioType { get; set; }
        public long BioType_EnumProperty { get; set; }
        public long Unk12 { get; set; }
        public long ChrBiologicalType { get; set; }
        public byte Unk13 { get; set; }
        public long ETresChrBiologicalType { get; set; }

        public long m_AttractionRate { get; set; }
        public long AttractionRate_FloatProperty { get; set; }
        public List<byte> Unk14 { get; set; } // 9 bytes
        public float AttractionRateValue { get; set; }

        public long m_MaxBodyStrongValue { get; set; }
        public long MaxBodyStrongValue_IntProperty { get; set; }
        public List<byte> Unk15 { get; set; } // 9 bytes
        public int MaxBodyStrongValue_Value { get; set; }

        public long m_MaxArmorHP { get; set; }
        public long MaxArmorHP_IntProperty { get; set; }
        public List<byte> Unk16 { get; set; } // 9 bytes
        public int MaxArmorHPValue { get; set; }

        public long m_DamageMin { get; set; }
        public long DamageMin_IntProperty { get; set; }
        public List<byte> Unk17 { get; set; } // 9 bytes
        public int DamageMinValue { get; set; }

        public long m_DamageMax { get; set; }
        public long DamageMax_IntProperty { get; set; }
        public List<byte> Unk18 { get; set; } // 9 bytes
        public int DamageMaxValue { get; set; }

        public long m_NeedReactionSameTeamZeroDmgAtk { get; set; }
        public long NeedReactionSameTeamZeroDmgAtk_BoolProperty { get; set; }
        public List<byte> Unk19 { get; set; } // 8 bytes
        public byte NeedReactionSameTeamZeroDmgAtkValue { get; set; }
        public byte ExtraUnk1 { get; set; }

        public long m_RevengeLimit { get; set; }
        public long RevengeLimit_FloatProperty { get; set; }
        public List<byte> Unk20 { get; set; } // 9 bytes
        public float RevengeLimitValue { get; set; }

        public long m_MaxRevengeCount { get; set; }
        public long MaxRevengeCount_IntProperty { get; set; }
        public List<byte> Unk21 { get; set; } // 9 bytes
        public int MaxRevengeCountValue { get; set; }

        public long m_RevengeCoolDownTime { get; set; }
        public long RevengeCoolDownTime_FloatProperty { get; set; }
        public List<byte> Unk22 { get; set; } // 9 bytes
        public float RevengeCoolDownTimeValue { get; set; }

        public long m_RevengeCoefficientPhysical { get; set; }
        public long RevengeCoefficientPhysical_FloatProperty { get; set; }
        public List<byte> Unk23 { get; set; } // 9 bytes
        public float RevengeCoefficientPhysicalValue { get; set; }

        public long m_RevengeCoefficientFire { get; set; }
        public long RevengeCoefficientFire_FloatProperty { get; set; }
        public List<byte> Unk24 { get; set; } // 9 bytes
        public float RevengeCoefficientFireValue { get; set; }

        public long m_RevengeCoefficientBlizzard { get; set; }
        public long RevengeCoefficientBlizzard_FloatProperty { get; set; }
        public List<byte> Unk25 { get; set; } // 9 bytes
        public float RevengeCoefficientBlizzardValue { get; set; }

        public long m_RevengeCoefficientThunder { get; set; }
        public long RevengeCoefficientThunder_FloatProperty { get; set; }
        public List<byte> Unk26 { get; set; } // 9 bytes
        public float RevengeCoefficientThunderValue { get; set; }

        public long m_RevengeCoefficientWater { get; set; }
        public long RevengeCoefficientWater_FloatProperty { get; set; }
        public List<byte> Unk27 { get; set; } // 9 bytes
        public float RevengeCoefficientWaterValue { get; set; }

        public long m_RevengeCoefficientAero { get; set; }
        public long RevengeCoefficientAero_FloatProperty { get; set; }
        public List<byte> Unk28 { get; set; } // 9 bytes
        public float RevengeCoefficientAeroValue { get; set; }

        public long m_RevengeCoefficientDark { get; set; }
        public long RevengeCoefficientDark_FloatProperty { get; set; }
        public List<byte> Unk29 { get; set; } // 9 bytes
        public float RevengeCoefficientDarkValue { get; set; }

        public long m_RevengeCoefficientNoType { get; set; }
        public long RevengeCoefficientNoType_FloatProperty { get; set; }
        public List<byte> Unk30 { get; set; } // 9 bytes
        public float RevengeCoefficientNoTypeValue { get; set; }

        public long m_AttrResistPhysical { get; set; }
        public long AttrResistPhysical_IntProperty { get; set; }
        public List<byte> Unk31 { get; set; } // 9 bytes
        public int AttrResistPhysicalValue { get; set; }

        public long m_AttrResistFire { get; set; }
        public long AttrResistFire_IntProperty { get; set; }
        public List<byte> Unk32 { get; set; } // 9 bytes
        public int AttrResistFireValue { get; set; }

        public long m_AttrResistBlizzard { get; set; }
        public long AttrResistBlizzard_IntProperty { get; set; }
        public List<byte> Unk33 { get; set; } // 9 bytes
        public int AttrResistBlizzardValue { get; set; }

        public long m_AttrResistThunder { get; set; }
        public long AttrResistThunder_IntProperty { get; set; }
        public List<byte> Unk34 { get; set; } // 9 bytes
        public int AttrResistThunderValue { get; set; }

        public long m_AttrResistWater { get; set; }
        public long AttrResistWater_IntProperty { get; set; }
        public List<byte> Unk35 { get; set; } // 9 bytes
        public int AttrResistWaterValue { get; set; }

        public long m_AttrResistAero { get; set; }
        public long AttrResistAero_IntProperty { get; set; }
        public List<byte> Unk36 { get; set; } // 9 bytes
        public int AttrResistAeroValue { get; set; }

        public long m_AttrResistDark { get; set; }
        public long AttrResistDark_IntProperty { get; set; }
        public List<byte> Unk37 { get; set; } // 9 bytes
        public int AttrResistDarkValue { get; set; }

        public long m_AttrResistNoType { get; set; }
        public long AttrResistNoType_IntProperty { get; set; }
        public List<byte> Unk38 { get; set; } // 9 bytes
        public int AttrResistNoTypeValue { get; set; }

        public long m_ResistRapidFire { get; set; }
        public long ResistRapidFire_IntProperty { get; set; }
        public List<byte> Unk39 { get; set; } // 9 bytes
        public int ResistRapidFireValue { get; set; }

        public long m_ResistComboParam { get; set; }
        public long ResistComboParam_IntProperty { get; set; }
        public List<byte> Unk40 { get; set; } // 9 bytes
        public int ResistComboParamValue { get; set; }

        public long m_AttrWeekPointPhysical { get; set; }
        public long AttrWeekPointPhysical_BoolProperty { get; set; }
        public List<byte> Unk41 { get; set; } // 8 bytes
        public byte AttrWeekPointPhysicalValue { get; set; }
        public byte ExtraUnk2 { get; set; }

        public long m_AttrWeekPointFire { get; set; }
        public long AttrWeekPointFire_BoolProperty { get; set; }
        public List<byte> Unk42 { get; set; } // 8 bytes
        public byte AttrWeekPointFireValue { get; set; }
        public byte ExtraUnk3 { get; set; }

        public long m_AttrWeekPointBlizzard { get; set; }
        public long AttrWeekPointBlizzard_BoolProperty { get; set; }
        public List<byte> Unk43 { get; set; } // 8 bytes
        public byte AttrWeekPointBlizzardValue { get; set; }
        public byte ExtraUnk4 { get; set; }

        public long m_AttrWeekPointThunder { get; set; }
        public long AttrWeekPointThunder_BoolProperty { get; set; }
        public List<byte> Unk44 { get; set; } // 8 bytes
        public byte AttrWeekPointThunderValue { get; set; }
        public byte ExtraUnk5 { get; set; }

        public long m_AttrWeekPointWater { get; set; }
        public long AttrWeekPointWater_BoolProperty { get; set; }
        public List<byte> Unk45 { get; set; } // 8 bytes
        public byte AttrWeekPointWaterValue { get; set; }
        public byte ExtraUnk6 { get; set; }

        public long m_AttrWeekPointAero { get; set; }
        public long AttrWeekPointAero_BoolProperty { get; set; }
        public List<byte> Unk46 { get; set; } // 8 bytes
        public byte AttrWeekPointAeroValue { get; set; }
        public byte ExtraUnk7 { get; set; }

        public long m_AttrWeekPointDark { get; set; }
        public long AttrWeekPointDark_BoolProperty { get; set; }
        public List<byte> Unk47 { get; set; } // 8 bytes
        public byte AttrWeekPointDarkValue { get; set; }
        public byte ExtraUnk8 { get; set; }

        public long m_AttrWeekPointNoType { get; set; }
        public long AttrWeekPointNoType_BoolProperty { get; set; }
        public List<byte> Unk48 { get; set; } // 8 bytes
        public byte AttrWeekPointNoTypeValue { get; set; }
        public byte ExtraUnk9 { get; set; }

        public long m_bResistEffectFreeFlow { get; set; }
        public long ResistEffectFreeFlow_BoolProperty { get; set; }
        public List<byte> Unk49 { get; set; } // 8 bytes
        public byte ResistEffectFreeFlowValue { get; set; }
        public byte ExtraUnk10 { get; set; }

        public long m_bResistEffectDeath { get; set; }
        public long ResistEffectDeath_BoolProperty { get; set; }
        public List<byte> Unk50 { get; set; } // 8 bytes
        public byte ResistEffectDeathValue { get; set; }
        public byte ExtraUnk11 { get; set; }

        public long m_bResistEffectCatch { get; set; }
        public long ResistEffectCatch_BoolProperty { get; set; }
        public List<byte> Unk51 { get; set; } // 8 bytes
        public byte ResistEffectCatchValue { get; set; }
        public byte ExtraUnk12 { get; set; }

        public long m_bResistEffectDrillBind { get; set; }
        public long ResistEffectDrillBind_BoolProperty { get; set; }
        public List<byte> Unk52 { get; set; } // 8 bytes
        public byte ResistEffectDrillBindValue { get; set; }
        public byte ExtraUnk13 { get; set; }

        public long m_bResistEffectYoBind { get; set; }
        public long ResistEffectYoBind_BoolProperty { get; set; }
        public List<byte> Unk53 { get; set; } // 8 bytes
        public byte ResistEffectYoBindValue { get; set; }
        public byte ExtraUnk14 { get; set; }

        public long m_bResistEffectRalphBind { get; set; }
        public long ResistEffectRalphBind_BoolProperty { get; set; }
        public List<byte> Unk54 { get; set; } // 8 bytes
        public byte ResistEffectRalphBindValue { get; set; }
        public byte ExtraUnk15 { get; set; }

        public long m_bResistEffectEnergyBurst { get; set; }
        public long ResistEffectEnergyBurst_BoolProperty { get; set; }
        public List<byte> Unk55 { get; set; } // 8 bytes
        public byte ResistEffectEnergyBurstValue { get; set; }
        public byte ExtraUnk16 { get; set; }

        public long m_bResistEffectFreeze { get; set; }
        public long ResistEffectFreeze_BoolProperty { get; set; }
        public List<byte> Unk56 { get; set; } // 8 bytes
        public byte ResistEffectFreezeValue { get; set; }
        public byte ExtraUnk17 { get; set; }

        public long m_bResistEffectStop { get; set; }
        public long ResistEffectStop_BoolProperty { get; set; }
        public List<byte> Unk57 { get; set; } // 8 bytes
        public byte ResistEffectStopValue { get; set; }
        public byte ExtraUnk18 { get; set; }

        public long m_bResistEffectMagnet { get; set; }
        public long ResistEffectMagnet_BoolProperty { get; set; }
        public List<byte> Unk58 { get; set; } // 8 bytes
        public byte ResistEffectMagnetValue { get; set; }
        public byte ExtraUnk19 { get; set; }

        public long m_bResistEffectStun { get; set; }
        public long ResistEffectStun_BoolProperty { get; set; }
        public List<byte> Unk59 { get; set; } // 8 bytes
        public byte ResistEffectStunValue { get; set; }
        public byte ExtraUnk20 { get; set; }

        public long m_bResistEffectSneeze { get; set; }
        public long ResistEffectSneeze_BoolProperty { get; set; }
        public List<byte> Unk60 { get; set; } // 8 bytes
        public byte ResistEffectSneezeValue { get; set; }
        public byte ExtraUnk21 { get; set; }

        public long m_bResistEffectHoney { get; set; }
        public long ResistEffectHoney_BoolProperty { get; set; }
        public List<byte> Unk61 { get; set; } // 8 bytes
        public byte ResistEffectHoneyValue { get; set; }
        public byte ExtraUnk22 { get; set; }

        public long m_bResistEffectCloud { get; set; }
        public long ResistEffectCloud_BoolProperty { get; set; }
        public List<byte> Unk62 { get; set; } // 8 bytes
        public byte ResistEffectCloudValue { get; set; }
        public byte ExtraUnk23 { get; set; }

        public long m_bResistEffectDischarge { get; set; }
        public long ResistEffectDischarge_BoolProperty { get; set; }
        public List<byte> Unk63 { get; set; } // 8 bytes
        public byte ResistEffectDischargeValue { get; set; }
        public byte ExtraUnk24 { get; set; }

        public long m_bResistEffectBurn { get; set; }
        public long ResistEffectBurn_BoolProperty { get; set; }
        public List<byte> Unk64 { get; set; } // 8 bytes
        public byte ResistEffectBurnValue { get; set; }
        public byte ExtraUnk25 { get; set; }

        public long m_bResistEffectPoleSpinTurn { get; set; }
        public long ResistEffectPoleSpinTurn_BoolProperty { get; set; }
        public List<byte> Unk65 { get; set; } // 8 bytes
        public byte ResistEffectPoleSpinTurnValue { get; set; }
        public byte ExtraUnk26 { get; set; }

        public long m_DropPrize1 { get; set; }
        public long DropPrize1_EnumProperty { get; set; }
        public long Unk66 { get; set; }
        public long DropPrizeID { get; set; }
        public byte Unk67 { get; set; }
        public long ETresDropPrizeID { get; set; }

        public long m_NumDropPrize1 { get; set; }
        public long NumDropPrize1_IntProperty { get; set; }
        public List<byte> Unk68 { get; set; } // 9 bytes
        public int NumDropPrize1Value { get; set; }

        public long m_DropPrize2 { get; set; }
        public long DropPrize2_EnumProperty { get; set; }
        public long Unk69 { get; set; }
        public long DropPrizeID2 { get; set; }
        public byte Unk70 { get; set; }
        public long ETresDropPrizeID2 { get; set; }

        public long m_NumDropPrize2 { get; set; }
        public long NumDropPrize2_IntProperty { get; set; }
        public List<byte> Unk71 { get; set; } // 9 bytes
        public int NumDropPrize2Value { get; set; }

        public long m_DropItem1 { get; set; }
        public long DropItem1_EnumProperty { get; set; }
        public long Unk72 { get; set; }
        public long DropItemID { get; set; }
        public byte Unk73 { get; set; }
        public long ETresDropItemID { get; set; }

        public long m_NumDropItem1 { get; set; }
        public long NumDropItem1_IntProperty { get; set; }
        public List<byte> Unk74 { get; set; } // 9 bytes
        public int NumDropItem1Value { get; set; }

        public long m_DropItem2 { get; set; }
        public long DropItem2_EnumProperty { get; set; }
        public long Unk75 { get; set; }
        public long DropItemID2 { get; set; }
        public byte Unk76 { get; set; }
        public long ETresDropItemID2 { get; set; }

        public long m_NumDropItem2 { get; set; }
        public long NumDropItem2_IntProperty { get; set; }
        public List<byte> Unk77 { get; set; } // 9 bytes
        public int NumDropItem2Value { get; set; }

        public long m_DropItem3 { get; set; }
        public long DropItem3_EnumProperty { get; set; }
        public long Unk78 { get; set; }
        public long DropItemID3 { get; set; }
        public byte Unk79 { get; set; }
        public long ETresDropItemID3 { get; set; }

        public long m_NumDropItem3 { get; set; }
        public long NumDropItem3_IntProperty { get; set; }
        public List<byte> Unk80 { get; set; } // 9 bytes
        public int NumDropItem3Value { get; set; }

        #endregion Props

        public long None { get; set; }


        public IDataTable Decompile(FileStream reader)
        {
            this.Id = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_MaxHitPoint = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MaxHitPoint_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk1 = reader.ReadBytesFromFileStream(9);
            this.MaxHitPointValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MaxHPRate = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MaxHPRate_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk2 = reader.ReadBytesFromFileStream(9);
            this.MaxHPRateValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MaxMagicPoint = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MaxMagicPoint_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk3 = reader.ReadBytesFromFileStream(9);
            this.MaxMagicPointValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MaxFocusPoint = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MaxFocusPoint_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk4 = reader.ReadBytesFromFileStream(9);
            this.MaxFocusPointValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttackPower = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttackPower_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk5 = reader.ReadBytesFromFileStream(9);
            this.AttackPowerValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MagicPower = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MagicPower_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk6 = reader.ReadBytesFromFileStream(9);
            this.MagicPowerValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_DefensePower = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DefensePower_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk7 = reader.ReadBytesFromFileStream(9);
            this.DefensePowerValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AbilityPoint = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AbilityPoint_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk8 = reader.ReadBytesFromFileStream(9);
            this.AbilityPointValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_ExpRate = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ExpRate_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk9 = reader.ReadBytesFromFileStream(9);
            this.ExpRateValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_BodyPushPower = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BodyPushPower_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk10 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BodyPushPowerLevel = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk11 = (byte)reader.ReadByte();
            this.ETresBodyPushPowerLevel = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_BioType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.BioType_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk12 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ChrBiologicalType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk13 = (byte)reader.ReadByte();
            this.ETresChrBiologicalType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_AttractionRate = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttractionRate_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk14 = reader.ReadBytesFromFileStream(9);
            this.AttractionRateValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MaxBodyStrongValue = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MaxBodyStrongValue_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk15 = reader.ReadBytesFromFileStream(9);
            this.MaxBodyStrongValue_Value = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MaxArmorHP = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MaxArmorHP_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk16 = reader.ReadBytesFromFileStream(9);
            this.MaxArmorHPValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_DamageMin = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DamageMin_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk17 = reader.ReadBytesFromFileStream(9);
            this.DamageMinValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_DamageMax = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DamageMax_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk18 = reader.ReadBytesFromFileStream(9);
            this.DamageMaxValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_NeedReactionSameTeamZeroDmgAtk = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NeedReactionSameTeamZeroDmgAtk_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk19 = reader.ReadBytesFromFileStream(8);
            this.NeedReactionSameTeamZeroDmgAtkValue = (byte)reader.ReadByte();
            this.ExtraUnk1 = (byte)reader.ReadByte();

            this.m_RevengeLimit = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.RevengeLimit_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk20 = reader.ReadBytesFromFileStream(9);
            this.RevengeLimitValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_MaxRevengeCount = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.MaxRevengeCount_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk21 = reader.ReadBytesFromFileStream(9);
            this.MaxRevengeCountValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_RevengeCoolDownTime = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.RevengeCoolDownTime_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk22 = reader.ReadBytesFromFileStream(9);
            this.RevengeCoolDownTimeValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_RevengeCoefficientPhysical = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.RevengeCoefficientPhysical_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk23 = reader.ReadBytesFromFileStream(9);
            this.RevengeCoefficientPhysicalValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_RevengeCoefficientFire = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.RevengeCoefficientFire_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk24 = reader.ReadBytesFromFileStream(9);
            this.RevengeCoefficientFireValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_RevengeCoefficientBlizzard = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.RevengeCoefficientBlizzard_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk25 = reader.ReadBytesFromFileStream(9);
            this.RevengeCoefficientBlizzardValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_RevengeCoefficientThunder = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.RevengeCoefficientThunder_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk26 = reader.ReadBytesFromFileStream(9);
            this.RevengeCoefficientThunderValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_RevengeCoefficientWater = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.RevengeCoefficientWater_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk27 = reader.ReadBytesFromFileStream(9);
            this.RevengeCoefficientWaterValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_RevengeCoefficientAero = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.RevengeCoefficientAero_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk28 = reader.ReadBytesFromFileStream(9);
            this.RevengeCoefficientAeroValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_RevengeCoefficientDark = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.RevengeCoefficientDark_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk29 = reader.ReadBytesFromFileStream(9);
            this.RevengeCoefficientDarkValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_RevengeCoefficientNoType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.RevengeCoefficientNoType_FloatProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk30 = reader.ReadBytesFromFileStream(9);
            this.RevengeCoefficientNoTypeValue = BitConverter.ToSingle(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistPhysical = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrResistPhysical_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk31 = reader.ReadBytesFromFileStream(9);
            this.AttrResistPhysicalValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistFire = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrResistFire_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk32 = reader.ReadBytesFromFileStream(9);
            this.AttrResistFireValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistBlizzard = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrResistBlizzard_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk33 = reader.ReadBytesFromFileStream(9);
            this.AttrResistBlizzardValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistThunder = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrResistThunder_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk34 = reader.ReadBytesFromFileStream(9);
            this.AttrResistThunderValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistWater = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrResistWater_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk35 = reader.ReadBytesFromFileStream(9);
            this.AttrResistWaterValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistAero = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrResistAero_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk36 = reader.ReadBytesFromFileStream(9);
            this.AttrResistAeroValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistDark = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrResistDark_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk37 = reader.ReadBytesFromFileStream(9);
            this.AttrResistDarkValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrResistNoType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrResistNoType_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk38 = reader.ReadBytesFromFileStream(9);
            this.AttrResistNoTypeValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_ResistRapidFire = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistRapidFire_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk39 = reader.ReadBytesFromFileStream(9);
            this.ResistRapidFireValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_ResistComboParam = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistComboParam_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk40 = reader.ReadBytesFromFileStream(9);
            this.ResistComboParamValue = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_AttrWeekPointPhysical = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrWeekPointPhysical_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk41 = reader.ReadBytesFromFileStream(8);
            this.AttrWeekPointPhysicalValue = (byte)reader.ReadByte();
            this.ExtraUnk2 = (byte)reader.ReadByte();

            this.m_AttrWeekPointFire = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrWeekPointFire_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk42 = reader.ReadBytesFromFileStream(8);
            this.AttrWeekPointFireValue = (byte)reader.ReadByte();
            this.ExtraUnk3 = (byte)reader.ReadByte();

            this.m_AttrWeekPointBlizzard = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrWeekPointBlizzard_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk43 = reader.ReadBytesFromFileStream(8);
            this.AttrWeekPointBlizzardValue = (byte)reader.ReadByte();
            this.ExtraUnk4 = (byte)reader.ReadByte();

            this.m_AttrWeekPointThunder = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrWeekPointThunder_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk44 = reader.ReadBytesFromFileStream(8);
            this.AttrWeekPointThunderValue = (byte)reader.ReadByte();
            this.ExtraUnk5 = (byte)reader.ReadByte();

            this.m_AttrWeekPointWater = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrWeekPointWater_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk45 = reader.ReadBytesFromFileStream(8);
            this.AttrWeekPointWaterValue = (byte)reader.ReadByte();
            this.ExtraUnk6 = (byte)reader.ReadByte();

            this.m_AttrWeekPointAero = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrWeekPointAero_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk46 = reader.ReadBytesFromFileStream(8);
            this.AttrWeekPointAeroValue = (byte)reader.ReadByte();
            this.ExtraUnk7 = (byte)reader.ReadByte();

            this.m_AttrWeekPointDark = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrWeekPointDark_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk47 = reader.ReadBytesFromFileStream(8);
            this.AttrWeekPointDarkValue = (byte)reader.ReadByte();
            this.ExtraUnk8 = (byte)reader.ReadByte();

            this.m_AttrWeekPointNoType = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.AttrWeekPointNoType_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk48 = reader.ReadBytesFromFileStream(8);
            this.AttrWeekPointNoTypeValue = (byte)reader.ReadByte();
            this.ExtraUnk9 = (byte)reader.ReadByte();

            this.m_bResistEffectFreeFlow = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectFreeFlow_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk49 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectFreeFlowValue = (byte)reader.ReadByte();
            this.ExtraUnk10 = (byte)reader.ReadByte();

            this.m_bResistEffectDeath = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectDeath_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk50 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectDeathValue = (byte)reader.ReadByte();
            this.ExtraUnk11 = (byte)reader.ReadByte();

            this.m_bResistEffectCatch = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectCatch_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk51 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectCatchValue = (byte)reader.ReadByte();
            this.ExtraUnk12 = (byte)reader.ReadByte();

            this.m_bResistEffectDrillBind = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectDrillBind_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk52 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectDrillBindValue = (byte)reader.ReadByte();
            this.ExtraUnk13 = (byte)reader.ReadByte();

            this.m_bResistEffectYoBind = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectYoBind_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk53 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectYoBindValue = (byte)reader.ReadByte();
            this.ExtraUnk14 = (byte)reader.ReadByte();

            this.m_bResistEffectRalphBind = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectRalphBind_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk54 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectRalphBindValue = (byte)reader.ReadByte();
            this.ExtraUnk15 = (byte)reader.ReadByte();

            this.m_bResistEffectEnergyBurst = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectEnergyBurst_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk55 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectEnergyBurstValue = (byte)reader.ReadByte();
            this.ExtraUnk16 = (byte)reader.ReadByte();

            this.m_bResistEffectFreeze = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectFreeze_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk56 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectFreezeValue = (byte)reader.ReadByte();
            this.ExtraUnk17 = (byte)reader.ReadByte();

            this.m_bResistEffectStop = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectStop_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk57 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectStopValue = (byte)reader.ReadByte();
            this.ExtraUnk18 = (byte)reader.ReadByte();

            this.m_bResistEffectMagnet = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectMagnet_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk58 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectMagnetValue = (byte)reader.ReadByte();
            this.ExtraUnk19 = (byte)reader.ReadByte();

            this.m_bResistEffectStun = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectStun_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk59 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectStunValue = (byte)reader.ReadByte();
            this.ExtraUnk20 = (byte)reader.ReadByte();

            this.m_bResistEffectSneeze = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectSneeze_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk60 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectSneezeValue = (byte)reader.ReadByte();
            this.ExtraUnk21 = (byte)reader.ReadByte();

            this.m_bResistEffectHoney = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectHoney_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk61 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectHoneyValue = (byte)reader.ReadByte();
            this.ExtraUnk22 = (byte)reader.ReadByte();

            this.m_bResistEffectCloud = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectCloud_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk62 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectCloudValue = (byte)reader.ReadByte();
            this.ExtraUnk23 = (byte)reader.ReadByte();

            this.m_bResistEffectDischarge = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectDischarge_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk63 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectDischargeValue = (byte)reader.ReadByte();
            this.ExtraUnk24 = (byte)reader.ReadByte();

            this.m_bResistEffectBurn = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectBurn_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk64 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectBurnValue = (byte)reader.ReadByte();
            this.ExtraUnk25 = (byte)reader.ReadByte();

            this.m_bResistEffectPoleSpinTurn = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.ResistEffectPoleSpinTurn_BoolProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk65 = reader.ReadBytesFromFileStream(8);
            this.ResistEffectPoleSpinTurnValue = (byte)reader.ReadByte();
            this.ExtraUnk26 = (byte)reader.ReadByte();

            this.m_DropPrize1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DropPrize1_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk66 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DropPrizeID = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk67 = (byte)reader.ReadByte();
            this.ETresDropPrizeID = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_NumDropPrize1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NumDropPrize1_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk68 = reader.ReadBytesFromFileStream(9);
            this.NumDropPrize1Value = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_DropPrize2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DropPrize2_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk69 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DropPrizeID2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk70 = (byte)reader.ReadByte();
            this.ETresDropPrizeID2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_NumDropPrize2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NumDropPrize2_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk71 = reader.ReadBytesFromFileStream(9);
            this.NumDropPrize2Value = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_DropItem1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DropItem1_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk72 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DropItemID = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk73 = (byte)reader.ReadByte();
            this.ETresDropItemID = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_NumDropItem1 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NumDropItem1_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk74 = reader.ReadBytesFromFileStream(9);
            this.NumDropItem1Value = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_DropItem2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DropItem2_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk75 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DropItemID2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk76 = (byte)reader.ReadByte();
            this.ETresDropItemID2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_NumDropItem2 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NumDropItem2_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk77 = reader.ReadBytesFromFileStream(9);
            this.NumDropItem2Value = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.m_DropItem3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DropItem3_EnumProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk78 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.DropItemID3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk79 = (byte)reader.ReadByte();
            this.ETresDropItemID3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            this.m_NumDropItem3 = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.NumDropItem3_IntProperty = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());
            this.Unk80 = reader.ReadBytesFromFileStream(9);
            this.NumDropItem3Value = BitConverter.ToInt32(reader.ReadBytesFromFileStream(4).ToArray());

            this.None = BitConverter.ToInt64(reader.ReadBytesFromFileStream(8).ToArray());

            return this;
        }

        public List<byte> Recompile()
        {
            var data = new List<byte>();

            data.AddRange(BitConverter.GetBytes(this.Id));

            data.AddRange(BitConverter.GetBytes(this.m_MaxHitPoint));
            data.AddRange(BitConverter.GetBytes(this.MaxHitPoint_IntProperty));
            data.AddRange(this.Unk1);
            data.AddRange(BitConverter.GetBytes(this.MaxHitPointValue));

            data.AddRange(BitConverter.GetBytes(this.m_MaxHPRate));
            data.AddRange(BitConverter.GetBytes(this.MaxHPRate_FloatProperty));
            data.AddRange(this.Unk2);
            data.AddRange(BitConverter.GetBytes(this.MaxHPRateValue));

            data.AddRange(BitConverter.GetBytes(this.m_MaxMagicPoint));
            data.AddRange(BitConverter.GetBytes(this.MaxMagicPoint_IntProperty));
            data.AddRange(this.Unk3);
            data.AddRange(BitConverter.GetBytes(this.MaxMagicPointValue));

            data.AddRange(BitConverter.GetBytes(this.m_MaxFocusPoint));
            data.AddRange(BitConverter.GetBytes(this.MaxFocusPoint_IntProperty));
            data.AddRange(this.Unk4);
            data.AddRange(BitConverter.GetBytes(this.MaxFocusPointValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttackPower));
            data.AddRange(BitConverter.GetBytes(this.AttackPower_IntProperty));
            data.AddRange(this.Unk5);
            data.AddRange(BitConverter.GetBytes(this.AttackPowerValue));

            data.AddRange(BitConverter.GetBytes(this.m_MagicPower));
            data.AddRange(BitConverter.GetBytes(this.MagicPower_IntProperty));
            data.AddRange(this.Unk6);
            data.AddRange(BitConverter.GetBytes(this.MagicPowerValue));

            data.AddRange(BitConverter.GetBytes(this.m_DefensePower));
            data.AddRange(BitConverter.GetBytes(this.DefensePower_IntProperty));
            data.AddRange(this.Unk7);
            data.AddRange(BitConverter.GetBytes(this.DefensePowerValue));

            data.AddRange(BitConverter.GetBytes(this.m_AbilityPoint));
            data.AddRange(BitConverter.GetBytes(this.AbilityPoint_IntProperty));
            data.AddRange(this.Unk8);
            data.AddRange(BitConverter.GetBytes(this.AbilityPointValue));

            data.AddRange(BitConverter.GetBytes(this.m_ExpRate));
            data.AddRange(BitConverter.GetBytes(this.ExpRate_FloatProperty));
            data.AddRange(this.Unk9);
            data.AddRange(BitConverter.GetBytes(this.ExpRateValue));

            data.AddRange(BitConverter.GetBytes(this.m_BodyPushPower));
            data.AddRange(BitConverter.GetBytes(this.BodyPushPower_EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk10));
            data.AddRange(BitConverter.GetBytes(this.BodyPushPowerLevel));
            data.Add(this.Unk11);
            data.AddRange(BitConverter.GetBytes(this.ETresBodyPushPowerLevel));

            data.AddRange(BitConverter.GetBytes(this.m_BioType));
            data.AddRange(BitConverter.GetBytes(this.BioType_EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk12));
            data.AddRange(BitConverter.GetBytes(this.ChrBiologicalType));
            data.Add(this.Unk13);
            data.AddRange(BitConverter.GetBytes(this.ETresChrBiologicalType));

            data.AddRange(BitConverter.GetBytes(this.m_AttractionRate));
            data.AddRange(BitConverter.GetBytes(this.AttractionRate_FloatProperty));
            data.AddRange(this.Unk14);
            data.AddRange(BitConverter.GetBytes(this.AttractionRateValue));

            data.AddRange(BitConverter.GetBytes(this.m_MaxBodyStrongValue));
            data.AddRange(BitConverter.GetBytes(this.MaxBodyStrongValue_IntProperty));
            data.AddRange(this.Unk15);
            data.AddRange(BitConverter.GetBytes(this.MaxBodyStrongValue_Value));

            data.AddRange(BitConverter.GetBytes(this.m_MaxArmorHP));
            data.AddRange(BitConverter.GetBytes(this.MaxArmorHP_IntProperty));
            data.AddRange(this.Unk16);
            data.AddRange(BitConverter.GetBytes(this.MaxArmorHPValue));

            data.AddRange(BitConverter.GetBytes(this.m_DamageMin));
            data.AddRange(BitConverter.GetBytes(this.DamageMin_IntProperty));
            data.AddRange(this.Unk17);
            data.AddRange(BitConverter.GetBytes(this.DamageMinValue));

            data.AddRange(BitConverter.GetBytes(this.m_DamageMax));
            data.AddRange(BitConverter.GetBytes(this.DamageMax_IntProperty));
            data.AddRange(this.Unk18);
            data.AddRange(BitConverter.GetBytes(this.DamageMaxValue));

            data.AddRange(BitConverter.GetBytes(this.m_NeedReactionSameTeamZeroDmgAtk));
            data.AddRange(BitConverter.GetBytes(this.NeedReactionSameTeamZeroDmgAtk_BoolProperty));
            data.AddRange(this.Unk19);
            data.Add(this.NeedReactionSameTeamZeroDmgAtkValue);
            data.Add(this.ExtraUnk1);

            data.AddRange(BitConverter.GetBytes(this.m_RevengeLimit));
            data.AddRange(BitConverter.GetBytes(this.RevengeLimit_FloatProperty));
            data.AddRange(this.Unk20);
            data.AddRange(BitConverter.GetBytes(this.RevengeLimitValue));

            data.AddRange(BitConverter.GetBytes(this.m_MaxRevengeCount));
            data.AddRange(BitConverter.GetBytes(this.MaxRevengeCount_IntProperty));
            data.AddRange(this.Unk21);
            data.AddRange(BitConverter.GetBytes(this.MaxRevengeCountValue));

            data.AddRange(BitConverter.GetBytes(this.m_RevengeCoolDownTime));
            data.AddRange(BitConverter.GetBytes(this.RevengeCoolDownTime_FloatProperty));
            data.AddRange(this.Unk22);
            data.AddRange(BitConverter.GetBytes(this.RevengeCoolDownTimeValue));

            data.AddRange(BitConverter.GetBytes(this.m_RevengeCoefficientPhysical));
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientPhysical_FloatProperty));
            data.AddRange(this.Unk23);
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientPhysicalValue));

            data.AddRange(BitConverter.GetBytes(this.m_RevengeCoefficientFire));
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientFire_FloatProperty));
            data.AddRange(this.Unk24);
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientFireValue));

            data.AddRange(BitConverter.GetBytes(this.m_RevengeCoefficientBlizzard));
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientBlizzard_FloatProperty));
            data.AddRange(this.Unk25);
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientBlizzardValue));

            data.AddRange(BitConverter.GetBytes(this.m_RevengeCoefficientThunder));
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientThunder_FloatProperty));
            data.AddRange(this.Unk26);
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientThunderValue));

            data.AddRange(BitConverter.GetBytes(this.m_RevengeCoefficientWater));
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientWater_FloatProperty));
            data.AddRange(this.Unk27);
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientWaterValue));

            data.AddRange(BitConverter.GetBytes(this.m_RevengeCoefficientAero));
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientAero_FloatProperty));
            data.AddRange(this.Unk28);
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientAeroValue));

            data.AddRange(BitConverter.GetBytes(this.m_RevengeCoefficientDark));
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientDark_FloatProperty));
            data.AddRange(this.Unk29);
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientDarkValue));

            data.AddRange(BitConverter.GetBytes(this.m_RevengeCoefficientNoType));
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientNoType_FloatProperty));
            data.AddRange(this.Unk30);
            data.AddRange(BitConverter.GetBytes(this.RevengeCoefficientNoTypeValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistPhysical));
            data.AddRange(BitConverter.GetBytes(this.AttrResistPhysical_IntProperty));
            data.AddRange(this.Unk31);
            data.AddRange(BitConverter.GetBytes(this.AttrResistPhysicalValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistFire));
            data.AddRange(BitConverter.GetBytes(this.AttrResistFire_IntProperty));
            data.AddRange(this.Unk32);
            data.AddRange(BitConverter.GetBytes(this.AttrResistFireValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistBlizzard));
            data.AddRange(BitConverter.GetBytes(this.AttrResistBlizzard_IntProperty));
            data.AddRange(this.Unk33);
            data.AddRange(BitConverter.GetBytes(this.AttrResistBlizzardValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistThunder));
            data.AddRange(BitConverter.GetBytes(this.AttrResistThunder_IntProperty));
            data.AddRange(this.Unk34);
            data.AddRange(BitConverter.GetBytes(this.AttrResistThunderValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistWater));
            data.AddRange(BitConverter.GetBytes(this.AttrResistWater_IntProperty));
            data.AddRange(this.Unk35);
            data.AddRange(BitConverter.GetBytes(this.AttrResistWaterValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistAero));
            data.AddRange(BitConverter.GetBytes(this.AttrResistAero_IntProperty));
            data.AddRange(this.Unk36);
            data.AddRange(BitConverter.GetBytes(this.AttrResistAeroValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistDark));
            data.AddRange(BitConverter.GetBytes(this.AttrResistDark_IntProperty));
            data.AddRange(this.Unk37);
            data.AddRange(BitConverter.GetBytes(this.AttrResistDarkValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrResistNoType));
            data.AddRange(BitConverter.GetBytes(this.AttrResistNoType_IntProperty));
            data.AddRange(this.Unk38);
            data.AddRange(BitConverter.GetBytes(this.AttrResistNoTypeValue));

            data.AddRange(BitConverter.GetBytes(this.m_ResistRapidFire));
            data.AddRange(BitConverter.GetBytes(this.ResistRapidFire_IntProperty));
            data.AddRange(this.Unk39);
            data.AddRange(BitConverter.GetBytes(this.ResistRapidFireValue));

            data.AddRange(BitConverter.GetBytes(this.m_ResistComboParam));
            data.AddRange(BitConverter.GetBytes(this.ResistComboParam_IntProperty));
            data.AddRange(this.Unk40);
            data.AddRange(BitConverter.GetBytes(this.ResistComboParamValue));

            data.AddRange(BitConverter.GetBytes(this.m_AttrWeekPointPhysical));
            data.AddRange(BitConverter.GetBytes(this.AttrWeekPointPhysical_BoolProperty));
            data.AddRange(this.Unk41);
            data.Add(this.AttrWeekPointPhysicalValue);
            data.Add(this.ExtraUnk2);

            data.AddRange(BitConverter.GetBytes(this.m_AttrWeekPointFire));
            data.AddRange(BitConverter.GetBytes(this.AttrWeekPointFire_BoolProperty));
            data.AddRange(this.Unk42);
            data.Add(this.AttrWeekPointFireValue);
            data.Add(this.ExtraUnk3);

            data.AddRange(BitConverter.GetBytes(this.m_AttrWeekPointBlizzard));
            data.AddRange(BitConverter.GetBytes(this.AttrWeekPointBlizzard_BoolProperty));
            data.AddRange(this.Unk43);
            data.Add(this.AttrWeekPointBlizzardValue);
            data.Add(this.ExtraUnk4);

            data.AddRange(BitConverter.GetBytes(this.m_AttrWeekPointThunder));
            data.AddRange(BitConverter.GetBytes(this.AttrWeekPointThunder_BoolProperty));
            data.AddRange(this.Unk44);
            data.Add(this.AttrWeekPointThunderValue);
            data.Add(this.ExtraUnk5);

            data.AddRange(BitConverter.GetBytes(this.m_AttrWeekPointWater));
            data.AddRange(BitConverter.GetBytes(this.AttrWeekPointWater_BoolProperty));
            data.AddRange(this.Unk45);
            data.Add(this.AttrWeekPointWaterValue);
            data.Add(this.ExtraUnk6);

            data.AddRange(BitConverter.GetBytes(this.m_AttrWeekPointAero));
            data.AddRange(BitConverter.GetBytes(this.AttrWeekPointAero_BoolProperty));
            data.AddRange(this.Unk46);
            data.Add(this.AttrWeekPointAeroValue);
            data.Add(this.ExtraUnk7);

            data.AddRange(BitConverter.GetBytes(this.m_AttrWeekPointDark));
            data.AddRange(BitConverter.GetBytes(this.AttrWeekPointDark_BoolProperty));
            data.AddRange(this.Unk47);
            data.Add(this.AttrWeekPointDarkValue);
            data.Add(this.ExtraUnk8);

            data.AddRange(BitConverter.GetBytes(this.m_AttrWeekPointNoType));
            data.AddRange(BitConverter.GetBytes(this.AttrWeekPointNoType_BoolProperty));
            data.AddRange(this.Unk48);
            data.Add(this.AttrWeekPointNoTypeValue);
            data.Add(this.ExtraUnk9);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectFreeFlow));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectFreeFlow_BoolProperty));
            data.AddRange(this.Unk49);
            data.Add(this.ResistEffectFreeFlowValue);
            data.Add(this.ExtraUnk10);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectDeath));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectDeath_BoolProperty));
            data.AddRange(this.Unk50);
            data.Add(this.ResistEffectDeathValue);
            data.Add(this.ExtraUnk11);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectCatch));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectCatch_BoolProperty));
            data.AddRange(this.Unk51);
            data.Add(this.ResistEffectCatchValue);
            data.Add(this.ExtraUnk12);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectDrillBind));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectDrillBind_BoolProperty));
            data.AddRange(this.Unk52);
            data.Add(this.ResistEffectDrillBindValue);
            data.Add(this.ExtraUnk13);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectYoBind));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectYoBind_BoolProperty));
            data.AddRange(this.Unk53);
            data.Add(this.ResistEffectYoBindValue);
            data.Add(this.ExtraUnk14);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectRalphBind));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectRalphBind_BoolProperty));
            data.AddRange(this.Unk54);
            data.Add(this.ResistEffectRalphBindValue);
            data.Add(this.ExtraUnk15);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectEnergyBurst));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectEnergyBurst_BoolProperty));
            data.AddRange(this.Unk55);
            data.Add(this.ResistEffectEnergyBurstValue);
            data.Add(this.ExtraUnk16);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectFreeze));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectFreeze_BoolProperty));
            data.AddRange(this.Unk56);
            data.Add(this.ResistEffectFreezeValue);
            data.Add(this.ExtraUnk17);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectStop));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectStop_BoolProperty));
            data.AddRange(this.Unk57);
            data.Add(this.ResistEffectStopValue);
            data.Add(this.ExtraUnk18);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectMagnet));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectMagnet_BoolProperty));
            data.AddRange(this.Unk58);
            data.Add(this.ResistEffectMagnetValue);
            data.Add(this.ExtraUnk19);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectStun));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectStun_BoolProperty));
            data.AddRange(this.Unk59);
            data.Add(this.ResistEffectStunValue);
            data.Add(this.ExtraUnk20);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectSneeze));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectSneeze_BoolProperty));
            data.AddRange(this.Unk60);
            data.Add(this.ResistEffectSneezeValue);
            data.Add(this.ExtraUnk21);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectHoney));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectHoney_BoolProperty));
            data.AddRange(this.Unk61);
            data.Add(this.ResistEffectHoneyValue);
            data.Add(this.ExtraUnk22);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectCloud));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectCloud_BoolProperty));
            data.AddRange(this.Unk62);
            data.Add(this.ResistEffectCloudValue);
            data.Add(this.ExtraUnk23);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectDischarge));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectDischarge_BoolProperty));
            data.AddRange(this.Unk63);
            data.Add(this.ResistEffectDischargeValue);
            data.Add(this.ExtraUnk24);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectBurn));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectBurn_BoolProperty));
            data.AddRange(this.Unk64);
            data.Add(this.ResistEffectBurnValue);
            data.Add(this.ExtraUnk25);

            data.AddRange(BitConverter.GetBytes(this.m_bResistEffectPoleSpinTurn));
            data.AddRange(BitConverter.GetBytes(this.ResistEffectPoleSpinTurn_BoolProperty));
            data.AddRange(this.Unk65);
            data.Add(this.ResistEffectPoleSpinTurnValue);
            data.Add(this.ExtraUnk26);

            data.AddRange(BitConverter.GetBytes(this.m_DropPrize1));
            data.AddRange(BitConverter.GetBytes(this.DropPrize1_EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk66));
            data.AddRange(BitConverter.GetBytes(this.DropPrizeID));
            data.Add(this.Unk67);
            data.AddRange(BitConverter.GetBytes(this.ETresDropPrizeID));

            data.AddRange(BitConverter.GetBytes(this.m_NumDropPrize1));
            data.AddRange(BitConverter.GetBytes(this.NumDropPrize1_IntProperty));
            data.AddRange(this.Unk68);
            data.AddRange(BitConverter.GetBytes(this.NumDropPrize1Value));

            data.AddRange(BitConverter.GetBytes(this.m_DropPrize2));
            data.AddRange(BitConverter.GetBytes(this.DropPrize2_EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk69));
            data.AddRange(BitConverter.GetBytes(this.DropPrizeID2));
            data.Add(this.Unk70);
            data.AddRange(BitConverter.GetBytes(this.ETresDropPrizeID2));

            data.AddRange(BitConverter.GetBytes(this.m_NumDropPrize2));
            data.AddRange(BitConverter.GetBytes(this.NumDropPrize2_IntProperty));
            data.AddRange(this.Unk71);
            data.AddRange(BitConverter.GetBytes(this.NumDropPrize2Value));

            data.AddRange(BitConverter.GetBytes(this.m_DropItem1));
            data.AddRange(BitConverter.GetBytes(this.DropItem1_EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk72));
            data.AddRange(BitConverter.GetBytes(this.DropItemID));
            data.Add(this.Unk73);
            data.AddRange(BitConverter.GetBytes(this.ETresDropItemID));

            data.AddRange(BitConverter.GetBytes(this.m_NumDropItem1));
            data.AddRange(BitConverter.GetBytes(this.NumDropItem1_IntProperty));
            data.AddRange(this.Unk74);
            data.AddRange(BitConverter.GetBytes(this.NumDropItem1Value));

            data.AddRange(BitConverter.GetBytes(this.m_DropItem2));
            data.AddRange(BitConverter.GetBytes(this.DropItem2_EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk75));
            data.AddRange(BitConverter.GetBytes(this.DropItemID2));
            data.Add(this.Unk76);
            data.AddRange(BitConverter.GetBytes(this.ETresDropItemID2));

            data.AddRange(BitConverter.GetBytes(this.m_NumDropItem2));
            data.AddRange(BitConverter.GetBytes(this.NumDropItem2_IntProperty));
            data.AddRange(this.Unk77);
            data.AddRange(BitConverter.GetBytes(this.NumDropItem2Value));

            data.AddRange(BitConverter.GetBytes(this.m_DropItem3));
            data.AddRange(BitConverter.GetBytes(this.DropItem3_EnumProperty));
            data.AddRange(BitConverter.GetBytes(this.Unk78));
            data.AddRange(BitConverter.GetBytes(this.DropItemID3));
            data.Add(this.Unk79);
            data.AddRange(BitConverter.GetBytes(this.ETresDropItemID3));

            data.AddRange(BitConverter.GetBytes(this.m_NumDropItem3));
            data.AddRange(BitConverter.GetBytes(this.NumDropItem3_IntProperty));
            data.AddRange(this.Unk80);
            data.AddRange(BitConverter.GetBytes(this.NumDropItem3Value));

            data.AddRange(BitConverter.GetBytes(this.None));

            return data;
        }
    }
}