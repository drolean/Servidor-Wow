namespace Common.Globals
{
    public enum AuthCMD : byte
    {
        CMD_AUTH_LOGON_CHALLENGE     = 0x0,
        CMD_AUTH_LOGON_PROOF         = 0x1,
        CMD_AUTH_RECONNECT_CHALLENGE = 0x2,
        CMD_AUTH_RECONNECT_PROOF     = 0x3,
        CMD_AUTH_AUTHENTIFICATOR     = 0x04, // ???CMD_AUTH_UPDATESRV 
        CMD_AUTH_REALMLIST           = 0x10,
        CMD_XFER_INITIATE            = 0x30,
        CMD_XFER_DATA                = 0x31,
        CMD_XFER_ACCEPT              = 0x32,
        CMD_XFER_RESUME              = 0x33,
        CMD_XFER_CANCEL              = 0x34,
        UNKNOW                       = byte.MaxValue,
    }

    public enum AccountState : byte
    {
        LOGIN_OK = 0x0,
        LOGIN_FAILED = 0x1,          // Unable to connect
        LOGIN_BANNED = 0x3,          // This World of Warcraft account has been closed and is no longer in service -- Please check the registered email address of this account for further information.
        LOGIN_UNKNOWN_ACCOUNT = 0x4, // The information you have entered is not valid.  Please check the spelling of the account name and password.  If you need help in retrieving a lost or stolen password and account, see www.worldofwarcraft.com for more information.
        LOGIN_BAD_PASS = 0x5,        // The information you have entered is not valid.  Please check the spelling of the account name and password.  If you need help in retrieving a lost or stolen password and account, see www.worldofwarcraft.com for more information.
        LOGIN_ALREADYONLINE = 0x6,   // This account is already logged into World of Warcraft.  Please check the spelling and try again.
        LOGIN_NOTIME = 0x7,          // You have used up your prepaid time for this account. Please purchase more to continue playing.
        LOGIN_DBBUSY = 0x8,          // Could not log in to World of Warcraft at this time.  Please try again later.
        LOGIN_BADVERSION = 0x9,      // Unable to validate game version.  This may be caused by file corruption or the interference of another program.  Please visit www.blizzard.com/support/wow/ for more information and possible solutions to this issue.
        LOGIN_DOWNLOADFILE = 0xa,
        LOGIN_SUSPENDED = 0xc,       // This World Of Warcraft account has been temporarily suspended. Please go to http://www.wow-europe.com/en/misc/banned.html for further information.
        LOGIN_PARENTALCONTROL = 0xf  // Access to this account has been blocked by parental controls.  Your settings may be changed in your account preferences at http://www.worldofwarcraft.com.
    }

}
