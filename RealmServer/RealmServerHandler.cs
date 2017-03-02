using System.Collections.Generic;
using System.IO;
using Common.Crypt;
using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace RealmServer
{

    #region CMSG_AUTH_SESSION
    public sealed class CmsgAuthSession : PacketReader
    {
        public int ClientVersion { get; }
        public int ClientSessionId { get; }
        public string ClientAccount { get; }
        public int ClientSeed { get; }
        public byte[] ClientHash;
        public int ClientAddOnsSize;

        public CmsgAuthSession(byte[] data) : base(data)
        {
            ClientVersion   = ReadInt32();
            ClientSessionId = ReadInt32();
            ClientAccount   = ReadCString();
            ClientSeed      = ReadInt32();
            ClientHash      = ReadBytes(20);         
            ClientAddOnsSize = ReadInt32();
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

    #region SMSG_ADDON_INFO
    internal sealed class SmsgAddonInfo : PacketServer
    {
        public SmsgAddonInfo(List<string> addOnsNames) : base(RealmCMD.SMSG_ADDON_INFO)
        {
            for (int i = 0; i <= 11/*addOnsNames.Count - 1*/; i++)
            {
                /*
                if (File.Exists($"interface\\{addOnsNames[i]}.pub"))
                {
                    Write((byte)2); // AddOn Type [1-enabled, 0-banned, 2-blizzard]
                    Write((byte)1);

                    FileStream fs = new FileStream($"interface\\{addOnsNames[i]}.pub", FileMode.Open, FileAccess.Read,
                        FileShare.Read, 258, FileOptions.SequentialScan);
                    byte[] fb = new byte[257];
                    fs.Read(fb, 0, 257);

                    //NOTE: Read from file
                    //AddByteArray(fb);

                    Write((uint)0);
                    Write((short)0);
                } else
                {
                    */
                    //We don't have hash data or already sent to client
                    Write((byte) 2); // AddOn Type [1-enabled, 0-banned, 2-blizzard]
                    Write((byte) 1);
                    Write((uint) 0);
                    Write((short) 0);                   
                //}
            }
        }
    }
    #endregion

    internal class RealmServerHandler
    {
        public static void OnAuthSession(RealmServerSession session, CmsgAuthSession handler)
        {
            // Check the version of client trying to connect [5875]

            // DONE: Check Account
            session.Users = MainForm.Database.GetAccount(handler.ClientAccount);

            // Kick if existing

            // Check if account is banned

            // DONE: Set Crypt Hash Player
            session.PacketCrypto = new VanillaCrypt();
            session.PacketCrypto.Init(session.Users.sessionkey);

            // Disconnect clients trying to enter with an invalid build
            //if (handler.Build < 5875 || handler.Build > 6141)

            // Disconnect clients trying to enter with an invalid build

            // If server full then queue, If GM/Admin let in

            // DONE: Addons info reading
            #region NOT USED
            var addonData = handler.ReadBytes((int)handler.BaseStream.Length - (int)handler.BaseStream.Position);
            var decompressed = ZLib.Decompress(addonData);
            //RealmServerSession.DumpPacket(decompressed);
            List<string> addOnsNames = new List<string>();
            using (var reader = new PacketReader(new MemoryStream(decompressed)))
            {
                var count = reader.BaseStream.Length / sizeof(int);
                for (var i = 0; i < count; ++i)
                {
                    //var addonName = reader.ReadString();
                    //if (addonName.Equals("")) continue;
                    //addOnsNames.Add(addonName);
                }
            }
            #endregion

            // Update [IP / Build]

            // Create Log 

            // Init Warden

            // DONE: Send Addon Packet
            session.SendPacket(new SmsgAddonInfo(addOnsNames));

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
