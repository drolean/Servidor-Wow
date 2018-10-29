using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_WHOIS represents a packet sent by the client when it wants to retrieve other players information.
    /// </summary>
    public sealed class CMSG_WHOIS : Common.Network.PacketReader
    {
        public string NamePlayer;

        public CMSG_WHOIS(byte[] data) : base(data)
        {
            NamePlayer = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_WHOIS] NamePlayer: {NamePlayer}");
#endif
        }
    }
}
