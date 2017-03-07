namespace Common.Globals
{
    //Got them from ItemSubClass.dbc
    public enum ITEM_CLASS : byte
    {
        ITEM_CLASS_CONSUMABLE = 0,
        ITEM_CLASS_CONTAINER = 1,
        ITEM_CLASS_WEAPON = 2,
        ITEM_CLASS_JEWELRY = 3,
        ITEM_CLASS_ARMOR = 4,
        ITEM_CLASS_REAGENT = 5,
        ITEM_CLASS_PROJECTILE = 6,
        ITEM_CLASS_TRADE_GOODS = 7,
        ITEM_CLASS_GENERIC = 8,
        ITEM_CLASS_BOOK = 9,
        ITEM_CLASS_MONEY = 10,
        ITEM_CLASS_QUIVER = 11,
        ITEM_CLASS_QUEST = 12,
        ITEM_CLASS_KEY = 13,
        ITEM_CLASS_PERMANENT = 14,
        ITEM_CLASS_JUNK = 15
    }

    public enum GMTicketSystemStatus
    {
        GMTICKET_SYSTEMSTATUS_ENABLED = 1,
        GMTICKET_SYSTEMSTATUS_DISABLED = 2,
        GMTICKET_SYSTEMSTATUS_SURVEY = 3
    }

    public enum GMTicketCreateResult
    {
        GMTICKET_ALREADY_HAVE = 1,
        GMTICKET_CREATE_OK = 2
    }

    public enum SHEATHE_SLOT : byte
    {
        SHEATHE_NONE = 0,
        SHEATHE_WEAPON = 1,
        SHEATHE_RANGED = 2
    }

    public enum FriendResult : byte
    {
        FRIEND_DB_ERROR = 0x0,
        FRIEND_LIST_FULL = 0x1,
        FRIEND_ONLINE = 0x2,
        FRIEND_OFFLINE = 0x3,
        FRIEND_NOT_FOUND = 0x4,
        FRIEND_REMOVED = 0x5,
        FRIEND_ADDED_ONLINE = 0x6,
        FRIEND_ADDED_OFFLINE = 0x7,
        FRIEND_ALREADY = 0x8,
        FRIEND_SELF = 0x9,
        FRIEND_ENEMY = 0xa,
        FRIEND_IGNORE_FULL = 0xb,
        FRIEND_IGNORE_SELF = 0xc,
        FRIEND_IGNORE_NOT_FOUND = 0xd,
        FRIEND_IGNORE_ALREADY = 0xe,
        FRIEND_IGNORE_ADDED = 0xf,
        FRIEND_IGNORE_REMOVED = 0x10
    }

    public enum ChatMsgs
    {
        CHAT_MSG_SAY = 0x00,
        CHAT_MSG_PARTY = 0x01,
        CHAT_MSG_RAID = 0x02,
        CHAT_MSG_GUILD = 0x03,
        CHAT_MSG_OFFICER = 0x04,
        CHAT_MSG_YELL = 0x05,
        CHAT_MSG_WHISPER = 0x06,
        CHAT_MSG_WHISPER_INFORM = 0x07,
        CHAT_MSG_EMOTE = 0x08,
        CHAT_MSG_TEXT_EMOTE = 0x09,
        CHAT_MSG_SYSTEM = 0x0A,
        CHAT_MSG_MONSTER_SAY = 0x0B,
        CHAT_MSG_MONSTER_YELL = 0x0C,
        CHAT_MSG_MONSTER_EMOTE = 0x0D,
        CHAT_MSG_CHANNEL = 0x0E,
        CHAT_MSG_CHANNEL_JOIN = 0x0F,
        CHAT_MSG_CHANNEL_LEAVE = 0x10,
        CHAT_MSG_CHANNEL_LIST = 0x11,
        CHAT_MSG_CHANNEL_NOTICE = 0x12,
        CHAT_MSG_CHANNEL_NOTICE_USER = 0x13,
        CHAT_MSG_AFK = 0x14,
        CHAT_MSG_DND = 0x15,
        CHAT_MSG_IGNORED = 0x16,
        CHAT_MSG_SKILL = 0x17,
        CHAT_MSG_LOOT = 0x18,
        CHAT_MSG_BG_SYSTEM_NEUTRAL = 0x52,
        CHAT_MSG_BG_SYSTEM_ALLIANCE = 0x53,
        CHAT_MSG_BG_SYSTEM_HORDE = 0x54,
        CHAT_MSG_RAID_LEADER = 0x57,
        CHAT_MSG_RAID_WARNING = 0x58,
        CHAT_MSG_BATTLEGROUND = 0x5C,
        CHAT_MSG_BATTLEGROUND_LEADER = 0x5D,
        CHAT_MSG_REPLY = 0x09,
        CHAT_MSG_MONSTER_PARTY = 0x30, // 0x0D, just selected some free random value for avoid duplicates with really existed values
        CHAT_MSG_MONSTER_WHISPER = 0x31, // 0x0F, just selected some free random value for avoid duplicates with really existed values
        // CHAT_MSG_MONEY                  = 0x1C,
        // CHAT_MSG_OPENING                = 0x1D,
        // CHAT_MSG_TRADESKILLS            = 0x1E,
        // CHAT_MSG_PET_INFO               = 0x1F,
        // CHAT_MSG_COMBAT_MISC_INFO       = 0x20,
        // CHAT_MSG_COMBAT_XP_GAIN         = 0x21,
        // CHAT_MSG_COMBAT_HONOR_GAIN      = 0x22,
        // CHAT_MSG_COMBAT_FACTION_CHANGE  = 0x23,
        CHAT_MSG_RAID_BOSS_WHISPER = 0x29,
        CHAT_MSG_RAID_BOSS_EMOTE = 0x2A
        // CHAT_MSG_FILTERED               = 0x2B,
        // CHAT_MSG_RESTRICTED             = 0x2E,
    }

    public enum ChatMessageType : byte
    {
        CHAT_MSG_SAY = 0x00,
        CHAT_MSG_PARTY = 0x01,
        CHAT_MSG_RAID = 0x02,
        CHAT_MSG_GUILD = 0x03,
        CHAT_MSG_OFFICER = 0x04,
        CHAT_MSG_YELL = 0x05,
        CHAT_MSG_WHISPER = 0x06,
        CHAT_MSG_WHISPER_INFORM = 0x07,
        CHAT_MSG_EMOTE = 0x08,
        CHAT_MSG_TEXT_EMOTE = 0x09,
        CHAT_MSG_SYSTEM = 0x0A,
        CHAT_MSG_MONSTER_SAY = 0x0B,
        CHAT_MSG_MONSTER_YELL = 0x0C,
        CHAT_MSG_MONSTER_EMOTE = 0x0D,
        CHAT_MSG_CHANNEL = 0x0E,
        CHAT_MSG_CHANNEL_JOIN = 0x0F,
        CHAT_MSG_CHANNEL_LEAVE = 0x10,
        CHAT_MSG_CHANNEL_LIST = 0x11,
        CHAT_MSG_CHANNEL_NOTICE = 0x12,
        CHAT_MSG_CHANNEL_NOTICE_USER = 0x13,
        CHAT_MSG_AFK = 0x14,
        CHAT_MSG_DND = 0x15,
        CHAT_MSG_IGNORED = 0x16,
        CHAT_MSG_SKILL = 0x17,
        CHAT_MSG_LOOT = 0x18,
        CHAT_MSG_BG_SYSTEM_NEUTRAL = 0x52,
        CHAT_MSG_BG_SYSTEM_ALLIANCE = 0x53,
        CHAT_MSG_BG_SYSTEM_HORDE = 0x54,
        CHAT_MSG_RAID_LEADER = 0x57,
        CHAT_MSG_RAID_WARNING = 0x58,
        CHAT_MSG_BATTLEGROUND = 0x5C,
        CHAT_MSG_BATTLEGROUND_LEADER = 0x5D,
        CHAT_MSG_REPLY = 0x09,
        CHAT_MSG_MONSTER_PARTY = 0x30, // 0x0D, just selected some free random value for avoid duplicates with really existed values
        CHAT_MSG_MONSTER_WHISPER = 0x31, // 0x0F, just selected some free random value for avoid duplicates with really existed values
        // CHAT_MSG_MONEY                  = 0x1C,
        // CHAT_MSG_OPENING                = 0x1D,
        // CHAT_MSG_TRADESKILLS            = 0x1E,
        // CHAT_MSG_PET_INFO               = 0x1F,
        // CHAT_MSG_COMBAT_MISC_INFO       = 0x20,
        // CHAT_MSG_COMBAT_XP_GAIN         = 0x21,
        // CHAT_MSG_COMBAT_HONOR_GAIN      = 0x22,
        // CHAT_MSG_COMBAT_FACTION_CHANGE  = 0x23,
        CHAT_MSG_RAID_BOSS_WHISPER = 0x29,
        CHAT_MSG_RAID_BOSS_EMOTE = 0x2A
        // CHAT_MSG_FILTERED               = 0x2B,
        // CHAT_MSG_RESTRICTED             = 0x2E,
    }

    public enum ChatMessageLanguage : byte
    {
        LANG_UNIVERSAL = 0,
        LANG_ORCISH = 1,
        LANG_DARNASSIAN = 2,
        LANG_TAURAHE = 3,
        LANG_DWARVISH = 6,
        LANG_COMMON = 7,
        LANG_DEMONIC = 8,
        LANG_TITAN = 9,
        LANG_THALASSIAN = 10,
        LANG_DRACONIC = 11,
        LANG_KALIMAG = 12,
        LANG_GNOMISH = 13,
        LANG_TROLL = 14,
        LANG_GUTTERSPEAK = 33
    }

    public enum TradeStatus : byte
    {
        TRADE_TARGET_UNAVIABLE = 0,     // "[NAME] is busy"
        TRADE_STATUS_OK = 1,            // BEGIN TRADE
        TRADE_TRADE_WINDOW_OPEN = 2,    // OPEN TRADE WINDOW
        TRADE_STATUS_CANCELED = 3,      // "Trade canceled"
        TRADE_STATUS_COMPLETE = 4,      // TRADE COMPLETE
        TRADE_TARGET_UNAVIABLE2 = 5,    // "[NAME] is busy"
        TRADE_TARGET_MISSING = 6,       // SOUND: I dont have a target
        TRADE_STATUS_UNACCEPT = 7,      // BACK TRADE
        TRADE_COMPLETE = 8,             // "Trade Complete"
        TRADE_UNK2 = 9,
        TRADE_TARGET_TOO_FAR = 10,      // "Trade target is too far away"
        TRADE_TARGET_DIFF_FACTION = 11, // "Trade is not party of your alliance"
        TRADE_TRADE_WINDOW_CLOSE = 12,  // CLOSE TRADE WINDOW
        TRADE_UNK3 = 13,
        TRADE_TARGET_IGNORING = 14,     // "[NAME] is ignoring you"
        TRADE_STUNNED = 15,             // "You are stunned"
        TRADE_TARGET_STUNNED = 16,      // "Target is stunned"
        TRADE_DEAD = 17,                // "You cannot do that when you are dead"
        TRADE_TARGET_DEAD = 18,         // "You cannot trade with dead players"
        TRADE_LOGOUT = 19,              // "You are loging out"
        TRADE_TARGET_LOGOUT = 20,       // "The player is loging out"
        TRADE_TRIAL_ACCOUNT = 21,       // "Trial accounts cannot perform that action"
        TRADE_STATUS_ONLY_CONJURED = 22 // "You can only trade conjured items... (cross realm BG related)."
    }

    public enum ChatChannelNotify
    {
        CHAT_JOINED_NOTICE = 0x00, //+ "%s joined channel.";
        CHAT_LEFT_NOTICE = 0x01, //+ "%s left channel.";
        // CHAT_SUSPENDED_NOTICE             = 0x01,            // "%s left channel.";
        CHAT_YOU_JOINED_NOTICE = 0x02, //+ "Joined Channel: [%s]"; -- You joined
        // CHAT_YOU_CHANGED_NOTICE           = 0x02,            // "Changed Channel: [%s]";
        CHAT_YOU_LEFT_NOTICE = 0x03, //+ "Left Channel: [%s]"; -- You left
        CHAT_WRONG_PASSWORD_NOTICE = 0x04, //+ "Wrong password for %s.";
        CHAT_NOT_MEMBER_NOTICE = 0x05, //+ "Not on channel %s.";
        CHAT_NOT_MODERATOR_NOTICE = 0x06, //+ "Not a moderator of %s.";
        CHAT_PASSWORD_CHANGED_NOTICE = 0x07, //+ "[%s] Password changed by %s.";
        CHAT_OWNER_CHANGED_NOTICE = 0x08, //+ "[%s] Owner changed to %s.";
        CHAT_PLAYER_NOT_FOUND_NOTICE = 0x09, //+ "[%s] Player %s was not found.";
        CHAT_NOT_OWNER_NOTICE = 0x0A, //+ "[%s] You are not the channel owner.";
        CHAT_CHANNEL_OWNER_NOTICE = 0x0B, //+ "[%s] Channel owner is %s.";
        CHAT_MODE_CHANGE_NOTICE = 0x0C, //?
        CHAT_ANNOUNCEMENTS_ON_NOTICE = 0x0D, //+ "[%s] Channel announcements enabled by %s.";
        CHAT_ANNOUNCEMENTS_OFF_NOTICE = 0x0E, //+ "[%s] Channel announcements disabled by %s.";
        CHAT_MODERATION_ON_NOTICE = 0x0F, //+ "[%s] Channel moderation enabled by %s.";
        CHAT_MODERATION_OFF_NOTICE = 0x10, //+ "[%s] Channel moderation disabled by %s.";
        CHAT_MUTED_NOTICE = 0x11, //+ "[%s] You do not have permission to speak.";
        CHAT_PLAYER_KICKED_NOTICE = 0x12, //? "[%s] Player %s kicked by %s.";
        CHAT_BANNED_NOTICE = 0x13, //+ "[%s] You are banned from that channel.";
        CHAT_PLAYER_BANNED_NOTICE = 0x14, //? "[%s] Player %s banned by %s.";
        CHAT_PLAYER_UNBANNED_NOTICE = 0x15, //? "[%s] Player %s unbanned by %s.";
        CHAT_PLAYER_NOT_BANNED_NOTICE = 0x16, //+ "[%s] Player %s is not banned.";
        CHAT_PLAYER_ALREADY_MEMBER_NOTICE = 0x17, //+ "[%s] Player %s is already on the channel.";
        CHAT_INVITE_NOTICE = 0x18, //+ "%2$s has invited you to join the channel '%1$s'.";
        CHAT_INVITE_WRONG_FACTION_NOTICE = 0x19, //+ "Target is in the wrong alliance for %s.";
        CHAT_WRONG_FACTION_NOTICE = 0x1A, //+ "Wrong alliance for %s.";
        CHAT_INVALID_NAME_NOTICE = 0x1B, //+ "Invalid channel name";
        CHAT_NOT_MODERATED_NOTICE = 0x1C, //+ "%s is not moderated";
        CHAT_PLAYER_INVITED_NOTICE = 0x1D, //+ "[%s] You invited %s to join the channel";
        CHAT_PLAYER_INVITE_BANNED_NOTICE = 0x1E, //+ "[%s] %s has been banned.";
        CHAT_THROTTLED_NOTICE = 0x1F
        //+ "[%s] The number of messages that can be sent to this channel is limited, please wait to send another message.";
    }

    public enum CharacterFlagState
    {
        CharacterFlagNone = 0x0,
        CharacterFlagUnk1 = 0x1,
        CharacterFlagUnk2 = 0x2,
        CharacterFlagLockedForTransfer = 0x4, //Character Locked for Paid Character Transfer
        CharacterFlagUnk4 = 0x8,
        CharacterFlagUnk5 = 0x10,
        CharacterFlagUnk6 = 0x20,
        CharacterFlagUnk7 = 0x40,
        CharacterFlagUnk8 = 0x80,
        CharacterFlagUnk9 = 0x100,
        CharacterFlagUnk10 = 0x200,
        CharacterFlagHideHelm = 0x400,
        CharacterFlagHideCloak = 0x800,
        CharacterFlagUnk13 = 0x1000,
        CharacterFlagGhost = 0x2000, //Player is ghost in char selection screen
        CharacterFlagRename = 0x4000, //On login player will be asked to change name
        CharacterFlagUnk16 = 0x8000,
        CharacterFlagUnk17 = 0x10000,
        CharacterFlagUnk18 = 0x20000,
        CharacterFlagUnk19 = 0x40000,
        CharacterFlagUnk20 = 0x80000,
        CharacterFlagUnk21 = 0x100000,
        CharacterFlagUnk22 = 0x200000,
        CharacterFlagUnk23 = 0x400000,
        CharacterFlagUnk24 = 0x800000,
        CharacterFlagLockedByBilling = 0x1000000,
        CharacterFlagDeclined = 0x2000000,
        CharacterFlagUnk27 = 0x4000000,
        CharacterFlagUnk28 = 0x8000000,
        CharacterFlagUnk29 = 0x10000000,
        CharacterFlagUnk30 = 0x20000000,
        CharacterFlagUnk31 = 0x40000000
        //CHARACTER_FLAG_UNK32 = 0x80000000,
    }
}
