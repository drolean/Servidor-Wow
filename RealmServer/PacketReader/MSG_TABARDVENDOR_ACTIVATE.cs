using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class MSG_TABARDVENDOR_ACTIVATE : Common.Network.PacketReader
    {
        public ulong TabardUid;

        public MSG_TABARDVENDOR_ACTIVATE(byte[] data) : base(data)
        {
            TabardUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[MSG_TABARDVENDOR_ACTIVATE] TabardUid: {TabardUid}");
#endif
        }
    }
}
