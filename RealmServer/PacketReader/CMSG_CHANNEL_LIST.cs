using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Handles an incoming channel list request
    /// </summary>
    public class CMSG_CHANNEL_LIST : Common.Network.PacketReader
    {
        public string ChannelName;

        public CMSG_CHANNEL_LIST(byte[] data) : base(data)
        {
            ChannelName = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CHANNEL_LIST] ChannelName: {ChannelName}");
#endif
        }
    }
}