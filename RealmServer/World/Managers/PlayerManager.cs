using System;
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
        internal static List<PlayerEntity> Players { get; set; }

        internal static void Boot()
        {
            Players = new List<PlayerEntity>();

            WorldManager.OnPlayerSpawn += OnPlayerSpawn;
            WorldManager.OnPlayerDespawn += OnPlayerDespawn;

            new Thread(Update).Start();

            Log.Print(LogType.RealmServer, "Loading PlayerManager ".PadRight(40, '.') + " [OK] ");
        }

        private static void OnPlayerSpawn(PlayerEntity playerEntity)
        {
            Players.Add(playerEntity);
        }

        private static void OnPlayerDespawn(PlayerEntity playerEntity)
        {
            Players.Remove(playerEntity);
        }

        private static void DespawnPlayer(PlayerEntity remote, PlayerEntity playerEntity)
        {
            var despawnPlayer = new List<ObjectEntity> {playerEntity};
            remote.Session.SendPacket(SMSG_UPDATE_OBJECT.CreateOutOfRangeUpdate(despawnPlayer));
            remote.KnownPlayers.Remove(playerEntity);
        }

        private static void SpawnPlayer(PlayerEntity remote, PlayerEntity playerEntity)
        {
            remote.Session.SendInventory(playerEntity.Session);
            remote.Session.SendPacket(SMSG_UPDATE_OBJECT.CreateCharacterUpdate(playerEntity.Character));
            remote.KnownPlayers.Add(playerEntity);
        }

        static List<SpawnCreatures> objetos = DatabaseModel.SpawnCreaturesCollection.Find(_ => true).ToList();

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

                        if (InRangeCheck(player, otherPlayer))
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

                    foreach (var objeto in objetos)
                    {
                        if (!player.KnownCreatures.Contains(objeto))
                            SpawnObjeto(player, objeto);
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

        private static void SpawnObjeto(PlayerEntity player, SpawnCreatures objeto)
        {
            Console.WriteLine($"Fazendo Spawn do NPC: {objeto.Uid}");
            player.Session.SendPacket(SMSG_UPDATE_OBJECT.CreateUnit(objeto));
            player.KnownCreatures.Add(objeto);
        }

        private static bool InRangeCheck(PlayerEntity playerEntityA, PlayerEntity playerEntityB)
        {
            var distance = GetDistance(playerEntityA.Character.SubMap.MapX, playerEntityA.Character.SubMap.MapY,
                playerEntityB.Character.SubMap.MapX, playerEntityB.Character.SubMap.MapY);

            return distance < 30; // DISTANCE
        }

        private static double GetDistance(float aX, float aY, float bX, float bY)
        {
            double a = aX - bX;
            double b = bY - aY;

            return Math.Sqrt(a * a + b * b);
        }
    }
}