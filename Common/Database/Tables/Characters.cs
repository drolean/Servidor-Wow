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
        public abstract DateTime? created_at { get; set; }

        [PersistedMember]
        public abstract DateTime? updated_at { get; set; }

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


        /*
        CREATE TABLE IF NOT EXISTS `characters` (
            `char_xp_rested` mediumint(3) NOT NULL DEFAULT '0',
            `char_logouttime` int(8) unsigned NOT NULL DEFAULT '0',
            `bindpoint_positionX` float NOT NULL DEFAULT '0',
            `bindpoint_positionY` float NOT NULL DEFAULT '0',
            `bindpoint_positionZ` float NOT NULL DEFAULT '0',
            `bindpoint_map_id` smallint(2) NOT NULL DEFAULT '0',
            `bindpoint_zone_id` smallint(2) NOT NULL DEFAULT '0',
            `char_guildId` int(1) NOT NULL DEFAULT '0',
            `char_guildRank` tinyint(1) unsigned NOT NULL DEFAULT '0',
            `char_guildPNote` varchar(255) NOT NULL DEFAULT '',
            `char_guildOffNote` varchar(255) NOT NULL DEFAULT '',
            `char_restState` tinyint(1) unsigned NOT NULL DEFAULT '0',
            `char_watchedFactionIndex` tinyint(1) unsigned NOT NULL DEFAULT '255',
            `char_reputation` text NOT NULL,
            `char_skillList` text NOT NULL,
            `char_auraList` text NOT NULL,
            `char_tutorialFlags` varchar(255) NOT NULL DEFAULT '',
            `char_taxiFlags` varchar(255) NOT NULL DEFAULT '',
            `char_actionBar` text NOT NULL,
            `char_mapExplored` text NOT NULL,
            `force_restrictions` tinyint(1) unsigned NOT NULL DEFAULT '0',
            `char_bankSlots` tinyint(1) unsigned NOT NULL DEFAULT '0',
            `char_transportGuid` bigint(8) unsigned NOT NULL DEFAULT '0',
          PRIMARY KEY(`char_guid`)
        ) ENGINE=MyISAM AUTO_INCREMENT = 57 DEFAULT CHARSET = utf8;
        */
    }
}
