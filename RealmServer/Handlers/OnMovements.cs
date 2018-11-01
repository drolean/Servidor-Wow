using System;
using System.Threading.Tasks;
using Common.Globals;
using RealmServer.Database;
using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer;
using RealmServer.PacketServer.Global;
using RealmServer.World.Managers;

namespace RealmServer.Handlers
{
    public class OnMovements
    {
        internal static RealmServerRouter.ProcessRealmPacketCallback<MSG_MOVE_FALL_LAND> Handler(RealmEnums code)
        {
            return async delegate(RealmServerSession session, MSG_MOVE_FALL_LAND handler)
            {
                await TransmitMovement(session, handler, code);

                foreach (var player in PlayerManager.Players)
                {
                    if (player != session.Entity)
                    {
                        Console.WriteLine(player.Character.Name);
                    }

                    if (player.UpdateCount <= 0)
                        continue;

                    Common.Network.PacketServer packet = SMSG_UPDATE_OBJECT.UpdateValues(player);
                    player.Session.SendPacket(packet);
                    WorldManager.SessionsWhoKnow(player).ForEach(s => s.SendPacket(packet));

                    /*
                        // Ignore self
                        if (player == otherPlayer) continue;

                    if (InRangeCheck(player.Character.SubMap, otherPlayer.Character.SubMap))
                    {
                        if (!player.KnownPlayers.Contains(otherPlayer))
                            PlayerManager.SpawnPlayer(player, otherPlayer);
                    }
                    else
                    {
                        if (player.KnownPlayers.Contains(otherPlayer))
                            PlayerManager.DespawnPlayer(player, otherPlayer);
                    }
                    */
                }
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
