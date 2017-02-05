using System;
using Shaolinq;

namespace Common.Database.Tables
{
    [DataAccessObject]
    public abstract class CharactersSpells : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        public override int Id { get; set; }

        [PersistedMember]
        public abstract Characters character { get; set; }

        [PersistedMember]
        public abstract int spell { get; set; }

        [PersistedMember]
        public abstract int active { get; set; }

        [PersistedMember]
        public abstract int cooldown { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }
    }
}
