using System;
using System.Collections.Generic;
using System.IO;
using Common.Crypt;
using Common.Globals;
using Common.Helpers;
using RealmServer.PacketReader;
using RealmServer.PacketServer;

namespace RealmServer.Handlers
{
    public class OnAuthSession
    {
        private static readonly List<string> AddOnsNames = new List<string>();

        public static void Handler(RealmServerSession session, CMSG_AUTH_SESSION handler)
        {
            // Search Account and set to session.Users
            session.Users = MainProgram.RealmServerDatabase.GetAccount(handler.ClientAccount);

            // Initializing Crypt for user session
            session.PacketCrypto = new VanillaCrypt();
            session.PacketCrypto.Init(session.Users.sessionkey);

            // Check basic addons instaleds
            CheckAddons(handler);

            // Send AuthOK
            session.SendPacket(new SMSG_AUTH_RESPONSE(LoginErrorCode.AUTH_OK));

            session.SendPacket(new SMSG_ADDON_INFO(AddOnsNames));
            // TODO Alternatice to handle errors
            /*
            RESPONSE_VERSION_MISMATCH
            AUTH_FAILED
            AUTH_UNAVAILABLE
            AUTH_SYSTEM_ERROR
            AUTH_ALREADY_LOGGING_IN
            AUTH_SUSPENDED

            session.SendPacket(new SMSG_CHAR_CREATE(LoginErrorCode.AUTH_FAILED));
            */
        }

        private static void CheckAddons(BinaryReader handler)
        {
            var addonData = handler.ReadBytes((int) handler.BaseStream.Length - (int) handler.BaseStream.Position);
            var decompressed = ZLib.Decompress(addonData);

            using (var reader = new Common.Network.PacketReader(new MemoryStream(decompressed)))
            {
                // TODO: find length addons
                for (var i = 0; i < 12; ++i)
                {
                    var addonName = reader.ReadCString();
                    var enabled = reader.ReadByte();
                    var crc = reader.ReadUInt32();
                    var unk7 = reader.ReadUInt32();
                    AddOnsNames.Add(addonName);

                    Console.WriteLine(@"Addon {0}: name {1}, enabled {2}, crc {3}, unk7 {4}", i, addonName, enabled,
                        crc, unk7);
                }
            }
        }
    }
}