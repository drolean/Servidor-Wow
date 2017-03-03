using System.Collections.Generic;
using RealmServer.Game.Entitys;

namespace RealmServer.Game.GameObjects
{
    public class GameObjectComponent : EntityComponent<GameObjectEntity>
    {
        internal override void GenerateEntitysForPlayer(PlayerEntity playerEntity)
        {
            /*
            List<WorldGameObjects> gameObjects = Main.Database.GetGameObjects(playerEntity, 1000); // DISTANCE

            gameObjects.ForEach(closeGo =>
            {
                AddEntityToWorld(new GameObjectEntity(closeGo));
            });
            */
        }

        internal override bool InRange(PlayerEntity playerEntity, GameObjectEntity entity, float range)
        {
            double distance = GetDistance(playerEntity.Character.MapX, playerEntity.Character.MapY,
                playerEntity.Character.MapX, playerEntity.Character.MapY);
            //entity.GameObjects.mapX, entity.GameObjects.mapY);
            return distance < 30; // DISTANCE
        }

        internal override List<GameObjectEntity> EntityListFromPlayer(PlayerEntity playerEntity)
        {
            return playerEntity.KnownGameObjects;
        }
    }
}