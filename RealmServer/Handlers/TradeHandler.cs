using System;
using Common.Globals;
using Common.Network;

namespace RealmServer.Handlers
{
    #region SMSG_TRADE_STATUS
    internal sealed class SmsgTradeStatus : PacketServer
    {
        public SmsgTradeStatus(TradeStatus status) : base(RealmCMD.SMSG_TRADE_STATUS)
        {
            Write((UInt32) status);
        }
    }
    #endregion

    internal class TradeHandler
    {
        internal static void OnCancelTrade(RealmServerSession session, PacketReader handler)
        {
            session.SendPacket(new SmsgTradeStatus(TradeStatus.TRADE_STATUS_CANCELED));
        }
    }
}