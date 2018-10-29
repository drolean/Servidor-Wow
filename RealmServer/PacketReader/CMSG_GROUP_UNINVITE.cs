using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GROUP_UNINVITE : Common.Network.PacketReader
    {
        public string NamePlayer;

        public CMSG_GROUP_UNINVITE(byte[] data) : base(data)
        {
            NamePlayer = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GROUP_UNINVITE] NamePlayer: {NamePlayer}");
#endif
        }
    }
}
