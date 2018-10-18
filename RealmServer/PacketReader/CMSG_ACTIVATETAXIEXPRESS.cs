using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_ACTIVATETAXIEXPRESS : Common.Network.PacketReader
    {
        public uint NodeCount;
        public uint TotalCost;
        public ulong Uid;

        public CMSG_ACTIVATETAXIEXPRESS(byte[] data) : base(data)
        {
            Uid = ReadUInt64();
            NodeCount = ReadUInt32();

            // TODO
            //for (int i = 0; i < NodeCount; ++i)
            //packet.ReadUInt32()

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_ACTIVATETAXIEXPRESS] Uid: {Uid}");
#endif
        }
    }
}