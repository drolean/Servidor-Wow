using System.Collections.Generic;
using RealmServer.World.Enititys;

namespace RealmServer.World.Managers
{
    public class WorldManager
    {
        public delegate void PlayerEvent(PlayerEntity playerEntity);

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
            var sessions = PlayersWhoKnow(playerEntity).ConvertAll(p => p.Session);

            if (includeSelf) sessions.Add(playerEntity.Session);

            return sessions;
        }
    }
}