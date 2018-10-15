using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_AUTH_CHALLENGE represents a server packet with the realm auth challenge,
    ///     server sends random bytes to be used as a hash salt.
    /// </summary>
    public sealed class SMSG_AUTH_CHALLENGE : Common.Network.PacketServer
    {
        /// <summary>
        /// </summary>
        public SMSG_AUTH_CHALLENGE() : base(RealmEnums.SMSG_AUTH_CHALLENGE)
        {
            Write((byte) 1);
            Write((byte) 2);
            Write((byte) 3);
            Write((byte) 4);
        }
    }
}