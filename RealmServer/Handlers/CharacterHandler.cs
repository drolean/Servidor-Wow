using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Common.Database;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using Common.Network;
using RealmServer.Game;
using RealmServer.Game.Managers;

namespace RealmServer.Handlers
{

    #region SMSG_CHAR_ENUM

    sealed class SmsgCharEnum : PacketServer
    {
        public SmsgCharEnum(List<Characters> characters) : base(RealmCMD.SMSG_CHAR_ENUM)
        {
            Write((byte) characters.Count);

            foreach (Characters character in characters)
            {
                Write((ulong) character.Id);
                WriteCString(character.name);
                Write((byte) character.race);
                Write((byte) character.classe);
                Write((byte) character.gender);

                Write(character.char_skin);
                Write(character.char_face);
                Write(character.char_hairStyle);
                Write(character.char_hairColor);
                Write(character.char_facialHair);

                Write(character.level); // int8
                Write(character.MapZone); // int32
                Write(character.MapId); // int32

                Write(character.MapX);
                Write(character.MapY);
                Write(character.MapZ);

                Write(0); // Guild ID
                // if DEAD or any Restriction 
                Write((int) CharacterFlagState.CharacterFlagNone);
                // RestState
                Write((byte) 0);

                // SELECT modelid, level, entry FROM character_pet WHERE owner =
                Write(0); // PetModel
                Write(0); // PetLevel
                Write(0); // PetFamily = SELECT family FROM creature_template WHERE entry

                // DONE: Get items
                var inventory = MainForm.Database.GetInventory(character);

                for (int slot = 0; slot < 20; slot++)
                {
                    CharactersInventorys checkItem = inventory.FirstOrDefault(b => b.slot == slot);

                    if (checkItem != null)
                    {
                        var item = XmlReader.GetItem((int) checkItem.item);

                        Write(item.displayId);
                        Write((byte) item.inventoryType);
                    }
                    else
                    {
                        Write(0);
                        Write((byte) 0);
                    }
                }
            }
        }
    }

    #endregion

    #region  CMSG_CHAR_CREATE

    sealed class CmsgCharCreate : PacketReader
    {
        public string Name { get; private set; }

        public byte Race { get; private set; }
        public byte Classe { get; private set; }
        public byte Gender { get; private set; }

        public byte Skin { get; private set; }
        public byte Face { get; private set; }
        public byte HairStyle { get; private set; }
        public byte HairColor { get; private set; }
        public byte FacialHair { get; private set; }

        public byte OutfitId { get; private set; }

        public CmsgCharCreate(byte[] data) : base(data)
        {
            Name = ReadCString();

            Race = ReadByte();
            Classe = ReadByte();
            Gender = ReadByte();

            Skin = ReadByte();
            Face = ReadByte();
            HairStyle = ReadByte();
            HairColor = ReadByte();
            FacialHair = ReadByte();
            OutfitId = ReadByte();
        }
    }

    #endregion

    #region SMSG_CHAR_CREATE

    sealed class SmsgCharCreate : PacketServer
    {
        public SmsgCharCreate(int code) : base(RealmCMD.SMSG_CHAR_CREATE)
        {
            Write((byte) code);
        }
    }

    #endregion

    #region CMSG_CHAR_RENAME

    sealed class CmsgCharRename : PacketReader
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public CmsgCharRename(byte[] data) : base(data)
        {
            Id = ReadInt64();
            Name = ReadCString();
        }
    }

    #endregion

    #region SMSG_CHAR_RENAME

    sealed class SmsgCharRename : PacketServer
    {
        public SmsgCharRename(int code) : base(RealmCMD.SMSG_CHAR_RENAME)
        {
            Write((byte) code);
        }
    }

    #endregion

    #region CMSG_CHAR_DELETE

    sealed class CmsgCharDelete : PacketReader
    {
        public int Id { get; private set; }

        public CmsgCharDelete(byte[] data) : base(data)
        {
            Id = (int) ReadUInt64();
        }
    }

    #endregion

    #region SMSG_CHAR_DELETE

    sealed class SmsgCharDelete : PacketServer
    {
        public SmsgCharDelete(LoginErrorCode code) : base(RealmCMD.SMSG_CHAR_DELETE)
        {
            Write((byte) code);
        }
    }

    #endregion

    #region CMSG_PLAYER_LOGIN

    sealed class CmsgPlayerLogin : PacketReader
    {
        public uint Guid { get; private set; }

        public CmsgPlayerLogin(byte[] data) : base(data)
        {
            Guid = ReadUInt32();
        }
    }

    #endregion

    #region CMSG_UPDATE_ACCOUNT_DATA

    sealed class CmsgUpdateAccountData : PacketReader
    {
        public uint DataId { get; private set; }
        public uint UncompressedSize { get; private set; }

        public CmsgUpdateAccountData(byte[] data) : base(data)
        {
            if (data.Length - 1 < 13)
                return;

            DataId = ReadUInt32();
            UncompressedSize = ReadUInt32();

            if (DataId > 7)
                // ReSharper disable once RedundantJumpStatement
                return;

            // How does Mangos Zero Handle the Account Data For the Character?

            // Clear the entry

            // Can not handle more than 65534 bytes

            // Check if it's compressed, if so, decompress it
        }
    }

    #endregion

    #region SMSG_ACCOUNT_DATA_TIMES

    sealed class SmsgAccountDataTimes : PacketServer
    {
        public SmsgAccountDataTimes() : base(RealmCMD.SMSG_ACCOUNT_DATA_TIMES)
        {
            this.WriteNullByte(128);
        }
    }

    #endregion

    #region SMSG_TRIGGER_CINEMATIC

    sealed class SmsgTriggerCinematic : PacketServer
    {
        public SmsgTriggerCinematic(Characters character) : base(RealmCMD.SMSG_TRIGGER_CINEMATIC)
        {
            try
            {
                Write((int) XmlReader.GetRace(character.race).init.Cinematic);
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }
    }

    #endregion

    #region SMSG_BINDPOINTUPDATE

    sealed class SmsgBindpointupdate : PacketServer
    {
        public SmsgBindpointupdate(Characters character) : base(RealmCMD.SMSG_BINDPOINTUPDATE)
        {
            Write(character.MapX);
            Write(character.MapY);
            Write(character.MapZ);
            Write((uint) character.MapId);
            Write((short) character.MapZone);
        }
    }

    #endregion

    #region SMSG_SET_REST_START

    sealed class SmsgSetRestStart : PacketServer
    {
        public SmsgSetRestStart() : base(RealmCMD.SMSG_SET_REST_START)
        {
            Write((byte) 0);
        }
    }

    #endregion

    #region SMSG_TUTORIAL_FLAGS

    sealed class SmsgTutorialFlags : PacketServer
    {
        //TODO Write the uint ids of 8 tutorial values
        public SmsgTutorialFlags() : base(RealmCMD.SMSG_TUTORIAL_FLAGS)
        {
            // [8*Int32] or [32 Bytes] or [256 Bits Flags] Total!!!
            for (int i = 0; i < 8; i++)
            {
                Write((byte) 0xff);
            }
        }
    }

    #endregion

    #region SMSG_LOGIN_VERIFY_WORLD

    sealed class SmsgLoginVerifyWorld : PacketServer
    {
        public SmsgLoginVerifyWorld(Characters character) : base(RealmCMD.SMSG_LOGIN_VERIFY_WORLD)
        {
            Write(character.MapId);
            Write(character.MapX);
            Write(character.MapY);
            Write(character.MapZ);
            Write(character.MapO);
        }
    }

    #endregion

    #region SMSG_CORPSE_RECLAIM_DELAY

    sealed class SmsgCorpseReclaimDelay : PacketServer
    {
        public SmsgCorpseReclaimDelay() : base(RealmCMD.SMSG_CORPSE_RECLAIM_DELAY)
        {
            Write(30 * 1000);
        }
    }

    #endregion

    #region SMSG_INITIAL_SPELLS

    sealed class SmsgInitialSpells : PacketServer
    {
        public SmsgInitialSpells(Characters character) : base(RealmCMD.SMSG_INITIAL_SPELLS)
        {
            // Implement Spell Cooldowns
            var spells = MainForm.Database.GetSpells(character);

            Write((byte) 0); //int8
            Write((ushort) spells.Count); //int16

            ushort slot = 1;
            foreach (CharactersSpells spell in spells)
            {
                Write((ushort) spell.spell); //uint16
                Write(slot++); //int16
            }

            Write((UInt16) spells.Count); //in16
        }
    }

    #endregion

    #region SMSG_INITIALIZE_FACTIONS

    sealed class SmsgInitializeFactions : PacketServer
    {

        public SmsgInitializeFactions(Characters character) : base(RealmCMD.SMSG_INITIALIZE_FACTIONS)
        {
            var factions = MainForm.Database.GetFactions(character);

            Write((uint) factions.Count);
            foreach (var fact in factions)
            {
                Write((byte) fact.flags); // Flag 
                Write(fact.standing); // Value
            }
        }
    }

    #endregion

    #region SMSG_ACTION_BUTTONS

    sealed class SmsgActionButtons : PacketServer
    {
        public SmsgActionButtons(Characters character) : base(RealmCMD.SMSG_ACTION_BUTTONS)
        {
            List<CharactersActionBars> savedButtons = MainForm.Database.GetActionBar(character);

            for (int button = 0; button < 120; button++) //  119    'or 480 ?
            {
                int index = savedButtons.FindIndex(b => b.button == button);

                CharactersActionBars currentButton = index != -1 ? savedButtons[index] : null;

                if (currentButton != null)
                {
                    UInt32 packedData = (UInt32) currentButton.action | (UInt32) currentButton.type << 24;
                    Write(packedData);
                    //Write((UInt16)currentButton.action);
                    //Write((int)currentButton.type);
                    //Write((int)currentButton.); ?? misc???
                }
                else
                    Write((UInt32) 0);
            }
        }
    }

    #endregion

    #region SMSG_LOGOUT_RESPONSE

    internal sealed class SmsgLogoutResponse : PacketServer
    {
        public SmsgLogoutResponse(LogoutResponseCode code) : base(RealmCMD.SMSG_LOGOUT_RESPONSE)
        {
            Write((UInt32) 0);
            Write((byte) code);
        }
    }

    #endregion

    #region SMSG_LOGOUT_COMPLETE

    internal sealed class SmsgLogoutComplete : PacketServer
    {
        public SmsgLogoutComplete() : base(RealmCMD.SMSG_LOGOUT_COMPLETE)
        {
            Write((byte) 0);
        }
    }

    #endregion

    #region SMSG_LOGOUT_CANCEL_ACK

    internal sealed class SmsgLogoutCancelAck : PacketServer
    {
        public SmsgLogoutCancelAck() : base(RealmCMD.SMSG_LOGOUT_CANCEL_ACK)
        {
            Write((byte) 0);
        }
    }

    #endregion

    #region SMSG_STANDSTATE_UPDATE

    internal sealed class SmsgStandstateUpdate : PacketServer
    {
        public SmsgStandstateUpdate(byte state) : base(RealmCMD.SMSG_STANDSTATE_UPDATE)
        {
            Write(state);
        }
    }

    #endregion

    #region SMSG_INIT_WORLD_STATES

    internal sealed class SmsgInitWorldStates : PacketServer
    {
        public SmsgInitWorldStates(Characters character) : base(RealmCMD.SMSG_INIT_WORLD_STATES)
        {
            ushort numberOfFields;

            switch (character.MapZone)
            {
                case 0:
                case 1:
                case 4:
                case 8:
                case 10:
                case 11:
                case 12:
                case 36:
                case 38:
                case 40:
                case 41:
                case 51:
                case 267:
                case 1519:
                case 1537:
                case 2257:
                case 2918:
                    numberOfFields = 6;
                    break;
                default:
                    numberOfFields = 10;
                    break;
            }

            Write((ulong) character.MapId);
            Write((uint) character.MapZone);
            Write((uint) 0); // Area ID????
            Write(numberOfFields);
            Write((ulong) 0x8d8);
            Write((ulong) 0x0);
            Write((ulong) 0x8d7);
            Write((ulong) 0x0);
            Write((ulong) 0x8d6);
            Write((ulong) 0x0);
            Write((ulong) 0x8d5);
            Write((ulong) 0x0);
            Write((ulong) 0x8d4);
            Write((ulong) 0x0);
            Write((ulong) 0x8d3);
            Write((ulong) 0x0);
        }
    }

    #endregion

    #region SMSG_LOGIN_SETTIMESPEED

    public sealed class SmsgLoginSettimespeed : PacketServer
    {
        public SmsgLoginSettimespeed() : base(RealmCMD.SMSG_LOGIN_SETTIMESPEED)
        {
            Write((uint) SecsToTimeBitFields(DateTime.Now)); // Time
            Write(0.01666667f); // Speed
        }

        public static int SecsToTimeBitFields(DateTime dateTime)
        {
            return (dateTime.Year - 100) << 24 | dateTime.Month << 20 | (dateTime.Day - 1) << 14 |
                   (int) dateTime.DayOfWeek << 11 | dateTime.Hour << 6 | dateTime.Minute;
        }
    }

    #endregion

    internal class CharacterHandler
    {
        private static Dictionary<RealmServerSession, DateTime> _logoutQueue;

        internal static void OnCharEnum(RealmServerSession session, byte[] data)
        {
            List<Characters> characters = MainForm.Database.GetCharacters(session.Users.username);
            session.SendPacket(new SmsgCharEnum(characters));
        }

        internal static void OnCharCreate(RealmServerSession session, CmsgCharCreate handler)
        {
            int result;

            // Char name Profane            result = (int) LoginErrorCode.CHAR_NAME_PROFANE;
            // Char name reserved           result = (int) LoginErrorCode.CHAR_NAME_RESERVED;
            // Char name invalid            result = (int) LoginErrorCode.CHAR_NAME_FAILURE;
            // Check Ally or Horde          result = (int) LoginErrorCode.CHAR_CREATE_PVP_TEAMS_VIOLATION;
            // Check char limit create      result = (int) LoginErrorCode.CHAR_CREATE_SERVER_LIMIT;

            // Check for both horde and alliance
            // Only if it's a pvp realm
            try
            {
                result = (int) LoginErrorCode.CHAR_CREATE_SUCCESS;
                MainForm.Database.CreateChar(handler, session.Users);
            }
            catch (Exception)
            {
                result = (int) LoginErrorCode.CHAR_CREATE_ERROR;
            }

            session.SendPacket(new SmsgCharCreate(result));
        }

        internal static void OnCharRename(RealmServerSession session, CmsgCharRename handler)
        {
            int result;

            // Check for existing name

            // DONE: Do the rename
            try
            {
                result = (int) LoginErrorCode.RESPONSE_SUCCESS;
                MainForm.Database.UpdateName(handler);
            }
            catch (Exception)
            {
                result = (int) LoginErrorCode.CHAR_NAME_FAILURE;
            }

            // DONE: Send response
            session.SendPacket(new SmsgCharRename(result));

            // ????? NEED TO REVIEW THIS to update CHAR LIST ENUM
            List<Characters> characters = MainForm.Database.GetCharacters(session.Users.username);
            session.SendPacket(new SmsgCharEnum(characters));
        }

        internal static void OnCharDelete(RealmServerSession session, CmsgCharDelete handler)
        {
            // if failed                CHAR_DELETE_FAILED
            // if waiting for transfer  CHAR_DELETE_FAILED_LOCKED_FOR_TRANSFER
            // if guild leader          CHAR_DELETE_FAILED_GUILD_LEADER
            MainForm.Database.DeleteCharacter(handler.Id);
            session.SendPacket(new SmsgCharDelete(LoginErrorCode.CHAR_DELETE_SUCCESS));
        }

        internal static void OnPlayerLogin(RealmServerSession session, CmsgPlayerLogin handler)
        {
            session.Character = MainForm.Database.GetCharacter(handler.Guid);

            // Change Player Status Online

            // Part One
            session.SendPacket(new SmsgLoginVerifyWorld(session.Character));
            session.SendPacket(new SmsgAccountDataTimes());
            session.SendMessageMotd($"Welcome to World of Warcraft.");
            session.SendMessageMotd($"Servidor do caralho vai curintia ....");

            // Part Two
            session.SendPacket(new SmsgSetRestStart());
            session.SendPacket(new SmsgBindpointupdate(session.Character));
            session.SendPacket(new SmsgTutorialFlags());
            session.SendPacket(new SmsgLoginSettimespeed());
            session.SendPacket(new SmsgInitialSpells(session.Character));
            session.SendPacket(new SmsgActionButtons(session.Character));
            session.SendPacket(new SmsgInitializeFactions(session.Character));

            // Send Cinematic if first time
            if (session.Character.is_movie_played == false)
                session.SendPacket(new SmsgTriggerCinematic(session.Character));

            // Part Three
            session.SendPacket(new SmsgCorpseReclaimDelay());

            // Spawn Player
            session.SendPacket(new SmsgInitWorldStates(session.Character));
            session.SendPacket(UpdateObject.CreateOwnCharacterUpdate(session.Character, out session.Entity));
            EntityManager.DispatchOnPlayerSpawn(session.Entity);


            // Nao sei
            session.Entity.Session = session;

            /*          
            // Cast talents and racial passive spells

            /////////////////////////////// PT1
            // Setting instance ID

            // Set player to transport

            // If we have changed map

            // Loading map cell if not loaded
           
            // SMSG_SET_PROFICIENCY

            // SMSG_UPDATE_AURA_DURATION

                //FillAllUpdateFlags();
                //SendUpdate() -> Contem ao envio dos itens

            // Adding to World
                // AddToWorld(Me)

            // Enable client moving
                // SendTimeSyncReq(client)

            // Send update on aura durations

            /////////////////////////////// PT2 
            // Update character status in database

            // Guild Message Of The Day

            // Social lists

            // Send "Friend online"

            // Send online notify for guild

            // Put back character in group if disconnected
            */
        }

        internal static void OnUpdateAccountData(RealmServerSession session, CmsgUpdateAccountData handler)
        {
            // Nao Implementado ????
        }

        internal static void OnLogoutRequest(RealmServerSession session, byte[] data)
        {
            // Lose Invisibility

            // ???? Can't log out in combat
            //if (session.Entity.IsInCombat)
            //{
            //session.SendPacket(new SmsgLogoutResponse(LogoutResponseCode.LOGOUT_RESPONSE_DENIED));
            //return;
            //}

            // Initialize packet
            // - Disable Turn
            // - StandState -> Sit
            // - Send packet

            // Let the client to exit

            // While logout, the player can't move

            // If the player is resting, then it's instant logout

            _logoutQueue = new Dictionary<RealmServerSession, DateTime>();

            if (_logoutQueue.ContainsKey(session)) _logoutQueue.Remove(session);

            session.SendPacket(new SmsgLogoutResponse(LogoutResponseCode.LOGOUT_RESPONSE_ACCEPTED));
            _logoutQueue.Add(session, DateTime.Now);

            Thread thread = new Thread(Update);
            thread.Start();
        }

        private static void Update()
        {
            while (true)
            {
                foreach (KeyValuePair<RealmServerSession, DateTime> entry in _logoutQueue.ToArray())
                {
                    if (DateTime.Now.Subtract(entry.Value).Seconds < 1) continue;
                    entry.Key.SendPacket(new SmsgLogoutComplete());
                    _logoutQueue.Remove(entry.Key);
                }

                Thread.Sleep(1000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal static void OnLogoutCancel(RealmServerSession session, PacketReader handler)
        {
            _logoutQueue.Remove(session);
            session.SendPacket(new SmsgLogoutCancelAck());
        }

        internal static void OnStandStateChange(RealmServerSession session, PacketReader handler)
        {
            // Precisa verificar ao sentar e levantar quando vai sentar de novo ele nao faz nada
            byte standState = handler.ReadByte();
            //client.Character.WatchedFactionIndex = faction
            //session.Entity.standState = standState;
            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_1, standState);
            session.SendPacket(new SmsgStandstateUpdate(standState));
        }
    }
}
