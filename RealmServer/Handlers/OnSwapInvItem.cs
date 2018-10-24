using System;
using Common.Database;
using Common.Database.Tables;
using MongoDB.Driver;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnSwapInvItem
    {
        public static void Handler(RealmServerSession session, CMSG_SWAP_INV_ITEM handler)
        {
            var subInventory = session.Character.SubInventorie.Find(x => x.Slot == handler.SrcSlot);

            if (subInventory == null)
                return;

            subInventory.Slot = handler.DstSlot;

            foreach (var variable in session.Character.SubInventorie)
                Console.WriteLine($@"Item: {variable.Item}  Slot: {variable.Slot}");

            DatabaseModel.CharacterCollection.UpdateOneAsync(
                Builders<Characters>.Filter.Where(x => x.Uid == session.Character.Uid),
                Builders<Characters>.Update.Set(x => x.SubInventorie, session.Character.SubInventorie)
            );

            session.SendInventory(session);
        }
    }
}