using AuthServer.PacketReader;
using Common.Crypt;
using Common.Globals;

namespace AuthServer.Handlers
{
    public class OnAuthLogonChallenge
    {
        internal static void Handler(AuthServerSession session, CMD_AUTH_LOGON_CHALLENGE packet)
        {
            if (!packet.Version.Contains("1.12"))
            {
                session.SendData(new PacketServer.CMD_AUTH_LOGON_CHALLENGE(AccountState.BADVERSION));
                return;
            }

            if (packet.Username.Length <= 3)
            {
                session.SendData(new PacketServer.CMD_AUTH_LOGON_CHALLENGE(AccountState.UNKNOWN_ACCOUNT));
                return;
            }

            session.User = MainProgram.Database.GetAccount(packet.Username).Result;

            if (session.User != null)
            {
                session.Srp = new Srp6(session.User.Username, session.User.Password.ToUpper());
                session.SendData(new PacketServer.CMD_AUTH_LOGON_CHALLENGE(session, AccountState.OK));
                return;
            }

            session.SendData(new PacketServer.CMD_AUTH_LOGON_CHALLENGE(AccountState.UNKNOWN_ACCOUNT));
        }
    }
}