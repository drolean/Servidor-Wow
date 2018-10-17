using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GUILD_LEADER : Common.Network.PacketReader
    {
        public string PlayerName;

        public CMSG_GUILD_LEADER(byte[] data) : base(data)
        {
            PlayerName = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_LEADER] PlayerName: {PlayerName}");
#endif
        }
    }
}