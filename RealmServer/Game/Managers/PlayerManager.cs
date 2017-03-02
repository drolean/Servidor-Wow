using System.Collections.Generic;
using System.Threading;
using Common.Helpers;
using Common.Network;
using RealmServer.Game.Entitys;

namespace RealmServer.Game.Managers
{
    internal class PlayerManager
    {
        public static List<PlayerEntity> Players { get; private set; }

        internal static void Boot()
        {
            Players = new List<PlayerEntity>();

            EntityManager.OnPlayerSpawn += OnPlayerSpawn;
            EntityManager.OnPlayerDespawn += OnPlayerDespawn;

            new Thread(Update).Start();

            Log.Print(LogType.Loading, $"PlayerManager Loaded ................. [OK]");
        }

        private static void Update()
        {
            while (true)
            {
                foreach (PlayerEntity player in Players)
                {
                    if (player.UpdateCount > 0)
                    {
                        PacketServer packet = UpdateObject.UpdateValues(player);
                        player.Session.SendPacket(packet);
                        //EntityManager.SessionsWhoKnow(player).ForEach(s => s.SendPacket(packet));
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

            // Should be sending playerEntity entityEntity
            remote.Session.SendPacket(UpdateObject.CreateOutOfRangeUpdate(despawnPlayer));

            // Add it to known players
            remote.KnownPlayers.Remove(playerEntity);
        }
    }
}