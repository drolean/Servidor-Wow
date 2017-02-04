using Shaolinq;

namespace Common.Database.Tables
{
    [DataAccessObject]
    public abstract class CharactersFactions : DataAccessObject<int>
    {
        [AutoIncrement] 
        [PersistedMember]
        public override int Id { get; set; }

        [PersistedMember]
        public abstract Users user { get; set; }

        [PersistedMember]
        public abstract int faction { get; set; }

        [PersistedMember]
        public abstract int standing { get; set; }

        [PersistedMember]
        public abstract int flags { get; set; }
    }
}
