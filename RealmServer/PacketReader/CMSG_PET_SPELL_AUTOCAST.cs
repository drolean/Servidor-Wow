using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PET_SPELL_AUTOCAST : Common.Network.PacketReader
    {
        public UInt64 Uid;
        public uint SpellId;
        public byte State;

        public CMSG_PET_SPELL_AUTOCAST(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            SpellId = ReadUInt32();
            State = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PET_SPELL_AUTOCAST] Uid: {Uid} SpellId: {SpellId} State: {State}");
#endif
        }
    }
}