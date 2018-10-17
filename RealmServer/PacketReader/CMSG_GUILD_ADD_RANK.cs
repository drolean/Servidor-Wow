using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GUILD_ADD_RANK : Common.Network.PacketReader
    {
        public string Name;

        public CMSG_GUILD_ADD_RANK(byte[] data) : base(data)
        {
            Name = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_ADD_RANK] Name: {Name}");
#endif
        }
    }
}