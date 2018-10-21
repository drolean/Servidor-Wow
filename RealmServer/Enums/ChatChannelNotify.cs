namespace RealmServer.Enums
{
    public enum ChatChannelNotify
    {
        /// <summary>
        ///     %s joined channel.
        /// </summary>
        JoinedNotice = 0x00,

        /// <summary>
        ///     %s left channel.
        /// </summary>
        LeftNotice = 0x01,

        /// <summary>
        ///     "Joined Channel: [%s]"; -- You joined
        /// </summary>
        YouJoinedNotice = 0x02,

        /// <summary>
        ///     "Left Channel: [%s]"; -- You left
        /// </summary>
        YouLeftNotice = 0x03,

        /// <summary>
        ///     Wrong password for %s.
        /// </summary>
        WrongPasswordNotice = 0x04,

        /// <summary>
        ///     Not on channel %s.
        /// </summary>
        NotMemberNotice = 0x05,

        /// <summary>
        ///     Not a moderator of %s.
        /// </summary>
        NotModeratorNotice = 0x06,

        /// <summary>
        ///     [%s] Password changed by %s.
        /// </summary>
        PasswordChangedNotice = 0x07,

        /// <summary>
        ///     [%s] Owner changed to %s.
        /// </summary>
        OwnerChangedNotice = 0x08,

        /// <summary>
        ///     [%s] Player %s was not found.
        /// </summary>
        PlayerNotFoundNotice = 0x09,

        /// <summary>
        ///     [%s] You are not the channel owner.
        /// </summary>
        NotOwnerNotice = 0x0A,

        /// <summary>
        ///     [%s] Channel owner is %s.
        /// </summary>
        ChannelOwnerNotice = 0x0B,

        MODE_CHANGE_NOTICE = 0x0C,

        /// <summary>
        ///     [%s] Channel announcements enabled by %s.
        /// </summary>
        AnnouncementsOnNotice = 0x0D,

        /// <summary>
        ///     [%s] Channel announcements disabled by %s.
        /// </summary>
        AnnouncementsOffNotice = 0x0E,

        /// <summary>
        ///     [%s] Channel moderation enabled by %s.
        /// </summary>
        ModerationOnNotice = 0x0F,

        /// <summary>
        ///     [%s] Channel moderation disabled by %s.
        /// </summary>
        ModerationOffNotice = 0x10,

        /// <summary>
        ///     [%s] You do not have permission to speak.
        /// </summary>
        MutedNotice = 0x11,

        /// <summary>
        ///     [%s] Player %s kicked by %s.
        /// </summary>
        PlayerKickedNotice = 0x12,

        /// <summary>
        ///     [%s] You are banned from that channel.
        /// </summary>
        BannedNotice = 0x13,

        /// <summary>
        ///     [%s] Player %s banned by %s.
        /// </summary>
        PlayerBannedNotice = 0x14,

        /// <summary>
        ///     [%s] Player %s unbanned by %s.
        /// </summary>
        PlayerUnbannedNotice = 0x15,

        /// <summary>
        ///     [%s] Player %s is not banned.
        /// </summary>
        PlayerNotBannedNotice = 0x16,

        /// <summary>
        ///     [%s] Player %s is already on the channel.
        /// </summary>
        PlayerAlreadyMemberNotice = 0x17,

        /// <summary>
        ///     %2$s has invited you to join the channel '%1$s'.
        /// </summary>
        InviteNotice = 0x18,

        /// <summary>
        ///     Target is in the wrong alliance for %s.
        /// </summary>
        InviteWrongFactionNotice = 0x19,

        /// <summary>
        ///     Wrong alliance for %s.
        /// </summary>
        WrongFactionNotice = 0x1A,

        /// <summary>
        ///     Invalid channel name
        /// </summary>
        InvalidNameNotice = 0x1B,

        /// <summary>
        ///     %s is not moderated
        /// </summary>
        NotModeratedNotice = 0x1C,

        /// <summary>
        ///     [%s] You invited %s to join the channel
        /// </summary>
        PlayerInvitedNotice = 0x1D,

        /// <summary>
        ///     [%s] %s has been banned.
        /// </summary>
        PlayerInviteBannedNotice = 0x1E,

        /// <summary>
        ///     [%s] The number of messages that can be sent to this channel is limited, please wait to send another message.
        /// </summary>
        ThrottledNotice = 0x1F
    }
}