using AuthServer.PacketReader;
using AuthServer.PacketServer;
using Common.Crypt;
using Common.Globals;
using Common.Helpers;

namespace AuthServer.Handlers
{
    public class OnAuthLogonChallenge
    {
        internal static void Handler(AuthServerSession session, AuthLogonChallenge packet)
        {
            Log.Print(LogType.AuthServer,
                $"[{session.ConnectionSocket.RemoteEndPoint}] {packet.Username} => " +
                $"WoW Version: {packet.Version} Build: {packet.Build} Lang: {packet.Country}");

            if (!packet.Version.Contains("1.12"))
            {
                session.SendData(new PsAuthLogonChallange(AccountState.BADVERSION));
                return;
            }

            if (packet.Username.Length <= 3)
            {
                session.SendData(new PsAuthLogonChallange(AccountState.UNKNOWN_ACCOUNT));
                return;
            }

            var user = MainProgram.Database.GetAccount(packet.Username);

            if (user != null)
            {
                session.AccountName = user?.Username;
                session.Srp = new Srp6(user.Username, user.Password.ToUpper());
                session.SendData(new PsAuthLogonChallange(session.Srp, AccountState.OK));
                return;
            }

            session.SendData(new PsAuthLogonChallange(AccountState.UNKNOWN_ACCOUNT));
        }
    }
}