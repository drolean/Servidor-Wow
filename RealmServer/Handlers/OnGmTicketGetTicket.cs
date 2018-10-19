using RealmServer.Enums;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    internal class OnGmTicketGetTicket
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            //session.SendPacket(new SMSG_GMTICKET_GETTICKET(TicketInfoResponse.NoTicket));
            session.SendPacket(new SMSG_GMTICKET_GETTICKET(TicketInfoResponse.Pending, "My ticket!!!!"));
            session.SendPacket(new SMSG_QUERY_TIME_RESPONSE());
        }
    }
}