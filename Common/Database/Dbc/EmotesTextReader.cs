using System.Linq;

namespace Common.Database.Dbc
{
    public class EmotesTextReader : DbcReader<EmotesText>
    {
        public static EmotesText GetData(int id)
        {
            return RecordDataIndexed.Values.ToArray().FirstOrDefault(a => a.Id == id);
        }
    }
}