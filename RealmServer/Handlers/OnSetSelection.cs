using System.Linq;
using RealmServer.Enums;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnSetSelection
    {
        public static void Handler(RealmServerSession session, CMSG_SET_SELECTION handler)
        {
            if (handler.TargetUid == 0)
                return;

            session.Character.Target = session.Entity.KnownPlayers
                .FirstOrDefault(s => s.Character.Uid == handler.TargetUid)?.Character;
            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_TARGET, handler.TargetUid);
        }
    }
}