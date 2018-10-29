using RealmServer.Enums;

namespace RealmServer.Handlers
{
    public class OnTogglePvp
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            session.Character.IsPvP = !session.Character.IsPvP;
            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS,
                session.Character.IsPvP ? UnitFlags.UNIT_FLAG_PVP : UnitFlags.UNIT_FLAG_NON_PVP_PLAYER);
        }
    }
}
