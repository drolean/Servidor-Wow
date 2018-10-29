using System;
using System.Linq;
using Common.Database.Tables;
using Common.Helpers;
using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer;
using Characters = RealmServer.Database.Characters;

namespace RealmServer.Handlers.Friends
{
    public class OnAddIgnore
    {
        public static void Handler(RealmServerSession session, CMSG_ADD_IGNORE handler)
        {
            var friend = Characters.FindCharacaterByName(handler.NamePlayer);

            // Not Found
            if (friend == null)
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.IGNORE_NOT_FOUND, null));
                return;
            }

            // Self
            if (friend.Name == session.Character.Name)
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.IGNORE_SELF, friend));
                return;
            }

            // Already
            if (session.Character.SubIgnoreds.Any(x => x.Uid == friend.Uid))
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.IGNORE_ALREADY, friend));
                return;
            }

            // Count
            if (session.Character.SubIgnoreds.Count >= 100)
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.IGNORE_FULL, friend));
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
                session.Character.SubIgnoreds.Add(new SubCharacterIgnored
                {
                    Uid = friend.Uid,
                    CreatedAt = DateTime.Now
                });

                Characters.UpdateCharacter(session.Character);
            }
            catch
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.Error, friend));
            }
            finally
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.IGNORE_ADDED, friend));
            }
        }
    }
}
