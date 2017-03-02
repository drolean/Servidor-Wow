using System.Collections.Generic;
using Common.Helpers;
using RealmServer.Game.Entitys;

namespace RealmServer.Game.Managers
{
    internal class PlayerManager
    {
        public static List<PlayerEntity> Players { get; private set; }

        internal static void Boot()
        {
            Players = new List<PlayerEntity>();

            Log.Print(LogType.Loading, $"PlayerManager Loaded ................. [OK]");
        }
    }
}
