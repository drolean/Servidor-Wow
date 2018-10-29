using RealmServer.Database;

namespace RealmServer.Handlers
{
    public class OnCompleteCinematic
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            session.Character.Cinematic = true;
            Characters.UpdateCharacter(session.Character);
        }
    }
}
