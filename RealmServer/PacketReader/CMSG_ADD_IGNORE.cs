using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Handles an incoming add ignore request
    /// </summary>
    public class CMSG_ADD_IGNORE : Common.Network.PacketReader
    {
        public string NamePlayer;

        public CMSG_ADD_IGNORE(byte[] data) : base(data)
        {
            NamePlayer = ReadCString();
#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_ADD_IGNORE] NamePlayer: {NamePlayer}");
#endif
        }
    }
}
