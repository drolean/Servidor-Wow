namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUTH_SESSION : Common.Network.PacketReader
    {
        public int ClientAddOnsSize;
        public byte[] ClientHash;

        public CMSG_AUTH_SESSION(byte[] data) : base(data)
        {
            ClientVersion = ReadInt32();
            ClientSessionId = ReadInt32();
            ClientAccount = ReadCString();
            ClientSeed = ReadInt32();
            ClientHash = ReadBytes(20);
            ClientAddOnsSize = ReadInt32();
        }

        public int ClientVersion { get; }
        public int ClientSessionId { get; }
        public string ClientAccount { get; }
        public int ClientSeed { get; }
    }
}