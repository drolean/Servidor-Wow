using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_ACTIVATETAXI : Common.Network.PacketReader
    {
        public int DstNode;
        public int SrcNode;
        public ulong Uid;

        public CMSG_ACTIVATETAXI(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            SrcNode = ReadInt32();
            DstNode = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_ACTIVATETAXI] Uid: {Uid} SrcNode: {SrcNode} DstNode: {DstNode}");
#endif
        }
    }
}