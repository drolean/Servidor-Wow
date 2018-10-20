using System.Threading.Tasks;
using Common.Globals;
using RealmServer.Database;
using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer.Global;

namespace RealmServer.Handlers
{
    public class OnMovements
    {
        internal static RealmServerRouter.ProcessRealmPacketCallback<MSG_MOVE_FALL_LAND> Handler(RealmEnums code)
        {
            return async delegate(RealmServerSession session, MSG_MOVE_FALL_LAND handler)
            {
                await TransmitMovement(session, handler, code);
            };
        }

        private static async Task TransmitMovement(RealmServerSession session, MSG_MOVE_FALL_LAND handler,
            RealmEnums code)
        {
            session.Character.SubMap.MapX = handler.MapX;
            session.Character.SubMap.MapY = handler.MapY;
            session.Character.SubMap.MapZ = handler.MapZ;
            session.Character.SubMap.MapO = handler.MapR;

            await Characters.UpdateMovement(session.Character);

            session.Entity.SetUpdateField((int) UnitFields.UNIT_NPC_EMOTESTATE, 0);

            session.Entity.KnownPlayers.ForEach(s => s.Session.SendPacket(new PsMovement(session, handler, code)));
        }
    }
}