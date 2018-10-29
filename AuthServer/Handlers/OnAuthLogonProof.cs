using AuthServer.PacketReader;
using Common.Crypt;
using Common.Globals;

namespace AuthServer.Handlers
{
    public class OnAuthLogonProof
    {
        internal static void Handler(AuthServerSession session, CMD_AUTH_LOGON_PROOF handler)
        {
            session.Srp.ClientEphemeral = handler.A.ToPositiveBigInteger();
            session.Srp.ClientProof = handler.M1.ToPositiveBigInteger();

            if (session.Srp.ClientProof == session.Srp.GenerateClientProof())
            {
                MainProgram.Database.SetSessionKey(session.User.Username, session.Srp.SessionKey.ToProperByteArray());
                session.SendData(new PacketServer.CMD_AUTH_LOGON_PROOF(session, AccountState.OK));
                return;
            }

            session.SendData(new PacketServer.CMD_AUTH_LOGON_PROOF(AccountState.UNKNOWN_ACCOUNT));
        }
    }
}
