using Common.Network;

namespace RealmServer.Handlers
{
    internal class GMHandler
    {
        internal static void OnGmTicketGetTicket(RealmServerSession session, PacketReader handler)
        {
            /* SMSG_GMTICKET_GETTICKET
             * if > 0
             * - Int32  = Ticket Available = 6
             * - String = Ticket Text
             * else
             * - Int32  = NoTicket = 10
             */

            // SMSG_QUERY_TIME_RESPONSE

        }
    }
}
