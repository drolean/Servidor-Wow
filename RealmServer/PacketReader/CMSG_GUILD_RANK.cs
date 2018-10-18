using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_GUILD_RANK : Common.Network.PacketReader
    {
        public string Name;
        public uint Privileges;
        public int RankId;

        public CMSG_GUILD_RANK(byte[] data) : base(data)
        {
            RankId = ReadInt32();
            Privileges = ReadUInt32();
            Name = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GUILD_RANK] RankId: {RankId} Privileges: {Privileges} RankName: {Name}");
#endif
        }
    }
}