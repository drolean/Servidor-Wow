namespace Common.Database.Dbc
{
    public class AreaTable : DbcRecordBase
    {
        public int AreaExploreFlag;
        public int AreaId;
        public int AreaLevel;
        public int AreaMapId;
        public string AreaName;
        public int AreaZone;
        public int AreaZoneType;

        public override int Read()
        {
            AreaId = GetInt32(0);
            AreaMapId = GetInt32(1); // May be needed in the future
            AreaZone = GetInt32(2);
            AreaExploreFlag = GetInt32(3);
            AreaZoneType = GetInt32(4);
            AreaLevel = GetInt32(10);
            AreaName = GetString(11);

            if (AreaLevel > 255)
                AreaLevel = 255;

            if (AreaLevel < 0)
                AreaLevel = 0;

            return AreaId;
        }
    }
}