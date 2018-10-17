using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_TRAINER_BUY_SPELL : Common.Network.PacketReader
    {
        public UInt64 VendorUid;
        public int SpellId;

        public CMSG_TRAINER_BUY_SPELL(byte[] data) : base(data)
        {
            VendorUid = ReadUInt64();
            SpellId = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_TRAINER_BUY_SPELL] VendorUid: {VendorUid} SpellId: {SpellId}");
#endif
        }
    }
}