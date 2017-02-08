using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.Game.Entitys
{
    public class PlayerEntity : ObjectEntity
    {
        public override int DataLength => (int)EUnitFields.UnitEnd - 0x4;

        public PlayerEntity(Characters character) : base(new ObjectGuid((uint) character.Id, TypeId.TypeidPlayer, HighGuid.HighguidMoTransport))
        {
            Guid = (uint)character.Id;
            SetUpdateField((int) EObjectFields.OBJECT_FIELD_TYPE, 25);
            SetUpdateField((int) EObjectFields.OBJECT_FIELD_SCALE_X, 1f);

            SetUpdateField((int) EUnitFields.UnitFieldLevel, 1);
            SetUpdateField((int) EUnitFields.UnitFieldFactiontemplate, 7);           
            SetUpdateField((int) EUnitFields.UnitFieldBytes0, (byte) character.race, 0);
            SetUpdateField((int) EUnitFields.UnitFieldBytes0, (byte) character.classe, 1);
            SetUpdateField((int) EUnitFields.UnitFieldBytes0, (byte) character.gender, 2);
        }
    }

    enum EUnitFields
    {
        UnitFieldCharm = 0x00 + EObjectFields.OBJECT_END, // Size:2
        UnitFieldSummon = 0x02 + EObjectFields.OBJECT_END, // Size:2
        UnitFieldCharmedby = 0x04 + EObjectFields.OBJECT_END, // Size:2
        UnitFieldSummonedby = 0x06 + EObjectFields.OBJECT_END, // Size:2
        UnitFieldCreatedby = 0x08 + EObjectFields.OBJECT_END, // Size:2
        UnitFieldTarget = 0x0A + EObjectFields.OBJECT_END, // Size:2
        UnitFieldPersuaded = 0x0C + EObjectFields.OBJECT_END, // Size:2
        UnitFieldChannelObject = 0x0E + EObjectFields.OBJECT_END, // Size:2
        UnitFieldHealth = 0x10 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldPower1 = 0x11 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldPower2 = 0x12 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldPower3 = 0x13 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldPower4 = 0x14 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldPower5 = 0x15 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMaxhealth = 0x16 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMaxpower1 = 0x17 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMaxpower2 = 0x18 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMaxpower3 = 0x19 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMaxpower4 = 0x1A + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMaxpower5 = 0x1B + EObjectFields.OBJECT_END, // Size:1
        UnitFieldLevel = 0x1C + EObjectFields.OBJECT_END, // Size:1
        UnitFieldFactiontemplate = 0x1D + EObjectFields.OBJECT_END, // Size:1
        UnitFieldBytes0 = 0x1E + EObjectFields.OBJECT_END, // Size:1
        UnitVirtualItemSlotDisplay = 0x1F + EObjectFields.OBJECT_END, // Size:3
        UnitVirtualItemSlotDisplay01 = 0x20 + EObjectFields.OBJECT_END,
        UnitVirtualItemSlotDisplay02 = 0x21 + EObjectFields.OBJECT_END,
        UnitVirtualItemInfo = 0x22 + EObjectFields.OBJECT_END, // Size:6
        UnitVirtualItemInfo01 = 0x23 + EObjectFields.OBJECT_END,
        UnitVirtualItemInfo02 = 0x24 + EObjectFields.OBJECT_END,
        UnitVirtualItemInfo03 = 0x25 + EObjectFields.OBJECT_END,
        UnitVirtualItemInfo04 = 0x26 + EObjectFields.OBJECT_END,
        UnitVirtualItemInfo05 = 0x27 + EObjectFields.OBJECT_END,
        UnitFieldFlags = 0x28 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldAura = 0x29 + EObjectFields.OBJECT_END, // Size:48
        UnitFieldAuraLast = 0x58 + EObjectFields.OBJECT_END,
        UnitFieldAuraflags = 0x59 + EObjectFields.OBJECT_END, // Size:6
        UnitFieldAuraflags01 = 0x5a + EObjectFields.OBJECT_END,
        UnitFieldAuraflags02 = 0x5b + EObjectFields.OBJECT_END,
        UnitFieldAuraflags03 = 0x5c + EObjectFields.OBJECT_END,
        UnitFieldAuraflags04 = 0x5d + EObjectFields.OBJECT_END,
        UnitFieldAuraflags05 = 0x5e + EObjectFields.OBJECT_END,
        UnitFieldAuralevels = 0x5f + EObjectFields.OBJECT_END, // Size:12
        UnitFieldAuralevelsLast = 0x6a + EObjectFields.OBJECT_END,
        UnitFieldAuraapplications = 0x6b + EObjectFields.OBJECT_END, // Size:12
        UnitFieldAuraapplicationsLast = 0x76 + EObjectFields.OBJECT_END,
        UnitFieldAurastate = 0x77 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldBaseattacktime = 0x78 + EObjectFields.OBJECT_END, // Size:2
        UnitFieldOffhandattacktime = 0x79 + EObjectFields.OBJECT_END, // Size:2
        UnitFieldRangedattacktime = 0x7a + EObjectFields.OBJECT_END, // Size:1
        UnitFieldBoundingradius = 0x7b + EObjectFields.OBJECT_END, // Size:1
        UnitFieldCombatreach = 0x7c + EObjectFields.OBJECT_END, // Size:1
        UnitFieldDisplayid = 0x7d + EObjectFields.OBJECT_END, // Size:1
        UnitFieldNativedisplayid = 0x7e + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMountdisplayid = 0x7f + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMindamage = 0x80 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMaxdamage = 0x81 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMinoffhanddamage = 0x82 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMaxoffhanddamage = 0x83 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldBytes1 = 0x84 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldPetnumber = 0x85 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldPetNameTimestamp = 0x86 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldPetexperience = 0x87 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldPetnextlevelexp = 0x88 + EObjectFields.OBJECT_END, // Size:1
        UnitDynamicFlags = 0x89 + EObjectFields.OBJECT_END, // Size:1
        UnitChannelSpell = 0x8a + EObjectFields.OBJECT_END, // Size:1
        UnitModCastSpeed = 0x8b + EObjectFields.OBJECT_END, // Size:1
        UnitCreatedBySpell = 0x8c + EObjectFields.OBJECT_END, // Size:1
        UnitNpcFlags = 0x8d + EObjectFields.OBJECT_END, // Size:1
        UnitNpcEmotestate = 0x8e + EObjectFields.OBJECT_END, // Size:1
        UnitTrainingPoints = 0x8f + EObjectFields.OBJECT_END, // Size:1
        UnitFieldStat0 = 0x90 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldStat1 = 0x91 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldStat2 = 0x92 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldStat3 = 0x93 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldStat4 = 0x94 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldResistances = 0x95 + EObjectFields.OBJECT_END, // Size:7
        UnitFieldResistances01 = 0x96 + EObjectFields.OBJECT_END,
        UnitFieldResistances02 = 0x97 + EObjectFields.OBJECT_END,
        UnitFieldResistances03 = 0x98 + EObjectFields.OBJECT_END,
        UnitFieldResistances04 = 0x99 + EObjectFields.OBJECT_END,
        UnitFieldResistances05 = 0x9a + EObjectFields.OBJECT_END,
        UnitFieldResistances06 = 0x9b + EObjectFields.OBJECT_END,
        UnitFieldBaseMana = 0x9c + EObjectFields.OBJECT_END, // Size:1
        UnitFieldBaseHealth = 0x9d + EObjectFields.OBJECT_END, // Size:1
        UnitFieldBytes2 = 0x9e + EObjectFields.OBJECT_END, // Size:1
        UnitFieldAttackPower = 0x9f + EObjectFields.OBJECT_END, // Size:1
        UnitFieldAttackPowerMods = 0xa0 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldAttackPowerMultiplier = 0xa1 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldRangedAttackPower = 0xa2 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldRangedAttackPowerMods = 0xa3 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldRangedAttackPowerMultiplier = 0xa4 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMinrangeddamage = 0xa5 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldMaxrangeddamage = 0xa6 + EObjectFields.OBJECT_END, // Size:1
        UnitFieldPowerCostModifier = 0xa7 + EObjectFields.OBJECT_END, // Size:7
        UnitFieldPowerCostModifier01 = 0xa8 + EObjectFields.OBJECT_END,
        UnitFieldPowerCostModifier02 = 0xa9 + EObjectFields.OBJECT_END,
        UnitFieldPowerCostModifier03 = 0xaa + EObjectFields.OBJECT_END,
        UnitFieldPowerCostModifier04 = 0xab + EObjectFields.OBJECT_END,
        UnitFieldPowerCostModifier05 = 0xac + EObjectFields.OBJECT_END,
        UnitFieldPowerCostModifier06 = 0xad + EObjectFields.OBJECT_END,
        UnitFieldPowerCostMultiplier = 0xae + EObjectFields.OBJECT_END, // Size:7
        UnitFieldPowerCostMultiplier01 = 0xaf + EObjectFields.OBJECT_END,
        UnitFieldPowerCostMultiplier02 = 0xb0 + EObjectFields.OBJECT_END,
        UnitFieldPowerCostMultiplier03 = 0xb1 + EObjectFields.OBJECT_END,
        UnitFieldPowerCostMultiplier04 = 0xb2 + EObjectFields.OBJECT_END,
        UnitFieldPowerCostMultiplier05 = 0xb3 + EObjectFields.OBJECT_END,
        UnitFieldPowerCostMultiplier06 = 0xb4 + EObjectFields.OBJECT_END,
        UnitFieldPadding = 0xb5 + EObjectFields.OBJECT_END,
        UnitEnd = 0xb6 + EObjectFields.OBJECT_END,

        PlayerDuelArbiter = 0x00 + UnitEnd, // Size:2
        PlayerFlags = 0x02 + UnitEnd, // Size:1
        PlayerGuildid = 0x03 + UnitEnd, // Size:1
        PlayerGuildrank = 0x04 + UnitEnd, // Size:1
        PlayerBytes = 0x05 + UnitEnd, // Size:1
        PlayerBytes2 = 0x06 + UnitEnd, // Size:1
        PlayerBytes3 = 0x07 + UnitEnd, // Size:1
        PlayerDuelTeam = 0x08 + UnitEnd, // Size:1
        PlayerGuildTimestamp = 0x09 + UnitEnd, // Size:1
        PlayerQuestLog11 = 0x0A + UnitEnd, // count = 20
        PlayerQuestLog12 = 0x0B + UnitEnd,
        PlayerQuestLog13 = 0x0C + UnitEnd,
        PlayerQuestLogLast1 = 0x43 + UnitEnd,
        PlayerQuestLogLast2 = 0x44 + UnitEnd,
        PlayerQuestLogLast3 = 0x45 + UnitEnd,
        PlayerVisibleItem1Creator = 0x46 + UnitEnd, // Size:2, count = 19
        PlayerVisibleItem10 = 0x48 + UnitEnd, // Size:8
        PlayerVisibleItem1Properties = 0x50 + UnitEnd, // Size:1
        PlayerVisibleItem1Pad = 0x51 + UnitEnd, // Size:1
        PlayerVisibleItemLastCreator = 0x11e + UnitEnd,
        PlayerVisibleItemLast0 = 0x120 + UnitEnd,
        PlayerVisibleItemLastProperties = 0x128 + UnitEnd,
        PlayerVisibleItemLastPad = 0x129 + UnitEnd,
        PlayerFieldInvSlotHead = 0x12a + UnitEnd, // Size:46
        PlayerFieldPackSlot1 = 0x158 + UnitEnd, // Size:32
        PlayerFieldPackSlotLast = 0x176 + UnitEnd,
        PlayerFieldBankSlot1 = 0x178 + UnitEnd, // Size:48
        PlayerFieldBankSlotLast = 0x1a6 + UnitEnd,
        PlayerFieldBankbagSlot1 = 0x1a8 + UnitEnd, // Size:12
        PlayerFieldBankbagSlotLast = 0xab2 + UnitEnd,
        PlayerFieldVendorbuybackSlot1 = 0x1b4 + UnitEnd, // Size:24
        PlayerFieldVendorbuybackSlotLast = 0x1ca + UnitEnd,
        PlayerFieldKeyringSlot1 = 0x1cc + UnitEnd, // Size:64
        PlayerFieldKeyringSlotLast = 0x20a + UnitEnd,
        PlayerFarsight = 0x20c + UnitEnd, // Size:2
        PlayerFieldComboTarget = 0x20e + UnitEnd, // Size:2
        PlayerXp = 0x210 + UnitEnd, // Size:1
        PlayerNextLevelXp = 0x211 + UnitEnd, // Size:1
        PlayerSkillInfo11 = 0x212 + UnitEnd, // Size:384
        PlayerCharacterPoints1 = 0x392 + UnitEnd, // Size:1
        PlayerCharacterPoints2 = 0x393 + UnitEnd, // Size:1
        PlayerTrackCreatures = 0x394 + UnitEnd, // Size:1
        PlayerTrackResources = 0x395 + UnitEnd, // Size:1
        PlayerBlockPercentage = 0x396 + UnitEnd, // Size:1
        PlayerDodgePercentage = 0x397 + UnitEnd, // Size:1
        PlayerParryPercentage = 0x398 + UnitEnd, // Size:1
        PlayerCritPercentage = 0x399 + UnitEnd, // Size:1
        PlayerRangedCritPercentage = 0x39a + UnitEnd, // Size:1
        PlayerExploredZones1 = 0x39b + UnitEnd, // Size:64
        PlayerRestStateExperience = 0x3db + UnitEnd, // Size:1
        PlayerFieldCoinage = 0x3dc + UnitEnd, // Size:1
        PlayerFieldPosstat0 = 0x3DD + UnitEnd, // Size:1
        PlayerFieldPosstat1 = 0x3DE + UnitEnd, // Size:1
        PlayerFieldPosstat2 = 0x3DF + UnitEnd, // Size:1
        PlayerFieldPosstat3 = 0x3E0 + UnitEnd, // Size:1
        PlayerFieldPosstat4 = 0x3E1 + UnitEnd, // Size:1
        PlayerFieldNegstat0 = 0x3E2 + UnitEnd, // Size:1
        PlayerFieldNegstat1 = 0x3E3 + UnitEnd, // Size:1
        PlayerFieldNegstat2 = 0x3E4 + UnitEnd, // Size:1
        PlayerFieldNegstat3 = 0x3E5 + UnitEnd, // Size:1,
        PlayerFieldNegstat4 = 0x3E6 + UnitEnd, // Size:1
        PlayerFieldResistancebuffmodspositive = 0x3E7 + UnitEnd, // Size:7
        PlayerFieldResistancebuffmodsnegative = 0x3EE + UnitEnd, // Size:7
        PlayerFieldModDamageDonePos = 0x3F5 + UnitEnd, // Size:7
        PlayerFieldModDamageDoneNeg = 0x3FC + UnitEnd, // Size:7
        PlayerFieldModDamageDonePct = 0x403 + UnitEnd, // Size:7
        PlayerFieldBytes = 0x40A + UnitEnd, // Size:1
        PlayerAmmoId = 0x40B + UnitEnd, // Size:1
        PlayerSelfResSpell = 0x40C + UnitEnd, // Size:1
        PlayerFieldPvpMedals = 0x40D + UnitEnd, // Size:1
        PlayerFieldBuybackPrice1 = 0x40E + UnitEnd, // count=12
        PlayerFieldBuybackPriceLast = 0x419 + UnitEnd,
        PlayerFieldBuybackTimestamp1 = 0x41A + UnitEnd, // count=12
        PlayerFieldBuybackTimestampLast = 0x425 + UnitEnd,
        PlayerFieldSessionKills = 0x426 + UnitEnd, // Size:1
        PlayerFieldYesterdayKills = 0x427 + UnitEnd, // Size:1
        PlayerFieldLastWeekKills = 0x428 + UnitEnd, // Size:1
        PlayerFieldThisWeekKills = 0x429 + UnitEnd, // Size:1
        PlayerFieldThisWeekContribution = 0x42a + UnitEnd, // Size:1
        PlayerFieldLifetimeHonorableKills = 0x42b + UnitEnd, // Size:1
        PlayerFieldLifetimeDishonorableKills = 0x42c + UnitEnd, // Size:1
        PlayerFieldYesterdayContribution = 0x42d + UnitEnd, // Size:1
        PlayerFieldLastWeekContribution = 0x42e + UnitEnd, // Size:1
        PlayerFieldLastWeekRank = 0x42f + UnitEnd, // Size:1
        PlayerFieldBytes2 = 0x430 + UnitEnd, // Size:1
        PlayerFieldWatchedFactionIndex = 0x431 + UnitEnd, // Size:1
        PlayerFieldCombatRating1 = 0x432 + UnitEnd, // Size:20

        PlayerEnd = 0x446 + UnitEnd
    }
}
