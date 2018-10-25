using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Handles an incoming channel password change request
    /// </summary>
    public class CMSG_CHANNEL_PASSWORD : Common.Network.PacketReader
    {
        public string ChannelName;
        public string ChannelPassword;

        public CMSG_CHANNEL_PASSWORD(byte[] data) : base(data)
        {
            ChannelName = ReadCString();
            ChannelPassword = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_CHANNEL_PASSWORD] ChannelName: {ChannelName} ChannelPassword: {ChannelPassword}");
#endif
        }
    }
}