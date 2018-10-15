namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Handles an incoming zone update notification.
    ///     But it only sends the current parent-zone and not the subzone.
    /// </summary>
    public sealed class CMSG_ZONEUPDATE : Common.Network.PacketReader
    {
        public CMSG_ZONEUPDATE(byte[] data) : base(data)
        {
            ZoneiD = ReadUInt32();
        }

        public uint ZoneiD { get; }
    }
}