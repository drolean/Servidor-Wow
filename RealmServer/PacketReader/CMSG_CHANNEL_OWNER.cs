using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_CHANNEL_OWNER : Common.Network.PacketReader
    {
        public string ChannelName;

        public CMSG_CHANNEL_OWNER(byte[] data) : base(data)
        {
            ChannelName = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CHANNEL_OWNER] ChannelName: {ChannelName}");
#endif
        }
    }
}