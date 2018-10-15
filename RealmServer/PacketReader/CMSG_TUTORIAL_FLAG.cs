namespace RealmServer.PacketReader
{
    public sealed class CMSG_TUTORIAL_FLAG : Common.Network.PacketReader
    {
        public int Flag;

        public CMSG_TUTORIAL_FLAG(byte[] data) : base(data)
        {
            Flag = ReadInt32();
        }
    }
}