using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace RealmServer
{
    #region CMSG_AUTH_SESSION
    public sealed class CmsgAuthSession : PacketReader
    {
        public int Build { get; private set; }
        public int Unk2 { get; private set; }
        public string User { get; private set; }

        public CmsgAuthSession(byte[] data) : base(data)
        {
            Build = ReadInt32();
            Unk2  = ReadInt32();
            User  = ReadCString();
        }
    }
    #endregion

    #region SMSG_AUTH_RESPONSE
    sealed class SmsgAuthResponse : PacketServer
    {
        public SmsgAuthResponse() : base(RealmCMD.SMSG_AUTH_RESPONSE)
        {
            Write((byte) 0x0C);
        }
    }
    #endregion

    internal class RealmServerHandler
    {
        public static void OnAuthSession(RealmServerSession session, CmsgAuthSession handler)
        {
            Log.Print(LogType.RealmServer, $"[{session.ConnectionSocket.RemoteEndPoint}] CMSG_AUTH_SESSION");

            //session.Users = mainForm.Database.GetAccount(handler.User);

            // Check Account
            //if(session.Users == null)
            session.SendPacket(new SmsgAuthResponse());//.WOW_FAIL_UNKNOWN_ACCOUNT));
            /*
            // Kick if existing
            Console.WriteLine(session.ConnectionId);

            // Set client.SS_Hash
            session.PacketCrypto = new VanillaCrypt();
            session.PacketCrypto.Init(session.Users.sessionkey);

            //DONE: Disconnect clients trying to enter with an invalid build
            //if (handler.Build < 5875 || handler.Build > 6141)
                //session.SendPacket(new SmsgAuthResponse(AuthResult.WOW_FAIL_VERSION_INVALID));

            // Disconnect clients trying to enter with an invalid build

            // If server full then queue, If GM/Admin let in

            // Send packet
            //session.SendPacket(new SmsgAuthResponse());
            */
        }
    }
}
