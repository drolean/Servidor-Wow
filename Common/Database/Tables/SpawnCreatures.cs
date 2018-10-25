using MongoDB.Bson;

namespace Common.Database.Tables
{
    public class SpawnCreatures
    {
        // Database
        public ObjectId Id { get; set; }
        public ulong Uid { get; set; }
        public int Entry { get; set; } = 0;

        public SubMap SubMap { get; set; }
        public SubStats SubStats { get; set; }
       
        public int CountDeath { get; set; }

        // TODO: if isQuest is true script change to spawn more times 
        public bool IsQuest { get; set; }
        /// <todo>
        /// if spawned for quests tag with virtual, be removed on next ticket OR
        /// if radius not have more than >= players
        /// </todo>
        public bool IsVirtual { get; set; }
        // TODO: if virtual set ParentUid to keep tracking CountDeath's.
        public ulong ParentUid { get; set; }
    }
}