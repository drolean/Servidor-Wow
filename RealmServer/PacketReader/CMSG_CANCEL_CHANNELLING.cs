using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_CANCEL_CHANNELLING : Common.Network.PacketReader
    {
        public uint SpellId;

        public CMSG_CANCEL_CHANNELLING(byte[] data) : base(data)
        {
            SpellId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CANCEL_CHANNELLING] SpellId: {SpellId}");
#endif
        }
    }
}
