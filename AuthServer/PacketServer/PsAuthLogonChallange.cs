using Common.Crypt;
using Common.Globals;
using Common.Helpers;

namespace AuthServer.PacketServer
{
    internal sealed class PsAuthLogonChallange : Common.Network.PacketServer
    {
        /// <summary>
        ///   Opcode          : byte;            0x00
        ///   Error           : byte;            AccountState
        ///   Size            : byte;            unkown1 is set to 0 by all private servers.
        ///   =====           : array of byte;   SRP6 server public ephemeral
        ///   =====           : byte;            generator_len is the length of the generator field following it. All servers (including ours) set this to 1.
        ///   =====           : array of byte;   All servers (including ours) hardcode this to 7
        ///   =====           : byte;            All servers (including ours) set this to 32.
        ///
        ///   =====           : array of byte;   All servers (including ours) set this to 0x894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7
        ///   =====           : array of byte;   A salt is a randomly generated value used to strengthen a user's password against attacks where pre-computations are performed
        ///
        ///   =====           : byte;            unknown2 is set to 16 random bytes by all servers.
        /// </summary>
        /// <param name="srp"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public PsAuthLogonChallange(Srp6 srp, AccountState result) : base(AuthCMD.CMD_AUTH_LOGON_CHALLENGE)
        {
            Write((byte) AuthCMD.CMD_AUTH_LOGON_CHALLENGE);
            Write((byte) result);
            Write((byte) 0);
            Write(srp.ServerEphemeral.ToProperByteArray());
            Write((byte) 1);
            Write(srp.Generator.ToByteArray());
            Write((byte) 32);
            Write(srp.Modulus.ToProperByteArray().Pad(32));
            Write(srp.Salt.ToProperByteArray().Pad(32));
            this.WriteNullByte(17);
        }
    }
}