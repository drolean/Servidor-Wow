using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GROUP_SET_LEADER : Common.Network.PacketReader
    {
        public string NamePlayer;

        public CMSG_GROUP_SET_LEADER(byte[] data) : base(data)
        {
            NamePlayer = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GROUP_SET_LEADER] NamePlayer: {NamePlayer}");
#endif
        }
    }
}