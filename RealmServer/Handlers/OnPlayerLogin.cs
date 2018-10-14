using RealmServer.Database;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnPlayerLogin
    {
        public static void Handler(RealmServerSession session, CMSG_PLAYER_LOGIN handler)
        {
            session.Character = Characters.GetCharacter(handler);

            session.SendPacket(new SMSG_LOGIN_VERIFY_WORLD(session.Character));
            session.SendPacket(new SMSG_ACCOUNT_DATA_TIMES());

            session.SendPacket(new SMSG_SET_REST_START(1000));
            session.SendPacket(new SMSG_BINDPOINTUPDATE(session.Character));
            session.SendPacket(new SMSG_TUTORIAL_FLAGS());

            session.SendPacket(new SMSG_LOGIN_SETTIMESPEED());
            session.SendPacket(new SMSG_CORPSE_RECLAIM_DELAY());

            session.SendPacket(new SMSG_INIT_WORLD_STATES(session.Character));

            session.SendPacket(SMSG_UPDATE_OBJECT.CreateOwnCharacterUpdate(session.Character, out session.Entity));
            //session.Entity.Session = session;
        }
    }
}