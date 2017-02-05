using System;
using Shaolinq;

namespace Common.Database.Tables
{
    [DataAccessObject]
    public abstract class CharactersInventorys : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        public override int Id { get; set; }

        [PersistedMember]
        public abstract Characters character { get; set; }

        [PersistedMember]
        public abstract ulong item { get; set; }

        [PersistedMember]
        public abstract ulong owner { get; set; }

        [PersistedMember]
        public abstract uint bag { get; set; }

        [PersistedMember]
        public abstract uint slot { get; set; }

        [PersistedMember]
        public abstract uint template { get; set; }

        [PersistedMember]
        public abstract uint stack { get; set; }

        [PersistedMember]
        public abstract uint spellCharge { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }

        [PersistedMember]
        public abstract DateTime? deleted_at { get; set; }
    }
}
