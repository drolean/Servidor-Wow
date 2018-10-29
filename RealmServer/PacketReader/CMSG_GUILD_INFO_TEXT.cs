using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GUILD_INFO_TEXT : Common.Network.PacketReader
    {
        public string Guild;

        public CMSG_GUILD_INFO_TEXT(byte[] data) : base(data)
        {
            Guild = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_INFO_TEXT] Guild: {Guild}");
#endif
        }
    }
}
