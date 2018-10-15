using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnSetFactionAtWar
    {
        public static void Handler(RealmServerSession session, CMSG_SET_FACTION_ATWAR handler)
        {
            /*
            // [7 Enabled] --- [5 Disabled]
            MainProgram.Database.FactionInative(session.Character.Id, faction + 1, (byte)(enabled == 1 ? 7 : 5));

            var factionDb = MainProgram.Database.FactionGet(session.Character, faction + 1);

            session.SendPacket(new SMSG_SET_FACTION_STANDING(faction, enabled, factionDb.standing));
            */
        }
    }
}