using System.Linq;
using RealmServer.Database;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnSetFactionInactive
    {
        public static void Handler(RealmServerSession session, CMSG_SET_FACTION_INACTIVE handler)
        {
            if (handler.FactionId == -1)
                handler.FactionId = 0xff;

            if (handler.FactionId < 0 || handler.FactionId > 255)
                return;

            var factionSystem = MainProgram.FactionReader.GetFaction(handler.FactionId);

            if (factionSystem == null)
                return;

            var faction = session.Character.SubFactions.FirstOrDefault(d => d.Faction == factionSystem.FactionId);


            if (faction != null)
                faction.Flags = handler.Inactive == 1 ? 49 : 17;

            Characters.UpdateCharacter(session.Character);
        }
    }
}
