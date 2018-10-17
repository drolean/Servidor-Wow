using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_CAST_SPELL : Common.Network.PacketReader
    {
        /*
        public byte CastCount;
        public uint Spell;
        public byte Flag;
        */
        public uint SpellId;
        public string Target;

        public CMSG_CAST_SPELL(byte[] data) : base(data)
        {
            SpellId = ReadUInt32();
            Target = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CAST_SPELL] SpellId: {SpellId} Target: {Target}");
#endif
        }
    }
}