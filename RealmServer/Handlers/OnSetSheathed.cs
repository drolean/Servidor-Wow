using RealmServer.Enums;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnSetSheathed
    {
        public static void Handler(RealmServerSession session, CMSG_SETSHEATHED handler)
        {
            session.Character.SheathType = handler.Sheathed;
            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_2, handler.Sheathed);
            //session.Entity.SetUpdateField((int)UnitFields.UNIT_FIELD_BYTES_1, (StandStates)handler.Sheathed);
            //SetUpdateField((int)UnitFields.UNIT_FIELD_BYTES_2, 0);
        }
    }
}
