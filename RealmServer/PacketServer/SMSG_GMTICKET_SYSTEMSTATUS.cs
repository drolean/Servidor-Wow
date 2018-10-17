using Common.Globals;
using RealmServer.Enums;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_GMTICKET_SYSTEMSTATUS : Common.Network.PacketServer
    {
        public SMSG_GMTICKET_SYSTEMSTATUS(GmTicketSystemStatus code) : base(RealmEnums.SMSG_GMTICKET_SYSTEMSTATUS)
        {
            Write((uint) code); // Time
        }
    }
}