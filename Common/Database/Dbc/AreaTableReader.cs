using System.Linq;

namespace Common.Database.Dbc
{
    public class AreaTableReader : DbcReader<AreaTable>
    {
        public AreaTable GetArea(int id)
        {
            return RecordDataIndexed.Values.ToArray().FirstOrDefault(a => a.AreaId == id);
        }
    }
}