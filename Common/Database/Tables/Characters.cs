using System;
using System.Collections.Generic;
using Common.Globals;
using MongoDB.Bson;

namespace Common.Database.Tables
{
    public class Characters
    {
        // Extra
        public byte StandState;
        public bool IsPvP;
        public int SheathType { get; set; }
        public Characters Target { get; set; }

        // Database
        public ObjectId Id { get; set; }
        public ulong Uid { get; set; }

        public ObjectId User { get; set; }
        public Realms Realm { get; set; }

        public Races Race { get; set; }
        public Classes Classe { get; set; }
        public Genders Gender { get; set; }
        public CharacterFlag Flag { get; set; }

        public string Name { get; set; }
        public byte Level { get; set; }
        public int Money { get; set; }
        public uint Xp { get; set; }

        public int WatchFaction { get; set; }
        public bool Cinematic { get; set; }
        //Online
        //TotalTime
        //LevelTime

        public SubMap SubMap { get; set; }
        public SubSkin SubSkin { get; set; }
        public SubStats SubStats { get; set; }

        public List<SubSpell> SubSpells { get; set; }
        public List<SubActionBar> SubActionBars { get; set; }
        public List<SubFaction> SubFactions { get; set; }
        public List<SubSkill> SubSkills { get; set; }
        public List<SubInventory> SubInventorie { get; set; }

        public SubResistances SubResistances { get; set; }
        public SubAccountData SubAccountData { get; set; }
        public List<SubTalents> SubTalents { get; set; }
        public List<SubTutorial> SubTutorial { get; set; }

        public List<SubCharacterFriend> SubFriends { get; set; }
        public List<SubCharacterIgnored> SubIgnoreds { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class SubCharacterFriend
    {
        public ulong Uid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class SubCharacterIgnored
    {
        public ulong Uid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class SubInventory
    {
        public int Item { get; set; }
        public int Slot { get; set; }
        public int Bag { get; set; }
        public int Owner { get; set; }
        public int Creator { get; set; }
        public int GiftCreator { get; set; }
        public int StackCount { get; set; }
        public int Durability { get; set; }
        public int Flags { get; set; }
        public int ChargesLeft { get; set; }
        public int TextId { get; set; }
        public int Enchantment { get; set; }
        public int RandomProperties { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class SubSkill
    {
        public int Skill { get; set; }
        public int Value { get; set; }
        public int Max { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SubFaction
    {
        public int Faction { get; set; }
        public int Flags { get; set; }
        public int Standing { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SubActionBar
    {
        public int Button { get; set; }
        public int Action { get; set; }
        public int Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SubSpell
    {
        public int Spell { get; set; }
        public int Active { get; set; }
        public int Cooldown { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SubAccountData
    {
        public string Macros { get; set; }
    }

    public class SubResistances
    {
        public uint Armor { get; set; }

        public uint Fire { get; set; }
        public uint Nature { get; set; }
        public uint Frost { get; set; }
        public uint Shadow { get; set; }
        public uint Arcane { get; set; }
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