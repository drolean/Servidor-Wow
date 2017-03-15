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
        public abstract ulong item { get; set; } // Id do item

        [PersistedMember]
        public abstract int bag { get; set; } // Id da Bag

        [PersistedMember]
        public abstract uint slot { get; set; } // Slot do item Gear + iventory

        [PersistedMember]
        public abstract uint stack { get; set; } // Quantidade de item no Stack

        [PersistedMember]
        public abstract uint durability { get; set; }

        [PersistedMember]
        public abstract uint flags { get; set; }

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
