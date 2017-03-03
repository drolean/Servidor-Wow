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
            //WriteCString(character.name);
            Write(Encoding.UTF8.GetBytes(character.name + '\0'));
            Write((byte)0); // realm name for cross realm BG usage
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

            Write((uint) ts.TotalSeconds);
        }
    }
    #endregion

    #region SMSG_TEXT_EMOTE
    internal sealed class SmsgTextEmote : PacketServer
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
    internal sealed class SmsgSetFactionStanding : PacketServer
    {
        public SmsgSetFactionStanding(int faction, byte enabled, int standing) : base(RealmCMD.SMSG_SET_FACTION_STANDING)
        {
            Write((uint) enabled); // flag.database
            Write((uint) faction); // id.database
            Write((uint) standing); // standing.database
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
            session.Entity.SetUpdateField((int) UnitFields.UNIT_NPC_EMOTESTATE, 0x0);

            // Find Creature / Player with the recv GUID

            // DONE: Send Packet
            session.SendPacket(new SmsgTextEmote(session.Character.Id, textEmote, (int) unk));
        }

        internal static void OnCompleteCinematic(RealmServerSession session, PacketReader handler)
        {
            MainForm.Database.UpdateCharacter(session.Character.Id, "firstlogin");
        }

        internal static void OnTutorialClear(RealmServerSession session, PacketReader handler)
        {
            // Aqui seta como feito tutorial
            Console.WriteLine("Tutoriais Feitos");
        }

        internal static void OnTutorialFlag(RealmServerSession session, PacketReader handler)
        {
            int flag = handler.ReadInt32();
            Console.WriteLine($@"vem FLAG [{flag}] {flag / 8} [{1 << 7 - flag % 8}]");
            //client.Character.TutorialFlags((Flag \ 8)) = client.Character.TutorialFlags((Flag \ 8)) + (1 << 7 - (Flag Mod 8))           
            //client.Character.TutorialFlags((Flag / 8)) == client.Character.TutorialFlags((Flag / 8)) + (1 << 7 - (Flag % 8))
        }

        internal static void OnTutorialReset(RealmServerSession session, PacketReader handler)
        {
            Console.WriteLine("Tutoriais Resetados");
        }

        // [DONE] Set Faction AT War
        internal static void OnSetFactionAtwar(RealmServerSession session, PacketReader handler)
        {
            // Adicionar Faccao a base de dados
            int faction = handler.ReadInt32();
            byte enabled = handler.ReadByte();

            // [7 Enabled] --- [5 Disabled]
            MainForm.Database.FactionInative(session.Character.Id, faction + 1, (byte) (enabled == 1 ? 7 : 5));

            var factionDb = MainForm.Database.FactionGet(session.Character, faction + 1);
            
            // SmsgSetFactionStanding
            session.SendPacket(new SmsgSetFactionStanding(faction, enabled, factionDb.standing));
        }

        // [DONE] Set Inactive Faction
        internal static void OnSetFactionInactive(RealmServerSession session, PacketReader handler)
        {
            // Adicionar Faccao a base de dados
            int faction = handler.ReadInt32();
            byte enabled = handler.ReadByte();

            // [17 Enabled] --- [49 Disabled]
            MainForm.Database.FactionInative(session.Character.Id, faction + 1, (byte) (enabled == 1 ? 49 : 17));
            /*
                7 = At War / 5 = Enable At War Button / 3 = At War / 2 = Enable At War Button / 33 = Inative / 37 = Inative com At War
                39 = Inative At War / 49 = Inative / 51 = Inative At War[Button Disabled] / 53 = Inative
                enabled = 17 / 19 = At War /  33 = Inative
            */
        }

        // [DONE] Set Watched Faction
        internal static void OnSetWatchedFaction(RealmServerSession session, PacketReader handler)
        {
            int faction = handler.ReadInt32();

            if (faction == -1)
                faction = 0xff;

            if (faction < 0 || faction > 255)
                return;

            MainForm.Database.UpdateCharacter(session.Character.Id, "watchFaction", faction.ToString());
            session.Entity.SetUpdateField((int) PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, faction);
        }
    }
}