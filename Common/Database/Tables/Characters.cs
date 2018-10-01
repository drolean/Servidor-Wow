using System;
using Platform.Validation;
using Shaolinq;
using Common.Globals;
using System.Threading;

namespace Common.Database.Tables
{
    [DataAccessObject]
    public abstract class Characters : DataAccessObject<int>
    {
        [AutoIncrement]
        [PersistedMember]
        public override int Id { get; set; }

        [PersistedMember]
        public abstract Users user { get; set; }

        [PersistedMember]
        public abstract Realms realm { get; set; }

        [PersistedMember, Unique]
        public abstract string name { get; set; }

        [PersistedMember]
        public abstract Races race { get; set; }

        [PersistedMember]
        public abstract Classes classe { get; set; }

        [PersistedMember]
        public abstract Genders gender { get; set; }

        [PersistedMember]
        public abstract byte  level { get; set; }

        [PersistedMember]
        public abstract int   money { get; set; }

        [PersistedMember]
        public abstract uint  xp { get; set; }

        [PersistedMember]
        public abstract byte  talent_points { get; set; }

        [PersistedMember]
        public abstract uint  flags { get; set; }

        [PersistedMember]
        public abstract int   MapId { get; set; }

        [PersistedMember]
        public abstract int   MapZone { get; set; }

        [PersistedMember]
        public abstract float MapX { get; set; }

        [PersistedMember]
        public abstract float MapY { get; set; }

        [PersistedMember]
        public abstract float MapZ { get; set; }

        [PersistedMember]
        public abstract float MapO { get; set; }

        [PersistedMember]
        public abstract byte char_skin { get; set; }

        [PersistedMember]
        public abstract byte char_face { get; set; }

        [PersistedMember]
        public abstract byte char_hairStyle { get; set; }

        [PersistedMember]
        public abstract byte char_hairColor { get; set; }

        [PersistedMember]
        public abstract byte char_facialHair { get; set; }

        [PersistedMember]
        public abstract bool is_online { get; set; }

        [PersistedMember]
        public abstract bool is_movie_played { get; set; }

        [PersistedMember]
        public abstract string tutorial { get; set; }

        [PersistedMember]
        public abstract int watched_faction { get; set; }

        [PersistedMember]
        public abstract uint stast_mana { get; set; }

        [PersistedMember]
        public abstract uint stats_energy { get; set; }

        [PersistedMember]
        public abstract uint stats_rage { get; set; }

        [PersistedMember]
        public abstract uint stats_life { get; set; }

        [PersistedMember]
        public abstract uint stats_manaType { get; set; }

        [PersistedMember]
        public abstract uint stats_strength { get; set; }

        [PersistedMember]
        public abstract uint stats_agility { get; set; }

        [PersistedMember]
        public abstract uint stats_stamina { get; set; }

        [PersistedMember]
        public abstract uint stats_intellect { get; set; }

        [PersistedMember]
        public abstract uint stats_spirit { get; set; }
        public Timer LogoutTimer { get; set; }

        [PersistedMember]
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }
    }
}
