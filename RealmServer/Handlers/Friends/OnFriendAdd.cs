using System;
using System.Linq;
using Common.Database;
using Common.Database.Tables;
using Common.Helpers;
using MongoDB.Driver;
using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer;
using RealmServer.World.Managers;
using Characters = RealmServer.Database.Characters;

namespace RealmServer.Handlers.Friends
{
    public class OnFriendAdd
    {
        public static void Handler(RealmServerSession session, CMSG_ADD_FRIEND handler)
        {
            var friend = Characters.FindCharacaterByName(handler.NamePlayer);

            // Not Found
            if (friend == null)
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.NOT_FOUND, null));
                return;
            }

            // Self
            if (friend.Name == session.Character.Name)
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.SELF, friend));
                return;
            }

            // Already
            if (session.Character.SubFriends.Any(x => x.Uid == friend.Uid))
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.ALREADY, friend));
                return;
            }

            // Count
            if (session.Character.SubFriends.Count >= 100)
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.LIST_FULL, friend));
                return;
            }

            // Faction
            if (Functions.GetCharacterSide(friend.Race) != Functions.GetCharacterSide(session.Character.Race))
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.ENEMY, friend));
                return;
            }

            try
            {
                DatabaseModel.CharacterCollection.UpdateOneAsync(
                    Builders<Common.Database.Tables.Characters>.Filter.Where(x => x.Uid == session.Character.Uid),
                    Builders<Common.Database.Tables.Characters>.Update.Push("SubFriends", new SubCharacterFriend
                    {
                        Uid = friend.Uid,
                        CreatedAt = DateTime.Now
                    })
                );

                session.Character.SubFriends.Add(new SubCharacterFriend
                {
                    Uid = friend.Uid,
                    CreatedAt = DateTime.Now
                });
            }
            catch
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.Error, friend));
            }
            finally
            {
                var status = PlayerManager.Players.Any(p => p.Character.Uid == friend.Uid);

                session.SendPacket(status
                    ? new SMSG_FRIEND_STATUS(FriendResults.ADDED_ONLINE, friend)
                    : new SMSG_FRIEND_STATUS(FriendResults.ADDED_OFFLINE, friend));
            }
        }
    }
}