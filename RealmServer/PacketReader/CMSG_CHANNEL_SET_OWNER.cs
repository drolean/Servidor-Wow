using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Handles an incoming owner set request
    /// </summary>
    public class CMSG_CHANNEL_SET_OWNER : Common.Network.PacketReader
    {
        public string ChannelName;
        public string ChannelNewOwner;

        public CMSG_CHANNEL_SET_OWNER(byte[] data) : base(data)
        {
            ChannelName = ReadCString();
            ChannelNewOwner = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_CHANNEL_SET_OWNER] ChannelName: {ChannelName} ChannelNewOwner: {ChannelNewOwner}");
#endif
        }
    }
}
