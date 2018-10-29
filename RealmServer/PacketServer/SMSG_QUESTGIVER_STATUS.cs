using Common.Globals;
using RealmServer.Enums;
using RealmServer.PacketReader;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_QUESTGIVER_STATUS : Common.Network.PacketServer
    {
        public SMSG_QUESTGIVER_STATUS(CMSG_QUESTGIVER_STATUS_QUERY handler, QuestgiverStatusFlag status) : base(
            RealmEnums.SMSG_QUESTGIVER_STATUS)
        {
            Write(handler.CreatureUid);
            Write((uint) status);
        }
    }
}
