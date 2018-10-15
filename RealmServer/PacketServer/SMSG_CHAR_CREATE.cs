using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_CHAR_CREATE represents a message sent by the server to confirm or invalidate a character creation.
    /// </summary>
    public sealed class SMSG_CHAR_CREATE : Common.Network.PacketServer
    {
        /// <summary>
        /// </summary>
        /// <param name="code"></param>
        public SMSG_CHAR_CREATE(LoginErrorCode code) : base(RealmEnums.SMSG_CHAR_CREATE)
        {
            Write((byte) code);
        }
    }
}