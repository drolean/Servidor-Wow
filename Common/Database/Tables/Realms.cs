using System;
using Common.Globals;
using MongoDB.Bson;

namespace Common.Database.Tables
{
    public class Realms
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public RealmType Type { get; set; }
        public RealmFlag Flag { get; set; }
        public RealmTimezone Timezone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}