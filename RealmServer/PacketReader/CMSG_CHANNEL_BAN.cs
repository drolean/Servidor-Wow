using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_CHANNEL_BAN : Common.Network.PacketReader
    {
        public string ChannelName;
        public string ChannelUser;

        public CMSG_CHANNEL_BAN(byte[] data) : base(data)
        {
            ChannelName = ReadCString();
            ChannelUser = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CHANNEL_BAN] ChannelName: {ChannelName} ChannelUser: {ChannelUser}");
#endif
        }
    }
}