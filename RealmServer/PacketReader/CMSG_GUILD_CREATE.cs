using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GUILD_CREATE : Common.Network.PacketReader
    {
        public string Name;

        public CMSG_GUILD_CREATE(byte[] data) : base(data)
        {
            Name = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_CREATE] Name: {Name}");
#endif
        }
    }
}
