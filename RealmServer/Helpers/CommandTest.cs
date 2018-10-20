using System.Timers;
using RealmServer.Enums;
using RealmServer.PacketServer;
using Timer = System.Timers.Timer;

namespace RealmServer.Helpers
{
    public class CommandTest
    {
        public static int Count;
        public static Timer TimerPlugin;
        public static RealmServerSession sessao;

        private static void CheckEvents(object source, ElapsedEventArgs e)
        {
            var inventory = sessao.Character.SubInventorie.Find(x => x.Slot == 15);

            sessao.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + Count, inventory.Item);
            sessao.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + Count, 0);
            sessao.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_CREATOR + Count, sessao.Character.Uid);
            Count++;
        }

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

            // LEVEL
            if (args[0] == "level")
                session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, int.Parse(args[1]));

            // EMOTE
            if (args[0] == "emote")
                session.Entity.SetUpdateField((int) UnitFields.UNIT_NPC_EMOTESTATE, int.Parse(args[1]));

            if (args[0] == "inv1")
            {
                // Generate Inventory
                foreach (var inventory in session.Character.SubInventorie)
                {
                    session.SendPacket(SMSG_UPDATE_OBJECT.CreateItem(inventory, session.Entity));
                }
            }

            if (args[0] == "inv4")
            {
                sessao = session;

                TimerPlugin = new Timer();
                TimerPlugin.Elapsed += CheckEvents;
                TimerPlugin.Interval = 1500;
                TimerPlugin.Enabled = true;
            }

            if (args[0] == "inv3")
            {
                foreach (var inventory in session.Character.SubInventorie)
                {
                    session.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + inventory.Slot * 12, inventory.Item);
                    session.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + inventory.Slot * 12, 0);
                    session.Entity.SetUpdateField((int)PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + inventory.Slot * 2, inventory.Item);
                }
            }

            if (args[0] == "inv2")
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