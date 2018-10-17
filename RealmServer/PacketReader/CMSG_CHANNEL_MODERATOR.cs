using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_CHANNEL_MODERATOR : Common.Network.PacketReader
    {
        public string ChannelName;
        public string ChannelUser;

        public CMSG_CHANNEL_MODERATOR(byte[] data) : base(data)
        {
            ChannelName = ReadCString();
            ChannelUser = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CHANNEL_MODERATOR] ChannelName: {ChannelName} ChannelUser: {ChannelUser}");
#endif
        }
    }
}