using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_MAIL_CREATE_TEXT_ITEM : Common.Network.PacketReader
    {
        public ulong MailboxUid;
        public int Mailid;

        public CMSG_MAIL_CREATE_TEXT_ITEM(byte[] data) : base(data)
        {
            MailboxUid = ReadUInt64();
            Mailid = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_MAIL_CREATE_TEXT_ITEM] MailboxUid: {MailboxUid} Mailid: {Mailid}");
#endif
        }
    }
}