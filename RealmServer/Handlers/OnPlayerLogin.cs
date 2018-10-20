using RealmServer.Database;
using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer;
using RealmServer.World.Managers;

namespace RealmServer.Handlers
{
    public class OnPlayerLogin
    {
        public static void Handler(RealmServerSession session, CMSG_PLAYER_LOGIN handler)
        {
            session.Character = Characters.GetCharacter(handler);

            session.SendPacket(new SMSG_LOGIN_VERIFY_WORLD(session.Character));
            session.SendPacket(new SMSG_ACCOUNT_DATA_TIMES());
            session.SendMessageMotd("Welcome to World of Warcraft."); // DONE
            session.SendMessageMotd("Server uptime:"); // DONE
            // SMSG_UPDATE_ACCOUNT_DATA

            session.SendPacket(new SMSG_SET_REST_START(1000));
            session.SendPacket(new SMSG_BINDPOINTUPDATE(session.Character));
            session.SendPacket(new SMSG_TUTORIAL_FLAGS());
            session.SendPacket(new SMSG_LOGIN_SETTIMESPEED());
            session.SendPacket(new SMSG_INITIAL_SPELLS(session.Character));
            session.SendPacket(new SMSG_ACTION_BUTTONS(session.Character));

            if (session.Character.Cinematic == false)
            {
                var chrRaces = MainProgram.ChrRacesReader.GetData(session.Character.Race);
                session.SendPacket(new SMSG_TRIGGER_CINEMATIC(chrRaces.CinematicId));
            }

            session.SendPacket(new SMSG_CORPSE_RECLAIM_DELAY());
            session.SendPacket(new SMSG_INIT_WORLD_STATES(session.Character));
            session.SendPacket(SMSG_UPDATE_OBJECT.CreateOwnCharacterUpdate(session.Character, out session.Entity));

            foreach (var inventory in session.Character.SubInventorie)
                session.SendPacket(SMSG_UPDATE_OBJECT.CreateItem(inventory, session.Entity));

            Inventory(session);

            session.Entity.Session = session;
            WorldManager.DispatchOnPlayerSpawn(session.Entity);
        }

        private static void Inventory(RealmServerSession session)
        {
            for (int j = 0; j < 112; j++)
            {
                var inventory = session.Character.SubInventorie.Find(x => x.Slot == j);
                if (inventory != null)
                {
                    if (j < 19)
                    {
                        session.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + inventory.Slot * 12,
                            inventory.Item);
                        session.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + j * 12, 0);
                    }

                    session.Entity.SetUpdateField((int) PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2,
                        inventory.Item);
                }
                else
                {
                    if (j < 19)
                    {
                        session.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + j * 12, 0);
                        session.Entity.SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + j * 12, 0);
                    }

                    session.Entity.SetUpdateField((int) PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2, 0);
                }
            }
        }
    }
}