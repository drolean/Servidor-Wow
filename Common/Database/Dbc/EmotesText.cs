namespace Common.Database.Dbc
{
    public class EmotesText : DbcRecordBase
    {
        public int EmoteId;
        public string EmoteName;
        public int Id;
        public int[] MEmoteText = new int[16];

        public override int Read()
        {
            Id = GetInt32(0);
            EmoteName = GetString(1);
            EmoteId = GetInt32(2);

            for (var i = 0; i < MEmoteText.Length; ++i)
                MEmoteText[i] = GetInt32(2 + i);

            return Id;
        }
    }
}