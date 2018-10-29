using RealmServer.Database;

namespace RealmServer.Handlers
{
    public class OnTutorialReset
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            session.Entity.TutorialFlags.ResetFlags();
            session.Character.TutorialFlags = session.Entity.TutorialFlags.FlagData;
            Characters.UpdateCharacter(session.Character);
        }
    }
}
