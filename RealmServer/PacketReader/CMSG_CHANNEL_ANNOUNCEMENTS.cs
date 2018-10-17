using Common.Helpers;

namespace RealmServer.PacketReader
{
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