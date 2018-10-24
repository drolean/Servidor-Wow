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
        /// <param name="srp"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public CMD_AUTH_LOGON_PROOF(Srp6 srp, AccountState result) : base(AuthCMD.CMD_AUTH_LOGON_PROOF)
        {
            Write((byte) AuthCMD.CMD_AUTH_LOGON_PROOF);
            Write((byte) result);
            Write(srp.ServerProof.ToByteArray().Pad(20));
            this.WriteNullByte(4);
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