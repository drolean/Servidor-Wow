using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_LEAVE_BATTLEFIELD : Common.Network.PacketReader
    {
        public ushort Id; // ????
        public int MapType;
        public byte Unk1;
        public byte Unk2;

        public CMSG_LEAVE_BATTLEFIELD(byte[] data) : base(data)
        {
            Unk1 = ReadByte();
            Unk2 = ReadByte();
            MapType = ReadInt32();
            Id = ReadUInt16();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_LEAVE_BATTLEFIELD] Unk1: {Unk1} Unk2: {Unk2} MapType: {MapType} Id: {Id}");
#endif
        }
    }
}
