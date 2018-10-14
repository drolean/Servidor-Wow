namespace RealmServer.Handlers
{
    internal class OnQueryTime
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            // session.User

            /*
             * SMSG_QUERY_TIME_RESPONSE
             *
             * Int32(TimeGetTime(""))
             */
        }
    }
}