using System.Collections.Generic;
using RealmServer.Game.Entitys;

namespace RealmServer.Game.Managers
{
    internal delegate void PlayerEvent(PlayerEntity playerEntity);

    internal class EntityManager
    {
        public static event PlayerEvent OnPlayerSpawn;
        public static event PlayerEvent OnPlayerDespawn;

        internal static void DispatchOnPlayerSpawn(PlayerEntity entity)
        {
            OnPlayerSpawn?.Invoke(entity);
        }

        internal static void DispatchOnPlayerDespawn(PlayerEntity playerEntity)
        {
            OnPlayerDespawn?.Invoke(playerEntity);
        }

        private static List<PlayerEntity> PlayersWhoKnow(PlayerEntity playerEntity)
        {
            return PlayerManager.Players.FindAll(p => p.KnownPlayers.Contains(playerEntity));
        }

        internal static List<RealmServerSession> SessionsWhoKnow(PlayerEntity playerEntity, bool includeSelf = false)
        {
            List<RealmServerSession> sessions = PlayersWhoKnow(playerEntity).ConvertAll(p => p.Session);

            if (includeSelf) sessions.Add(playerEntity.Session);

            return sessions;
        }
    }
}