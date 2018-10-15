using System.Linq;
using Common.Globals;

namespace Common.Database.Dbc
{
    public class CharStartOutfitReader : DbcReader<CharStartOutfit>
    {
        public CharStartOutfit Get(Classes classe, Races race, Genders gender)
        {
            return
                RecordDataIndexed.Values.ToArray()
                    .FirstOrDefault(c => c.Class == (int) classe && c.Race == (int) race && c.Gender == (int) gender);
        }
    }
}