using System;
using Common.Globals;
using Shaolinq;

namespace Common.Database.Tables
{
    [DataAccessObject]
    public abstract class Realms : DataAccessObject<int>
    {
        [AutoIncrement] [PersistedMember] public override int Id { get; set; }

        [PersistedMember] public abstract RealmType type { get; set; }

        [PersistedMember] public abstract RealmFlag flag { get; set; }

        [PersistedMember] public abstract RealmTimezone timezone { get; set; }

        [PersistedMember] public abstract string name { get; set; }

        [PersistedMember] public abstract string address { get; set; }

        [PersistedMember] public abstract DateTime? created_at { get; set; }

        [PersistedMember] public abstract DateTime? updated_at { get; set; }
    }
}