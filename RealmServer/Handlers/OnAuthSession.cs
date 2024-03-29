﻿using System;
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
            // Search Account and set to session.User
            session.User = MainProgram.RealmServerDatabase.GetAccount(handler.ClientAccount);

            // Initializing Crypt for user session
            session.PacketCrypto = new VanillaCrypt();
            session.PacketCrypto.Init(session.User.SessionKey);

            // Check basic addons instaleds
            CheckAddons(handler);

            // Send AuthOK
            session.SendPacket(new SMSG_AUTH_RESPONSE(LoginErrorCode.AUTH_OK));
            session.SendPacket(new SMSG_ADDON_INFO(AddOnsNames));
        }

        private static void CheckAddons(BinaryReader handler)
        {
            AddOnsNames.Clear();

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

                    Console.WriteLine(@"Addon {0}: [ {1} ] enabled: {2} | crc: {3} | unk7: {4}",
                        i.ToString().PadRight(2, ' '), addonName.PadRight(30, ' '), enabled,
                        crc, unk7);
                }
            }
        }
    }
}
