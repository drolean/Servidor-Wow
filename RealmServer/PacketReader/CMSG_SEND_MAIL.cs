using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SEND_MAIL : Common.Network.PacketReader
    {
        public uint Cod;
        public byte ItemCount;
        public ulong MailboxUid;
        public string Message;

        public uint Money;
        public string RecipientName;
        public uint Stationary;
        public string Subject;
        public uint Unk1;

        public CMSG_SEND_MAIL(byte[] data) : base(data)
        {
            MailboxUid = ReadUInt64();
            RecipientName = ReadCString();
            Subject = ReadCString();
            Message = ReadCString();
            Stationary = ReadUInt32();
            Unk1 = ReadUInt32();
            ItemCount = ReadByte();

            for (var i = 0; i < ItemCount; i++)
            {
                var slot = ReadByte();
                var itemUid = ReadUInt64();
                Log.Print(LogType.Debug, $"[CMSG_SEND_MAIL] Slot: {slot} ItemUid: {itemUid}");
            }

            Money = ReadUInt32();
            Cod = ReadUInt32();

            //var unknown2 = ReadUInt64();
            //var unknown4 = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SEND_MAIL] MailboxUid: {MailboxUid} RecipientName: {RecipientName} " +
                                     $"Subject: {Subject} Message: {Message} Stationary: {Stationary} Unk1: {Unk1} " +
                                     $"ItemCount: {ItemCount} Money: {Money} Cod: {Cod}");
#endif
        }
    }
}