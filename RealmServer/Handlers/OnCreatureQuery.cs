using Common.Database;
using MongoDB.Driver;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnCreatureQuery
    {
        public static void Handler(RealmServerSession session, CMSG_CREATURE_QUERY handler)
        {
            var creature = DatabaseModel.CreaturesCollection.Find(x => x.Entry == (int) handler.CreatureEntry).First();

            if (creature == null)
                return;

            session.SendPacket(new SMSG_CREATURE_QUERY_RESPONSE(creature));
        }
    }
}
