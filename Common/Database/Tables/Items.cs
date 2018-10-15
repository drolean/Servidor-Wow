using System.Collections.Generic;
using MongoDB.Bson;

namespace Common.Database.Tables
{
    public class SubRequire
    {
        public int Level { get; set; }
        public int Skill { get; set; }
        public int SkillRank { get; set; }
        public int Spell { get; set; }
        public int HonorRank { get; set; }
        public int CityRank { get; set; }
        public int ReputationFaction { get; set; }
        public int ReputationRank { get; set; }
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
        public float Min { get; set; }
        public float Max { get; set; }
    }

    public class SubResistence
    {
        public int Armor { get; set; }
        public int Holy { get; set; }
        public int Fire { get; set; }
        public int Nature { get; set; }
        public int Frost { get; set; }
        public int Shadow { get; set; }
        public int Arcane { get; set; }
    }

    /// <summary>
    ///     Max 5
    /// </summary>
    public class SubItemSpell
    {
        public int Id { get; set; }
        public int Trigger { get; set; }
        public int Charges { get; set; }
        public float PpmRate { get; set; }
        public int CoolDown { get; set; }
        public int Category { get; set; }
        public int CategoryCooldown { get; set; }
    }

    public class Items
    {
        public ObjectId Id { get; set; }

        public int Entry { get; set; }
        public int Class { get; set; }
        public int SubClass { get; set; }
        public string Name { get; set; }
        public int DisplayId { get; set; }
        public int Quality { get; set; }
        public int Flags { get; set; }
        public int BuyCount { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public int InventoryType { get; set; }
        public int AllowableClass { get; set; }
        public int AllowableRace { get; set; }
        public int ItemLevel { get; set; }

        public SubRequire SubRequired { get; set; }
        public List<SubStat> SubStats { get; set; }
        public List<SubDamage> SubDamages { get; set; }
        public SubResistence SubResistences { get; set; }
        public List<SubItemSpell> SubSpells { get; set; }

        public int MaxCount { get; set; }
        public int Stackable { get; set; }
        public int ContainerSlots { get; set; }

        public int Delay { get; set; }
        public int AmmoType { get; set; }
        public float RangedModRange { get; set; }

        public int Bonding { get; set; }
        public string Description { get; set; }
        public int PageText { get; set; }
        public int LanguageId { get; set; }
        public int PageMaterial { get; set; }
        public int StartQuest { get; set; }
        public int LockId { get; set; }
        public int Material { get; set; }
        public int Sheath { get; set; }
        public int RandomProperty { get; set; }
        public int Block { get; set; }
        public int ItemSet { get; set; }
        public int MaxDurability { get; set; }
        public int Area { get; set; }
        public int Map { get; set; }
        public int BagFamily { get; set; }
        public string ScriptName { get; set; }
        public int DisenchantId { get; set; }
        public int FoodType { get; set; }
        public int MinMoneyLoot { get; set; }
        public int MaxMoneyLoot { get; set; }
        public int Duration { get; set; }
        public int ExtraFlags { get; set; }
    }
}