using Common.Helpers;

namespace RealmServer.PacketReader
{
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