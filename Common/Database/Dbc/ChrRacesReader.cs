using System.Linq;
using Common.Globals;

namespace Common.Database.Dbc
{
    public class ChrRacesReader : DbcReader<ChrRaces>
    {
        public ChrRaces GetData(Races id)
        {
            return RecordDataIndexed.Values.ToArray().FirstOrDefault(a => a.RaceId == (int) id);
        }
    }
}