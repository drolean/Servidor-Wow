using Common.Crypt;
using Common.Globals;
using Common.Helpers;

namespace AuthServer.PacketServer
{
    internal sealed class PsAuthLogonProof : Common.Network.PacketServer
    {
        /// <summary>
        /// </summary>
        /// <param name="srp"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public PsAuthLogonProof(Srp6 srp, AccountState result) : base(AuthCMD.CMD_AUTH_LOGON_PROOF)
        {
            Write((byte) AuthCMD.CMD_AUTH_LOGON_PROOF);
            Write((byte) result);
            Write(srp.ServerProof.ToByteArray().Pad(20));
            this.WriteNullByte(4);
        }
    }
}