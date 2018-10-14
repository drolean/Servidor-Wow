using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnUpdateAccountData
    {
        /// <summary>
        ///     Packet received of the client to ping the server.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="handler"></param>
        public static void Handler(RealmServerSession session, CMSG_UPDATE_ACCOUNT_DATA handler)
        {
            //session.SendPacket(new SMSG_PONG(handler.Latency));
        }
    }
}