using MongoDB.Bson;

namespace Common.Database.Tables
{
    public class Creatures
    {
        public ObjectId Id { get; set; }

        public int Entry { get; set; }
        public int Patch { get; set; }
        public int KillCredit1 { get; set; }
        public int KillCredit2 { get; set; }
        public int Modelid1 { get; set; }
        public int Modelid2 { get; set; }
        public int Modelid3 { get; set; }
        public int Modelid4 { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public int GossipMenuId { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int MinHealth { get; set; }
        public int MaxHealth { get; set; }
        public int MinMana { get; set; }
        public int MaxMana { get; set; }
        public int Armor { get; set; }
        public int FactionA { get; set; }
        public int FactionH { get; set; }
        public int NpcFlag { get; set; }
        public float SpeedWalk { get; set; }
        public float SpeedRun { get; set; }
        public float Scale { get; set; }
        public int Rank { get; set; }
        public float MinDmg { get; set; }
        public float MaxDmg { get; set; }
        public int DmgSchool { get; set; }
        public int AttackPower { get; set; }
        public float DmgMultiplier { get; set; }
        public int BaseAttackTime { get; set; }
        public int RangeAttackTime { get; set; }
        public int UnitClass { get; set; }
        public int UnitFlags { get; set; }
        public int DynamicFlags { get; set; }
        public int Family { get; set; }
        public int TrainerType { get; set; }
        public int TrainerSpell { get; set; }
        public int TrainerClass { get; set; }
        public int TrainerRace { get; set; }
        public float MinRangeDmg { get; set; }
        public float MaxRangeDmg { get; set; }
        public int RangedAttackPower { get; set; }
        public int Type { get; set; }
        public int TypeFlags { get; set; }
        public int Lootid { get; set; }
        public int PickPocketLoot { get; set; }
        public int SkinLoot { get; set; }
        public int Resistance1 { get; set; }
        public int Resistance2 { get; set; }
        public int Resistance3 { get; set; }
        public int Resistance4 { get; set; }
        public int Resistance5 { get; set; }
        public int Resistance6 { get; set; }
        public int Spell1 { get; set; }
        public int Spell2 { get; set; }
        public int Spell3 { get; set; }
        public int Spell4 { get; set; }
        public int SpellsTemplate { get; set; }
        public int PetSpellDataId { get; set; }
        public int MinGold { get; set; }
        public int MaxGold { get; set; }
        public string AiName { get; set; }
        public int MovementType { get; set; }
        public int InhabitType { get; set; }
        public int Civilian { get; set; }
        public int RacialLeader { get; set; }
        public int RegenHealth { get; set; }
        public int EquipmentId { get; set; }
        public int TrainerId { get; set; }
        public int VendorId { get; set; }
        public int MechanicImmuneMask { get; set; }
        public int SchoolImmuneMask { get; set; }
        public int FlagsExtra { get; set; }
        public string ScriptName { get; set; }
        /*
        public string Patch { get; set; } = string.Empty;
        public int Entry { get; set; }

        public SubCreatureFaction Alliance { get; set; }
        public SubCreatureFaction Horde { get; set; }

        public string Name { get; set; }
        public string SubName { get; set; }
        public int Class { get; set; }
        public int Race { get; set; }

        // Unique Level with rand???
        public int LevelMin { get; set; }
        public int LevelMax { get; set; }

        // Unique Health???
        public int HealthMin { get; set; }
        public int HealthMax { get; set; }

        public int ManaMin { get; set; }
        public int ManaMax { get; set; }

        public float DamageMin { get; set; }
        public float DamageMax { get; set; }

        public float RangeDamageMin { get; set; }
        public float RangeDamageMax { get; set; }
       
        public float Speed { get; set; }
        public float Scale { get; set; }
        public int Rank { get; set; }

        public int AttackPower { get; set; }
        public int BaseAttackTime { get; set; }
        public int RangeAttackTime { get; set; }

        public int NpcFlag { get; set; }
        public int UnitFlag { get; set; }
        public int DynamicFlag { get; set; }

        public int Family { get; set; }
        public int TrainerType { get; set; }
        public int TrainerSpell { get; set; }

        public int RangedAttackPower { get; set; }

        private int DamageSchool;

        public SubResistence SubResistences { get; set; }

        public int Type { get; set; }
        public int TypeFlag { get; set; }

        public int LootItems { get; set; }
        public int LootPickPocket { get; set; }
        public int LootSkin { get; set; }
        public int LootGold { get; set; }

        public int Equipments { get; set; }
        */
    }

    public class SubCreatureFaction
    {
        public int MaleModel { get; set; } = 0;
        public int FemaleModel { get; set; } = 0;
        public int Faction { get; set; } = 0;
    }
}