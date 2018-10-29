using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_ACTIONBAR_TOGGLES : Common.Network.PacketReader
    {
        public byte ActionBar;

        public CMSG_SET_ACTIONBAR_TOGGLES(byte[] data) : base(data)
        {
            ActionBar = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_ACTIONBAR_TOGGLES] ActionBar: {ActionBar}");
#endif
        }
    }
}
