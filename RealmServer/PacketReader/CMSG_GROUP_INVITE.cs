using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GROUP_INVITE : Common.Network.PacketReader
    {
        public string NamePlayer;

        public CMSG_GROUP_INVITE(byte[] data) : base(data)
        {
            NamePlayer = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GROUP_INVITE] NamePlayer: {NamePlayer}");
#endif
        }
    }
}