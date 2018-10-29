using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_PLAYED_TIME : Common.Network.PacketServer
    {
        public SMSG_PLAYED_TIME(Characters character) : base(RealmEnums.CMSG_PLAYED_TIME)
        {
            Write(1); // GetTotalPlayedTime
            Write(1); // GetLevelPlayedTime
        }
    }
}
