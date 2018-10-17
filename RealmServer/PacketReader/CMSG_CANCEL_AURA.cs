using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_CANCEL_AURA : Common.Network.PacketReader
    {
        public uint SpellId;

        public CMSG_CANCEL_AURA(byte[] data) : base(data)
        {
            SpellId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CANCEL_AURA] SpellId: {SpellId}");
#endif
        }
    }
}