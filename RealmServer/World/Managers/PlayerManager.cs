using System.Collections.Generic;
using System.Threading;
using Common.Database;
using Common.Database.Tables;
using Common.Helpers;
using MongoDB.Driver;
using RealmServer.PacketServer;
using RealmServer.World.Enititys;

namespace RealmServer.World.Managers
{
    public class PlayerManager
    {
        private static readonly List<SpawnCreatures> Objetos =
            DatabaseModel.SpawnCreaturesCollection.Find(_ => true).ToList();

        internal static List<PlayerEntity> Players { get; set; }

        /// <summary>
        /// </summary>
        internal static void Boot()
        {
            Players = new List<PlayerEntity>();

            WorldManager.OnPlayerSpawn += OnPlayerSpawn;
            WorldManager.OnPlayerDespawn += OnPlayerDespawn;

            new Thread(Update).Start();

            Log.Print(LogType.RealmServer, "Loading PlayerManager ".PadRight(40, '.') + " [OK] ");
        }

        /// <summary>
        /// </summary>
        /// <param name="playerEntity"></param>
        private static void OnPlayerSpawn(PlayerEntity playerEntity)
        {
            Players.Add(playerEntity);
        }

        /// <summary>
        /// </summary>
        /// <param name="playerEntity"></param>
        private static void OnPlayerDespawn(PlayerEntity playerEntity)
        {
            Players.Remove(playerEntity);
        }

        /// <summary>
        /// </summary>
        /// <param name="remote"></param>
        /// <param name="playerEntity"></param>
        private static void DespawnPlayer(PlayerEntity remote, PlayerEntity playerEntity)
        {
            var despawnPlayer = new List<ObjectEntity> {playerEntity};
            remote.Session.SendPacket(SMSG_UPDATE_OBJECT.CreateOutOfRangeUpdate(despawnPlayer));
            remote.KnownPlayers.Remove(playerEntity);
        }

        /// <summary>
        /// </summary>
        /// <param name="remote"></param>
        /// <param name="playerEntity"></param>
        private static void SpawnPlayer(PlayerEntity remote, PlayerEntity playerEntity)
        {
            remote.Session.SendInventory(playerEntity.Session);
            remote.Session.SendPacket(SMSG_UPDATE_OBJECT.CreateCharacterUpdate(playerEntity.Character));
            remote.KnownPlayers.Add(playerEntity);
        }

        /// <summary>
        /// </summary>
        private static void Update()
        {
            while (true)
            {
                foreach (var player in Players)
                {
                    foreach (var otherPlayer in Players)
                    {
                        // Ignore self
                        if (player == otherPlayer) continue;

                        if (InRangeCheck(player.Character.SubMap, otherPlayer.Character.SubMap))
                        {
                            if (!player.KnownPlayers.Contains(otherPlayer))
                                SpawnPlayer(player, otherPlayer);
                        }
                        else
                        {
                            if (player.KnownPlayers.Contains(otherPlayer))
                                DespawnPlayer(player, otherPlayer);
                        }
                    }

                    foreach (var creature in Objetos)
                    {
                        if (InRangeCheck(player.Character.SubMap, creature.SubMap))
                        {
                            if (!player.KnownCreatures.Contains(creature))
                                SpawnCreatures(player, creature);
                        }
                        else
                        {
                            if (player.KnownCreatures.Contains(creature))
                                DespawnCreature(player, creature);
                        }
                    }

                    if (player.UpdateCount > 0)
                    {
                        Common.Network.PacketServer packet = SMSG_UPDATE_OBJECT.UpdateValues(player);
                        player.Session.SendPacket(packet);
                        WorldManager.SessionsWhoKnow(player).ForEach(s => s.SendPacket(packet));
                    }
                }

                // Fix????
                Thread.Sleep(100);
            }
        }

        private static void DespawnCreature(PlayerEntity player, SpawnCreatures creature)
        {
            var despawnCreature = new List<SpawnCreatures> { creature };
            player.Session.SendPacket(SMSG_UPDATE_OBJECT.CreateOutOfRangeUpdate(despawnCreature));
            player.KnownCreatures.Remove(creature);
        }

        private static bool InRangeCheck(SubMap character, SubMap entity)
        {
            var distance = Utils.GetDistance(character.MapX, character.MapY,
                entity.MapX, entity.MapY);

            return distance <= 30; // Config.Instance.RangeDistanceLimit;
        }

        /// <summary>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="creature"></param>
        private static void SpawnCreatures(PlayerEntity player, SpawnCreatures creature)
        {
            player.Session.SendPacket(SMSG_UPDATE_OBJECT.CreateUnit(creature));
            player.KnownCreatures.Add(creature);
        }

        /// <summary>
        /// </summary>
        /// <param name="playerEntityA"></param>
        /// <param name="playerEntityB"></param>
        /// <returns></returns>
        private static bool InRangeCheck(PlayerEntity playerEntityA, PlayerEntity playerEntityB)
        {
            var distance = Utils.GetDistance(playerEntityA.Character.SubMap.MapX, playerEntityA.Character.SubMap.MapY,
                playerEntityB.Character.SubMap.MapX, playerEntityB.Character.SubMap.MapY);

            return distance < Config.Instance.RangeDistanceLimit;
        }
    }
}
