using System;
using System.Collections;

namespace Common.Database.Dbc
{
    public class Faction : DbcRecordBase
    {
        public int FactionFlag;
        public int FactionId;
        public string FactionName;
        public int[] Flags = new int[4];
        public int[] ReputationFlags = new int[4];
        public int[] ReputationStats = new int[4];

        public override int Read()
        {
            FactionId = GetInt32(0);
            FactionFlag = GetInt32(1);

            Flags[0] = GetInt32(2);
            Flags[1] = GetInt32(3);
            Flags[2] = GetInt32(4);
            Flags[3] = GetInt32(5);

            ReputationStats[0] = GetInt32(10);
            ReputationStats[1] = GetInt32(11);
            ReputationStats[2] = GetInt32(12);
            ReputationStats[3] = GetInt32(13);

            ReputationFlags[0] = GetInt32(14);
            ReputationFlags[1] = GetInt32(15);
            ReputationFlags[2] = GetInt32(16);
            ReputationFlags[3] = GetInt32(17);

            FactionName = GetString(19);

            return FactionId;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}