using System;
using Shaolinq;

namespace Common.Database.Tables
{
    [DataAccessObject]
    public abstract class CharactersActionBars : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        public override int Id { get; set; }

        [PersistedMember]
        public abstract Characters character { get; set; }

        [PersistedMember]
        public abstract int button { get; set; }

        [PersistedMember]
        public abstract int action { get; set; }

        [PersistedMember]
        public abstract int type { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }
    }
}
