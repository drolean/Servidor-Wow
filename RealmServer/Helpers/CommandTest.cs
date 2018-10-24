using System;
using System.Timers;
using Common.Database;
using Common.Database.Tables;
using Common.Helpers;
using MongoDB.Driver;
using RealmServer.Enums;
using RealmServer.PacketServer;

namespace RealmServer.Helpers
{
    public class CommandTest
    {
        public static int Count;
        public static Timer TimerPlugin;
        public static RealmServerSession Sessao;

        public CommandTest(RealmServerSession session, string message)
        {
            var args = message.ToLower().Split(' ');

            if (args.Length == 0)
                return;

            if (args[0] == "gm")
            {
                session.SendPacket(new SMSG_GMTICKET_GETTICKET(TicketInfoResponse.Pending, "Hello my friend"));
                session.SendPacket(new SMSG_QUERY_TIME_RESPONSE());
                Console.WriteLine(Count);
                Count++;
            }

            if (args[0] == "npc")
            {
                var creature = DatabaseModel.CreaturesCollection.Find(x => x.Entry == int.Parse(args[1])).First();

                if (creature == null)
                    return;

                var unitTest = new SpawnCreatures
                {
                    Uid = Utils.GenerateRandUlong(),
                    Entry = creature.Entry,
                    SubMap = new SubMap
                    {
                        MapId = session.Character.SubMap.MapId,
                        MapZone = session.Character.SubMap.MapZone,
                        MapX = session.Character.SubMap.MapX,
                        MapY = session.Character.SubMap.MapY,
                        MapZ = session.Character.SubMap.MapZ,
                        MapO = session.Character.SubMap.MapO
                    }
                };

                DatabaseModel.SpawnCreaturesCollection.InsertOne(unitTest);

                //session.SendPacket(SMSG_UPDATE_OBJECT.CreateUnit(session.Character, creature));
            }

            // SHOW GPS
            if (args[0] == "gps")
            {
                session.SendMessageMotd(
                    $"MapX: {session.Character.SubMap.MapX} = MapY: {session.Character.SubMap.MapY} = " +
                    $"MapZ: {session.Character.SubMap.MapZ} = MapO: {session.Character.SubMap.MapO}"
                );
                session.SendMessageMotd($@"Players: {session.Entity.KnownPlayers.Count}");
            }

            // LEVEL
            if (args[0] == "level")
                session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, int.Parse(args[1]));

            // EMOTE
            if (args[0] == "emote")
                session.Entity.SetUpdateField((int) UnitFields.UNIT_NPC_EMOTESTATE, int.Parse(args[1]));

            if (args[0] == "inv1")
                foreach (var inventory in session.Character.SubInventorie)
                    session.SendPacket(SMSG_UPDATE_OBJECT.CreateItem(inventory, session.Entity));

            if (args[0] == "inv4")
            {
                Sessao = session;

                TimerPlugin = new Timer();
                TimerPlugin.Elapsed += CheckEvents;
                TimerPlugin.Interval = 1500;
                TimerPlugin.Enabled = true;
            }

            if (args[0] == "inv3")
                foreach (var inventory in session.Character.SubInventorie)
                {
                    session.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + inventory.Slot * 12,
                        inventory.Item);
                    session.Entity.SetUpdateField(
                        (int) PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + inventory.Slot * 12, 0);
                    session.Entity.SetUpdateField((int) PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + inventory.Slot * 2,
                        inventory.Item);
                }

            if (args[0] == "inv2")
                for (var j = 0; j < 112; j++)
                {
                    var inventory = session.Character.SubInventorie.Find(x => x.Slot == j);
                    if (inventory != null)
                    {
                        if (j < 19)
                        {
                            session.Entity.SetUpdateField(
                                (int) PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + inventory.Slot * 12, inventory.Item);
                            session.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + j * 12,
                                0);
                        }

                        session.Entity.SetUpdateField((int) PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2,
                            inventory.Item);
                    }
                    else
                    {
                        if (j < 19)
                        {
                            session.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + j * 12, 0);
                            session.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + j * 12,
                                0);
                        }

                        session.Entity.SetUpdateField((int) PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2, 0);
                    }
                }
        }

        private static void CheckEvents(object source, ElapsedEventArgs e)
        {
            var inventory = Sessao.Character.SubInventorie.Find(x => x.Slot == 15);

            Sessao.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + Count, inventory.Item);
            Sessao.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + Count, 0);
            Sessao.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_CREATOR + Count,
                Sessao.Character.Uid);
            Count++;
        }
    }
}