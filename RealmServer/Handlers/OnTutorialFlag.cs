using RealmServer.Database;
using RealmServer.PacketReader;

namespace RealmServer.Handlers
{
    public class OnTutorialFlag
    {
        public static void Handler(RealmServerSession session, CMSG_TUTORIAL_FLAG handler)
        {
            if (handler.Flag >= 256U)
                return;

            session.Entity.TutorialFlags.SetFlag(handler.Flag);
            session.Character.TutorialFlags = session.Entity.TutorialFlags.FlagData;
            Characters.UpdateCharacter(session.Character);
        }
    }
}