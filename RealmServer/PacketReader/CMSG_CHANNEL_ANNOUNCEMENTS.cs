using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Handles a request of toggling the announce mode of the channel
    /// </summary>
    public class CMSG_CHANNEL_ANNOUNCEMENTS : Common.Network.PacketReader
    {
        public string ChannelName;

        public CMSG_CHANNEL_ANNOUNCEMENTS(byte[] data) : base(data)
        {
            ChannelName = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CHANNEL_ANNOUNCEMENTS] ChannelName: {ChannelName}");
#endif
        }
    }
}