namespace Common.Globals
{
    public enum KeyRingSlots : byte
    {
        //32 Slots?
        KEYRING_SLOT_START = 81,
        KEYRING_SLOT_1 = 81,
        KEYRING_SLOT_2 = 82,
        KEYRING_SLOT_31 = 112,
        KEYRING_SLOT_32 = 113,
        KEYRING_SLOT_END = 113
    }

    public enum EquipmentSlots : byte
    {
        //19 slots total
        EQUIPMENT_SLOT_START = 0,
        EQUIPMENT_SLOT_HEAD = 0,
        EQUIPMENT_SLOT_NECK = 1,
        EQUIPMENT_SLOT_SHOULDERS = 2,
        EQUIPMENT_SLOT_BODY = 3,
        EQUIPMENT_SLOT_CHEST = 4,
        EQUIPMENT_SLOT_WAIST = 5,
        EQUIPMENT_SLOT_LEGS = 6,
        EQUIPMENT_SLOT_FEET = 7,
        EQUIPMENT_SLOT_WRISTS = 8,
        EQUIPMENT_SLOT_HANDS = 9,
        EQUIPMENT_SLOT_FINGER1 = 10,
        EQUIPMENT_SLOT_FINGER2 = 11,
        EQUIPMENT_SLOT_TRINKET1 = 12,
        EQUIPMENT_SLOT_TRINKET2 = 13,
        EQUIPMENT_SLOT_BACK = 14,
        EQUIPMENT_SLOT_MAINHAND = 15,
        EQUIPMENT_SLOT_OFFHAND = 16,
        EQUIPMENT_SLOT_RANGED = 17,
        EQUIPMENT_SLOT_TABARD = 18,
        EQUIPMENT_SLOT_END = 19
    }

    public enum WeaponAttackType : byte
    {
        BaseAttack = 0,
        OffAttack = 1,
        RangedAttack = 2
    }

    public enum DamageTypes : byte
    {
        DmgPhysical = 0,
        DmgHoly = 1,
        DmgFire = 2,
        DmgNature = 3,
        DmgFrost = 4,
        DmgShadow = 5,
        DmgArcane = 6
    }

    public enum ManaTypes
    {
        TypeMana = 0,
        TypeRage = 1,
        TypeFocus = 2,
        TypeEnergy = 3,
        TypeHappiness = 4,
        TypeHealth = -2
    }

    public enum FactionTemplates
    {
        None = 0
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

    /**************/

    public enum ObjectFields
    {
        OBJECT_FIELD_GUID    = 0x0, // 0x000 - Size: 2 - Type: GUID  - Flags: PUBLIC
        OBJECT_FIELD_TYPE    = 0x2, // 0x002 - Size: 1 - Type: INT   - Flags: PUBLIC
        OBJECT_FIELD_ENTRY   = 0x3, // 0x003 - Size: 1 - Type: INT   - Flags: PUBLIC
        OBJECT_FIELD_SCALE_X = 0x4, // 0x004 - Size: 1 - Type: FLOAT - Flags: PUBLIC
        OBJECT_FIELD_PADDING = 0x5, // 0x005 - Size: 1 - Type: INT   - Flags: NONE
        OBJECT_END           = 0x6,
    }

    public enum UnitFields
    {
        UNIT_FIELD_CHARM = 0x00 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_SUMMON = 0x02 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CHARMEDBY = 0x04 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_SUMMONEDBY = 0x06 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CREATEDBY = 0x08 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_TARGET = 0x0A + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_PERSUADED = 0x0C + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CHANNEL_OBJECT = 0x0E + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_HEALTH = 0x10 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER1 = 0x11 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER2 = 0x12 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER3 = 0x13 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER4 = 0x14 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER5 = 0x15 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXHEALTH = 0x16 + ObjectFields.OBJECT_END, // Size:1 
        UNIT_FIELD_MAXPOWER1 = 0x17 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER2 = 0x18 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER3 = 0x19 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER4 = 0x1A + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER5 = 0x1B + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_LEVEL = 0x1C + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_FACTIONTEMPLATE = 0x1D + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_0 = 0x1E + ObjectFields.OBJECT_END, // Size:1
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY = 0x1F + ObjectFields.OBJECT_END, // Size:3
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_01 = 0x20 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_02 = 0x21 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO = 0x22 + ObjectFields.OBJECT_END, // Size:6
        UNIT_VIRTUAL_ITEM_INFO_01 = 0x23 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_02 = 0x24 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_03 = 0x25 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_04 = 0x26 + ObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_05 = 0x27 + ObjectFields.OBJECT_END,
        UNIT_FIELD_FLAGS = 0x28 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_AURA = 0x29 + ObjectFields.OBJECT_END, // Size:48
        UNIT_FIELD_AURA_LAST = 0x58 + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS = 0x59 + ObjectFields.OBJECT_END, // Size:6
        UNIT_FIELD_AURAFLAGS_01 = 0x5a + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_02 = 0x5b + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_03 = 0x5c + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_04 = 0x5d + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_05 = 0x5e + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURALEVELS = 0x5f + ObjectFields.OBJECT_END, // Size:12
        UNIT_FIELD_AURALEVELS_LAST = 0x6a + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURAAPPLICATIONS = 0x6b + ObjectFields.OBJECT_END, // Size:12
        UNIT_FIELD_AURAAPPLICATIONS_LAST = 0x76 + ObjectFields.OBJECT_END,
        UNIT_FIELD_AURASTATE = 0x77 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BASEATTACKTIME = 0x78 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_OFFHANDATTACKTIME = 0x79 + ObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_RANGEDATTACKTIME = 0x7a + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BOUNDINGRADIUS = 0x7b + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_COMBATREACH = 0x7c + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_DISPLAYID = 0x7d + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_NATIVEDISPLAYID = 0x7e + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MOUNTDISPLAYID = 0x7f + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINDAMAGE = 0x80 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXDAMAGE = 0x81 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINOFFHANDDAMAGE = 0x82 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXOFFHANDDAMAGE = 0x83 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_1 = 0x84 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETNUMBER = 0x85 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PET_NAME_TIMESTAMP = 0x86 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETEXPERIENCE = 0x87 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETNEXTLEVELEXP = 0x88 + ObjectFields.OBJECT_END, // Size:1
        UNIT_DYNAMIC_FLAGS = 0x89 + ObjectFields.OBJECT_END, // Size:1
        UNIT_CHANNEL_SPELL = 0x8a + ObjectFields.OBJECT_END, // Size:1
        UNIT_MOD_CAST_SPEED = 0x8b + ObjectFields.OBJECT_END, // Size:1
        UNIT_CREATED_BY_SPELL = 0x8c + ObjectFields.OBJECT_END, // Size:1
        UNIT_NPC_FLAGS = 0x8d + ObjectFields.OBJECT_END, // Size:1
        UNIT_NPC_EMOTESTATE = 0x8e + ObjectFields.OBJECT_END, // Size:1
        UNIT_TRAINING_POINTS = 0x8f + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT0 = 0x90 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT1 = 0x91 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT2 = 0x92 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT3 = 0x93 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT4 = 0x94 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RESISTANCES = 0x95 + ObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_RESISTANCES_01 = 0x96 + ObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_02 = 0x97 + ObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_03 = 0x98 + ObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_04 = 0x99 + ObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_05 = 0x9a + ObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_06 = 0x9b + ObjectFields.OBJECT_END,
        UNIT_FIELD_BASE_MANA = 0x9c + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BASE_HEALTH = 0x9d + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_2 = 0x9e + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER = 0x9f + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER_MODS = 0xa0 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER = 0xa1 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER = 0xa2 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER_MODS = 0xa3 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = 0xa4 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINRANGEDDAMAGE = 0xa5 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXRANGEDDAMAGE = 0xa6 + ObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER_COST_MODIFIER = 0xa7 + ObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_POWER_COST_MODIFIER_01 = 0xa8 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_02 = 0xa9 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_03 = 0xaa + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_04 = 0xab + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_05 = 0xac + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_06 = 0xad + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER = 0xae + ObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_POWER_COST_MULTIPLIER_01 = 0xaf + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_02 = 0xb0 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_03 = 0xb1 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_04 = 0xb2 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_05 = 0xb3 + ObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_06 = 0xb4 + ObjectFields.OBJECT_END,
        UNIT_FIELD_PADDING = 0xb5 + ObjectFields.OBJECT_END,
        UNIT_END = 0xb6 + ObjectFields.OBJECT_END,
    }

    public enum PlayerFields
    {
        PLAYER_DUEL_ARBITER = 0x00 + UnitFields.UNIT_END, // Size:2
        PLAYER_FLAGS = 0x02 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILDID = 0x03 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILDRANK = 0x04 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES = 0x05 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES_2 = 0x06 + UnitFields.UNIT_END, // Size:1
        PLAYER_BYTES_3 = 0x07 + UnitFields.UNIT_END, // Size:1
        PLAYER_DUEL_TEAM = 0x08 + UnitFields.UNIT_END, // Size:1
        PLAYER_GUILD_TIMESTAMP = 0x09 + UnitFields.UNIT_END, // Size:1
        PLAYER_QUEST_LOG_1_1 = 0x0A + UnitFields.UNIT_END, // count = 20
        PLAYER_QUEST_LOG_1_2 = 0x0B + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_1_3 = 0x0C + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_1 = 0x43 + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_2 = 0x44 + UnitFields.UNIT_END,
        PLAYER_QUEST_LOG_LAST_3 = 0x45 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_1_CREATOR = 0x46 + UnitFields.UNIT_END, // Size:2, count = 19
        PLAYER_VISIBLE_ITEM_1_0 = 0x48 + UnitFields.UNIT_END, // Size:8
        PLAYER_VISIBLE_ITEM_1_PROPERTIES = 0x50 + UnitFields.UNIT_END, // Size:1
        PLAYER_VISIBLE_ITEM_1_PAD = 0x51 + UnitFields.UNIT_END, // Size:1
        PLAYER_VISIBLE_ITEM_LAST_CREATOR = 0x11e + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_0 = 0x120 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_PROPERTIES = 0x128 + UnitFields.UNIT_END,
        PLAYER_VISIBLE_ITEM_LAST_PAD = 0x129 + UnitFields.UNIT_END,
        PLAYER_FIELD_INV_SLOT_HEAD = 0x12a + UnitFields.UNIT_END, // Size:46
        PLAYER_FIELD_PACK_SLOT_1 = 0x158 + UnitFields.UNIT_END, // Size:32
        PLAYER_FIELD_PACK_SLOT_LAST = 0x176 + UnitFields.UNIT_END,
        PLAYER_FIELD_BANK_SLOT_1 = 0x178 + UnitFields.UNIT_END, // Size:48
        PLAYER_FIELD_BANK_SLOT_LAST = 0x1a6 + UnitFields.UNIT_END,
        PLAYER_FIELD_BANKBAG_SLOT_1 = 0x1a8 + UnitFields.UNIT_END, // Size:12
        PLAYER_FIELD_BANKBAG_SLOT_LAST = 0xab2 + UnitFields.UNIT_END,
        PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = 0x1b4 + UnitFields.UNIT_END, // Size:24
        PLAYER_FIELD_VENDORBUYBACK_SLOT_LAST = 0x1ca + UnitFields.UNIT_END,
        PLAYER_FIELD_KEYRING_SLOT_1 = 0x1cc + UnitFields.UNIT_END, // Size:64
        PLAYER_FIELD_KEYRING_SLOT_LAST = 0x20a + UnitFields.UNIT_END,
        PLAYER_FARSIGHT = 0x20c + UnitFields.UNIT_END, // Size:2
        PLAYER_FIELD_COMBO_TARGET = 0x20e + UnitFields.UNIT_END, // Size:2
        PLAYER_XP = 0x210 + UnitFields.UNIT_END, // Size:1
        PLAYER_NEXT_LEVEL_XP = 0x211 + UnitFields.UNIT_END, // Size:1
        PLAYER_SKILL_INFO_1_1 = 0x212 + UnitFields.UNIT_END, // Size:384
        PLAYER_SKILL_PROP_1_1 = 0x213 + UnitFields.UNIT_END, // Size:384

        PLAYER_CHARACTER_POINTS1 = 0x392 + UnitFields.UNIT_END, // Size:1
        PLAYER_CHARACTER_POINTS2 = 0x393 + UnitFields.UNIT_END, // Size:1
        PLAYER_TRACK_CREATURES = 0x394 + UnitFields.UNIT_END, // Size:1
        PLAYER_TRACK_RESOURCES = 0x395 + UnitFields.UNIT_END, // Size:1
        PLAYER_BLOCK_PERCENTAGE = 0x396 + UnitFields.UNIT_END, // Size:1
        PLAYER_DODGE_PERCENTAGE = 0x397 + UnitFields.UNIT_END, // Size:1
        PLAYER_PARRY_PERCENTAGE = 0x398 + UnitFields.UNIT_END, // Size:1
        PLAYER_CRIT_PERCENTAGE = 0x399 + UnitFields.UNIT_END, // Size:1
        PLAYER_RANGED_CRIT_PERCENTAGE = 0x39a + UnitFields.UNIT_END, // Size:1
        PLAYER_EXPLORED_ZONES_1 = 0x39b + UnitFields.UNIT_END, // Size:64
        PLAYER_REST_STATE_EXPERIENCE = 0x3db + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_COINAGE = 0x3dc + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT0 = 0x3DD + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT1 = 0x3DE + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT2 = 0x3DF + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT3 = 0x3E0 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_POSSTAT4 = 0x3E1 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT0 = 0x3E2 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT1 = 0x3E3 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT2 = 0x3E4 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_NEGSTAT3 = 0x3E5 + UnitFields.UNIT_END, // Size:1,
        PLAYER_FIELD_NEGSTAT4 = 0x3E6 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE = 0x3E7 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE = 0x3EE + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS = 0x3F5 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = 0x3FC + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = 0x403 + UnitFields.UNIT_END, // Size:7
        PLAYER_FIELD_BYTES = 0x40A + UnitFields.UNIT_END, // Size:1
        PLAYER_AMMO_ID = 0x40B + UnitFields.UNIT_END, // Size:1
        PLAYER_SELF_RES_SPELL = 0x40C + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_PVP_MEDALS = 0x40D + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_BUYBACK_PRICE_1 = 0x40E + UnitFields.UNIT_END, // count=12
        PLAYER_FIELD_BUYBACK_PRICE_LAST = 0x419 + UnitFields.UNIT_END,
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1 = 0x41A + UnitFields.UNIT_END, // count=12
        PLAYER_FIELD_BUYBACK_TIMESTAMP_LAST = 0x425 + UnitFields.UNIT_END,
        PLAYER_FIELD_SESSION_KILLS = 0x426 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_YESTERDAY_KILLS = 0x427 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_KILLS = 0x428 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_THIS_WEEK_KILLS = 0x429 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_THIS_WEEK_CONTRIBUTION = 0x42a + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LIFETIME_HONORABLE_KILLS = 0x42b + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LIFETIME_DISHONORABLE_KILLS = 0x42c + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_YESTERDAY_CONTRIBUTION = 0x42d + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_CONTRIBUTION = 0x42e + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_LAST_WEEK_RANK = 0x42f + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_BYTES2 = 0x430 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_WATCHED_FACTION_INDEX = 0x431 + UnitFields.UNIT_END, // Size:1
        PLAYER_FIELD_COMBAT_RATING_1 = 0x432 + UnitFields.UNIT_END, // Size:20

        PLAYER_END = 0x446 + UnitFields.UNIT_END
    };

    /*
    public enum EUnitFields
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
    
    public enum EPlayerFields
    {
        PLAYER_DUEL_ARBITER = 0x00 + EUnitFields.UnitEnd, // Size:2 - Type: GUID - Flags: PUBLIC
        PLAYER_FLAGS = 0x02 + EUnitFields.UnitEnd, // Size:1 - Type: INT - Flags: PUBLIC
        PLAYER_GUILDID = 0x03 + EUnitFields.UnitEnd, // Size:1
        PLAYER_GUILDRANK = 0x04 + EUnitFields.UnitEnd, // Size:1
        PLAYER_BYTES = 0x05 + EUnitFields.UnitEnd, // Size:1 - Type: BYTES - Flags: PUBLIC
        PLAYER_BYTES_2 = 0x06 + EUnitFields.UnitEnd, // Size:1 - Type: BYTES - Flags: PUBLIC
        PLAYER_BYTES_3 = 0x07 + EUnitFields.UnitEnd, // Size:1 - Type: BYTES - Flags: PUBLIC
        PLAYER_DUEL_TEAM = 0x08 + EUnitFields.UnitEnd, // Size:1
        PLAYER_GUILD_TIMESTAMP = 0x09 + EUnitFields.UnitEnd, // Size:1
        PLAYER_QUEST_LOG_1_1 = 0x0A + EUnitFields.UnitEnd, // count = 20
        PLAYER_QUEST_LOG_1_2 = 0x0B + EUnitFields.UnitEnd,
        PLAYER_QUEST_LOG_1_3 = 0x0C + EUnitFields.UnitEnd,
        PLAYER_QUEST_LOG_LAST_1 = 0x43 + EUnitFields.UnitEnd,
        PLAYER_QUEST_LOG_LAST_2 = 0x44 + EUnitFields.UnitEnd,
        PLAYER_QUEST_LOG_LAST_3 = 0x45 + EUnitFields.UnitEnd,
        PLAYER_VISIBLE_ITEM_1_CREATOR = 0x46 + EUnitFields.UnitEnd, // Size:2, count = 19
        PLAYER_VISIBLE_ITEM_1_0 = 0x48 + EUnitFields.UnitEnd, // Size:8
        PLAYER_VISIBLE_ITEM_1_PROPERTIES = 0x50 + EUnitFields.UnitEnd, // Size:1
        PLAYER_VISIBLE_ITEM_1_PAD = 0x51 + EUnitFields.UnitEnd, // Size:1
        PLAYER_VISIBLE_ITEM_LAST_CREATOR = 0x11e + EUnitFields.UnitEnd,
        PLAYER_VISIBLE_ITEM_LAST_0 = 0x120 + EUnitFields.UnitEnd,
        PLAYER_VISIBLE_ITEM_LAST_PROPERTIES = 0x128 + EUnitFields.UnitEnd,
        PLAYER_VISIBLE_ITEM_LAST_PAD = 0x129 + EUnitFields.UnitEnd,
        PLAYER_FIELD_INV_SLOT_HEAD = 0x12a + EUnitFields.UnitEnd, // Size:46
        PLAYER_FIELD_PACK_SLOT_1 = 0x158 + EUnitFields.UnitEnd, // Size:32
        PLAYER_FIELD_PACK_SLOT_LAST = 0x176 + EUnitFields.UnitEnd,
        PLAYER_FIELD_BANK_SLOT_1 = 0x178 + EUnitFields.UnitEnd, // Size:48
        PLAYER_FIELD_BANK_SLOT_LAST = 0x1a6 + EUnitFields.UnitEnd,
        PLAYER_FIELD_BANKBAG_SLOT_1 = 0x1a8 + EUnitFields.UnitEnd, // Size:12
        PLAYER_FIELD_BANKBAG_SLOT_LAST = 0xab2 + EUnitFields.UnitEnd,
        PLAYER_FIELD_VENDORBUYBACK_SLOT_1 = 0x1b4 + EUnitFields.UnitEnd, // Size:24
        PLAYER_FIELD_VENDORBUYBACK_SLOT_LAST = 0x1ca + EUnitFields.UnitEnd,
        PLAYER_FIELD_KEYRING_SLOT_1 = 0x1cc + EUnitFields.UnitEnd, // Size:64
        PLAYER_FIELD_KEYRING_SLOT_LAST = 0x20a + EUnitFields.UnitEnd,
        PLAYER_FARSIGHT = 0x20c + EUnitFields.UnitEnd, // Size:2
        PLAYER_FIELD_COMBO_TARGET = 0x20e + EUnitFields.UnitEnd, // Size:2
        PLAYER_XP = 0x210 + EUnitFields.UnitEnd, // Size:1
        PLAYER_NEXT_LEVEL_XP = 0x211 + EUnitFields.UnitEnd, // Size:1
        PLAYER_SKILL_INFO_1_1 = 0x212 + EUnitFields.UnitEnd, // Size:384
        PLAYER_SKILL_PROP_1_1 = 0x213 + EUnitFields.UnitEnd, // Size:384

        PLAYER_CHARACTER_POINTS1 = 0x392 + EUnitFields.UnitEnd, // Size:1
        PLAYER_CHARACTER_POINTS2 = 0x393 + EUnitFields.UnitEnd, // Size:1
        PLAYER_TRACK_CREATURES = 0x394 + EUnitFields.UnitEnd, // Size:1
        PLAYER_TRACK_RESOURCES = 0x395 + EUnitFields.UnitEnd, // Size:1
        PLAYER_BLOCK_PERCENTAGE = 0x396 + EUnitFields.UnitEnd, // Size:1
        PLAYER_DODGE_PERCENTAGE = 0x397 + EUnitFields.UnitEnd, // Size:1
        PLAYER_PARRY_PERCENTAGE = 0x398 + EUnitFields.UnitEnd, // Size:1
        PLAYER_CRIT_PERCENTAGE = 0x399 + EUnitFields.UnitEnd, // Size:1
        PLAYER_RANGED_CRIT_PERCENTAGE = 0x39a + EUnitFields.UnitEnd, // Size:1
        PLAYER_EXPLORED_ZONES_1 = 0x39b + EUnitFields.UnitEnd, // Size:64
        PLAYER_REST_STATE_EXPERIENCE = 0x3db + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_COINAGE = 0x3dc + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_POSSTAT0 = 0x3dd + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_POSSTAT1 = 0x3de + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_POSSTAT2 = 0x3df + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_POSSTAT3 = 0x3e0 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_POSSTAT4 = 0x3e1 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_NEGSTAT0 = 0x3e2 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_NEGSTAT1 = 0x3e3 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_NEGSTAT2 = 0x3e4 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_NEGSTAT3 = 0x3e5 + EUnitFields.UnitEnd, // Size:1,
        PLAYER_FIELD_NEGSTAT4 = 0x3e6 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_RESISTANCEBUFFMODSPOSITIVE = 0x3e7 + EUnitFields.UnitEnd, // Size:7
        PLAYER_FIELD_RESISTANCEBUFFMODSNEGATIVE = 0x3ee + EUnitFields.UnitEnd, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_POS = 0x3f5 + EUnitFields.UnitEnd, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_NEG = 0x3fc + EUnitFields.UnitEnd, // Size:7
        PLAYER_FIELD_MOD_DAMAGE_DONE_PCT = 0x403 + EUnitFields.UnitEnd, // Size:7
        PLAYER_FIELD_BYTES                       = 0x40a + EUnitFields.UnitEnd, // Size:1
        PLAYER_AMMO_ID                           = 0x40b + EUnitFields.UnitEnd, // Size:1
        PLAYER_SELF_RES_SPELL                    = 0x40c + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_PVP_MEDALS                  = 0x40d + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_BUYBACK_PRICE_1             = 0x40e + EUnitFields.UnitEnd, // count=12
        PLAYER_FIELD_BUYBACK_PRICE_LAST          = 0x419 + EUnitFields.UnitEnd,
        PLAYER_FIELD_BUYBACK_TIMESTAMP_1         = 0x41a + EUnitFields.UnitEnd, // count=12
        PLAYER_FIELD_BUYBACK_TIMESTAMP_LAST      = 0x425 + EUnitFields.UnitEnd,
        PLAYER_FIELD_SESSION_KILLS               = 0x426 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_YESTERDAY_KILLS             = 0x427 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_LAST_WEEK_KILLS             = 0x428 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_THIS_WEEK_KILLS             = 0x429 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_THIS_WEEK_CONTRIBUTION      = 0x42a + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_LIFETIME_HONORABLE_KILLS    = 0x42b + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_LIFETIME_DISHONORABLE_KILLS = 0x42c + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_YESTERDAY_CONTRIBUTION      = 0x42d + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_LAST_WEEK_CONTRIBUTION      = 0x42e + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_LAST_WEEK_RANK              = 0x42f + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_BYTES2                      = 0x430 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_WATCHED_FACTION_INDEX       = 0x431 + EUnitFields.UnitEnd, // Size:1
        PLAYER_FIELD_COMBAT_RATING_1             = 0x432 + EUnitFields.UnitEnd, // Size:20

        PLAYER_END = 0x446 + EUnitFields.UnitEnd,
    }
    
    public enum EUnitFieldsOLDMODAFOCA
    {
        UNIT_FIELD_CHARM = 0x00 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_SUMMON = 0x02 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CHARMEDBY = 0x04 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_SUMMONEDBY = 0x06 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CREATEDBY = 0x08 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_TARGET = 0x0A + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_PERSUADED = 0x0C + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_CHANNEL_OBJECT = 0x0E + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_HEALTH = 0x10 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER1 = 0x11 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER2 = 0x12 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER3 = 0x13 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER4 = 0x14 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER5 = 0x15 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXHEALTH = 0x16 + EObjectFields.OBJECT_END, // Size:1 
        UNIT_FIELD_MAXPOWER1 = 0x17 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER2 = 0x18 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER3 = 0x19 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER4 = 0x1A + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXPOWER5 = 0x1B + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_LEVEL = 0x1C + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_FACTIONTEMPLATE = 0x1D + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_0 = 0x1E + EObjectFields.OBJECT_END, // Size:1
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY = 0x1F + EObjectFields.OBJECT_END, // Size:3
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_01 = 0x20 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_SLOT_DISPLAY_02 = 0x21 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO = 0x22 + EObjectFields.OBJECT_END, // Size:6
        UNIT_VIRTUAL_ITEM_INFO_01 = 0x23 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_02 = 0x24 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_03 = 0x25 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_04 = 0x26 + EObjectFields.OBJECT_END,
        UNIT_VIRTUAL_ITEM_INFO_05 = 0x27 + EObjectFields.OBJECT_END,
        UNIT_FIELD_FLAGS = 0x28 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_AURA = 0x29 + EObjectFields.OBJECT_END, // Size:48
        UNIT_FIELD_AURA_LAST = 0x58 + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS = 0x59 + EObjectFields.OBJECT_END, // Size:6
        UNIT_FIELD_AURAFLAGS_01 = 0x5a + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_02 = 0x5b + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_03 = 0x5c + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_04 = 0x5d + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAFLAGS_05 = 0x5e + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURALEVELS = 0x5f + EObjectFields.OBJECT_END, // Size:12
        UNIT_FIELD_AURALEVELS_LAST = 0x6a + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURAAPPLICATIONS = 0x6b + EObjectFields.OBJECT_END, // Size:12
        UNIT_FIELD_AURAAPPLICATIONS_LAST = 0x76 + EObjectFields.OBJECT_END,
        UNIT_FIELD_AURASTATE = 0x77 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BASEATTACKTIME = 0x78 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_OFFHANDATTACKTIME = 0x79 + EObjectFields.OBJECT_END, // Size:2
        UNIT_FIELD_RANGEDATTACKTIME = 0x7a + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BOUNDINGRADIUS = 0x7b + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_COMBATREACH = 0x7c + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_DISPLAYID = 0x7d + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_NATIVEDISPLAYID = 0x7e + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MOUNTDISPLAYID = 0x7f + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINDAMAGE = 0x80 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXDAMAGE = 0x81 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINOFFHANDDAMAGE = 0x82 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXOFFHANDDAMAGE = 0x83 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_1 = 0x84 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETNUMBER = 0x85 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PET_NAME_TIMESTAMP = 0x86 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETEXPERIENCE = 0x87 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_PETNEXTLEVELEXP = 0x88 + EObjectFields.OBJECT_END, // Size:1
        UNIT_DYNAMIC_FLAGS = 0x89 + EObjectFields.OBJECT_END, // Size:1
        UNIT_CHANNEL_SPELL = 0x8a + EObjectFields.OBJECT_END, // Size:1
        UNIT_MOD_CAST_SPEED = 0x8b + EObjectFields.OBJECT_END, // Size:1
        UNIT_CREATED_BY_SPELL = 0x8c + EObjectFields.OBJECT_END, // Size:1
        UNIT_NPC_FLAGS = 0x8d + EObjectFields.OBJECT_END, // Size:1
        UNIT_NPC_EMOTESTATE = 0x8e + EObjectFields.OBJECT_END, // Size:1
        UNIT_TRAINING_POINTS = 0x8f + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT0 = 0x90 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT1 = 0x91 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT2 = 0x92 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT3 = 0x93 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_STAT4 = 0x94 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RESISTANCES = 0x95 + EObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_RESISTANCES_01 = 0x96 + EObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_02 = 0x97 + EObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_03 = 0x98 + EObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_04 = 0x99 + EObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_05 = 0x9a + EObjectFields.OBJECT_END,
        UNIT_FIELD_RESISTANCES_06 = 0x9b + EObjectFields.OBJECT_END,
        UNIT_FIELD_BASE_MANA = 0x9c + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BASE_HEALTH = 0x9d + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_BYTES_2 = 0x9e + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER = 0x9f + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER_MODS = 0xa0 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_ATTACK_POWER_MULTIPLIER = 0xa1 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER = 0xa2 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER_MODS = 0xa3 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER = 0xa4 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MINRANGEDDAMAGE = 0xa5 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_MAXRANGEDDAMAGE = 0xa6 + EObjectFields.OBJECT_END, // Size:1
        UNIT_FIELD_POWER_COST_MODIFIER = 0xa7 + EObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_POWER_COST_MODIFIER_01 = 0xa8 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_02 = 0xa9 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_03 = 0xaa + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_04 = 0xab + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_05 = 0xac + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MODIFIER_06 = 0xad + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER = 0xae + EObjectFields.OBJECT_END, // Size:7
        UNIT_FIELD_POWER_COST_MULTIPLIER_01 = 0xaf + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_02 = 0xb0 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_03 = 0xb1 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_04 = 0xb2 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_05 = 0xb3 + EObjectFields.OBJECT_END,
        UNIT_FIELD_POWER_COST_MULTIPLIER_06 = 0xb4 + EObjectFields.OBJECT_END,
        UNIT_FIELD_PADDING = 0xb5 + EObjectFields.OBJECT_END,
        UNIT_END = 0xb6 + EObjectFields.OBJECT_END,

        UNIT_FIELD_STRENGTH = UNIT_FIELD_STAT0,
        UNIT_FIELD_AGILITY = UNIT_FIELD_STAT1,
        UNIT_FIELD_STAMINA = UNIT_FIELD_STAT2,
        UNIT_FIELD_SPIRIT = UNIT_FIELD_STAT3,
        UNIT_FIELD_INTELLECT = UNIT_FIELD_STAT4
    }
    */
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
}
