using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_AUTH_RESPONSE represents a message sent by the server whenever a CMSG_AUTH_SESSION message is sent.
    /// </summary>
    internal sealed class SMSG_AUTH_RESPONSE : Common.Network.PacketServer
    {
        /// <summary>
        ///     SMSG_AUTH_RESPONSE happens after a client requested to login.
        /// </summary>
        /// <param name="state">LoginErroCode</param>
        /// <param name="count">Count to queue position</param>
        public SMSG_AUTH_RESPONSE(LoginErrorCode state, int count = 0) : base(RealmEnums.SMSG_AUTH_RESPONSE)
        {
            Write((byte) state);
            Write((uint) 0); // BillingTimeRemaining
            Write((byte) 0); // BillingPlanFlags
            Write((uint) 0); // BillingTimeRested
            Write((byte) 0); // Expansion Level [0=normal, 1=TBC, 2=WOTLK, 3=CATA, 4=MOP]
            Write((uint) count); // Queue Position
            Write((byte) 0); // Free Transfer
        }
    }
}