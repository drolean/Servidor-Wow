using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GROUP_SWAP_SUB_GROUP : Common.Network.PacketReader
    {
        public string NameSource;
        public string NameTarget;

        public CMSG_GROUP_SWAP_SUB_GROUP(byte[] data) : base(data)
        {
            NameSource = ReadCString();
            NameTarget = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GROUP_SWAP_SUB_GROUP] NameSource: {NameSource} NameTarget: {NameTarget}");
#endif
        }
    }
}