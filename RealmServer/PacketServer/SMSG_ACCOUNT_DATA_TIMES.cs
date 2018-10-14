using Common.Globals;
using Common.Helpers;

namespace RealmServer.PacketServer
{
    public class SMSG_ACCOUNT_DATA_TIMES : Common.Network.PacketServer
    {
        public SMSG_ACCOUNT_DATA_TIMES() : base(RealmEnums.SMSG_ACCOUNT_DATA_TIMES)
        {
            this.WriteBytes(new byte[80]);
        }
    }
}