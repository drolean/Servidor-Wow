using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnUpdateAccountData
    {
        /// <summary>
        ///     Received Data from user (macro, settings)
        /// </summary>
        /// <param name="session"></param>
        /// <param name="handler"></param>
        public static void Handler(RealmServerSession session, CMSG_UPDATE_ACCOUNT_DATA handler)
        {
            // User Macro
            if (handler.Type == 4)
            {
            }

            // Character Macro
            if (handler.Type == 5)
            {
            }

            // User ? Character Data
            if (handler.Type == 7)
            {
            }
        }
    }
}