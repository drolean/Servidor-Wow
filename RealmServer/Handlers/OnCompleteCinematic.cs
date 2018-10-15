using Common.Database;
using Common.Database.Tables;
using MongoDB.Driver;

namespace RealmServer.Handlers
{
    public class OnCompleteCinematic
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            // TODO: change to model
            DatabaseModel.CharacterCollection.UpdateOne(
                Builders<Characters>.Filter.Where(x => x.Uid == session.Character.Uid),
                Builders<Characters>.Update.Set(x => x.Cinematic, true)
            );
        }
    }
}