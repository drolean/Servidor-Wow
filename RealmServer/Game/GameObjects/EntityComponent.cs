using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RealmServer.Game.Entitys;
using RealmServer.Game.Managers;

namespace RealmServer.Game.GameObjects
{
    public abstract class EntityComponent<T> where T : BaseEntity
    {
        internal List<T> Entitys;
        internal abstract void GenerateEntitysForPlayer(PlayerEntity playerEntity);
        internal abstract bool InRange(PlayerEntity playerEntity, T entity, float range);
        internal abstract List<T> EntityListFromPlayer(PlayerEntity playerEntity);

        protected EntityComponent()
        {
            Entitys = new List<T>();
            new Thread(UpdateThread).Start();
            EntityManager.OnPlayerSpawn += World_OnPlayerSpawn;
        }

        private void World_OnPlayerSpawn(PlayerEntity playerEntity)
        {
            GenerateEntitysForPlayer(playerEntity);
        }

        private void UpdateThread()
        {
            while (true)
            {
                Update();
                Thread.Sleep(500);
            }
        }

        private bool Contains(T entity)
        {
            return Entitys.FindAll(e => (e as ObjectEntity).ObjectGuid.RawGuid == (entity as ObjectEntity).ObjectGuid.RawGuid).Any();
        }

        internal virtual void Update()
        {
            // Spawning && Despawning
            foreach (PlayerEntity player in PlayerManager.Players)
            {
                foreach (T entity in Entitys)
                {
                    if (InRange(player, entity, MainForm.DistanciaFoda) && !PlayerKnowsEntity(player, entity)) // DISTANCE
                        SpawnEntityForPlayer(player, entity);

                    if (!InRange(player, entity, MainForm.DistanciaFoda) && PlayerKnowsEntity(player, entity)) // DISTANCE
                        DespawnEntityForPlayer(player, entity);
                }
            }
        }

        internal virtual void DespawnEntityForPlayer(PlayerEntity playerEntity, T entity)
        {
            EntityListFromPlayer(playerEntity).Remove(entity);
        }

        internal virtual void SpawnEntityForPlayer(PlayerEntity playerEntity, T entity)
        {
            EntityListFromPlayer(playerEntity).Add(entity);
        }

        internal bool PlayerKnowsEntity(PlayerEntity playerEntity, T entity)
        {
            return EntityListFromPlayer(playerEntity).Contains(entity);
        }

        internal double GetDistance(float aX, float aY, float bX, float bY)
        {
            double a = aX - bX;
            double b = bY - aY;

            return Math.Sqrt(a * a + b * b);
        }

        internal virtual void AddEntityToWorld(T entity)
        {
            if (!Contains(entity))
                Entitys.Add(entity);
        }
    }
}