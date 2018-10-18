namespace RealmServer.Enums
{
    public enum TradeStatus : byte
    {
        /// <summary>
        /// [NAME] is busy
        /// </summary>
        TargetUnaviable = 0,

        /// <summary>
        /// BEGIN TRADE
        /// </summary>
        StatusOk = 1,

        /// <summary>
        /// OPEM TRADE WINDOW
        /// </summary>
        WindowOpen = 2,

        /// <summary>
        /// Trade canceled
        /// </summary>
        StatusCanceled = 3,

        /// <summary>
        /// TRADE COMPLETE
        /// </summary>
        StatusComplete = 4,

        /// <summary>
        /// [NAME] is busy
        /// </summary>
        TargetUnaviable2 = 5,

        /// <summary>
        /// SOUND: I dont have a target
        /// </summary>
        TargetMissing = 6,

        /// <summary>
        /// BACK TRADE
        /// </summary>
        StatusUnaccept = 7,

        /// <summary>
        /// Trade Complete
        /// </summary>
        Complete = 8,

        /// <summary>
        /// UNKNOW***
        /// </summary>
        Unk2 = 9,

        /// <summary>
        /// Trade target is too far away
        /// </summary>
        TargetTooFar = 10,

        /// <summary>
        /// Trade is not party of your alliance
        /// </summary>
        TargetDiffFaction = 11,

        /// <summary>
        /// CLOSE TRADE WINDOW
        /// </summary>
        WindowClose = 12,

        /// <summary>
        /// UNKNOW****
        /// </summary>
        Unk3 = 13,

        /// <summary>
        /// [NAME] is ignoring you
        /// </summary>
        TargetIgnoring = 14,

        /// <summary>
        /// You are stunned
        /// </summary>
        Stunned = 15,

        /// <summary>
        /// Target is stunned
        /// </summary>
        TargetStunned = 16,

        /// <summary>
        /// You cannot do that when you are dead
        /// </summary>
        Dead = 17,

        /// <summary>
        /// You cannot trade with dead players
        /// </summary>
        TargetDead = 18,

        /// <summary>
        /// You are loging out
        /// </summary>
        Logout = 19,

        /// <summary>
        /// The player is loging out
        /// </summary>
        TargetLogout = 20,

        /// <summary>
        /// Trial accounts cannot perform that action
        /// </summary>
        TrialAccount = 21,

        /// <summary>
        /// You can only trade conjured items... (cross realm BG related).
        /// </summary>
        StatusOnlyConjured = 22
    }
}