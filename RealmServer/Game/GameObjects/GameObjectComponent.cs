using System;
using System.Collections.Generic;
using Common.Database.Xml;
using RealmServer.Game.Entitys;

namespace RealmServer.Game.GameObjects
{
    public class GameObjectComponent : EntityComponent<GameObjectEntity>
    {
        internal override void GenerateEntitysForPlayer(PlayerEntity playerEntity)
        {
            Console.WriteLine(@"GenerateEntitysForPlayer");
            List<zoneObjeto> gameObjects = MainForm.Database.GetGameObjects(playerEntity, 1000); // DISTANCE

            gameObjects.ForEach(closeGo =>
            {
                Console.WriteLine(closeGo.id);
                //AddEntityToWorld(new GameObjectEntity(closeGo));
                playerEntity.Session.SendPacket(UpdateObject.CreateGameObject(closeGo));
            });
        }

        internal override bool InRange(PlayerEntity playerEntity, GameObjectEntity entity, float range)
        {
            double distance = GetDistance(playerEntity.Character.MapX, playerEntity.Character.MapY,
                playerEntity.Character.MapX, playerEntity.Character.MapY);
            return distance < 30;
        }

        internal override List<GameObjectEntity> EntityListFromPlayer(PlayerEntity playerEntity)
        {
            return playerEntity.KnownGameObjects;
        }
    }
}