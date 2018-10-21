using Common.Database;
using MongoDB.Driver;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnItemQuerySingle
    {
        public static void Handler(RealmServerSession session, CMSG_ITEM_QUERY_SINGLE handler)
        {
            if (handler.ItemId == 0)
            {
                var item = DatabaseModel.ItemsCollection.Find(x => x.Entry == (int) handler.ItemIdo).FirstOrDefault();

                if (item == null)
                    return;

                session.SendPacket(new SMSG_ITEM_QUERY_SINGLE_RESPONSE(item));
            }
            else
            {

                var item = DatabaseModel.ItemsCollection.Find(x => x.Entry == (int) handler.ItemId).FirstOrDefault();

                if (item == null)
                    return;

                session.SendPacket(new SMSG_ITEM_QUERY_SINGLE_RESPONSE(item));
            }
        }
    }
}