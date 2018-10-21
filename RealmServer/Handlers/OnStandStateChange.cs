using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnStandStateChange
    {
        public static void Handler(RealmServerSession session, CMSG_STANDSTATECHANGE handler)
        {
            session.Character.StandState = handler.StandState;
            session.Entity.SetUpdateField((int)UnitFields.UNIT_FIELD_BYTES_1, (StandStates) handler.StandState);
            session.SendPacket(new SMSG_STANDSTATE_UPDATE(handler.StandState));
        }
    }
}