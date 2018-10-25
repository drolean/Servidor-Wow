using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_TUTORIAL_FLAG : Common.Network.PacketReader
    {
        public uint Flag;

        public CMSG_TUTORIAL_FLAG(byte[] data) : base(data)
        {
            Flag = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_TUTORIAL_FLAG] Flag: {Flag}");
#endif
        }
    }
}