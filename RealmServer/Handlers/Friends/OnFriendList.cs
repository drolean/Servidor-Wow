using RealmServer.PacketServer;

namespace RealmServer.Handlers.Friends
{
    public class OnFriendList
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            // Send Friend List
            session.SendPacket(new SMSG_FRIEND_LIST(session.Character));

            // Send Friend Ignored List
            session.SendPacket(new SMSG_IGNORE_LIST(session.Character));
        }
    }
}
