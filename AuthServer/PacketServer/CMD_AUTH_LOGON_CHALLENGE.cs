using Common.Crypt;
using Common.Globals;
using Common.Helpers;

namespace AuthServer.PacketServer
{
    internal sealed class CMD_AUTH_LOGON_CHALLENGE : Common.Network.PacketServer
    {
        /// <summary>
        ///     Initial authentication step, the client sent a challenge.
        ///     Opcode          : byte;            0x00
        ///     Error           : byte;            AccountState
        ///     Size            : byte;            unkown1 is set to 0 by all private servers.
        ///     =====           : array of byte;   SRP6 server public ephemeral
        ///     =====           : byte;            generator_len is the length of the generator field following it. All servers
        ///     (including ours) set this to 1.
        ///     =====           : array of byte;   All servers (including ours) hardcode this to 7
        ///     =====           : byte;            All servers (including ours) set this to 32.
        ///     =====           : array of byte;   All servers (including ours) set this to
        ///     0x894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7
        ///     =====           : array of byte;   A salt is a randomly generated value used to strengthen a user's password
        ///     against attacks where pre-computations are performed
        ///     =====           : byte;            unknown2 is set to 16 random bytes by all servers.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public CMD_AUTH_LOGON_CHALLENGE(AuthServerSession session, AccountState result) : base(
            AuthCMD.CMD_AUTH_LOGON_CHALLENGE)
        {
            Write((byte) AuthCMD.CMD_AUTH_LOGON_CHALLENGE);
            Write((byte) result);

            if (session.User.BannetAt != null)
            {
                Write((byte) AccountState.BANNED);
                return;
            }

            if (session.User.Online)
            {
                Write((byte) AccountState.ALREADYONLINE);
                return;
            }

            Write((byte) AccountState.OK);
            Write(session.Srp.ServerEphemeral.ToProperByteArray());
            Write((byte) 1);
            Write(session.Srp.Generator.ToByteArray());
            Write((byte) 32);
            Write(session.Srp.Modulus.ToProperByteArray().Pad(32));
            Write(session.Srp.Salt.ToProperByteArray().Pad(32));
            this.WriteNullByte(16);

            // https://github.com/vmangos/core/blob/58e83ed56bd863c3ad433931a3a08b5f4fac5e76/src/realmd/AuthSocket.cpp#L561
            //Write((byte) 1); // securityFlags, only '1' is available in classic (PIN input)
            //Write((uint) 1234567);
            //this.WriteNullByte(16); //16 Bytes Random

            // Off 2FA 
            Write((byte) 0);
        }

        /// <summary>
        ///     Initial authentication step, the client sent a challenge.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public CMD_AUTH_LOGON_CHALLENGE(AccountState result) : base(AuthCMD.CMD_AUTH_LOGON_CHALLENGE)
        {
            Write((byte) LoginErrorCode.RESPONSE_FAILURE);
            Write((byte) result);
        }
    }
}
