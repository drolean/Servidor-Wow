using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GUILD_PROMOTE : Common.Network.PacketReader
    {
        public string PlayerName;

        public CMSG_GUILD_PROMOTE(byte[] data) : base(data)
        {
            PlayerName = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_PROMOTE] PlayerName: {PlayerName}");
#endif
        }
    }
}
