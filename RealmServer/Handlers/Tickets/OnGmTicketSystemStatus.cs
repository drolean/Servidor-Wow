using RealmServer.Enums;
using RealmServer.PacketServer;

namespace RealmServer.Handlers.Tickets
{
    public class OnGmTicketSystemStatus
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            session.SendPacket(new SMSG_GMTICKET_SYSTEMSTATUS(GmTicketSystemStatus.Enabled));
        }
    }
}
