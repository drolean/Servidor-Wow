using System;
using RealmServer.Enums;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    internal static class OnGmTicketCreate
    {
        internal static void Handler(RealmServerSession session, CMSG_GMTICKET_CREATE handler)
        {
            // TODO: Waiting for transfer NOT DELETE
            // TODO: Guild Leader NOT DELETE
            try
            {
                session.SendPacket(new SMSG_GMTICKET_CREATE(GmTicketCreateResult.AlreadyHave));
            }
            catch (Exception)
            {
                session.SendPacket(new SMSG_GMTICKET_CREATE(GmTicketCreateResult.AlreadyHave));
            }
        }
    }
}