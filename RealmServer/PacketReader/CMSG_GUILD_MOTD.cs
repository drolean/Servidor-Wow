using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GUILD_MOTD : Common.Network.PacketReader
    {
        public string Motd;

        public CMSG_GUILD_MOTD(byte[] data) : base(data)
        {
            Motd = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_MOTD] Motd: {Motd}");
#endif
        }
    }
}