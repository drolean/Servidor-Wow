using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnPing
    {
        /// <summary>
        ///     Packet received of the client to ping the server.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="handler"></param>
        public static void Handler(RealmServerSession session, CMSG_PING handler)
        {
            session.SendPacket(new SMSG_PONG(handler.Latency));
        }
    }
}
