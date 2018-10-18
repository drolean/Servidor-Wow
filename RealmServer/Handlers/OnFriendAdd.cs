using RealmServer.Database;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnFriendAdd
    {
        public static void Handler(RealmServerSession session, CMSG_ADD_FRIEND handler)
        {
            var Friend = Characters.FindCharacaterByName(handler.NamePlayer);
        }
    }
}