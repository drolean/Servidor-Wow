using System.Collections.Generic;
using Common.Database.Tables;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnCharEnum 
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            List<Characters> characters = MainProgram.RealmServerDatabase.GetCharacters(session.Users.username);

            session.SendPacket(new SMSG_CHAR_ENUM(characters));
        }
    }
}