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
        }

        public ulong Id { get; }
    }
}