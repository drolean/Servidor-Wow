using System.Linq;
using RealmServer.Database;
using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers.Friends
{
    public class OnDelIgnore
    {
        public static void Handler(RealmServerSession session, CMSG_DEL_IGNORE handler)
        {
            var friend = Characters.FindCharacaterByUid(handler.PlayerUid);

            try
            {
                var item = session.Character.SubIgnoreds.Single(r => r.Uid == friend.Uid);
                session.Character.SubIgnoreds.Remove(item);

                Characters.UpdateCharacter(session.Character);
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