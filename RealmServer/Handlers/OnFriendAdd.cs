using System;
using System.Linq;
using Common.Database;
using Common.Database.Tables;
using Common.Helpers;
using MongoDB.Driver;
using RealmServer.PacketReader;
using RealmServer.PacketServer;
using RealmServer.World.Managers;
using Characters = RealmServer.Database.Characters;

namespace RealmServer.Handlers
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
                bool status = PlayerManager.Players.Any(p => p.Character.Uid == friend.Uid);

                session.SendPacket(status
                    ? new SMSG_FRIEND_STATUS(FriendResults.ADDED_ONLINE, friend)
                    : new SMSG_FRIEND_STATUS(FriendResults.ADDED_OFFLINE, friend));
            }
        }
    }

    public enum FriendStatus : byte
    {
        Offline = 0,
        Online = 1,
        Afk = 2,
        Unk3 = 3,
        Dnd = 4
    }

    public enum FriendResults : byte
    {
        DB_ERROR = 0x0,
        LIST_FULL = 0x1,
        ONLINE = 0x2,
        OFFLINE = 0x3,
        NOT_FOUND = 0x4,
        REMOVED = 0x5,
        ADDED_ONLINE = 0x6,
        ADDED_OFFLINE = 0x7,
        ALREADY = 0x8,
        SELF = 0x9,
        ENEMY = 0xA,
        IGNORE_FULL = 0xB,
        IGNORE_SELF = 0xC,
        IGNORE_NOT_FOUND = 0xD,
        IGNORE_ALREADY = 0xE,
        IGNORE_ADDED = 0xF,
        IGNORE_REMOVED = 0x10,
        NameAmbiguous = 0x11,
        Error = 0x12
    }
}