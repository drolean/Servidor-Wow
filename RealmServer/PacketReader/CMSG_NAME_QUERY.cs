using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_NAME_QUERY represents a packet sent by the client when it wants to retrieve other players information.
    /// </summary>
    public sealed class CMSG_NAME_QUERY : Common.Network.PacketReader
    {
        public CMSG_NAME_QUERY(byte[] data) : base(data)
        {
            PlayerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_NAME_QUERY] PlayerUid: {PlayerUid}");
#endif
        }

        public ulong PlayerUid { get; }
    }
}
