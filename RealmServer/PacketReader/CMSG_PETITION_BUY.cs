using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_PETITION_BUY : Common.Network.PacketReader
    {
        public ulong PetitionerUid;

        public CMSG_PETITION_BUY(byte[] data) : base(data)
        {
            PetitionerUid = ReadUInt64();
            /*
        packet.GetInt64()
        packet.GetInt32()
        Dim Name As String = packet.GetString
        If (packet.Data.Length - 1) < 26 + Name.Length + 5 * 8 + 2 + 1 + 4 + 4 Then Exit Sub
        packet.GetInt64()
        packet.GetInt64()
        packet.GetInt64()
        packet.GetInt64()
        packet.GetInt64()
        packet.GetInt16()
        packet.GetInt8()
        Dim Index As Integer = packet.GetInt32
        packet.GetInt32()
             */

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PETITION_BUY] PetitionerUid: {PetitionerUid}");
#endif
        }
    }
}
