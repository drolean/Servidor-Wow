using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_CANCEL_CAST : Common.Network.PacketReader
    {
        /*
        public byte CastCount;
        public uint Spell;
        public byte Flag;
        */
        public uint SpellId;

        public CMSG_CANCEL_CAST(byte[] data) : base(data)
        {
            SpellId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CANCEL_CAST] SpellId: {SpellId}");
#endif
        }
    }
}
