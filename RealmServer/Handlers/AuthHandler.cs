using System.Collections.Generic;

namespace RealmServer.Handlers
{
    internal class AuthHandler
    {
        public static void OnCharEnum(RealmServerSession session, byte[] data)
        {
            List<Character> characters = MainForm.Database.GetCharacters(session.Users.username);
            session.SendPacket(new SmsgCharEnum(characters));
        }
    }
}
