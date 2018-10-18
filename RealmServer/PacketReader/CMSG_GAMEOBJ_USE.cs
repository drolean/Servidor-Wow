using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_GAMEOBJ_USE : Common.Network.PacketReader
    {
        public UInt64 GameObjectUid;

        public CMSG_GAMEOBJ_USE(byte[] data) : base(data)
        {
            GameObjectUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GAMEOBJ_USE] GameObjectUid: {GameObjectUid}");
#endif
        }
    }
}