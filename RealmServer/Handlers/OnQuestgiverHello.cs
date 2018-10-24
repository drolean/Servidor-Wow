using System.Linq;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnQuestgiverHello
    {
        public static void Handler(RealmServerSession session, CMSG_QUESTGIVER_HELLO handler)
        {
            var npcEntry = session.Entity.KnownCreatures.FirstOrDefault(s => s.Uid == handler.Uid);

            if (npcEntry != null)
                return;

            // if have multiple quest send this 
            session.SendPacket(new SMSG_QUESTGIVER_QUEST_LIST(npcEntry, handler));
        }
    }
}