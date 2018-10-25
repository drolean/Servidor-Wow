using RealmServer.Database;

namespace RealmServer.Handlers
{
    public class OnTutorialClear
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            session.Entity.TutorialFlags.ClearFlags();
            session.Character.TutorialFlags = session.Entity.TutorialFlags.FlagData;
            Characters.UpdateCharacter(session.Character);
        }
    }
}