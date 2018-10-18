namespace RealmServer.Handlers
{
    public class OnCancelTrade
    {
        public static void Handler(RealmServerSession session, byte[] data)
        {
            // Send to another player
            //session.SendPacket(new SMSG_TRADE_STATUS(TradeStatus.TRADE_STATUS_CANCELED));
        }
    }
}