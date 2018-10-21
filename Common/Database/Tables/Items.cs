using System.Collections.Generic;
using MongoDB.Bson;

namespace Common.Database.Tables
{
    public class SubRequire
    {
        public int Level { get; set; } = 0;
        public int Skill { get; set; } = 0;
        public int SkillRank { get; set; } = 0;
        public int Spell { get; set; } = 0;
        public int HonorRank { get; set; } = 0;
        public int CityRank { get; set; } = 0;
        public int ReputationFaction { get; set; } = 0;
        public int ReputationRank { get; set; } = 0;
    }

    /// <summary>
    ///     Max 10
    /// </summary>
    public class SubStat
    {
        public int Type { get; set; }
        public int Value { get; set; }
    }

    /// <summary>
    ///     Max 5
    /// </summary>
    public class SubDamage
    {
        public int Type { get; set; }
        public float Min { get; set; } = 0;
        public float Max { get; set; } = 0;
    }

    public class SubResistence
    {
        public int Armor { get; set; } = 0;
        public int Holy { get; set; } = 0;
        public int Fire { get; set; } = 0;
        public int Nature { get; set; } = 0;
        public int Frost { get; set; } = 0;
        public int Shadow { get; set; } = 0;
        public int Arcane { get; set; } = 0;
    }

    /// <summary>
    ///     Max 5
    /// </summary>
    public class SubItemSpell
    {
        public int Id { get; set; } = 0;
        public int Trigger { get; set; } = 0;
        public int Charges { get; set; } = 0;
        public float PpmRate { get; set; } = 0;
        public int CoolDown { get; set; } = -1;
        public int Category { get; set; } = 0;
        public int CategoryCooldown { get; set; } = -1;
    }

    public class Items
    {
        public ObjectId Id { get; set; }

        public int Entry { get; set; } = 0;
        public int Class { get; set; } = 0;
        public int SubClass { get; set; } = 0;
        public string Name { get; set; } = "NotFound";
        public int DisplayId { get; set; } = 0;
        public int Quality { get; set; } = 0;
        public int Flags { get; set; } = 0;
        public int BuyCount { get; set; } = 1;
        public int BuyPrice { get; set; } = 0;
        public int SellPrice { get; set; } = 0;
        public int InventoryType { get; set; } = 0;
        public int AllowableClass { get; set; } = -1;
        public int AllowableRace { get; set; } = -1;
        public int ItemLevel { get; set; } = 0;

        public SubRequire SubRequired { get; set; }
        public List<SubStat> SubStats { get; set; }
        public List<SubDamage> SubDamages { get; set; }
        public SubResistence SubResistences { get; set; }
        public List<SubItemSpell> SubSpells { get; set; }

        public int MaxCount { get; set; } = 0;
        public int Stackable { get; set; } = 1;
        public int ContainerSlots { get; set; } = 0;

        public int Delay { get; set; } = 0;
        public int AmmoType { get; set; } = 0;
        public float RangedModRange { get; set; } = 0;

        public int Bonding { get; set; } = 0;
        public string Description { get; set; }
        public int PageText { get; set; } = 0;
        public int LanguageId { get; set; } = 0;
        public int PageMaterial { get; set; } = 0;
        public int StartQuest { get; set; } = 0;
        public int LockId { get; set; } = 0;
        public int Material { get; set; } = 0;
        public int Sheath { get; set; } = 0;
        public int RandomProperty { get; set; } = 0;
        public int Block { get; set; } = 0;
        public int ItemSet { get; set; } = 0;
        public int MaxDurability { get; set; } = 0;
        public int Area { get; set; } = 0;
        public int Map { get; set; } = 0;
        public int BagFamily { get; set; } = 0;
        public string ScriptName { get; set; }
        public int DisenchantId { get; set; } = 0;
        public int FoodType { get; set; } = 0;
        public int MinMoneyLoot { get; set; } = 0;
        public int MaxMoneyLoot { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public int ExtraFlags { get; set; } = 0;
    }
}