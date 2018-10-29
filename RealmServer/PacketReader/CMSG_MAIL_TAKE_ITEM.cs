using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_MAIL_TAKE_ITEM : Common.Network.PacketReader
    {
        public int ItemId;
        public ulong MailboxUid;
        public int Mailid;

        public CMSG_MAIL_TAKE_ITEM(byte[] data) : base(data)
        {
            MailboxUid = ReadUInt64();
            Mailid = ReadInt32();
            ItemId = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_MAIL_TAKE_ITEM] MailboxUid: {MailboxUid} Mailid: {Mailid} ItemId: {ItemId}");
#endif
        }
    }
}
