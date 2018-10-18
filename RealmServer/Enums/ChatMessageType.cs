namespace RealmServer.Enums
{
    public enum ChatMessageType : byte
    {
        Say = 0x00,
        Party = 0x01,
        Raid = 0x02,
        Guild = 0x03,
        Officer = 0x04,
        Yell = 0x05,
        Whisper = 0x06,
        WhisperInform = 0x07,
        Emote = 0x08,
        TextEmote = 0x09,
        System = 0x0A,
        MonsterSay = 0x0B,
        MonsterYell = 0x0C,
        MonsterEmote = 0x0D,
        Channel = 0x0E,
        ChannelJoin = 0x0F,
        ChannelLeave = 0x10,
        ChannelList = 0x11,
        ChannelNotice = 0x12,
        ChannelNoticeUser = 0x13,
        Afk = 0x14,
        Dnd = 0x15,
        Ignored = 0x16,
        Skill = 0x17,
        Loot = 0x18,
        BgSystemNeutral = 0x52,
        BgSystemAlliance = 0x53,
        BgSystemHorde = 0x54,
        RaidLeader = 0x57,
        RaidWarning = 0x58,
        Battleground = 0x5C,
        BattlegroundLeader = 0x5D,
        Reply = 0x09,

        MonsterParty = 0x30, // 0x0D, 

        MonsterWhisper = 0x31, // 0x0F

        // MONEY                  = 0x1C,
        // OPENING                = 0x1D,
        // TRADESKILLS            = 0x1E,
        // PET_INFO               = 0x1F,
        // COMBAT_MISC_INFO       = 0x20,
        // COMBAT_XP_GAIN         = 0x21,
        // COMBAT_HONOR_GAIN      = 0x22,
        // COMBAT_FACTION_CHANGE  = 0x23,
        RaidBossWhisper = 0x29,

        RaidBossEmote = 0x2A
        // FILTERED               = 0x2B,
        // RESTRICTED             = 0x2E,
    }
}