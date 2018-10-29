using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_ITEM_TEXT_QUERY : Common.Network.PacketReader
    {
        public uint ItemTextId;
        public uint MailOrItemId;
        public uint Unk1;

        public CMSG_ITEM_TEXT_QUERY(byte[] data) : base(data)
        {
            /*
            Dim MailID As Integer = packet.GetInt32
            Dim GameObjectGUID as ulong = packet.GetuInt64
             */
            ItemTextId = ReadUInt32();
            MailOrItemId = ReadUInt32();
            Unk1 = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_ITEM_TEXT_QUERY] ItemTextId: {ItemTextId} MailOrItemId: {MailOrItemId} Unk1: {Unk1}");
#endif
        }
    }
}
