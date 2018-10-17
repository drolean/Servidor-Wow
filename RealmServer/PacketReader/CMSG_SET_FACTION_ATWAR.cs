using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_FACTION_ATWAR : Common.Network.PacketReader
    {
        public byte Enabled;
        public int Faction;

        public CMSG_SET_FACTION_ATWAR(byte[] data) : base(data)
        {
            Faction = ReadInt32();
            Enabled = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_FACTION_ATWAR] Faction: {Faction} Enabled: {Enabled}");
#endif
        }
    }
}