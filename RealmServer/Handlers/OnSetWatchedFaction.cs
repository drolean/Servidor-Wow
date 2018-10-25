using RealmServer.Database;
using RealmServer.Enums;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnSetWatchedFaction
    {
        public static void Handler(RealmServerSession session, CMSG_SET_WATCHED_FACTION handler)
        {
            if (handler.FactionId == -1)
                handler.FactionId = 0xff;

            if (handler.FactionId < 0 || handler.FactionId > 255)
                return;

            session.Entity.SetUpdateField((int) PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, handler.FactionId);
            session.Character.WatchFaction = handler.FactionId;
            Characters.UpdateCharacter(session.Character);
        }
    }
}