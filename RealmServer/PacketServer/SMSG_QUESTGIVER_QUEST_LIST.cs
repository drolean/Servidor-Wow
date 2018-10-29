using Common.Database.Tables;
using Common.Globals;
using RealmServer.PacketReader;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_QUESTGIVER_QUEST_LIST : Common.Network.PacketServer
    {
        public SMSG_QUESTGIVER_QUEST_LIST(SpawnCreatures npcEntry, CMSG_QUESTGIVER_HELLO handler) : base(RealmEnums
            .SMSG_QUESTGIVER_QUEST_LIST)
        {
            Write(npcEntry.Uid);
            WriteCString("Title Strng");
            Write(1); // EmoteDelay
            Write(1); // Emote

            Write((byte) 1); // count
            //for
            Write(1); //id
            Write(1); //Status
            Write(1); //level
            WriteCString("Your Place In The World");
        }
    }
}
