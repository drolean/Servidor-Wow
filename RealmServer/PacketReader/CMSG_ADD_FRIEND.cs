using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Handles an incoming add friend request
    /// </summary>
    public class CMSG_ADD_FRIEND : Common.Network.PacketReader
    {
        public string NamePlayer;

        public CMSG_ADD_FRIEND(byte[] data) : base(data)
        {
            NamePlayer = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_ADD_FRIEND] NamePlayer: {NamePlayer}");
#endif
        }
    }
}