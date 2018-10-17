using Common.Globals;
using RealmServer.Enums;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_GMTICKET_CREATE : Common.Network.PacketServer
    {
        public SMSG_GMTICKET_CREATE(GmTicketCreateResult state) : base(RealmEnums.SMSG_GMTICKET_CREATE)
        {
            Write((uint) state);
        }
    }
}