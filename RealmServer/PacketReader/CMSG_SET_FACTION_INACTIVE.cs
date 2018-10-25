using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_FACTION_INACTIVE : Common.Network.PacketReader
    {
        public byte Inactive;
        public int FactionId;

        public CMSG_SET_FACTION_INACTIVE(byte[] data) : base(data)
        {
            FactionId = ReadInt32();
            Inactive = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_FACTION_INACTIVE] FactionId: {FactionId} Inactive: {Inactive}");
#endif
        }
    }
}