namespace Common.Database.Dbc
{
    public class ChrRaces : DbcRecordBase
    {
        public int CinematicId;
        public int FactionId;
        public int ModelF;
        public int ModelM;
        public string Name;
        public int RaceId;
        public uint TaxiMask;
        public int TeamId; //1 = Horde / 7 = Alliance

        public override int Read()
        {
            RaceId = GetInt32(0);
            FactionId = GetInt32(2);
            ModelM = GetInt32(4);
            ModelF = GetInt32(5);
            TeamId = GetInt32(8);
            TaxiMask = GetUInt32(14);
            CinematicId = GetInt32(16);
            Name = GetString(17);

            return RaceId;
        }
    }
}