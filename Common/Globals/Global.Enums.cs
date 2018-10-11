namespace Common.Globals
{
    public enum LogoutResponseCode : byte
    {
        LOGOUT_RESPONSE_ACCEPTED = 0x0,
        LOGOUT_RESPONSE_DENIED = 0xc
    }

    public enum BillingPlanFlags
    {
        SESSION_NONE = 0x00,
        SESSION_UNUSED = 0x01,
        SESSION_RECURRING_BILL = 0x02,
        SESSION_FREE_TRIAL = 0x04,
        SESSION_IGR = 0x08,
        SESSION_USAGE = 0x10,
        SESSION_TIME_MIXTURE = 0x20,
        SESSION_RESTRICTED = 0x40,
        SESSION_ENABLE_CAIS = 0x80
    }

    /// <summary>
    ///     Enumeration of all authentication operations.
    /// </summary>
    public enum AuthCMD : byte
    {
        /// <summary>
        ///     Initial authentication step, the client sent a challenge.
        ///     - Authentication Logon Challenge
        /// </summary>
        CMD_AUTH_LOGON_CHALLENGE = 0x00,

        /// <summary>
        ///     Second authentication step, the client is sending his proof.
        ///     - Authentication Logon Proof
        /// </summary>
        CMD_AUTH_LOGON_PROOF = 0x01,
        CMD_AUTH_RECONNECT_CHALLENGE = 0x02,
        CMD_AUTH_RECONNECT_PROOF = 0x03,
        CMD_AUTH_AUTHENTIFICATOR = 0x04, // ???CMD_AUTH_UPDATESRV 

        /// <summary>
        ///     Third authentication step, the client is requesting the realm list.
        ///     - Realm List
        /// </summary>
        CMD_AUTH_REALMLIST = 0x10,

        /// <summary>
        ///     Transfer Initiate.
        /// </summary>
        CMD_XFER_INITIATE = 0x30,

        /// <summary>
        ///     Transfer Data.
        /// </summary>
        CMD_XFER_DATA = 0x31,

        /// <summary>
        ///     Transfer Accept.
        /// </summary>
        CMD_XFER_ACCEPT = 0x32,

        /// <summary>
        ///     Transfer Resume.
        /// </summary>
        CMD_XFER_RESUME = 0x33,

        /// <summary>
        ///     Transfer Cancel.
        /// </summary>
        CMD_XFER_CANCEL = 0x34,
        UNKNOW = byte.MaxValue
    }

    public enum AccountState : byte
    {
        OK = 0x00,

        /// <summary>
        ///     Unable to connect
        /// </summary>
        FAILED = 0x01,

        /// <summary>
        ///     This World of Warcraft account has been closed and is no longer in service -- Please check the registered email
        ///     address of this account for further information.
        /// </summary>
        BANNED = 0x03,

        /// <summary>
        ///     The information you have entered is not valid.  Please check the spelling of the account name and password.  If you
        ///     need help in retrieving a lost or stolen password and account, see www.worldofwarcraft.com for more information.
        /// </summary>
        UNKNOWN_ACCOUNT = 0x04,

        /// <summary>
        ///     The information you have entered is not valid.  Please check the spelling of the account name and password.  If you
        ///     need help in retrieving a lost or stolen password and account, see www.worldofwarcraft.com for more information.
        /// </summary>
        BAD_PASS = 0x05,

        /// <summary>
        ///     This account is already logged into World of Warcraft.  Please check the spelling and try again.
        /// </summary>
        ALREADYONLINE = 0x06,

        /// <summary>
        ///     You have used up your prepaid time for this account. Please purchase more to continue playing.
        /// </summary>
        NOTIME = 0x07,

        /// <summary>
        ///     Could not log in to World of Warcraft at this time.  Please try again later.
        /// </summary>
        DBBUSY = 0x08,

        /// <summary>
        ///     Unable to validate game version.  This may be caused by file corruption or the interference of another program.
        ///     Please visit www.blizzard.com/support/wow/ for more information and possible solutions to this issue.
        /// </summary>
        BADVERSION = 0x09,

        DOWNLOADFILE = 0x0A,

        /// <summary>
        ///     This World Of Warcraft account has been temporarily suspended. Please go to
        ///     https://worldofwarcraft.com/en/misc/banned.html for further information.
        /// </summary>
        SUSPENDED = 0x0C,

        /// <summary>
        ///     Access to this account has been blocked by parental controls.  Your settings may be changed in your account
        ///     preferences at http://www.worldofwarcraft.com.
        /// </summary>
        PARENTALCONTROL = 0x0F
    }

    public enum LoginErrorCode : byte
    {
        RESPONSE_SUCCESS = 0x00, // Success
        RESPONSE_FAILURE = 0x01, // Failure
        RESPONSE_CANCELLED = 0x02, // Canceled
        RESPONSE_DISCONNECTED = 0x03, // Disconnect from server
        RESPONSE_FAILED_TO_CONNECT = 0x04, // Failed to connect
        RESPONSE_CONNECTED = 0x05, // Connected
        RESPONSE_VERSION_MISMATCH = 0x06, // Wrong client version

        CSTATUS_CONNECTING = 0x07, // Connecting to server
        CSTATUS_NEGOTIATING_SECURITY = 0x08, // Negotiating security
        CSTATUS_NEGOTIATION_COMPLETE = 0x09, // Negotiating security complete
        CSTATUS_NEGOTIATION_FAILED = 0x0A, // Negotiating security failed	

        AUTH_REJECT = 0x0B, // Login unavailable - Please contact Tech Support

        /// <summary>
        ///     Authentication successful
        ///     - Used on SMSG_AUTH_RESPONSE
        /// </summary>
        AUTH_OK = 0x0C,

        /// <summary>
        ///     Authentication failed
        ///     - Used on SMSG_AUTH_RESPONSE
        /// </summary>
        AUTH_FAILED = 0x0E,

        AUTH_BAD_SERVER_PROOF = 0x0F, // Server is not valid
        AUTH_UNAVAILABLE = 0x10, // System unavailable 
        AUTH_SYSTEM_ERROR = 0x11, // System error
        AUTH_BILLING_ERROR = 0x12, // Billing system error = 18
        AUTH_BILLING_EXPIRED = 0x13, // Account billing has expired
        AUTH_VERSION_MISMATCH = 0x14, // Wrong client version
        AUTH_UNKNOWN_ACCOUNT = 0x15, // Unknown account
        AUTH_INCORRECT_PASSWORD = 0x16, // Incorrect password
        AUTH_SESSION_EXPIRED = 0x17, // Session expired
        AUTH_SERVER_SHUTTING_DOWN = 0x18, // Server Shutting Down
        AUTH_ALREADY_LOGGING_IN = 0x19, // Already logged in
        AUTH_LOGIN_SERVER_NOT_FOUND = 0x1A, // Invalid login server

        /// <summary>
        ///     Position in Queue
        ///     - Used on SMSG_AUTH_RESPONSE
        /// </summary>
        AUTH_WAIT_QUEUE = 0x1B,

        AUTH_BANNED = 0x1C, // This account has been banned
        AUTH_ALREADY_ONLINE = 0x1D, // This character is still logged on
        AUTH_NO_TIME = 0x1E, // Your WoW subscription has expired
        AUTH_DB_BUSY = 0x1F, // This session has timed out
        AUTH_SUSPENDED = 0x20, // This account has been temporarily suspended
        AUTH_PARENTAL_CONTROL = 0x21, // Access to this account blocked by parental controls 

        REALM_LIST_IN_PROGRESS = 0x22, // Retrieving realmlist
        REALM_LIST_SUCCESS = 0x23, // Realmlist retrieved
        REALM_LIST_FAILED = 0x24, // Unable to connect to realmlist server
        REALM_LIST_INVALID = 0x25, // Invalid realmlist
        REALM_LIST_REALM_NOT_FOUND = 0x26, // The game server is currently down

        ACCOUNT_CREATE_IN_PROGRESS = 0x27, // Creating account
        ACCOUNT_CREATE_SUCCESS = 0x28, // Account created
        ACCOUNT_CREATE_FAILED = 0x29, // Account creation failed

        CHAR_LIST_RETRIEVING = 0x2A, // Retrieving character list
        CHAR_LIST_RETRIEVED = 0x2B, // Character list retrieved
        CHAR_LIST_FAILED = 0x2C, // Error retrieving character list

        CHAR_CREATE_IN_PROGRESS = 0x2D, // Creating character
        CHAR_CREATE_SUCCESS = 0x2E, // Character created [OK Pass]
        CHAR_CREATE_ERROR = 0x2F, // Error creating character
        CHAR_CREATE_FAILED = 0x30, // Character creation failed
        CHAR_CREATE_NAME_IN_USE = 0x31, // That name is unavailable
        CHAR_CREATE_DISABLED = 0x32, // Creation of that race/class is disabled
        CHAR_CREATE_PVP_TEAMS_VIOLATION = 0x33, // You cannot have both horde and alliance character at pvp realm
        CHAR_CREATE_SERVER_LIMIT = 0x34, // You already have the maximum number of characters allowed on this realm
        CHAR_CREATE_ACCOUNT_LIMIT = 0x35, // You already have the maximum number of characters allowed on this account.
        CHAR_CREATE_SERVER_QUEUE = 0x36, // The server is currently queued
        CHAR_CREATE_ONLY_EXISTING = 0x37, // Only players who have characters on this realm..

        CHAR_DELETE_IN_PROGRESS = 0x38, // Deleting character
        CHAR_DELETE_SUCCESS = 0x39, // Character deleted
        CHAR_DELETE_FAILED = 0x3A, // Char deletion failed

        CHAR_DELETE_FAILED_LOCKED_FOR_TRANSFER =
            0x3B, // your char is current lock as part of the paid chart transfer process

        CHAR_LOGIN_IN_PROGRESS = 0x3C, // entering the world of warcraft
        CHAR_LOGIN_SUCCESS = 0x3D, // login succesful
        CHAR_LOGIN_NO_WORLD = 0x3E, // world server is down
        CHAR_LOGIN_NAME_ALREADY_EXISTS = 0x3F, // char with that name already exists
        CHAR_LOGIN_INSTANCE_INAVAILABLE = 0x40, // no instance server are available
        CHAR_LOGIN_FAILED = 0x41, // login failed
        CHAR_LOGIN_DISABLED = 0x42, // login for that race, class or char is currently disabled.
        CHAR_LOGIN_NOCHAR = 0x43, // char not found
        CHAR_LOGIN_CHAR_LOCKED = 0x44, // your char is current lock as part of the paid chart transfer process      

        CHAR_NAME_ENTER = 0x45, // Enter a name for your character
        CHAR_NAME_TOO_SHORT = 0x46, // Names must be atleast 2 characters long
        CHAR_NAME_TOO_LONG = 0x47, // Names must be no more then 12 characters
        CHAR_NAME_ONLY_LETTERS = 0x48, // Names can only contain letters
        CHAR_NAME_MIXED_LANGUAGES = 0x49, // Names must contain only one language
        CHAR_NAME_PROFANE = 0x4A, // That name contains profanity
        CHAR_NAME_RESERVED = 0x4B, // That name is unavailable
        CHAR_NAME_MULTIPLE_APOSTROPHES = 0x4C, // You cannot use an apostrophe as the first or last char of your name
        CHAR_NAME_APOSTROPHES = 0x4D, // You can only have one apostrophe
        CHAR_NAME_THREE_CONSECUTIVE = 0x4E, // You cannot use the same letter three times consecutively
        CHAR_NAME_INVALID_SPACE = 0x4F, // You cannot use space as the first or last character of your name
        CHAR_NAME_FAILURE = 0x51 // Invalid character name
    }

    public enum RealmType : byte
    {
        Normal = 0x00,
        PVP = 0x01,
        RP = 0x06,
        RPPVP = 0x08
    }

    public enum RealmFlag : byte
    {
        Low = 0x00,
        Invalid = 0x01, // Red
        Offline = 0x02, // Red
        Recommended = 0x20,
        NewPlayers = 0x40,
        Full = 0x80
    }

    public enum RealmTimezone : byte
    {
        AnyLocale = 0x00,
        UnitedStates = 0x01,
        Development = 0x02,
        Tournament = 0x05,
        LatinAmerica = 0x04,
        TestServer = 0x1A,
        QAServer = 0x1C
    }

    public enum Genders
    {
        Male = 0,
        Female = 1
    }

    public enum Classes
    {
        Warrior = 1,
        Paladin = 2,
        Hunter = 3,
        Rogue = 4,
        Priest = 5,
        Shaman = 7,
        Mage = 8,
        Warlock = 9,
        Druid = 11
    }

    public enum Races
    {
        Human = 1,
        Orc = 2,
        Dwarf = 3,
        NightElf = 4,
        Undead = 5,
        Tauren = 6,
        Gnome = 7,
        Troll = 8
    }

    public enum CharacterFlag
    {
        None = 0x0,

        /// <summary>
        ///     Character Locked for Paid Character Transfer
        /// </summary>
        LockedForTransfer = 0x4,

        HideHelm = 0x400,

        HideCloak = 0x800,

        /// <summary>
        ///     Player is ghost in char selection screen
        /// </summary>
        Ghost = 0x2000,

        /// <summary>
        ///     On login player will be asked to change name
        /// </summary>
        Rename = 0x4000,

        LockedByBilling = 0x1000000,

        Declined = 0x2000000
    }
}