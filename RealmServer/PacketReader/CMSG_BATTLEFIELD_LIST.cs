using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BATTLEFIELD_LIST : Common.Network.PacketReader
    {
        public uint BgId;
        public bool FromGui;
        public byte Unk1;

        public CMSG_BATTLEFIELD_LIST(byte[] data) : base(data)
        {
            //Dim GUID As ULong = packet.GetUInt64
            BgId = ReadUInt32();
            FromGui = ReadBoolean();
            Unk1 = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BATTLEFIELD_LIST] BgId: {BgId} FromGui: {FromGui} Unk1: {Unk1}");
#endif
        }
    }
}