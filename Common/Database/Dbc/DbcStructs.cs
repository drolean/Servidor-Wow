using System;
using System.Linq;

namespace Common.Database.Dbc
{

    #region AreaTable.dbc
    public class AreaTableStore : DBCStore<AreaTable> { }

    public class AreaTable : DBCRecordBase
    {
        public Int32 areaID;
        public Int32 areaMapID;
        public Int32 areaZone;
        public Int32 areaExploreFlag;
        public Int32 areaZoneType;
        public Int32 areaLevel;
        public String areaName;

        public override int Read()
        {
            areaID = GetInt32(0);
            areaMapID = GetInt32(1); // May be needed in the future
            areaZone = GetInt32(2);
            areaExploreFlag = GetInt32(3);
            areaZoneType = GetInt32(4);
            areaLevel = GetInt32(10);
            areaName = GetString(11);

            if (areaLevel > 255)
                areaLevel = 255;

            if (areaLevel < 0)
                areaLevel = 0;

            return (int)areaID;
        }
    }
    #endregion

    #region Faction.dbc
    public class FactionStore : DBCStore<Faction>
    {
        public Faction GetFaction(int Id)
        {
            return RecordDataIndexed.Values.ToArray().Where(a => a.factionFlag == Id).FirstOrDefault();
        }
    }

    public class Faction : DBCRecordBase
    {
        public Int32 factionID;
        public Int32 factionFlag;
        public Int32[] flags = new int[4];
        public Int32[] reputationStats = new int[4];
        public Int32[] reputationFlags = new int[4];
        public String factionName;

        public override int Read()
        {
            factionID = GetInt32(0);
            factionFlag = GetInt32(1);

            flags[0] = GetInt32(2);
            flags[1] = GetInt32(3);
            flags[2] = GetInt32(4);
            flags[3] = GetInt32(5);

            reputationStats[0] = GetInt32(10);
            reputationStats[1] = GetInt32(11);
            reputationStats[2] = GetInt32(12);
            reputationStats[3] = GetInt32(13);

            reputationFlags[0] = GetInt32(14);
            reputationFlags[1] = GetInt32(15);
            reputationFlags[2] = GetInt32(16);
            reputationFlags[3] = GetInt32(17);

            factionName = GetString(15);

            return (int)factionID;
        }
    }
    #endregion

    #region CharStartOutfit.dbc
    public class CharStartOutfitStore : DBCStore<CharStartOutfit>
    {
        public CharStartOutfit Get(UInt32 Class, UInt32 Race, UInt32 Gender)
        {
            return RecordDataIndexed.Values.ToArray().Where(c => c.Class == Class && c.Race == Race && c.Gender == Gender).FirstOrDefault();
        }
    }

    public class CharStartOutfit : DBCRecordBase
    {
        public UInt32 ID = 0;
        public UInt32 Race = 0;
        public UInt32 Class = 0;
        public UInt32 Gender = 0;
        public UInt32[] Items = new UInt32[24];

        public override int Read()
        {
            ID = GetUInt32(0);

            var tmp = GetUInt32(1);
            Race = tmp & 0xFF;
            Class = (tmp >> 8) & 0xFF;
            Gender = (tmp >> 16) & 0xFF;

            for (int i = 0; i < Items.Length; ++i)
                Items[i] = GetUInt32(2 + i);

            return (int)ID;
        }
    }
    #endregion
}