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
            Write(handler.Uid);
            WriteCString("Title Strng");
            Write(1); // delay
            Write(1); // emote

            Write((byte) 1); // count

            //for
            Write(102); //id
            Write(102); //icon
            Write(1); //level
            WriteCString("Your Place In The World");
        }
    }
}