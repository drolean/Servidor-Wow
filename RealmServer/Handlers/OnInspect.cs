using System.Linq;
using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnInspect
    {
        public static void Handler(RealmServerSession session, CMSG_INSPECT handler)
        {
            if (handler.PlayerUid == 0)
                return;

            session.Character.Target = session.Entity.KnownPlayers
                .FirstOrDefault(s => s.Character.Uid == handler.PlayerUid)?.Character;

            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_TARGET, handler.PlayerUid);

            session.SendPacket(new SMSG_INSPECT(handler.PlayerUid));
        }
    }
}