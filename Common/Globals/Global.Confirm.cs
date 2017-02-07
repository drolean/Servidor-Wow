namespace Common.Globals
{
    public enum WeaponAttackType : byte
    {
        BASE_ATTACK = 0,
        OFF_ATTACK = 1,
        RANGED_ATTACK = 2
    }

    public enum DamageTypes : byte
    {
        DMG_PHYSICAL = 0,
        DMG_HOLY = 1,
        DMG_FIRE = 2,
        DMG_NATURE = 3,
        DMG_FROST = 4,
        DMG_SHADOW = 5,
        DMG_ARCANE = 6
    }

    public enum PlayerFlags
    {
        PLAYER_FLAGS_GROUP_LEADER = 0x1,
        PLAYER_FLAGS_AFK = 0x2,
        PLAYER_FLAGS_DND = 0x4,
        PLAYER_FLAGS_GM = 0x8,                      // GM Prefix
        PLAYER_FLAGS_DEAD = 0x10,
        PLAYER_FLAGS_RESTING = 0x20,
        PLAYER_FLAGS_UNK7 = 0x40,                   // Admin Prefix?
        PLAYER_FLAGS_FFA_PVP = 0x80,
        PLAYER_FLAGS_CONTESTED_PVP = 0x100,
        PLAYER_FLAGS_IN_PVP = 0x200,
        PLAYER_FLAGS_HIDE_HELM = 0x400,
        PLAYER_FLAGS_HIDE_CLOAK = 0x800,
        PLAYER_FLAGS_PARTIAL_PLAY_TIME = 0x1000,
        PLAYER_FLAGS_IS_OUT_OF_BOUNDS = 0x4000,     // Out of Bounds
        PLAYER_FLAGS_UNK15 = 0x8000,                // Dev Prefix?
        PLAYER_FLAGS_SANCTUARY = 0x10000,
        PLAYER_FLAGS_NO_PLAY_TIME = 0x2000,
        PLAYER_FLAGS_PVP_TIMER = 0x40000
    }

    public enum EPlayerFields
    {
        //PLAYER_SELECTION = EUnitFields.UNIT_END + &H0                                 ' 0x0B6 - Size: 2 - Type: GUID - Flags: PUBLIC
        PLAYER_DUEL_ARBITER = EUnitFields.UNIT_END + 0x0,
        // 0x0B8 - Size: 2 - Type: GUID - Flags: PUBLIC
        PLAYER_FLAGS = EUnitFields.UNIT_END + 0x2,
        // 0x0BA - Size: 1 - Type: INT - Flags: PUBLIC
        PLAYER_GUILDID = EUnitFields.UNIT_END + 0x3,
        // 0x0BB - Size: 1 - Type: INT - Flags: PUBLIC
        PLAYER_GUILDRANK = EUnitFields.UNIT_END + 0x4,
        // 0x0BC - Size: 1 - Type: INT - Flags: PUBLIC
        PLAYER_BYTES = EUnitFields.UNIT_END + 0x5,
        // 0x0BD - Size: 1 - Type: BYTES - Flags: PUBLIC
        PLAYER_BYTES_2 = EUnitFields.UNIT_END + 0x6,
        // 0x0BE - Size: 1 - Type: BYTES - Flags: PUBLIC
        PLAYER_BYTES_3 = EUnitFields.UNIT_END + 0x7,
        // 0x0BF - Size: 1 - Type: BYTES - Flags: PUBLIC
        PLAYER_DUEL_TEAM = EUnitFields.UNIT_END + 0x8,
        // 0x0C0 - Size: 1 - Type: INT - Flags: PUBLIC
        PLAYER_GUILD_TIMESTAMP = EUnitFields.UNIT_END + 0x9,
        // 0x0C1 - Size: 1 - Type: INT - Flags: PUBLIC
        PLAYER_QUEST_LOG_1_1 = EUnitFields.UNIT_END + 0xa,
        // 0x0C2 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        PLAYER_QUEST_LOG_1_2 = EUnitFields.UNIT_END + 0xb,
        // 0x0C3 - Size: 2 - Type: INT - Flags: PRIVATE
        PLAYER_QUEST_LOG_1_3 = EUnitFields.UNIT_END + 0xc,
        //PLAYER_QUEST_LOG_2_1 = EUnitFields.UNIT_END + &HF                             ' 0x0C5 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_2_2 = EUnitFields.UNIT_END + &H10                            ' 0x0C6 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_3_1 = EUnitFields.UNIT_END + &H12                            ' 0x0C8 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_3_2 = EUnitFields.UNIT_END + &H13                            ' 0x0C9 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_4_1 = EUnitFields.UNIT_END + &H15                            ' 0x0CB - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_4_2 = EUnitFields.UNIT_END + &H16                            ' 0x0CC - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_5_1 = EUnitFields.UNIT_END + &H18                            ' 0x0CE - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_5_2 = EUnitFields.UNIT_END + &H19                            ' 0x0CF - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_6_1 = EUnitFields.UNIT_END + &H1B                            ' 0x0D1 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_6_2 = EUnitFields.UNIT_END + &H1C                            ' 0x0D2 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_7_1 = EUnitFields.UNIT_END + &H1E                            ' 0x0D4 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_7_2 = EUnitFields.UNIT_END + &H1F                            ' 0x0D5 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_8_1 = EUnitFields.UNIT_END + &H21                            ' 0x0D7 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_8_2 = EUnitFields.UNIT_END + &H22                            ' 0x0D8 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_9_1 = EUnitFields.UNIT_END + &H24                            ' 0x0DA - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_9_2 = EUnitFields.UNIT_END + &H25                            ' 0x0DB - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_10_1 = EUnitFields.UNIT_END + &H27                           ' 0x0DD - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_10_2 = EUnitFields.UNIT_END + &H28                           ' 0x0DE - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_11_1 = EUnitFields.UNIT_END + &H2A                           ' 0x0E0 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_11_2 = EUnitFields.UNIT_END + &H2B                           ' 0x0E1 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_12_1 = EUnitFields.UNIT_END + &H2D                           ' 0x0E3 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_12_2 = EUnitFields.UNIT_END + &H2E                           ' 0x0E4 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_13_1 = EUnitFields.UNIT_END + &H30                           ' 0x0E6 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_13_2 = EUnitFields.UNIT_END + &H31                           ' 0x0E7 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_14_1 = EUnitFields.UNIT_END + &H33                           ' 0x0E9 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_14_2 = EUnitFields.UNIT_END + &H34                           ' 0x0EA - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_15_1 = EUnitFields.UNIT_END + &H36                           ' 0x0EC - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_15_2 = EUnitFields.UNIT_END + &H37                           ' 0x0ED - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_16_1 = EUnitFields.UNIT_END + &H39                           ' 0x0EF - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_16_2 = EUnitFields.UNIT_END + &H3A                           ' 0x0F0 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_17_1 = EUnitFields.UNIT_END + &H3C                           ' 0x0F2 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_17_2 = EUnitFields.UNIT_END + &H3D                           ' 0x0F3 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_18_1 = EUnitFields.UNIT_END + &H3F                           ' 0x0F5 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_18_2 = EUnitFields.UNIT_END + &H40                           ' 0x0F6 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_19_1 = EUnitFields.UNIT_END + &H42                           ' 0x0F8 - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_19_2 = EUnitFields.UNIT_END + &H43                           ' 0x0F9 - Size: 2 - Type: INT - Flags: PRIVATE
        //PLAYER_QUEST_LOG_20_1 = EUnitFields.UNIT_END + &H45                           ' 0x0FB - Size: 1 - Type: INT - Flags: GROUP_ONLY
        //PLAYER_QUEST_LOG_20_2 = EUnitFields.UNIT_END + &H46                           ' 0x0FC - Size: 2 - Type: INT - Flags: PRIVATE
        PLAYER_QUEST_LOG_LAST_1 = EUnitFields.UNIT_END + 0x43,
        PLAYER_QUEST_LOG_LAST_2 = EUnitFields.UNIT_END + 0x44,
        PLAYER_QUEST_LOG_LAST_3 = EUnitFields.UNIT_END + 0x45,
        PLAYER_VISIBLE_ITEM_1_CREATOR = EUnitFields.UNIT_END + 0x46,
        // 0x0FE - Size: 2 - Type: GUID - Flags: PUBLIC
        PLAYER_VISIBLE_ITEM_1_0 = EUnitFields.UNIT_END + 0x48,
        // 0x100 - Size: 8 - Type: INT - Flags: PUBLIC
        PLAYER_VISIBLE_ITEM_1_PROPERTIES = EUnitFields.UNIT_END + 0x50,
        // 0x108 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        PLAYER_VISIBLE_ITEM_1_PAD = EUnitFields.UNIT_END + 0x51,
        // 0x109 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_2_CREATOR = EUnitFields.UNIT_END + &H54                   ' 0x10A - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_2_0 = EUnitFields.UNIT_END + &H56                         ' 0x10C - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_2_PROPERTIES = EUnitFields.UNIT_END + &H5E                ' 0x114 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_2_PAD = EUnitFields.UNIT_END + &H5F                       ' 0x115 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_3_CREATOR = EUnitFields.UNIT_END + &H60                   ' 0x116 - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_3_0 = EUnitFields.UNIT_END + &H62                         ' 0x118 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_3_PROPERTIES = EUnitFields.UNIT_END + &H6A                ' 0x120 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_3_PAD = EUnitFields.UNIT_END + &H6B                       ' 0x121 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_4_CREATOR = EUnitFields.UNIT_END + &H6C                   ' 0x122 - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_4_0 = EUnitFields.UNIT_END + &H6E                         ' 0x124 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_4_PROPERTIES = EUnitFields.UNIT_END + &H76                ' 0x12C - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_4_PAD = EUnitFields.UNIT_END + &H77                       ' 0x12D - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_5_CREATOR = EUnitFields.UNIT_END + &H78                   ' 0x12E - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_5_0 = EUnitFields.UNIT_END + &H7A                         ' 0x130 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_5_PROPERTIES = EUnitFields.UNIT_END + &H82                ' 0x138 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_5_PAD = EUnitFields.UNIT_END + &H83                       ' 0x139 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_6_CREATOR = EUnitFields.UNIT_END + &H84                   ' 0x13A - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_6_0 = EUnitFields.UNIT_END + &H86                         ' 0x13C - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_6_PROPERTIES = EUnitFields.UNIT_END + &H8E                ' 0x144 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_6_PAD = EUnitFields.UNIT_END + &H8F                       ' 0x145 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_7_CREATOR = EUnitFields.UNIT_END + &H90                   ' 0x146 - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_7_0 = EUnitFields.UNIT_END + &H92                         ' 0x148 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_7_PROPERTIES = EUnitFields.UNIT_END + &H9A                ' 0x150 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_7_PAD = EUnitFields.UNIT_END + &H9B                       ' 0x151 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_8_CREATOR = EUnitFields.UNIT_END + &H9C                   ' 0x152 - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_8_0 = EUnitFields.UNIT_END + &H9E                         ' 0x154 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_8_PROPERTIES = EUnitFields.UNIT_END + &HA6                ' 0x15C - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_8_PAD = EUnitFields.UNIT_END + &HA7                       ' 0x15D - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_9_CREATOR = EUnitFields.UNIT_END + &HA8                   ' 0x15E - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_9_0 = EUnitFields.UNIT_END + &HAA                         ' 0x160 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_9_PROPERTIES = EUnitFields.UNIT_END + &HB2                ' 0x168 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_9_PAD = EUnitFields.UNIT_END + &HB3                       ' 0x169 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_10_CREATOR = EUnitFields.UNIT_END + &HB4                  ' 0x16A - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_10_0 = EUnitFields.UNIT_END + &HB6                        ' 0x16C - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_10_PROPERTIES = EUnitFields.UNIT_END + &HBE               ' 0x174 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_10_PAD = EUnitFields.UNIT_END + &HBF                      ' 0x175 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_11_CREATOR = EUnitFields.UNIT_END + &HC0                  ' 0x176 - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_11_0 = EUnitFields.UNIT_END + &HC2                        ' 0x178 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_11_PROPERTIES = EUnitFields.UNIT_END + &HCA               ' 0x180 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_11_PAD = EUnitFields.UNIT_END + &HCB                      ' 0x181 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_12_CREATOR = EUnitFields.UNIT_END + &HCC                  ' 0x182 - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_12_0 = EUnitFields.UNIT_END + &HCE                        ' 0x184 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_12_PROPERTIES = EUnitFields.UNIT_END + &HD6               ' 0x18C - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_12_PAD = EUnitFields.UNIT_END + &HD7                      ' 0x18D - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_13_CREATOR = EUnitFields.UNIT_END + &HD8                  ' 0x18E - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_13_0 = EUnitFields.UNIT_END + &HDA                        ' 0x190 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_13_PROPERTIES = EUnitFields.UNIT_END + &HE2               ' 0x198 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_13_PAD = EUnitFields.UNIT_END + &HE3                      ' 0x199 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_14_CREATOR = EUnitFields.UNIT_END + &HE4                  ' 0x19A - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_14_0 = EUnitFields.UNIT_END + &HE6                        ' 0x19C - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_14_PROPERTIES = EUnitFields.UNIT_END + &HEE               ' 0x1A4 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_14_PAD = EUnitFields.UNIT_END + &HEF                      ' 0x1A5 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_15_CREATOR = EUnitFields.UNIT_END + &HF0                  ' 0x1A6 - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_15_0 = EUnitFields.UNIT_END + &HF2                        ' 0x1A8 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_15_PROPERTIES = EUnitFields.UNIT_END + &HFA               ' 0x1B0 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_15_PAD = EUnitFields.UNIT_END + &HFB                      ' 0x1B1 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_16_CREATOR = EUnitFields.UNIT_END + &HFC                  ' 0x1B2 - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_16_0 = EUnitFields.UNIT_END + &HFE                        ' 0x1B4 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_16_PROPERTIES = EUnitFields.UNIT_END + &H106              ' 0x1BC - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_16_PAD = EUnitFields.UNIT_END + &H107                     ' 0x1BD - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_17_CREATOR = EUnitFields.UNIT_END + &H108                 ' 0x1BE - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_17_0 = EUnitFields.UNIT_END + &H10A                       ' 0x1C0 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_17_PROPERTIES = EUnitFields.UNIT_END + &H112              ' 0x1C8 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_17_PAD = EUnitFields.UNIT_END + &H113                     ' 0x1C9 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_18_CREATOR = EUnitFields.UNIT_END + &H114                 ' 0x1CA - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_18_0 = EUnitFields.UNIT_END + &H116                       ' 0x1CC - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_18_PROPERTIES = EUnitFields.UNIT_END + &H11E              ' 0x1D4 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_18_PAD = EUnitFields.UNIT_END + &H11F                     ' 0x1D5 - Size: 1 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_19_CREATOR = EUnitFields.UNIT_END + &H120                 ' 0x1D6 - Size: 2 - Type: GUID - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_19_0 = EUnitFields.UNIT_END + &H122                       ' 0x1D8 - Size: 8 - Type: INT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_19_PROPERTIES = EUnitFields.UNIT_END + &H12A              ' 0x1E0 - Size: 1 - Type: TWO_SHORT - Flags: PUBLIC
        //PLAYER_VISIBLE_ITEM_19_PAD = EUnitFields.UNIT_END + &H12B                     ' 0x1E1 - Size: 1 - Type: INT - Flags: PUBLIC
        PLAYER_VISIBLE_ITEM_LAST_CREATOR = EUnitFields.UNIT_END + 0x11e,
        PLAYER_VISIBLE_ITEM_LAST_0 = EUnitFields.UNIT_END + 0x120,
        PLAYER_VISIBLE_ITEM_LAST_PROPERTIES = EUnitFields.UNIT_END + 0x128,
        PLAYER_VISIBLE_ITEM_LAST_PAD = EUnitFields.UNIT_END + 0x129,
        PLAYER_FIELD_INV_SLOT_HEAD = EUnitFields.UNIT_END + 0x12a,
        // 0x1E2 - Size: 46 - Type: GUID - Flags: PRIVATE
        PLAYER_FIELD_PACK_SLOT_1 = EUnitFields.UNIT_END + 0x158,
        // 0x210 - Size: 32 - Type: GUID - Flags: PRIVATE
        PLAYER_FIELD_PACK_SLOT_LAST = EUnitFields.UNIT_END + 0x176,
        PLAYER_FIELD_BANK_SLOT_1 = EUnitFields.UNIT_END + 0x178,
        // 0x230 - Size: 48 - Type: GUID - Flags: PRIVATE
        PLAYER_FIELD_BANK_SLOT_LAST = EUnitFields.UNIT_END + 0x1a6,
        PLAYER_FIELD_BANKBAG_SLOT_1 = EUnitFields.UNIT_END + 0x1a8,
        // 0x260 - Size: 12 - Type: GUID - Flags: PRIVATE
        PLAYER_FIELD_BANKBAG_SLOT_LAST = EUnitFields.UNIT_END + 0xab2,
        PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = EUnitFields.UNIT_END + 0x1b4,
        // 0x26C - Size: 24 - Type: GUID - Flags: PRIVATE
        PLAYER_FIELD_VENDORBUYBACK_SLOT_LAST = EUnitFields.UNIT_END + 0x1ca,
        PLAYER_FIELD_KEYRING_SLOT_1 = EUnitFields.UNIT_END + 0x1cc,
        // 0x284 - Size: 64 - Type: GUID - Flags: PRIVATE
        PLAYER_FIELD_KEYRING_SLOT_LAST = EUnitFields.UNIT_END + 0x20a,

        PLAYER_FARSIGHT = EUnitFields.UNIT_END + 0x20c,
        // 0x2C4 - Size: 2 - Type: GUID - Flags: PRIVATE
        PLAYER_FIELD_COMBO_TARGET = EUnitFields.UNIT_END + 0x20e,
        // 0x2C6 - Size: 2 - Type: GUID - Flags: PRIVATE
        PLAYER_XP = EUnitFields.UNIT_END + 0x210,
        // 0x2C8 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_NEXT_LEVEL_XP = EUnitFields.UNIT_END + 0x211,
        // 0x2C9 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_SKILL_INFO_1_1 = EUnitFields.UNIT_END + 0x212,
        // 0x2CA - Size: 384 - Type: TWO_SHORT - Flags: PRIVATE
        PLAYER_CHARACTER_POINTS1 = EUnitFields.UNIT_END + 0x392,
        // 0x44A - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_CHARACTER_POINTS2 = EUnitFields.UNIT_END + 0x393,
        // 0x44B - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_TRACK_CREATURES = EUnitFields.UNIT_END + 0x394,
        // 0x44C - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_TRACK_RESOURCES = EUnitFields.UNIT_END + 0x395,
        // 0x44D - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_BLOCK_PERCENTAGE = EUnitFields.UNIT_END + 0x396,
        // 0x44E - Size: 1 - Type: FLOAT - Flags: PRIVATE
        PLAYER_DODGE_PERCENTAGE = EUnitFields.UNIT_END + 0x397,
        // 0x44F - Size: 1 - Type: FLOAT - Flags: PRIVATE
        PLAYER_PARRY_PERCENTAGE = EUnitFields.UNIT_END + 0x398,
        // 0x450 - Size: 1 - Type: FLOAT - Flags: PRIVATE
        PLAYER_CRIT_PERCENTAGE = EUnitFields.UNIT_END + 0x399,
        // 0x451 - Size: 1 - Type: FLOAT - Flags: PRIVATE
        PLAYER_RANGED_CRIT_PERCENTAGE = EUnitFields.UNIT_END + 0x39a,
        // 0x452 - Size: 1 - Type: FLOAT - Flags: PRIVATE
        PLAYER_EXPLORED_ZONES_1 = EUnitFields.UNIT_END + 0x39b,
        // 0x453 - Size: 64 - Type: BYTES - Flags: PRIVATE
        PLAYER_REST_STATE_EXPERIENCE = EUnitFields.UNIT_END + 0x3db,
        // 0x493 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_COINAGE = EUnitFields.UNIT_END + 0x3dc,
        // 0x494 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_POSSTAT0 = EUnitFields.UNIT_END + 0x3dd,
        // 0x495 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_POSSTAT1 = EUnitFields.UNIT_END + 0x3de,
        // 0x496 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_POSSTAT2 = EUnitFields.UNIT_END + 0x3df,
        // 0x497 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_POSSTAT3 = EUnitFields.UNIT_END + 0x3e0,
        // 0x498 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_POSSTAT4 = EUnitFields.UNIT_END + 0x3e1,
        // 0x499 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_NEGSTAT0 = EUnitFields.UNIT_END + 0x3e2,
        // 0x49A - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_NEGSTAT1 = EUnitFields.UNIT_END + 0x3e3,
        // 0x49B - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_NEGSTAT2 = EUnitFields.UNIT_END + 0x3e4,
        // 0x49C - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_NEGSTAT3 = EUnitFields.UNIT_END + 0x3e5,
        // 0x49D - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_NEGSTAT4 = EUnitFields.UNIT_END + 0x3e6,
        // 0x49E - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE = EUnitFields.UNIT_END + 0x3e7,
        // 0x49F - Size: 7 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE = EUnitFields.UNIT_END + 0x3ee,
        // 0x4A6 - Size: 7 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS = EUnitFields.UNIT_END + 0x3f5,
        // 0x4AD - Size: 7 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = EUnitFields.UNIT_END + 0x3fc,
        // 0x4B4 - Size: 7 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = EUnitFields.UNIT_END + 0x403,
        // 0x4BB - Size: 7 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_BYTES = EUnitFields.UNIT_END + 0x40a,
        // 0x4C2 - Size: 1 - Type: BYTES - Flags: PRIVATE
        PLAYER_AMMO_ID = EUnitFields.UNIT_END + 0x40b,
        // 0x4C3 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_SELF_RES_SPELL = EUnitFields.UNIT_END + 0x40c,
        // 0x4C4 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_PVP_MEDALS = EUnitFields.UNIT_END + 0x40d,
        // 0x4C5 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_BUYBACK_PRICE_1 = EUnitFields.UNIT_END + 0x40e,
        // 0x4C6 - Size: 12 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_BUYBACK_PRICE_LAST = EUnitFields.UNIT_END + 0x419,
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = EUnitFields.UNIT_END + 0x41a,
        // 0x4D2 - Size: 12 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_BUYBACK_TIMESTAMP_LAST = EUnitFields.UNIT_END + 0x425,
        PLAYER_FIELD_SESSION_KILLS = EUnitFields.UNIT_END + 0x426,
        // 0x4DE - Size: 1 - Type: TWO_SHORT - Flags: PRIVATE
        PLAYER_FIELD_YESTERDAY_KILLS = EUnitFields.UNIT_END + 0x427,
        // 0x4DF - Size: 1 - Type: TWO_SHORT - Flags: PRIVATE
        PLAYER_FIELD_LAST_WEEK_KILLS = EUnitFields.UNIT_END + 0x428,
        // 0x4E0 - Size: 1 - Type: TWO_SHORT - Flags: PRIVATE
        PLAYER_FIELD_THIS_WEEK_KILLS = EUnitFields.UNIT_END + 0x429,
        // 0x4E1 - Size: 1 - Type: TWO_SHORT - Flags: PRIVATE
        PLAYER_FIELD_THIS_WEEK_CONTRIBUTION = EUnitFields.UNIT_END + 0x42a,
        // 0x4E2 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_LIFETIME_HONORBALE_KILLS = EUnitFields.UNIT_END + 0x42b,
        // 0x4E3 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_LIFETIME_DISHONORBALE_KILLS = EUnitFields.UNIT_END + 0x42c,
        // 0x4E4 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_YESTERDAY_CONTRIBUTION = EUnitFields.UNIT_END + 0x42d,
        // 0x4E5 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_LAST_WEEK_CONTRIBUTION = EUnitFields.UNIT_END + 0x42e,
        // 0x4E6 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_LAST_WEEK_RANK = EUnitFields.UNIT_END + 0x42f,
        // 0x4E7 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_BYTES2 = EUnitFields.UNIT_END + 0x430,
        // 0x4E8 - Size: 1 - Type: BYTES - Flags: PRIVATE
        PLAYER_FIELD_WATCHED_FACTION_INDEX = EUnitFields.UNIT_END + 0x431,
        // 0x4E9 - Size: 1 - Type: INT - Flags: PRIVATE
        PLAYER_FIELD_COMBAT_RATING_1 = EUnitFields.UNIT_END + 0x432,
        PLAYER_END = EUnitFields.UNIT_END + 0x446
        // 0x4EA
    }

    public enum EObjectFields
    {
        OBJECT_FIELD_GUID = 0x0, // 0x000 - Size: 2 - Type: GUID  - Flags: PUBLIC
        OBJECT_FIELD_TYPE = 0x2, // 0x002 - Size: 1 - Type: INT   - Flags: PUBLIC
        OBJECT_FIELD_ENTRY = 0x3, // 0x003 - Size: 1 - Type: INT   - Flags: PUBLIC
        OBJECT_FIELD_SCALE_X = 0x4, // 0x004 - Size: 1 - Type: FLOAT - Flags: PUBLIC
        OBJECT_FIELD_PADDING = 0x5, // 0x005 - Size: 1 - Type: INT   - Flags: NONE
        OBJECT_END = 0x6
    }

    public enum EUnitFields
    {
        UNIT_FIELD_CHARM = EObjectFields.OBJECT_END + 0x0,                              // 0x006 - Size: 2 - Type: GUID - Flags: PUBLIC
        UNIT_FIELD_SUMMON = EObjectFields.OBJECT_END + 0x2,                             // 0x008 - Size: 2 - Type: GUID - Flags: PUBLIC
        UNIT_FIELD_CHARMEDBY = EObjectFields.OBJECT_END + 0x4,                          // 0x00A - Size: 2 - Type: GUID - Flags: PUBLIC
        UNIT_FIELD_SUMMONEDBY = EObjectFields.OBJECT_END + 0x6,                         // 0x00C - Size: 2 - Type: GUID - Flags: PUBLIC
        UNIT_FIELD_CREATEDBY = EObjectFields.OBJECT_END + 0x8,                          // 0x00E - Size: 2 - Type: GUID - Flags: PUBLIC
        UNIT_FIELD_TARGET = EObjectFields.OBJECT_END + 0xa,                             // 0x010 - Size: 2 - Type: GUID - Flags: PUBLIC
        UNIT_FIELD_PERSUADED = EObjectFields.OBJECT_END + 0xc,                          // 0x012 - Size: 2 - Type: GUID - Flags: PUBLIC
        UNIT_FIELD_CHANNEL_OBJECT = EObjectFields.OBJECT_END + 0xe,                     // 0x014 - Size: 2 - Type: GUID - Flags: PUBLIC
        UNIT_FIELD_HEALTH = EObjectFields.OBJECT_END + 0x10,                            // 0x016 - Size: 1 - Type: INT - Flags: DYNAMIC
        UNIT_FIELD_POWER1 = EObjectFields.OBJECT_END + 0x11,                            // 0x017 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_POWER2 = EObjectFields.OBJECT_END + 0x12,                            // 0x018 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_POWER3 = EObjectFields.OBJECT_END + 0x13,                            // 0x019 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_POWER4 = EObjectFields.OBJECT_END + 0x14,                            // 0x01A - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_POWER5 = EObjectFields.OBJECT_END + 0x15,                            // 0x01B - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_MAXHEALTH = EObjectFields.OBJECT_END + 0x16,                         // 0x01C - Size: 1 - Type: INT - Flags: DYNAMIC
        UNIT_FIELD_MAXPOWER1 = EObjectFields.OBJECT_END + 0x17,                         // 0x01D - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_MAXPOWER2 = EObjectFields.OBJECT_END + 0x18,                         // 0x01E - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_MAXPOWER3 = EObjectFields.OBJECT_END + 0x19,                         // 0x01F - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_MAXPOWER4 = EObjectFields.OBJECT_END + 0x1a,                         // 0x020 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_MAXPOWER5 = EObjectFields.OBJECT_END + 0x1b,                         // 0x021 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_LEVEL = EObjectFields.OBJECT_END + 0x1c,                             // 0x022 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_FACTIONTEMPLATE = EObjectFields.OBJECT_END + 0x1d,                   // 0x023 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_BYTES_0 = EObjectFields.OBJECT_END + 0x1e,                           // 0x024 - Size: 1 - Type: BYTES - Flags: PUBLIC
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY = EObjectFields.OBJECT_END + 0x1f,               // 0x025 - Size: 3 - Type: INT - Flags: PUBLIC
        UNIT_VIRTUAL_ITEM_INFO = EObjectFields.OBJECT_END + 0x22,                       // 0x028 - Size: 6 - Type: BYTES - Flags: PUBLIC
        UNIT_FIELD_FLAGS = EObjectFields.OBJECT_END + 0x28,                             // 0x02E - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_AURA = EObjectFields.OBJECT_END + 0x29,                              // 0x02F - Size: 48 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_AURAFLAGS = EObjectFields.OBJECT_END + 0x59,                         // 0x05F - Size: 6 - Type: BYTES - Flags: PUBLIC
        UNIT_FIELD_AURALEVELS = EObjectFields.OBJECT_END + 0x5f,                        // 0x065 - Size: 12 - Type: BYTES - Flags: PUBLIC
        UNIT_FIELD_AURAAPPLICATIONS = EObjectFields.OBJECT_END + 0x6b,                  // 0x071 - Size: 12 - Type: BYTES - Flags: PUBLIC
        UNIT_FIELD_AURASTATE = EObjectFields.OBJECT_END + 0x77,                         // 0x07D - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_BASEATTACKTIME = EObjectFields.OBJECT_END + 0x78,                    // 0x07E - Size: 2 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_OFFHANDATTACKTIME = EObjectFields.OBJECT_END + 0x79,
        UNIT_FIELD_RANGEDATTACKTIME = EObjectFields.OBJECT_END + 0x7a,                  // 0x080 - Size: 1 - Type: INT - Flags: PRIVATE
        UNIT_FIELD_BOUNDINGRADIUS = EObjectFields.OBJECT_END + 0x7b,                    // 0x081 - Size: 1 - Type: FLOAT - Flags: PUBLIC
        UNIT_FIELD_COMBATREACH = EObjectFields.OBJECT_END + 0x7c,                       // 0x082 - Size: 1 - Type: FLOAT - Flags: PUBLIC
        UNIT_FIELD_DISPLAYID = EObjectFields.OBJECT_END + 0x7d,                         // 0x083 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_NATIVEDISPLAYID = EObjectFields.OBJECT_END + 0x7e,                   // 0x084 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_MOUNTDISPLAYID = EObjectFields.OBJECT_END + 0x7f,                    // 0x085 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_MINDAMAGE = EObjectFields.OBJECT_END + 0x80,                         // 0x086 - Size: 1 - Type: FLOAT - Flags: PRIVATE + OWNER_ONLY + UNK3
        UNIT_FIELD_MAXDAMAGE = EObjectFields.OBJECT_END + 0x81,                         // 0x087 - Size: 1 - Type: FLOAT - Flags: PRIVATE + OWNER_ONLY + UNK3
        UNIT_FIELD_MINOFFHANDDAMAGE = EObjectFields.OBJECT_END + 0x82,                  // 0x088 - Size: 1 - Type: FLOAT - Flags: PRIVATE + OWNER_ONLY + UNK3
        UNIT_FIELD_MAXOFFHANDDAMAGE = EObjectFields.OBJECT_END + 0x83,                  // 0x089 - Size: 1 - Type: FLOAT - Flags: PRIVATE + OWNER_ONLY + UNK3
        UNIT_FIELD_BYTES_1 = EObjectFields.OBJECT_END + 0x84,                           // 0x08A - Size: 1 - Type: BYTES - Flags: PUBLIC
        UNIT_FIELD_PETNUMBER = EObjectFields.OBJECT_END + 0x85,                         // 0x08B - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_PET_NAME_TIMESTAMP = EObjectFields.OBJECT_END + 0x86,                // 0x08C - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_FIELD_PETEXPERIENCE = EObjectFields.OBJECT_END + 0x87,                     // 0x08D - Size: 1 - Type: INT - Flags: OWNER_ONLY
        UNIT_FIELD_PETNEXTLEVELEXP = EObjectFields.OBJECT_END + 0x88,                   // 0x08E - Size: 1 - Type: INT - Flags: OWNER_ONLY
        UNIT_DYNAMIC_FLAGS = EObjectFields.OBJECT_END + 0x89,                           // 0x08F - Size: 1 - Type: INT - Flags: DYNAMIC
        UNIT_CHANNEL_SPELL = EObjectFields.OBJECT_END + 0x8a,                           // 0x090 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_MOD_CAST_SPEED = EObjectFields.OBJECT_END + 0x8b,                          // 0x091 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_CREATED_BY_SPELL = EObjectFields.OBJECT_END + 0x8c,                        // 0x092 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_NPC_FLAGS = EObjectFields.OBJECT_END + 0x8d,                               // 0x093 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_NPC_EMOTESTATE = EObjectFields.OBJECT_END + 0x8e,                          // 0x094 - Size: 1 - Type: INT - Flags: PUBLIC
        UNIT_TRAINING_POINTS = EObjectFields.OBJECT_END + 0x8f,                         // 0x095 - Size: 1 - Type: TWO_SHORT - Flags: OWNER_ONLY
        UNIT_FIELD_STAT0 = EObjectFields.OBJECT_END + 0x90,                             // 0x096 - Size: 1 - Type: INT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_STAT1 = EObjectFields.OBJECT_END + 0x91,                             // 0x097 - Size: 1 - Type: INT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_STAT2 = EObjectFields.OBJECT_END + 0x92,                             // 0x098 - Size: 1 - Type: INT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_STAT3 = EObjectFields.OBJECT_END + 0x93,                             // 0x099 - Size: 1 - Type: INT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_STAT4 = EObjectFields.OBJECT_END + 0x94,                             // 0x09A - Size: 1 - Type: INT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_RESISTANCES = EObjectFields.OBJECT_END + 0x95,                       // 0x09B - Size: 7 - Type: INT - Flags: PRIVATE + OWNER_ONLY + UNK3
        UNIT_FIELD_BASE_MANA = EObjectFields.OBJECT_END + 0x9c,                         // 0x0A2 - Size: 1 - Type: INT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_BASE_HEALTH = EObjectFields.OBJECT_END + 0x9d,                       // 0x0A3 - Size: 1 - Type: INT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_BYTES_2 = EObjectFields.OBJECT_END + 0x9e,                           // 0x0A4 - Size: 1 - Type: BYTES - Flags: PUBLIC
        UNIT_FIELD_ATTACK_POWER = EObjectFields.OBJECT_END + 0x9f,                      // 0x0A5 - Size: 1 - Type: INT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_ATTACK_POWER_MODS = EObjectFields.OBJECT_END + 0xa0,                 // 0x0A6 - Size: 1 - Type: TWO_SHORT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER = EObjectFields.OBJECT_END + 0xa1,           // 0x0A7 - Size: 1 - Type: FLOAT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_RANGED_ATTACK_POWER = EObjectFields.OBJECT_END + 0xa2,               // 0x0A8 - Size: 1 - Type: INT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_RANGED_ATTACK_POWER_MODS = EObjectFields.OBJECT_END + 0xa3,          // 0x0A9 - Size: 1 - Type: TWO_SHORT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = EObjectFields.OBJECT_END + 0xa4,    // 0x0AA - Size: 1 - Type: FLOAT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_MINRANGEDDAMAGE = EObjectFields.OBJECT_END + 0xa5,                   // 0x0AB - Size: 1 - Type: FLOAT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_MAXRANGEDDAMAGE = EObjectFields.OBJECT_END + 0xa6,                   // 0x0AC - Size: 1 - Type: FLOAT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_POWER_COST_MODIFIER = EObjectFields.OBJECT_END + 0xa7,               // 0x0AD - Size: 7 - Type: INT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_POWER_COST_MULTIPLIER = EObjectFields.OBJECT_END + 0xae,             // 0x0B4 - Size: 7 - Type: FLOAT - Flags: PRIVATE + OWNER_ONLY
        UNIT_FIELD_PADDING = EObjectFields.OBJECT_END + 0xb5,                           // 0x0BB - Size: 1 - Type: INT - Flags: NONE
        UNIT_END = EObjectFields.OBJECT_END + 0xb6,                                     // 0x0BC

        UNIT_FIELD_STRENGTH = UNIT_FIELD_STAT0,
        UNIT_FIELD_AGILITY = UNIT_FIELD_STAT1,
        UNIT_FIELD_STAMINA = UNIT_FIELD_STAT2,
        UNIT_FIELD_SPIRIT = UNIT_FIELD_STAT3,
        UNIT_FIELD_INTELLECT = UNIT_FIELD_STAT4
    }

    public enum FactionTemplates
    {
        None = 0
    }

    public enum UnitFlags
    {
        UNIT_FLAG_NONE = 0x0,
        UNIT_FLAG_UNK1 = 0x1,
        UNIT_FLAG_NOT_ATTACKABLE = 0x2,		// Unit is not attackable
        UNIT_FLAG_DISABLE_MOVE = 0x4,		// Unit is frozen, rooted or stunned
        UNIT_FLAG_ATTACKABLE = 0x8,			// Unit becomes temporarily hostile, shows in red, allows attack
        UNIT_FLAG_RENAME = 0x10,
        UNIT_FLAG_RESTING = 0x20,
        UNIT_FLAG_UNK5 = 0x40,
        UNIT_FLAG_NOT_ATTACKABLE_1 = 0x80,	// Unit cannot be attacked by player, shows no attack cursor
        UNIT_FLAG_UNK6 = 0x100,
        UNIT_FLAG_UNK7 = 0x200,
        UNIT_FLAG_NON_PVP_PLAYER = UNIT_FLAG_ATTACKABLE + UNIT_FLAG_NOT_ATTACKABLE_1, // Unit cannot be attacked by player, shows in blue
        UNIT_FLAG_LOOTING = 0x400,
        UNIT_FLAG_PET_IN_COMBAT = 0x800,
        UNIT_FLAG_PVP = 0x1000,
        UNIT_FLAG_SILENCED = 0x2000,
        UNIT_FLAG_DEAD = 0x4000,
        UNIT_FLAG_UNK11 = 0x8000,
        UNIT_FLAG_ROOTED = 0x10000,
        UNIT_FLAG_PACIFIED = 0x20000,
        UNIT_FLAG_STUNTED = 0x40000,
        UNIT_FLAG_IN_COMBAT = 0x80000,
        UNIT_FLAG_TAXI_FLIGHT = 0x100000,
        UNIT_FLAG_DISARMED = 0x200000,
        UNIT_FLAG_CONFUSED = 0x400000,
        UNIT_FLAG_FLEEING = 0x800000,
        UNIT_FLAG_UNK21 = 0x1000000,
        UNIT_FLAG_NOT_SELECTABLE = 0x2000000,
        UNIT_FLAG_SKINNABLE = 0x4000000,
        UNIT_FLAG_MOUNT = 0x8000000,
        UNIT_FLAG_UNK25 = 0x10000000,
        UNIT_FLAG_UNK26 = 0x20000000,
        UNIT_FLAG_SKINNABLE_AND_DEAD = UNIT_FLAG_SKINNABLE + UNIT_FLAG_DEAD,
        UNIT_FLAG_SPIRITHEALER = UNIT_FLAG_UNK21 + UNIT_FLAG_NOT_ATTACKABLE + UNIT_FLAG_DISABLE_MOVE + UNIT_FLAG_RESTING + UNIT_FLAG_UNK5,
        UNIT_FLAG_SHEATHE = 0x40000000
    }

    public enum InventorySlots
    {
        SLOT_START = 0,
        SLOT_HEAD = 0,
        SLOT_NECK = 1,
        SLOT_SHOULDERS = 2,
        SLOT_SHIRT = 3,
        SLOT_CHEST = 4,
        SLOT_WAIST = 5,
        SLOT_LEGS = 6,
        SLOT_FEET = 7,
        SLOT_WRISTS = 8,
        SLOT_HANDS = 9,
        SLOT_FINGERL = 10,
        SLOT_FINGERR = 11,
        SLOT_TRINKETL = 12,
        SLOT_TRINKETR = 13,
        SLOT_BACK = 14,
        SLOT_MAINHAND = 15,
        SLOT_OFFHAND = 16,
        SLOT_RANGED = 17,
        SLOT_TABARD = 18,
        SLOT_END = 19,

        // Misc Types
        SLOT_BAG_START = 19,
        SLOT_BAG1 = 19,
        SLOT_BAG2 = 20,
        SLOT_BAG3 = 21,
        SLOT_BAG4 = 22,
        SLOT_INBACKPACK = 23,
        SLOT_BAG_END = 23,

        SLOT_ITEM_START = 23,
        SLOT_ITEM_END = 39,

        SLOT_BANK_ITEM_START = 39,
        SLOT_BANK_ITEM_END = 63,
        SLOT_BANK_BAG_1 = 63,
        SLOT_BANK_BAG_2 = 64,
        SLOT_BANK_BAG_3 = 65,
        SLOT_BANK_BAG_4 = 66,
        SLOT_BANK_BAG_5 = 67,
        SLOT_BANK_BAG_6 = 68,
        SLOT_BANK_END = 69
    }

    public enum ManaTypes : int
    {
        TYPE_MANA = 0,
        TYPE_RAGE = 1,
        TYPE_FOCUS = 2,
        TYPE_ENERGY = 3,
        TYPE_HAPPINESS = 4,
        TYPE_HEALTH = -2
    }
}
