using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace RealmServer.Handlers
{
    #region SMSG_GMTICKET_SYSTEMSTATUS
    sealed class SmsgGmticketSystemstatus : PacketServer
    {
        public SmsgGmticketSystemstatus(GMTicketSystemStatus opcode) : base(RealmCMD.SMSG_GMTICKET_SYSTEMSTATUS)
        {
            // Make Packet
            //SMSG_GMTICKET_SYSTEMSTATUS.AddInt32(GMTicketSystemStatus.GMTICKET_SYSTEMSTATUS_SURVEY)
            Write((uint) opcode);
        }
    }
    #endregion

    #region SMSG_GMTICKET_CREATE
    sealed class SmsgGmticketCreate : PacketServer
    {
        public SmsgGmticketCreate(GMTicketCreateResult opcode) : base(RealmCMD.SMSG_GMTICKET_CREATE)
        {
            // Make Packet
            //SMSG_GMTICKET_SYSTEMSTATUS.AddInt32(GMTicketSystemStatus.GMTICKET_SYSTEMSTATUS_SURVEY)
            Write((uint)opcode);
        }
    }
    #endregion

    internal class GmHandler
    {
        internal static void OnGmTicketGetTicket(RealmServerSession session, PacketReader handler)
        {
            /* SMSG_GMTICKET_GETTICKET
             * if > 0
             * - Int32  = Ticket Available = 6
             * - String = Ticket Text
             * else
             * - Int32  = NoTicket = 10
             */

            // SMSG_QUERY_TIME_RESPONSE

        }

        internal static void OnGmTicketSystemStatus(RealmServerSession session, PacketReader handler)
        {
            // check if system is available
            session.SendPacket(new SmsgGmticketSystemstatus(GMTicketSystemStatus.GMTICKET_SYSTEMSTATUS_ENABLED));
        }

        internal static void OnGmTicketCreate(RealmServerSession session, PacketReader handler)
        {
            uint ticketMap = handler.ReadUInt32();
            float ticketX  = handler.ReadSingle();
            float ticketY  = handler.ReadSingle();
            float ticketZ  = handler.ReadSingle();
            string ticketText = handler.ReadCString();

            Log.Print(LogType.Debug, $"Ticket recebido [{ticketMap}] [{ticketX}] [{ticketY}] [{ticketZ}] [{ticketText}] ");

            session.SendPacket(new SmsgGmticketCreate(GMTicketCreateResult.GMTICKET_CREATE_OK));
        }
    }
}