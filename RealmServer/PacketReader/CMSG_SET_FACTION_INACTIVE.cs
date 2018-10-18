using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_FACTION_INACTIVE : Common.Network.PacketReader
    {
        public uint ReplistId;
        public byte Inactive;

        public CMSG_SET_FACTION_INACTIVE(byte[] data) : base(data)
        {
            ReplistId = ReadUInt32();
            Inactive = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_FACTION_INACTIVE] ReplistId: {ReplistId} Inactive: {Inactive}");
#endif
        }
    }
}