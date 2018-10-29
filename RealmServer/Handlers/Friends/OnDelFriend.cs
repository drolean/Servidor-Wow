using System.Linq;
using RealmServer.Database;
using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers.Friends
{
    public class OnDelFriend
    {
        public static void Handler(RealmServerSession session, CMSG_DEL_FRIEND handler)
        {
            var friend = Characters.FindCharacaterByUid(handler.PlayerUid);

            try
            {
                var item = session.Character.SubFriends.Single(r => r.Uid == friend.Uid);
                session.Character.SubFriends.Remove(item);

                Characters.UpdateCharacter(session.Character);
            }
            catch
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.DB_ERROR, friend));
            }
            finally
            {
                session.SendPacket(new SMSG_FRIEND_STATUS(FriendResults.REMOVED, friend));
            }
        }
    }
}
