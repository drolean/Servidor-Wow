using MongoDB.Bson;

namespace Common.Database.Tables
{
    public class Creatures
    {
        public ObjectId Id { get; set; }

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
    }

    public class SubCreatureFaction
    {
        public int MaleModel { get; set; } = 0;
        public int FemaleModel { get; set; } = 0;
        public int Faction { get; set; } = 0;
    }
}