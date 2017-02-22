using Common.Network;

namespace RealmServer.Handlers
{
    internal class MailHandler
    {
        internal static void OnQueryNextMailTime(RealmServerSession session, PacketReader handler)
        {
            /* MSG_QUERY_NEXT_MAIL_TIME
             * if > 0 
             * Int32 = 0
             * ELSE
             * Int8 = 0
             * Int8 = 0xC0
             * Int8 = 0xA8
             * Int8 = 0xC7
             */
        }
    }
}