namespace RealmServer.Handlers
{
    internal class OnQueryNextMailTime
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            // session.User

            /*
             * MSG_QUERY_NEXT_MAIL_TIME
             *
             * @if mail > 0
             *   Int32(0)
             * @else
             *   Int8(0)
             *   Int8(&HC0)
             *   Int8(&HA8)
             *   Int8(&HC7)
             */
        }
    }
}