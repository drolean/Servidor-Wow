using System;
using System.Collections.Generic;
using System.Threading;
using Common.Helpers;
using Common.Network;
using RealmServer.Game.Entitys;

namespace RealmServer.Game.Managers
{
    internal class PlayerManager
    {
        public static int DistanciaFoda = 20;

        public static List<PlayerEntity> Players { get; private set; }

        internal static void Boot()
        {
            Players = new List<PlayerEntity>();

            EntityManager.OnPlayerSpawn += OnPlayerSpawn;
            EntityManager.OnPlayerDespawn += OnPlayerDespawn;

            new Thread(Update).Start();

            Log.Print(LogType.Loading, "PlayerManager Loaded ................. [OK]");
        }

        private static void Update()
        {
            while (true)
            {
                foreach (PlayerEntity player in Players)
                {
                    foreach (PlayerEntity otherPlayer in Players)
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

                    if (player.UpdateCount > 0)
                    {
                        PacketServer packet = UpdateObject.UpdateValues(player);
                        player.Session.SendPacket(packet);
                        EntityManager.SessionsWhoKnow(player).ForEach(s => s.SendPacket(packet));
                    }
                }

                // Fix????
                Thread.Sleep(100);
            }
        }

        private static void OnPlayerSpawn(PlayerEntity playerEntity)
        {
            Players.Add(playerEntity);
        }

        private static void OnPlayerDespawn(PlayerEntity playerEntity)
        {
            foreach (PlayerEntity remotePlayer in Players)
            {
                if (playerEntity == remotePlayer) continue;

                if (remotePlayer.KnownPlayers.Contains(playerEntity))
                    DespawnPlayer(remotePlayer, playerEntity);
            }

            Players.Remove(playerEntity);
        }

        private static void DespawnPlayer(PlayerEntity remote, PlayerEntity playerEntity)
        {
            List<ObjectEntity> despawnPlayer = new List<ObjectEntity> { playerEntity };
            remote.Session.SendPacket(UpdateObject.CreateOutOfRangeUpdate(despawnPlayer));
            remote.KnownPlayers.Remove(playerEntity);
        }

        private static void SpawnPlayer(PlayerEntity remote, PlayerEntity playerEntity)
        {
            remote.Session.SendPacket(UpdateObject.CreateCharacterUpdate(playerEntity.Character));
            remote.KnownPlayers.Add(playerEntity);
        }

        internal static bool InRangeCheck(PlayerEntity playerEntityA, PlayerEntity playerEntityB)
        {
            double distance = GetDistance(playerEntityA.Character.MapX, playerEntityA.Character.MapY, playerEntityB.Character.MapX, playerEntityB.Character.MapY);
            return distance < DistanciaFoda; // DISTANCE
        }

        internal static double GetDistance(float aX, float aY, float bX, float bY)
        {
            double a = aX - bX;
            double b = bY - aY;

            return Math.Sqrt(a * a + b * b);
        }
    }
}