using Common.Crypt;
using Common.Globals;
using Common.Helpers;

namespace AuthServer.PacketServer
{
    internal sealed class CMD_AUTH_LOGON_PROOF : Common.Network.PacketServer
    {
        /// <summary>
        ///     Second authentication step, the client is sending his proof.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public CMD_AUTH_LOGON_PROOF(AuthServerSession session, AccountState result) : base(AuthCMD.CMD_AUTH_LOGON_PROOF)
        {
            Write((byte) AuthCMD.CMD_AUTH_LOGON_PROOF);
            Write((byte) result);
            Write(session.Srp.ServerProof.ToByteArray().Pad(20));
            this.WriteNullByte(4);

            if (session.Packet.Version.Contains("2.4"))
                this.WriteNullByte(6);
        }

        /// <summary>
        ///     Second authentication step, the client is sending his proof.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public CMD_AUTH_LOGON_PROOF(AccountState result) : base(AuthCMD.CMD_AUTH_LOGON_PROOF)
        {
            Write((byte) AuthCMD.CMD_AUTH_LOGON_PROOF);
            Write((byte) result);
        }
    }
}
