using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_GET_MAIL_LIST : Common.Network.PacketReader
    {
        public ulong MailListUid;

        public CMSG_GET_MAIL_LIST(byte[] data) : base(data)
        {
            MailListUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GET_MAIL_LIST] MailListUid: {MailListUid}");
#endif
        }
    }
}