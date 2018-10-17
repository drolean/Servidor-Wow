using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_GMTICKET_CREATE : Common.Network.PacketReader
    {
        public uint MapId;
        public float MapX;
        public float MapY;
        public float MapZ;
        public string Text;
        public string Future;

        public CMSG_GMTICKET_CREATE(byte[] data) : base(data)
        {
            MapId = ReadUInt32();
            MapX = ReadSingle();
            MapY = ReadSingle();
            MapZ = ReadSingle();
            Text = ReadCString();
            Future = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GMTICKET_CREATE] MapId: {MapId} MapX: {MapX} MapY: {MapY} MapZ: {MapZ}" +
                                     $"Text: {Text} Future: {Future}");
#endif
        }
    }
}