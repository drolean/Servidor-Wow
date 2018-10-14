namespace RealmServer.PacketReader
{
    public class CMSG_JOIN_CHANNEL : Common.Network.PacketReader
    {
        public CMSG_JOIN_CHANNEL(byte[] data) : base(data)
        {
            Channel = ReadCString();
            Password = ReadCString();
        }

        public string Channel { get; }
        public string Password { get; }
    }
}