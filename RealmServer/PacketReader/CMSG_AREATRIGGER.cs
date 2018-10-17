using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AREATRIGGER : Common.Network.PacketReader
    {
        public int Area;

        public CMSG_AREATRIGGER(byte[] data) : base(data)
        {
            Area = ReadInt32();
#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AREATRIGGER] Area: {Area}");
#endif
        }
    }
}