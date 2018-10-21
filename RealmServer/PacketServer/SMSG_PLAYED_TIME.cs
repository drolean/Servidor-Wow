using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_PLAYED_TIME : Common.Network.PacketServer
    {
        public SMSG_PLAYED_TIME(Characters character) : base(RealmEnums.CMSG_PLAYED_TIME)
        {
            Write((int) 1); // GetTotalPlayedTime
            Write((int) 1); // GetLevelPlayedTime
        }
    }
}