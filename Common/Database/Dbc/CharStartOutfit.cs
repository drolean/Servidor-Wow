namespace Common.Database.Dbc
{
    public class CharStartOutfit : DbcRecordBase
    {
        public uint Class;
        public uint Gender;
        public int Id;
        public int[] Items = new int[12];
        public uint Race;

        public override int Read()
        {
            Id = GetInt32(0);

            var tmp = GetUInt32(1);
            Race = tmp & 0xFF;
            Class = (tmp >> 8) & 0xFF;
            Gender = (tmp >> 16) & 0xFF;

            for (var i = 0; i < Items.Length; ++i)
                Items[i] = GetInt32(2 + i);

            return Id;
        }
    }
}