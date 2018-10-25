using System;
using System.Linq;
using Common.Database;
using MongoDB.Driver;
using RealmServer.Database;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnDelIgnore
    {
        public static void Handler(RealmServerSession session, CMSG_DEL_IGNORE handler)
        {
            var friend = Characters.FindCharacaterByUid(handler.PlayerUid);

            try
            {
                var pull = Builders<Common.Database.Tables.Characters>.Update.PullFilter(x => x.SubIgnoreds,
                    a => a.Uid == handler.PlayerUid);
                var filter = Builders<Common.Database.Tables.Characters>.Filter.And(
                    Builders<Common.Database.Tables.Characters>.Filter.Eq(a => a.Uid, session.Character.Uid),
                    Builders<Common.Database.Tables.Characters>.Filter.ElemMatch(q => q.SubIgnoreds,
                        t => t.Uid == handler.PlayerUid));
                DatabaseModel.CharacterCollection.UpdateOneAsync(filter, pull);

                var item = session.Character.SubIgnoreds.Single(r => r.Uid == friend.Uid);
                session.Character.SubIgnoreds.Remove(item);
            }
            catch
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.DB_ERROR, friend));
            }
            finally
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.IGNORE_REMOVED, friend));
            }
        }
    }
}