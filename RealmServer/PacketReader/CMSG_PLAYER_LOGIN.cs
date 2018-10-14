namespace RealmServer.PacketReader
{
    public sealed class CMSG_PLAYER_LOGIN : Common.Network.PacketReader
    {
        public ulong Id { get; }

        public CMSG_PLAYER_LOGIN(byte[] data) : base(data)
        {
            Id = ReadUInt64();
        }
    }
}