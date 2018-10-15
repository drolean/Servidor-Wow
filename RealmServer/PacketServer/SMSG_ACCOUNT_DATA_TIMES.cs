using Common.Globals;
using Common.Helpers;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_ACCOUNT_DATA_TIMES represents a message sent by the server whenever a
    ///     CMSG_PLAYER_LOGIN message is sent and that the player is succesfully logged in.
    ///     Current time on the serverpackets, required or gameserverpackets keeps frozen.
    /// </summary>
    public class SMSG_ACCOUNT_DATA_TIMES : Common.Network.PacketServer
    {
        /// <summary>
        /// </summary>
        public SMSG_ACCOUNT_DATA_TIMES() : base(RealmEnums.SMSG_ACCOUNT_DATA_TIMES)
        {
            this.WriteBytes(new byte[80]);
        }
    }
}