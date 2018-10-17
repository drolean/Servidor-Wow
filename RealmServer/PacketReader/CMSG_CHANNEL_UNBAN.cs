using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_CHANNEL_UNBAN : Common.Network.PacketReader
    {
        public string ChannelName;
        public string ChannelUser;

        public CMSG_CHANNEL_UNBAN(byte[] data) : base(data)
        {
            ChannelName = ReadCString();
            ChannelUser = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CHANNEL_UNBAN] ChannelName: {ChannelName} ChannelUser: {ChannelUser}");
#endif
        }
    }
}