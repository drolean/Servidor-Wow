using System;
using System.Text;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using Common.Network;

namespace RealmServer.Handlers
{
    #region SMSG_NAME_QUERY_RESPONSE
    public sealed class SmsgNameQueryResponse : PacketServer
    {
        public SmsgNameQueryResponse(Characters character) : base(RealmCMD.SMSG_NAME_QUERY_RESPONSE)
        {
            Write((ulong) character.Id);
            Write(Encoding.UTF8.GetBytes(character.name + '\0'));
            //Write((byte)0); // realm name for cross realm BG usage
            Write((uint) character.race);
            Write((uint) character.gender);
            Write((uint) character.classe);
        }
    }
    #endregion

    #region SMSG_QUERY_TIME_RESPONSE
    internal sealed class SmsgQueryTimeResponse : PacketServer
    {
        public SmsgQueryTimeResponse() : base(RealmCMD.SMSG_QUERY_TIME_RESPONSE)
        {
            DateTime baseDate = new DateTime(1970, 1, 1);
            TimeSpan ts = DateTime.Now - baseDate;

            Write((uint)ts.TotalSeconds);
        }
    }
    #endregion

    #region SMSG_TEXT_EMOTE
    public sealed class SmsgTextEmote : PacketServer
    {
        public SmsgTextEmote(int guid, uint emoteId, int textId) : base(RealmCMD.SMSG_TEXT_EMOTE)
        {
            Write((ulong) guid);
            Write(emoteId);
            Write((uint) textId);
            Write((uint) 1);
            Write((byte) 0);
        }
    }
    #endregion

    #region SMSG_SET_FACTION_STANDING
    public sealed class SmsgSetFactionStanding : PacketServer
    {
        public SmsgSetFactionStanding() : base(RealmCMD.SMSG_SET_FACTION_STANDING)
        {
            //response.AddInt32(Client.Character.Reputation(faction).Flags)
            //response.AddInt32(faction)
            //response.AddInt32(Client.Character.Reputation(faction).Value)
        }
    }
    #endregion   

    internal class MiscHandler
    {
        internal static void OnNameQuery(RealmServerSession session, PacketReader handler)
        {
            if (handler.BaseStream.Length < 12)
                return;

            ulong guid = handler.ReadUInt64();

            // Asking for player name
            Characters target = MainForm.Database.GetCharacter((uint) guid);

            if (target != null)
                session.SendPacket(new SmsgNameQueryResponse(target));

            // Asking for creature name (only used in quests?)

        }

        internal static void OnSetActiveMover(RealmServerSession session, PacketReader handler)
        {
            //ulong guid = handler.ReadUInt64();
        }

        internal static void OnQueryTime(RealmServerSession session, byte[] data)
        {
            session.SendPacket(new SmsgQueryTimeResponse());
        }

        internal static void OnBattlefieldStatus(RealmServerSession session, PacketReader handler)
        {
            // ???? nao implementado
        }

        internal static void OnMeetingstoneInfo(RealmServerSession session, PacketReader handler)
        {
            // ???? nao implementado
        }

        internal static void OnTextEmote(RealmServerSession session, PacketReader handler)
        {
            if (handler.BaseStream.Length < 20) return;
                
            uint textEmote = handler.ReadUInt32();
            uint unk = handler.ReadUInt32();
            //ulong guid = handler.ReadUInt64();

            Log.Print(LogType.Debug, $"[{session.ConnectionSocket}] CMSG_TEXT_EMOTE [TextEmote={textEmote} Unk={unk}]");

            // Some quests needs emotes being done

            // Doing emotes to guards

            // Send Emote animation
            session.Entity.SetUpdateField((int)UnitFields.UNIT_NPC_EMOTESTATE, 0x0);

            // Find Creature / Player with the recv GUID

            // DONE: Send Packet
            session.SendPacket(new SmsgTextEmote(session.Character.Id, textEmote, (int) unk));
        }

        internal static void OnCompleteCinematic(RealmServerSession session, PacketReader handler)
        {
            MainForm.Database.UpdateCharacter(session.Character.Id, "firstlogin");
        }

        internal static void OnSetFactionAtwar(RealmServerSession session, PacketReader handler)
        {
            uint faction = handler.ReadUInt32();
            byte enabled = handler.ReadByte();
            
            Log.Print(LogType.Debug, $"CMSG_SET_FACTION_ATWAR [faction={faction} enabled={enabled}]");

            // SmsgSetFactionStanding
        }

        internal static void OnSetFactionInactive(RealmServerSession session, PacketReader handler)
        {
            uint faction = handler.ReadUInt32();
            byte enabled = handler.ReadByte();

            Log.Print(LogType.Debug, $"CMSG_SET_FACTION_INACTIVE [faction={faction} enabled={enabled}]");
        }

        public static int aba = 0;
        internal static void OnSetWatchedFaction(RealmServerSession session, PacketReader handler)
        {
            uint faction = handler.ReadUInt32();
            // eae funcionou
            session.Entity.SetUpdateField((int)PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, aba);
            Log.Print(LogType.Debug, $"CMSG_SET_WATCHED_FACTION [faction={faction}] => {aba}");

            aba++;

            //client.Character.WatchedFactionIndex = faction
            //client.Character.SetUpdateFlag(EPlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, faction)
            //client.Character.SendCharacterUpdate(False)
        }
    }
}