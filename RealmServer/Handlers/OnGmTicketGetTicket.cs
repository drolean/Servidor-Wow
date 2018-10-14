namespace RealmServer.Handlers
{
    internal class OnGmTicketGetTicket
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            // session.User

            /*
             * SMSG_GMTICKET_GETTICKET
             *
             * @if Available
             * Int32(GMTicketGetResult.GMTICKET_AVAILABLE)
             * String(MySQLResult.Rows(0).Item("ticket_text"))
             * @else
             * Int32(GMTicketGetResult.GMTICKET_NOTICKET)
             */

            /*
             * SMSG_QUERY_TIME_RESPONSE
             */
        }
    }
}