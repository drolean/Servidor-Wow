using System.Linq;

namespace Common.Database.Dbc
{
    public class MapReader : DbcReader<Map>
    {
        public Map GetArea(int id)
        {
            return RecordDataIndexed.Values.ToArray().FirstOrDefault(a => a.MapId == id);
        }
    }
}