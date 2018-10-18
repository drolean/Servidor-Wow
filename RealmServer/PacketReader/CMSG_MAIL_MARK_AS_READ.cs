using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_MAIL_MARK_AS_READ : Common.Network.PacketReader
    {
        public UInt64 MailboxUid;
        public int Mailid;

        public CMSG_MAIL_MARK_AS_READ(byte[] data) : base(data)
        {
            MailboxUid = ReadUInt64();
            Mailid = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_MAIL_MARK_AS_READ] MailboxUid: {MailboxUid} Mailid: {Mailid}");
#endif
        }
    }
}