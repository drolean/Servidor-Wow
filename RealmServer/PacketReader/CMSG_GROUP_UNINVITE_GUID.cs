using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_GROUP_UNINVITE_GUID : Common.Network.PacketReader
    {
        public UInt64 PlayerUid;

        public CMSG_GROUP_UNINVITE_GUID(byte[] data) : base(data)
        {
            PlayerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GROUP_UNINVITE_GUID] PlayerUid: {PlayerUid}");
#endif
        }
    }
}