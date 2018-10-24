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
    }
}