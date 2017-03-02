using RealmServer.Game.Entitys;

namespace RealmServer.Game.Managers
{
    public delegate void PlayerEvent(PlayerEntity playerEntity);

    internal class EntityManager
    {
        public static event PlayerEvent OnPlayerSpawn;

        internal static void DispatchOnPlayerSpawn(PlayerEntity entity)
        {
            OnPlayerSpawn?.Invoke(entity);
        }
    }
}
