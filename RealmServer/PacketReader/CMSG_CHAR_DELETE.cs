using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_CHAR_DELETE represents a packet sent by the client whenever it tries to delete a character.
    /// </summary>
    public sealed class CMSG_CHAR_DELETE : Common.Network.PacketReader
    {
        public CMSG_CHAR_DELETE(byte[] data) : base(data)
        {
            Id = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_CHAR_DELETE] Id: {Id}");
#endif
        }

        public ulong Id { get; }
    }
}
