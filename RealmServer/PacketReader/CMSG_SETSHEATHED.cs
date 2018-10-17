using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SETSHEATHED : Common.Network.PacketReader
    {
        public int Sheathed; // TODO: or byte???

        public CMSG_SETSHEATHED(byte[] data) : base(data)
        {
            Sheathed = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SETSHEATHED] Sheathed: {Sheathed}");
#endif
        }
    }
}