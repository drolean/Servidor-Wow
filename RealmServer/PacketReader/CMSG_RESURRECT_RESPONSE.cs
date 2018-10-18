using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_RESURRECT_RESPONSE : Common.Network.PacketReader
    {
        public ulong ObjectUid;
        public byte Status;

        public CMSG_RESURRECT_RESPONSE(byte[] data) : base(data)
        {
            ObjectUid = ReadUInt64();
            Status = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_RESURRECT_RESPONSE] ObjectUid: {ObjectUid} Status: {Status}");
#endif
        }
    }
}