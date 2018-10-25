using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Handles a request of toggling the moderate mode of the channel
    /// </summary>
    public class CMSG_CHANNEL_MODERATE : Common.Network.PacketReader
    {
        public string ChannelName;

        public CMSG_CHANNEL_MODERATE(byte[] data) : base(data)
        {
            ChannelName = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CHANNEL_MODERATE] ChannelName: {ChannelName}");
#endif
        }
    }
}