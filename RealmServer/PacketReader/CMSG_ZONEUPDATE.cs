namespace RealmServer.PacketReader
{
    public sealed class CMSG_ZONEUPDATE : Common.Network.PacketReader
    {
        public CMSG_ZONEUPDATE(byte[] data) : base(data)
        {
            ZoneiD = ReadUInt32();
        }

        public uint ZoneiD { get; }
    }
}