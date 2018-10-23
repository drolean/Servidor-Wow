using System;
using Common.Database;
using Common.Database.Tables;
using MongoDB.Driver;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnItemQuerySingle
    {
        private static Items _item;

        public static void Handler(RealmServerSession session, CMSG_ITEM_QUERY_SINGLE handler)
        {
            _item = handler.ItemId == 0
                ? DatabaseModel.ItemsCollection.Find(x => x.Entry == (int) handler.ItemIdo).FirstOrDefault()
                : DatabaseModel.ItemsCollection.Find(x => x.Entry == (int) handler.ItemId).FirstOrDefault();

            if (_item == null)
                return;

            // Update UsedAt
            DatabaseModel.ItemsCollection.UpdateOneAsync(
                Builders<Common.Database.Tables.Items>.Filter.Where(x => x.Id == _item.Id),
                Builders<Common.Database.Tables.Items>.Update.Set(x => x.UsedAt, DateTime.Now)
            );

            session.SendPacket(new SMSG_ITEM_QUERY_SINGLE_RESPONSE(_item));
        }
    }
}