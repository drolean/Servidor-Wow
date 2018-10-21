using System.Collections.Generic;
using System.Linq;
using Common.Globals;

namespace Common.Database.Dbc
{
    public class FactionReader : DbcReader<Faction>
    {
        public Faction GetFaction(int id)
        {
            return RecordDataIndexed.Values.ToArray().FirstOrDefault(a => a.FactionFlag == id);
        }

        public List<string> GenerateFactions(Races race)
        {
            var listReturn = new List<string>();

            for (var i = 0; i < 63; i++)
            {
                var faction = GetFaction(i);

                if (faction == null) continue;

                for (var flags = 0; flags < 4; flags++)
                    if (HaveFlag(faction.Flags[flags], race - 1))
                        listReturn.Add(
                            $"{faction.FactionId}, {faction.ReputationFlags[flags]}, {faction.ReputationStats[flags]}");

            }

            return listReturn;
        }

        private static bool HaveFlag(int value, Races race)
        {
            value = value >> (int) race;
            value = value % 2;

            if (value == 1) return true;

            return false;
        }
    }
}