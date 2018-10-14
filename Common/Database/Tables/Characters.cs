using System;
using System.Collections.Generic;
using Common.Globals;
using MongoDB.Bson;

namespace Common.Database.Tables
{
    public class Characters
    {
        public ObjectId Id { get; set; }
        public ulong Uid { get; set; }

        public ObjectId User { get; set; }
        public Realms Realm { get; set; }

        public Races Race { get; set; }
        public Classes Classe { get; set; }
        public Genders Gender { get; set; }

        public string Name { get; set; }
        public byte Level { get; set; }
        public int Money { get; set; }
        public uint Xp { get; set; }

        public SubMap SubMap { get; set; }
        public SubSkin SubSkin { get; set; }
        public List<SubStats> SubStats { get; set; }
        public List<SubTalents> SubTalents { get; set; }
        public List<SubTutorial> SubTutorial { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class SubTalents
    {
        public byte Skin { get; set; }
        public byte Face { get; set; }
        public byte HairStyle { get; set; }
        public byte HairColor { get; set; }
        public byte FacialHair { get; set; }
    }

    public class SubTutorial
    {
        public byte Tutorial { get; set; }
    }

    public class SubStats
    {
        public uint Mana { get; set; }
        public uint Energy { get; set; }
        public uint Rage { get; set; }
        public uint Life { get; set; }
        public uint ManaType { get; set; }
        public uint Strength { get; set; }
        public uint Agility { get; set; }
        public uint Stamina { get; set; }
        public uint Intellect { get; set; }
        public uint Spirit { get; set; }
    }

    public class SubSkin
    {
        public byte Skin { get; set; }
        public byte Face { get; set; }
        public byte HairStyle { get; set; }
        public byte HairColor { get; set; }
        public byte FacialHair { get; set; }
    }

    public class SubMap
    {
        public int MapId { get; set; }
        public int MapZone { get; set; }
        public float MapX { get; set; }
        public float MapY { get; set; }
        public float MapZ { get; set; }
        public float MapO { get; set; }
    }


    /*
    public byte talent_points { get; set; }
    public uint flags { get; set; }


    public bool is_online { get; set; }
    public bool is_movie_played { get; set; }
    public string tutorial { get; set; }
    public int watched_faction { get; set; }

    public Timer LogoutTimer { get; set; }

    public uint Latency { get; set; }
}
*/
}