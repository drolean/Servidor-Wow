using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Common.Crypt;
using Common.Globals;
using Common.Helpers;
using Common.Network;
using RealmServer.Handlers;

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
            ClientVersion    = ReadInt32();
            ClientSessionId  = ReadInt32();
            ClientAccount    = ReadCString();
            ClientSeed       = ReadInt32();
            ClientHash       = ReadBytes(20);
            ClientAddOnsSize = ReadInt32();
        }
    }
    #endregion

    #region SMSG_AUTH_RESPONSE
    internal sealed class SmsgAuthResponse : PacketServer
    {
        /// <summary>
        /// Send Auth Response to Client
        /// </summary>
        /// <param name="state">LoginErroCode</param>
        /// <param name="count">Count to queue position</param>
        /// <returns></returns>
        public SmsgAuthResponse(LoginErrorCode state, int count = 0) : base(RealmCMD.SMSG_AUTH_RESPONSE)
        {
            Write((byte) state);
            Write((uint) count);
            // idk
            Write((byte) 0x78); // BillingPlanFlags
            Write((uint) 0);    // BillingTimeRested
            Write((byte) 0);    // Expansion Level [0 - normal, 1 - TBC]
            Write((byte) 0);    // Server Expansion
        }
    }
    #endregion

    #region CMSG_PING
    public sealed class CmsgPing : PacketReader
    {
        public uint Ping { get; }
        public uint Latency { get; }

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
            Write((ulong) ping);
        }
    }
    #endregion

    #region SMSG_ADDON_INFO
    internal sealed class SmsgAddonInfo : PacketServer
    {
        public List<string> AddOnsNames { get; }

        public SmsgAddonInfo(List<string> addOnsNames) : base(RealmCMD.SMSG_ADDON_INFO)
        {
            AddOnsNames = addOnsNames;
            for (int i = 0; i <= addOnsNames.Count; i++)
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
                    //We don't have hash data or already sent to client
                    Write((byte) 2); // AddOn Type [1-enabled, 0-banned, 2-blizzard]
                    Write((byte) 1);
                    Write((uint) 0);
                    Write((short) 0);
                }
                */
            }
        }
    }
    #endregion

    internal class RealmServerHandler
    {
        public static void OnAuthSession(RealmServerSession session, CmsgAuthSession handler)
        {
            // DONE: Check the version of client trying to connect [5875]
            if (handler.ClientVersion < 5875 || handler.ClientVersion > 6141)
            {
                Log.Print(LogType.Error, $"[{session.ConnectionRemoteIp}] Invalid WOW Version {handler.ClientVersion}");
                session.SendPacket(new SmsgAuthResponse(LoginErrorCode.AUTH_REJECT));
                Thread.Sleep(1500);
                session.ConnectionSocket.Disconnect(false);
                return;
            }

            // DONE: Check Account
            session.Users = MainProgram.Database.GetAccount(handler.ClientAccount);

            // TODO: Kick if existing

            // DONE: Check if account is banned
            if (session.Users.bannet_at != null)
            {
                Log.Print(LogType.Error, $"[{session.ConnectionRemoteIp}] User Banner (second) Check");
                session.SendPacket(new SmsgAuthResponse(LoginErrorCode.AUTH_REJECT));
                Thread.Sleep(1500);
                session.ConnectionSocket.Disconnect(false);
                return;
            }

            // DONE: Set Crypt Hash Player
            session.PacketCrypto = new VanillaCrypt();
            session.PacketCrypto.Init(session.Users.sessionkey);

            // TODO: If server full then queue, If GM/Admin let in
            /*
            Log.Print(LogType.RealmServer,
                $"[{session.ConnectionRemoteIp}] AUTH_WAIT_QUEUE: Server player limit reached!");
            session.SendPacket(new SmsgAuthResponse(LoginErrorCode.AUTH_WAIT_QUEUE, 10));
            */

            // DONE: Addons info reading
            var addonData = handler.ReadBytes((int)handler.BaseStream.Length - (int)handler.BaseStream.Position);
            var decompressed = ZLib.Decompress(addonData);
            List<string> addOnsNames = new List<string>();
            using (var reader = new PacketReader(new MemoryStream(decompressed)))
            {
                // TODO: need a size for FOR rights
                for (var i = 0; i < 12; ++i)
                {
                    var addonName = reader.ReadCString();
                    var enabled   = reader.ReadByte();
                    var crc       = reader.ReadUInt32();
                    var unk7      = reader.ReadUInt32();
                    //Console.WriteLine(@"Addon {0}: name {1}, enabled {2}, crc {3}, unk7 {4}", i, addonName, enabled, crc, unk7);
                    addOnsNames.Add(addonName);
                }
            }

            // Update [IP / Build]

            // Create Log

            // Init Warden

            // DONE: Send Addon Packet
            session.SendPacket(new SmsgAddonInfo(addOnsNames));

            // DONE: Send packet
            session.SendPacket(new SmsgAuthResponse(LoginErrorCode.AUTH_OK));
        }

        public static void OnPingPacket(RealmServerSession session, CmsgPing handler)
        {
            session.SendPacket(new SmsgPong(handler.Ping));

            // Set latency to char
            //if (session.Character != null)
                //session.Character.Latency = handler.Latency;

            //Console.WriteLine(session.Character.Latency);
        }
    }
}
