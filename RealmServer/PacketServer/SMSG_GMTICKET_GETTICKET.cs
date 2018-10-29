using Common.Globals;
using RealmServer.Enums;

namespace RealmServer.PacketServer
{
    public sealed class SMSG_GMTICKET_GETTICKET : Common.Network.PacketServer
    {
        public SMSG_GMTICKET_GETTICKET(TicketInfoResponse code) : base(RealmEnums.SMSG_GMTICKET_GETTICKET)
        {
            Write((int) code);
        }

        public SMSG_GMTICKET_GETTICKET(TicketInfoResponse code, string msg) : base(RealmEnums.SMSG_GMTICKET_GETTICKET)
        {
            Write((int) code);
            WriteCString(msg);
        }
    }
}
