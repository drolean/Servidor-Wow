using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PET_CAST_SPELL : Common.Network.PacketReader
    {
        public UInt64 PetUid;
        public byte CastCount;
        public uint SpellId;
        public byte Unk;

        public CMSG_PET_CAST_SPELL(byte[] data) : base(data)
        {
            PetUid = ReadUInt64();
            CastCount = ReadByte();
            SpellId = ReadUInt32();
            Unk = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PET_CAST_SPELL] PetUid: {PetUid} CastCount: {CastCount} SpellId: {SpellId} Unk: {Unk}");
#endif
        }
    }
}