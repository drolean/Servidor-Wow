using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_CHAR_DELETE represents a message sent by the server to confirm or invalidate a character deletion.
    /// </summary>
    public sealed class SMSG_CHAR_DELETE : Common.Network.PacketServer
    {
        /// <summary>
        ///     Sends a character delete reply to the client.
        /// </summary>
        /// <param name="code"></param>
        public SMSG_CHAR_DELETE(LoginErrorCode code) : base(RealmEnums.SMSG_CHAR_DELETE)
        {
            Write((byte) code);
        }
    }
}