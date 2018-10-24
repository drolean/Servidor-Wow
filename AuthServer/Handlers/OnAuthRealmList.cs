using AuthServer.PacketServer;

namespace AuthServer.Handlers
{
    public class OnAuthRealmList
    {
        internal static void Handler(AuthServerSession session, byte[] data)
        {
            var realms = MainProgram.Database.GetRealms();
            session.SendPacket(new CMD_AUTH_REALMLIST(realms, session.User.Username));
        }
    }
}