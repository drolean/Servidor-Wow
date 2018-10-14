using AuthServer.PacketReader;
using AuthServer.PacketServer;
using Common.Crypt;
using Common.Globals;

namespace AuthServer.Handlers
{
    public class OnAuthLogonProof
    {
        internal static void Handler(AuthServerSession session, AuthLogonProof handler)
        {
            session.Srp.ClientEphemeral = handler.A.ToPositiveBigInteger();
            session.Srp.ClientProof = handler.M1.ToPositiveBigInteger();

            if (session.Srp.ClientProof == session.Srp.GenerateClientProof())
            {
                MainProgram.Database.SetSessionKey(session.AccountName, session.Srp.SessionKey.ToProperByteArray());
                session.SendData(new PsAuthLogonProof(session.Srp, AccountState.OK));
                return;
            }

            session.SendData(new PsAuthLogonProof(AccountState.UNKNOWN_ACCOUNT));
        }
    }
}