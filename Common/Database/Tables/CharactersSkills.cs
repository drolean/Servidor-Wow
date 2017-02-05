using System;
using Shaolinq;

namespace Common.Database.Tables
{
    [DataAccessObject]
    public abstract class CharactersSkills : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        public override int Id { get; set; }

        [PersistedMember]
        public abstract Characters character { get; set; }

        [PersistedMember]
        public abstract int skill { get; set; }

        [PersistedMember]
        public abstract int value { get; set; }

        [PersistedMember]
        public abstract int max { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }
    }
}
