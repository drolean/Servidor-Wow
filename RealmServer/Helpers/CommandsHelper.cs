using System;
using System.Linq;
using System.Threading;
using Common.Database;
using Common.Globals;
using RealmServer.Game;
using RealmServer.Handlers;

namespace RealmServer.Helpers
{
    internal class CommandsHelper
    {
        public static int Aba = 17;
        public static UnitFlags Value = UnitFlags.UNIT_FLAG_NONE;
        public CommandsHelper(RealmServerSession session, string message)
        {
            string[] splitMessage = message.Split(' ');

            Console.WriteLine($@"[Comando]: {splitMessage[0].ToLower()}");

            if(splitMessage[0].ToLower() == "db")
                XmlReader.Boot();

            if (splitMessage[0].ToLower() == "obj")
                session.SendPacket(UpdateObject.CreateGameObject(session.Character.MapX, session.Character.MapY, session.Character.MapZ));

            if (splitMessage[0].ToLower() == "unt")
                session.SendPacket(UpdateObject.CreateUnit(session.Character.MapX, session.Character.MapY, session.Character.MapZ, session.Character.MapO));

            if (splitMessage[0].ToLower() == "item")
            {
                Console.WriteLine($@"Veio item aqui {Aba} => {int.Parse(splitMessage[1].ToLower())}");
                MainProgram.Database.ItemUpdate(int.Parse(splitMessage[1].ToLower()));

                Thread.Sleep(1500);

                var inventory = MainProgram.Database.GetInventory(session.Character);
                for (int j = 0; j < 112; j++)
                {
                    if (inventory.Find(item => item.slot == j) != null)
                    {
                        if (j < 19)
                        {
                            session.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + (int)inventory.Find(item => item.slot == j).slot * 12, inventory.Find(item => item.slot == j).item);
                            session.Entity.SetUpdateField((int)PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + j * 12, 0);
                        }

                        session.Entity.SetUpdateField((int)PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2, inventory.Find(item => item.slot == j).item);

                        session.SendPacket(UpdateObject.CreateItem(inventory.Find(item => item.slot == j), session.Character));
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
                Aba++;
            }

            if (splitMessage[0].ToLower() == "gps")
            {
                session.SendMessageMotd(
                    $"MapX: {session.Character.MapX} = MapY: {session.Character.MapY} = MapZ: {session.Character.MapZ} = MapO: {session.Character.MapO}");
                Console.WriteLine($@"MapX: {session.Character.MapX} = MapY: {session.Character.MapY} = MapZ: {session.Character.MapZ} = MapO: {session.Character.MapO}");
                Console.WriteLine(@"----------------------------");
                Console.WriteLine($@"Players: {session.Entity.KnownPlayers.Count}");
                Console.WriteLine($@"Objects: {session.Entity.KnownGameObjects.Count} ");
                // Creatures
                // Corpses
                // You are seen by:
            }

            if (splitMessage[0].ToLower() == "a")
            {
                //session.SendPacket(new SmsgSetRestStart());
                session.SendPacket(new SmsgTriggerCinematic(int.Parse(splitMessage[1].ToLower())));
            }

            if (splitMessage[0].ToLower() == "c")
            {
                session.Entity.SetUpdateField((int) UnitFields.UNIT_NPC_EMOTESTATE, int.Parse(splitMessage[1].ToLower()));
            }

            if (splitMessage[0].ToLower() == "b")
            {
                // Disable Movement
                session.Entity.SetUpdateField((int)UnitFields.UNIT_FIELD_FLAGS, UnitFlags.UNIT_FLAG_STUNTED); // UNIT_FLAG_STUNTED
                Console.WriteLine($@"Atual: [{Value}] NEW {Enum.GetValues(typeof(UnitFlags)).Cast<UnitFlags>().SkipWhile(e => e != Value).Skip(1).First()}");
                Value = Enum.GetValues(typeof(UnitFlags)).Cast<UnitFlags>().SkipWhile(e => e != Value).Skip(1).First();

                // StandState -> Sit 
                session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_1, 1); //StandStates.STANDSTATE_SIT);

                Thread.Sleep(1000);

                session.SendPacket(new SmsgStandstateUpdate(1));

                Console.WriteLine(Aba);
                Aba++;
            }

            if (splitMessage[0].ToLower() == "emote")
            {
                session.SendPacket(new SmsgTextEmote((int) session.Entity.ObjectGuid.RawGuid,
                    Convert.ToUInt32(splitMessage[2]), Convert.ToInt32(splitMessage[1])));
            }

            if (splitMessage[0].ToLower() == "vai")
            {
                string attributeName = splitMessage[1].ToLower();
                string attributeValue = splitMessage[2];

                switch (attributeName)
                {
                    case "l":
                        session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, int.Parse(attributeValue));
                        break;

                    case "s":
                        session.Entity.Scale = float.Parse(attributeValue);
                        break;

                    case "g":
                        session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0,
                            (byte) int.Parse(attributeValue), 2);
                        break;

                    case "m":
                        session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, int.Parse(attributeValue));
                        break;

                    case "e":
                        session.Entity.SetUpdateField((int) UnitFields.UNIT_NPC_EMOTESTATE,
                            (byte) int.Parse(attributeValue));
                        break;
                }
            }
        }
    }
}
 