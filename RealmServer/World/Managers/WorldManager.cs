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
    }
}