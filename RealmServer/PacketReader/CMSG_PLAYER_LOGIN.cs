namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_PLAYER_LOGIN represents a packet sent by the client when it tries to login a character.
    /// </summary>
    public sealed class CMSG_PLAYER_LOGIN : Common.Network.PacketReader
    {
        public CMSG_PLAYER_LOGIN(byte[] data) : base(data)
        {
            Id = ReadUInt64();
        }

        public ulong Id { get; }
    }
}