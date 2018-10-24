using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Common.Database.Tables
{
    public class SubCreaturesQuests
    {
        public int Quest { get; set; }
    }

    public class SubCreaturesModels
    {
        public int Model { get; set; }
    }

    public class SubCreaturesStats
    {
        public int Level { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public float SpeedWalk { get; set; }
        public float SpeedRun { get; set; }
        public float Scale { get; set; }
    }

    public class SubCreaturesFlags
    {
        public int Npc { get; set; }
        public int Unit { get; set; }
        public int Dynamic { get; set; }
        public int Type { get; set; }
    }

    public class Creatures
    {
        public ObjectId Id { get; set; }

        public string Locations { get; set; }
        public string Patch { get; set; }
        public int PatchMangos { get; set; }

        public int Entry { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public List<SubCreaturesModels> SubModels { get; set; }
        public SubCreaturesStats SubStats { get; set; }
        public SubCreaturesFlags SubFlags { get; set; }
        public SubResistence SubResistences { get; set; }
        public int FactionAlliance { get; set; }
        public int FactionHorde { get; set; }
        public int Rank { get; set; }
        public int Family { get; set; }
        public int Type { get; set; }
        public int Civilian { get; set; }
        public int RacialLeader { get; set; }
        public List<SubCreaturesQuests> SubQuests { get; set; }

        // Internal purpose
        public int CountSpawn { get; set; }
        public DateTime? UsedAt { get; set; }
    }
}