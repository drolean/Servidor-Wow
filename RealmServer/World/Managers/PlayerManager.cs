using System;
using System.Collections.Generic;
using System.Threading;
using Common.Helpers;
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

        private static void SpawnPlayer(PlayerEntity remote, PlayerEntity playerEntity)
        {
            remote.Session.SendPacket(SMSG_UPDATE_OBJECT.CreateCharacterUpdate(playerEntity.Character));
            remote.KnownPlayers.Add(playerEntity);
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

                        if (!player.KnownPlayers.Contains(otherPlayer))
                            SpawnPlayer(player, otherPlayer);
                    }

                    if (player.UpdateCount > 0)
                    {
                        Common.Network.PacketServer packet = SMSG_UPDATE_OBJECT.UpdateValues(player);
                        player.Session.SendPacket(packet);
                    }
                }

                // Fix????
                Thread.Sleep(100);
            }
        }
    }
}