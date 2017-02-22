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
            Write((uint)0x0C);
            Write((uint)0x30); // BillingTimeRemaining
            Write((byte)0x78); // BillingPlanFlags
            Write((uint)0);    // BillingTimeRested
            Write((byte)0);    // Expansion Level [0 - normal, 1 - TBC]
            Write((byte)0);    // Server Expansion
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
            // DONE: Check Account
            session.Users = MainForm.Database.GetAccount(handler.User);

            // Kick if existing

            // DONE: Set Crypt Hash Player
            session.PacketCrypto = new VanillaCrypt();
            session.PacketCrypto.Init(session.Users.sessionkey);

            // Disconnect clients trying to enter with an invalid build
            //if (handler.Build < 5875 || handler.Build > 6141)

            // Disconnect clients trying to enter with an invalid build

            // If server full then queue, If GM/Admin let in

            // Addons info reading

            // DONE: Send packet
            session.SendPacket(new SmsgAuthResponse());
        }

        public static void OnPingPacket(RealmServerSession session, CmsgPing handler)
        {
            session.SendPacket(new SmsgPong(handler.Ping));

            // Set latency to char
//            if (session.Character != null)
//                session.Character.Latency = handler.Latency;
        }
    }
}
