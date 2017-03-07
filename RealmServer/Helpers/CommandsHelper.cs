using System;
using System.Linq;
using System.Threading;
using Common.Globals;
using RealmServer.Handlers;

namespace RealmServer.Helpers
{
    internal class CommandsHelper
    {
        public static int aba = 0;
        public static UnitFlags Value = UnitFlags.UNIT_FLAG_NONE;
        public CommandsHelper(RealmServerSession session, string message)
        {
            string[] splitMessage = message.Split(' ');

            Console.WriteLine($@"[Comando]: {splitMessage[0].ToLower()}");

            if (splitMessage[0].ToLower() == "gps")
            {
                session.SendMessageMotd(
                    $"MapX: {session.Character.MapX} = MapY: {session.Character.MapY} = MapZ: {session.Character.MapZ} = MapO: {session.Character.MapO}");
                Console.WriteLine($@"MapX: {session.Character.MapX} = MapY: {session.Character.MapY} = MapZ: {session.Character.MapZ} = MapO: {session.Character.MapO}");
                Console.WriteLine($@"----------------------------");
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

                session.SendPacket(new SmsgStandstateUpdate((byte) 1));

                Console.WriteLine(aba);
                aba++;
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

                    default:
                        break;
                }
            }
        }
    }
}
 