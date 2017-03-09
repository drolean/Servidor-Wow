using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Helpers;
using Common.Network;
using RealmServer.Game.Entitys;
using Common.Database.Xml;

namespace RealmServer.Game.Managers
{
    internal class PlayerManager
    {
        // Internal
        internal static List<PlayerEntity> Players { get; set; }

        internal static void Boot()
        {
            Players = new List<PlayerEntity>();

            WorldManager.OnPlayerSpawn += OnPlayerSpawn;
            WorldManager.OnPlayerDespawn += OnPlayerDespawn;

            new Thread(Update).Start();

            Log.Print(LogType.Loading, "PlayerManager Loaded ................. [OK]");
        }

        private static bool InRangeCheck(PlayerEntity playerEntityA, PlayerEntity playerEntityB)
        {
            double distance = GetDistance(playerEntityA.Character.MapX, playerEntityA.Character.MapY, playerEntityB.Character.MapX, playerEntityB.Character.MapY);
            return distance < MainForm.DistanciaFoda; // DISTANCE
        }

        private static double GetDistance(float aX, float aY, float bX, float bY)
        {
            double a = aX - bX;
            double b = bY - aY;

            return Math.Sqrt(a * a + b * b);
        }

        private static void Update()
        {
            while (true)
            {
                foreach (PlayerEntity player in Players)
                {
                    #region Checa Players
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
                    #endregion

                    #region Checa Objetos para adicionar ao mapa
                    var objetos = MainForm.Database.GetGameObjects(player, MainForm.DistanciaFoda);
                    if (objetos.Any())
                    {
                        foreach (zoneObjeto objeto in objetos)
                        {
                            if (!player.KnownGameObjects.Contains(objeto)) 
                                SpawnObjeto(player, objeto);
                        }
                    }
                    #endregion 

                    // NPC Spawns

                    // Atualização de coisas do jogo
                    if (player.UpdateCount > 0)
                    {
                        PacketServer packet = UpdateObject.UpdateValues(player);
                        player.Session.SendPacket(packet);
                        WorldManager.SessionsWhoKnow(player).ForEach(s => s.SendPacket(packet));
                    }
                }

                // Fix????
                Thread.Sleep(100);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        // Objeto

        private static void SpawnObjeto(PlayerEntity player, zoneObjeto objeto)
        {
            player.Session.SendPacket(UpdateObject.CreateGameObject(objeto));
            player.KnownGameObjects.Add(objeto);
        }

        // Player
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
    }
}