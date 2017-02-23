using System;
using System.Collections.Generic;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace RealmServer.Handlers
{
    #region SMSG_FRIEND_LIST
    sealed class SmsgFriendList : PacketServer
    {
        public SmsgFriendList(int charId) : base(RealmCMD.SMSG_FRIEND_LIST)
        {
            // Query Database

            // Make the packet
            /*
            SMSG_FRIEND_LIST.AddInt8(q.Rows.Count)

            foreach (DataRow r in q.Rows)
            {
                ulong GUID = r.Item("friend");
                SMSG_FRIEND_LIST.AddUInt64(GUID);
                //Player GUID
                if (CHARACTERs.ContainsKey(GUID) && CHARACTERs(GUID).IsInWorld)
                {
                    //If CType(CHARACTERs(guid), CharacterObject).DND Then
                    //    SMSG_FRIEND_LIST.AddInt8(FriendStatus.FRIEND_STATUS_DND)
                    //ElseIf CType(CHARACTERs(guid), CharacterObject).AFK Then
                    //    SMSG_FRIEND_LIST.AddInt8(FriendStatus.FRIEND_STATUS_AFK)
                    //Else
                    SMSG_FRIEND_LIST.AddInt8(FriendStatus.FRIEND_STATUS_ONLINE);
                    //End If
                    SMSG_FRIEND_LIST.AddInt32(CHARACTERs(GUID).Zone);
                    //Area
                    SMSG_FRIEND_LIST.AddInt32(CHARACTERs(GUID).Level);
                    //Level
                    SMSG_FRIEND_LIST.AddInt32(CHARACTERs(GUID).Classe);
                    //Class
                }
                else
                {
                    SMSG_FRIEND_LIST.AddInt8(FriendStatus.FRIEND_STATUS_OFFLINE);
                }
            }
            */
            // nao tem ninguem
            Write(0);
        }
    }
    #endregion

    #region SMSG_IGNORE_LIST
    sealed class SmsgIgnoreList : PacketServer
    {
        public SmsgIgnoreList(int charId) : base(RealmCMD.SMSG_IGNORE_LIST)
        {
            // Query Database

            // Make Packet
            /*
            if (q.Rows.Count > 0)
            {
                SMSG_IGNORE_LIST.AddInt8(q.Rows.Count);

                foreach (DataRow r in q.Rows)
                {
                    SMSG_IGNORE_LIST.AddUInt64(r.Item("friend"));
                    //Player GUID
                }
            }
            else
            {
                SMSG_IGNORE_LIST.AddInt8(0);
            }
            */
        }
    }
    #endregion

    #region SMSG_FRIEND_STATUS
    sealed class SmsgFriendStatus : PacketServer
    {
        public SmsgFriendStatus(Characters character, FriendResult opcode) : base(RealmCMD.SMSG_FRIEND_STATUS)
        {
            // Make Packet
            /*
            response.AddInt8(FriendResult.FRIEND_IGNORE_NOT_FOUND) // todos sao assim
            response.AddUInt64(GUID)

            // FRIEND_ADDED_OFFLINE
            response.AddString(name)

            // FRIEND_ADDED_ONLINE
            response.AddString(name)
            If CHARACTERs(GUID).DND Then
            response.AddInt8(FriendStatus.FRIEND_STATUS_DND)
            ElseIf CHARACTERs(GUID).AFK Then
            response.AddInt8(FriendStatus.FRIEND_STATUS_AFK)
            Else
            response.AddInt8(FriendStatus.FRIEND_STATUS_ONLINE)
            End If
            response.AddInt32(CHARACTERs(GUID).Zone)
            response.AddInt32(CHARACTERs(GUID).Level)
            response.AddInt32(CHARACTERs(GUID).Classe)
            */
        }
    }
    #endregion

    #region SMSG_WHO
    sealed class SmsgWho : PacketServer
    {
        public SmsgWho(Characters character, FriendResult opcode) : base(RealmCMD.SMSG_WHO)
        {
            // Make Packet
            /*
            response.AddInt32(results.Count)
            response.AddInt32(results.Count)

            For Each GUID As ULong In results
                response.AddString(CHARACTERs(GUID).Name)           'Name
                If CHARACTERs(GUID).Guild IsNot Nothing Then
                    response.AddString(CHARACTERs(GUID).Guild.Name) 'Guild Name
                Else
                    response.AddString("")                          'Guild Name
                End If
                response.AddInt32(CHARACTERs(GUID).Level)           'Level
                response.AddInt32(CHARACTERs(GUID).Classe)          'Class
                response.AddInt32(CHARACTERs(GUID).Race)            'Race
                response.AddInt32(CHARACTERs(GUID).Zone)            'Zone ID
            Next
            */
        }
    }
    #endregion

    internal class SocialHandler
    {
        internal static void OnFriendList(RealmServerSession session, PacketReader handler)
        {
            session.SendPacket(new SmsgFriendList(session.Character.Id));
            session.SendPacket(new SmsgIgnoreList(session.Character.Id));
        }

        internal static void OnAddFriend(RealmServerSession session, PacketReader handler)
        {
            string Name = handler.ReadCString();

            // Query Database 

            // SmsgFriendStatus
        }

        internal static void OnAddIgnore(RealmServerSession session, PacketReader handler)
        {
            string Name = handler.ReadCString();

            // Query Database 

            // SmsgFriendStatus
        }

        internal static void OnDelFriend(RealmServerSession session, PacketReader handler)
        {
            ulong GUID = handler.ReadUInt64();

            // Query Database 

            // SmsgFriendStatus
        }

        internal static void OnDelIgnore(RealmServerSession session, PacketReader handler)
        {
            ulong GUID = handler.ReadUInt64();

            // Query Database 

            // SmsgFriendStatus
        }

        internal static void OnWho(RealmServerSession session, PacketReader handler)
        {
            uint levelMinimum = handler.ReadUInt32(); // 0
            uint levelMaximum = handler.ReadUInt32(); // 100
            string namePlayer = handler.ReadCString();
            string nameGuild  = handler.ReadCString();
            uint maskRace     = handler.ReadUInt32();
            uint maskClass    = handler.ReadUInt32();
            uint zonesCount   = handler.ReadUInt32();

            //Limited to 10
            if (zonesCount > 10)
                return;

            List<uint> zones = new List<uint>();
            for (int i = 1; i <= zonesCount; i++)
            {
                zones.Add(handler.ReadUInt32());
            }

            uint stringsCount = handler.ReadUInt32();

            //Limited to 4
            if (stringsCount > 4)
                return;

            List<string> Strings = new List<string>();
            for (int i = 1; i <= stringsCount; i++)
            {
                Strings.Add(handler.ReadCString());
            }

            Log.Print(LogType.Debug,
                $"[{session.ConnectionSocket}] CMSG_WHO [P:'{namePlayer}' G:'{nameGuild}' L:{levelMinimum}-{levelMaximum} C:{maskClass:X} R:{maskRace:X}]");

            // Don't show GMs?

            // List first 49 characters (like original)

            // SmsgWho
        }
    }

    public enum FriendResult : byte
    {
        FRIEND_DB_ERROR = 0x0,
        FRIEND_LIST_FULL = 0x1,
        FRIEND_ONLINE = 0x2,
        FRIEND_OFFLINE = 0x3,
        FRIEND_NOT_FOUND = 0x4,
        FRIEND_REMOVED = 0x5,
        FRIEND_ADDED_ONLINE = 0x6,
        FRIEND_ADDED_OFFLINE = 0x7,
        FRIEND_ALREADY = 0x8,
        FRIEND_SELF = 0x9,
        FRIEND_ENEMY = 0xa,
        FRIEND_IGNORE_FULL = 0xb,
        FRIEND_IGNORE_SELF = 0xc,
        FRIEND_IGNORE_NOT_FOUND = 0xd,
        FRIEND_IGNORE_ALREADY = 0xe,
        FRIEND_IGNORE_ADDED = 0xf,
        FRIEND_IGNORE_REMOVED = 0x10
    }
}
