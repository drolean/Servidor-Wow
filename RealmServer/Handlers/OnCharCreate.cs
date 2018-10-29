using System;
using System.Threading.Tasks;
using Common.Globals;
using Common.Helpers;
using RealmServer.Database;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnCharCreate
    {
        internal static async void Handler(RealmServerSession session, CMSG_CHAR_CREATE handler)
        {
            session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_SUCCESS));

            // If limit character reached
            if (Characters.GetCharacters(session.User).Count >= Config.Instance.LimitCharacterRealm)
            {
                session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_SERVER_LIMIT));
                return;
            }

            try
            {
                Characters.Create(handler, session.User);
            }
            catch (Exception)
            {
                session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_ERROR));
            }
            finally
            {
                await Task.Delay(2500);
                session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.CHAR_CREATE_SUCCESS));
            }
        }
    }
}
