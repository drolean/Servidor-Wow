using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnZoneUpdate
    {
        public static void Handler(RealmServerSession session, CMSG_ZONEUPDATE handler)
        {
            session.Character.SubMap.MapZone = (int) handler.ZoneiD;
        }
    }
}