using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class MSG_MINIMAP_PING : Common.Network.PacketReader
    {
        public float MapX;
        public float MapY;

        public MSG_MINIMAP_PING(byte[] data) : base(data)
        {
            MapX = ReadSingle();
            MapY = ReadSingle();

#if DEBUG
            Log.Print(LogType.Debug, $"[MSG_MINIMAP_PING] MapX: {MapX} MapY: {MapY}");
#endif
        }
    }
}