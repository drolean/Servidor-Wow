using Common.Helpers;

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

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_ZONEUPDATE] ZoneiD: {ZoneiD}");
#endif
        }

        public uint ZoneiD { get; }
    }
}
