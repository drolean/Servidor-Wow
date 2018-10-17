using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PET_ACTION : Common.Network.PacketReader
    {
        public UInt64 Uid;
        public ushort SpellId;
        public ushort SpellFlag;
        public UInt64 TargetUid;

        public CMSG_PET_ACTION(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            SpellId = ReadUInt16();
            SpellFlag = ReadUInt16();
            TargetUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PET_ACTION] Uid: {Uid} SpellId: {SpellId} SpellFlag: {SpellFlag} TargetUid: {TargetUid}");
#endif
        }
    }
}