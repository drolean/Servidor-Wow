using Common.Crypt;
using Common.Globals;
using Common.Network;

namespace RealmServer
{

    #region CMSG_AUTH_SESSION
    public sealed class CmsgAuthSession : PacketReader
    {
        public int Build { get; private set; }
        public int Unk2 { get; private set; }
        public string User { get; }

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
            /*
                AUTH_OK = 0x0C,
                AUTH_FAILED = 0x0D,
                AUTH_WAIT_QUEUE = 0x1B,
            */
            Write((ulong) 0x0C);
        }
    }
    #endregion

    #region CMSG_PING
    public sealed class CmsgPing : PacketReader
    {
        public uint Ping { get; private set; }
        public uint Latency { get; private set; }

        public CmsgPing(byte[] data) : base(data)
        {
            Ping    = ReadUInt32();
            Latency = ReadUInt32();
        }
    }
    #endregion

    #region SMSG_PONG
    public sealed class SmsgPong : PacketServer
    {
        public SmsgPong(uint ping) : base(RealmCMD.SMSG_PONG)
        {
            Write((ulong)ping);
        }
    }
    #endregion

    internal class RealmServerHandler
    {
        public static void OnAuthSession(RealmServerSession session, CmsgAuthSession handler)
        {
            session.Users = MainForm.Database.GetAccount(handler.User);
            session.PacketCrypto = new VanillaCrypt();
            session.PacketCrypto.Init(session.Users.sessionkey);
            session.SendPacket(new SmsgAuthResponse());

            // Check Account

            // Kick if existing

            // Set client.SS_Hash

            // Disconnect clients trying to enter with an invalid build
            //if (handler.Build < 5875 || handler.Build > 6141)

            // Disconnect clients trying to enter with an invalid build

            // If server full then queue, If GM/Admin let in

            // Send packet
        }

        public static void OnPingPacket(RealmServerSession session, CmsgPing handler)
        {
            session.SendPacket(new SmsgPong(handler.Ping));
        }
    }
}
