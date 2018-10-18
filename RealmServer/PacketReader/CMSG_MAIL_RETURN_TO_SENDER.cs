using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_MAIL_RETURN_TO_SENDER : Common.Network.PacketReader
    {
        public ulong MailboxUid;
        public int Mailid;

        public CMSG_MAIL_RETURN_TO_SENDER(byte[] data) : base(data)
        {
            MailboxUid = ReadUInt64();
            Mailid = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_MAIL_RETURN_TO_SENDER] MailboxUid: {MailboxUid} Mailid: {Mailid}");
#endif
        }
    }
}