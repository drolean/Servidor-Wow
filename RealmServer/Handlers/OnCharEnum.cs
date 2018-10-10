using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnCharEnum
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            var characters = MainProgram.RealmServerDatabase.GetCharacters(session.Users.username);

            session.SendPacket(new SMSG_CHAR_ENUM(characters));
        }
    }
}