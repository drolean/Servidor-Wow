using System;
using Common.Database;
using MongoDB.Driver;
using RealmServer.Enums;
using RealmServer.World;

namespace RealmServer.Helpers
{
    public class CommandTest
    {
        public CommandTest(RealmServerSession session, string message)
        {
            string[] args = message.ToLower().Split(' ');

            if (args.Length == 0)
                return;

            // SHOW GPS
            if (args[0] == "gps")
            {
                session.SendMessageMotd(
                    $"MapX: {session.Character.SubMap.MapX} = MapY: {session.Character.SubMap.MapY} = " +
                    $"MapZ: {session.Character.SubMap.MapZ} = MapO: {session.Character.SubMap.MapO}"
                );
            }

            // EMOTE
            if (args[0] == "emote")
                session.Entity.SetUpdateField((int) UnitFields.UNIT_NPC_EMOTESTATE, int.Parse(args[1]));

            if (args[0] == "in")
            {
                for (int j = 0; j < 112; j++)
                {
                    var inventory = session.Character.SubInventorie.Find(x => x.Slot == j);
                    if (inventory != null)
                    {
                        if (j < 19)
                        {
                            session.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + inventory.Slot * 12, inventory.Item);
                            session.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + j * 12, 0);
                        }

                        session.Entity.SetUpdateField((int)PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2, inventory.Item);
                        session.SendPacket(UpdateObject.CreateItem(inventory, session.Character));

                        var item = DatabaseModel.ItemsCollection.Find(x => x.Entry == inventory.Item).FirstOrDefault();
                        //session.SendPacket(new SMSG_ITEM_QUERY_SINGLE_RESPONSE(item));
                    }
                    else
                    {
                        if (j < 19)
                        {
                            session.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + j * 12, 0);
                            session.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + j * 12, 0);
                        }

                        session.Entity.SetUpdateField((int)PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2, 0);
                    }
                }
            }
        }
    }
}