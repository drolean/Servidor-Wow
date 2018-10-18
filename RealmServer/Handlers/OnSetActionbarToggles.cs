using RealmServer.Enums;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnSetActionbarToggles
    {
        public static void Handler(RealmServerSession session, CMSG_SET_ACTIONBAR_TOGGLES handler)
        {
            session.Entity.SetUpdateField((int) PlayerFields.PLAYER_FIELD_BYTES, 2, handler.ActionBar);
        }
    }
}