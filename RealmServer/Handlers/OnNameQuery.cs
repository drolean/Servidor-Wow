using RealmServer.Database;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnNameQuery
    {
        public static void Handler(RealmServerSession session, CMSG_NAME_QUERY handler)
        {
            var target = Characters.FindCharacaterByUid(handler.Uid);

            if (target != null)
                session.SendPacket(new SMSG_NAME_QUERY_RESPONSE(target));
        }
    }
}