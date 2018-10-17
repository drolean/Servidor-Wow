using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GUILD_INVITE : Common.Network.PacketReader
    {
        public string PlayerName;

        public CMSG_GUILD_INVITE(byte[] data) : base(data)
        {
            PlayerName = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_INVITE] PlayerName: {PlayerName}");
#endif
        }
    }
}