using Common.Helpers;

namespace RealmServer.PacketReader
{
    public class CMSG_GMTICKET_UPDATETEXT : Common.Network.PacketReader
    {
        public string Message;

        public CMSG_GMTICKET_UPDATETEXT(byte[] data) : base(data)
        {
            Message = ReadCString();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GMTICKET_UPDATETEXT] Message: {Message}");
#endif
        }
    }
}
