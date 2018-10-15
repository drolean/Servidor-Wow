namespace Common.Database.Dbc
{
    public class Map : DbcRecordBase
    {
        public int MapId;
        public string MapName;
        public string MapString;

        public override int Read()
        {
            MapId = GetInt32(0);
            MapName = GetString(1);
            MapString = GetString(4);

            return MapId;
        }
    }
}