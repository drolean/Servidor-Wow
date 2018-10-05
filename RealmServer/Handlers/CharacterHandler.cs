using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Database;
using Common.Database.Dbc;
using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using Common.Network;
using RealmServer.Game;
using RealmServer.Game.Managers;

namespace RealmServer.Handlers
{

    #region SMSG_CHAR_ENUM

    internal sealed class SmsgCharEnum : PacketServer
    {
        /// <summary>
        /// @for
        ///   guid
        ///   name
        ///   race
        ///   class
        ///   gender
        ///   
        ///   charSkin
        ///   charFace
        ///   charHairStyle
        ///   charHairColor
        ///   charFacialHair
        ///   
        ///   charLevel
        ///   charMapZone
        ///   charMapId
        ///   charMapX
        ///   charMapY
        ///   charMapZ
        ///   
        ///   charGuildId
        /// </summary>
        /// <param name="characters"></param>
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
                //Write((int) CharacterFlagState.CharacterFlagDeclined);
                Write((int) MainProgram.IsJitI);
                // RestState
                Write((byte) 0);

                // SELECT modelid, level, entry FROM character_pet WHERE owner =
                Write(0); // PetModel
                Write(0); // PetLevel
                Write(0); // PetFamily = SELECT family FROM creature_template WHERE entry

                // DONE: Get items
                var inventory = MainProgram.Database.GetInventory(character);

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

            Console.WriteLine(MainProgram.IsJitI);

            MainProgram.IsJitI++;
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
        public SmsgCharCreate(LoginErrorCode code) : base(RealmCMD.SMSG_CHAR_CREATE)
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

    internal sealed class CmsgPlayerLogin : PacketReader
    {
        public uint Guid { get; }

        public CmsgPlayerLogin(byte[] data) : base(data)
        {
            Guid = ReadUInt32();
        }
    }

    #endregion

    #region CMSG_UPDATE_ACCOUNT_DATA

    internal sealed class CmsgUpdateAccountData : PacketReader
    {
        public uint DataId { get; }
        public uint UncompressedSize { get; }

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
    /// <summary>
    /// Current time on the server, required or client keeps frozen
    /// </summary>
    internal sealed class SmsgAccountDataTimes : PacketServer
    {
        public SmsgAccountDataTimes() : base(RealmCMD.SMSG_ACCOUNT_DATA_TIMES)
        {
            this.WriteBytes(new byte[80]);
        }
    }

    #endregion

    #region SMSG_TRIGGER_CINEMATIC

    internal sealed class SmsgTriggerCinematic : PacketServer
    {
        public SmsgTriggerCinematic(int cinematic) : base(RealmCMD.SMSG_TRIGGER_CINEMATIC)
        {
            Write(cinematic);
        }
    }

    #endregion

    #region SMSG_BINDPOINTUPDATE

    internal sealed class SmsgBindPointUpdate : PacketServer
    {
        public SmsgBindPointUpdate(Characters character) : base(RealmCMD.SMSG_BINDPOINTUPDATE)
        {
            Write(character.MapX);
            Write(character.MapY);
            Write(character.MapZ);
            Write((uint) character.MapId);
            Write((uint) character.MapZone);
        }
    }

    #endregion

    #region SMSG_SET_REST_START

    internal sealed class SmsgSetRestStart : PacketServer
    {
        public SmsgSetRestStart() : base(RealmCMD.SMSG_SET_REST_START)
        {
            Log.Print(LogType.Implement, "NEED A REVIEW");
            Write((uint) 120);
        }
    }

    #endregion

    #region SMSG_TUTORIAL_FLAGS
    /// <summary>
    /// Send tutorial flags to client.
    ///
    /// VANILLA: max tutorial 8.
    ///
    /// Options:
    ///   -1 = to disable all tutorial
    ///    0 = Welcome & Questgiver
    ///    1 = Welcome
    /// </summary>
    /// <todo>
    /// Review this code to send only if user need.
    /// </todo>
    internal sealed class SmsgTutorialFlags : PacketServer
    {
        public SmsgTutorialFlags() : base(RealmCMD.SMSG_TUTORIAL_FLAGS)
        {
            for (int i = 0; i < 8; i++)
                Write(-1);
        }
    }

    #endregion

    #region SMSG_LOGIN_VERIFY_WORLD

    internal sealed class SmsgLoginVerifyWorld : PacketServer
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
    /// <summary>
    /// Send a delay to ressurection.
    /// </summary>
    internal sealed class SmsgCorpseReclaimDelay : PacketServer
    {
        public SmsgCorpseReclaimDelay() : base(RealmCMD.SMSG_CORPSE_RECLAIM_DELAY)
        {
            Write(MainProgram.ReclaimCorpseTime * 1000);
        }
    }

    #endregion

    #region SMSG_INITIAL_SPELLS

    internal sealed class SmsgInitialSpells : PacketServer
    {
        public SmsgInitialSpells(Characters character) : base(RealmCMD.SMSG_INITIAL_SPELLS)
        {
            // Implement Spell Cooldowns
            var spells = MainProgram.Database.GetSpells(character);

            Write((byte) 0); //int8
            Write((ushort) spells.Count); //int16

            ushort slot = 1;
            foreach (CharactersSpells spell in spells)
            {
                Log.Print(LogType.RealmServer, $"[{character.name}] Spell ....: {spell.spell}");
                Log.Print(LogType.RealmServer, $"[{character.name}] S Active .: {spell.active}");
                Log.Print(LogType.RealmServer, $"[{character.name}] SSlot ....: {slot}");

                Write((ushort) spell.spell); //uint16
                Write(slot++); //int16
            }

            Write((UInt16) spells.Count); //in16
        }
    }

    #endregion

    #region SMSG_INITIALIZE_FACTIONS

    internal sealed class SmsgInitializeFactions : PacketServer
    {

        public SmsgInitializeFactions(Characters character) : base(RealmCMD.SMSG_INITIALIZE_FACTIONS)
        {
            var factions = MainProgram.Database.GetFactions(character);

            Write((uint) factions.Count);
            foreach (var fact in factions)
            {
                Log.Print(LogType.RealmServer, $"[{character.name}] Faction ...: {fact.faction}");
                Log.Print(LogType.RealmServer, $"[{character.name}] Fac Flag ..: {fact.flags}");
                Log.Print(LogType.RealmServer, $"[{character.name}] Fac Stand .: {fact.standing}");
                Write((byte) fact.flags); // Flag
                Write(fact.standing); // Value
            }
        }
    }

    #endregion

    #region SMSG_ACTION_BUTTONS

    internal sealed class SmsgActionButtons : PacketServer
    {
        public SmsgActionButtons(Characters character) : base(RealmCMD.SMSG_ACTION_BUTTONS)
        {
            List<CharactersActionBars> savedButtons = MainProgram.Database.GetActionBar(character);

            for (int button = 0; button < 120; button++) //  119    'or 480 ?
            {
                int index = savedButtons.FindIndex(b => b.button == button);

                CharactersActionBars currentButton = index != -1 ? savedButtons[index] : null;

                if (currentButton != null)
                {
                    Log.Print(LogType.RealmServer, $"[{character.name}] Act Action .: {currentButton.action}");
                    Log.Print(LogType.RealmServer, $"[{character.name}] Act Button .: {currentButton.button}");
                    Log.Print(LogType.RealmServer, $"[{character.name}] Act Type ...: {currentButton.type}");

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
    /// <summary>
    /// set the INITIAL World State values. This needs to be used each time you're creating a new World State.
    /// </summary>
    /// <para>
    /// - MapId
    /// - ZoneId
    /// - AreaId
    /// </para>
    internal sealed class SmsgInitWorldStates : PacketServer
    {
        public SmsgInitWorldStates(Characters character) : base(RealmCMD.SMSG_INIT_WORLD_STATES)
        {
            ushort numberOfFields;

            switch (character.MapZone)
            {
                case 2918:
                    numberOfFields = 6;
                    break;
                default:
                    numberOfFields = 10;
                    break;
            }

            Write((ulong) character.MapId);
            Write((uint) character.MapZone);
            Write((uint) 0); // Area ID
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
    /// <summary>
    /// represents a message sent by the server to define the world timespeed.
    /// </summary>
    public sealed class SmsgLoginSetTimeSpeed : PacketServer
    {
        public SmsgLoginSetTimeSpeed() : base(RealmCMD.SMSG_LOGIN_SETTIMESPEED)
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

    #region SMSG_FORCE_MOVE_ROOT
    internal sealed class SmsgForceMoveRoot : PacketServer
    {
        public SmsgForceMoveRoot(int characterId) : base(RealmCMD.SMSG_FORCE_MOVE_ROOT)
        {
            this.WritePackedUInt64((ulong) characterId);
            Write((uint) 0);
        }
    }
    #endregion

    #region SMSG_FORCE_MOVE_UNROOT
    internal sealed class SmsgForceMoveUnroot : PacketServer
    {
        public SmsgForceMoveUnroot(int characterId) : base(RealmCMD.SMSG_FORCE_MOVE_UNROOT)
        {
            this.WritePackedUInt64((ulong)characterId);
            Write((uint)0);
        }
    }
    #endregion

    internal class CharacterHandler
    {
        #region Logout
        private static Dictionary<RealmServerSession, DateTime> _logoutQueue;

        private static void Update(int sec)
        {
            while (true)
            {
                foreach (KeyValuePair<RealmServerSession, DateTime> entry in _logoutQueue.ToArray())
                {
                    if (DateTime.Now.Subtract(entry.Value).Seconds < sec) continue;
                    entry.Key.SendPacket(new SmsgLogoutComplete());
                    _logoutQueue.Remove(entry.Key);
                    WorldManager.DispatchOnPlayerDespawn(entry.Key.Entity);
                }

                Thread.Sleep(1000);
            }
        }
        #endregion

        /// <summary>
        /// This message is sent by the client whenever it request character list.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        internal static void OnCharEnum(RealmServerSession session, byte[] data)
        {
            List<Characters> characters = MainProgram.Database.GetCharacters(session.Users.username);
            session.SendPacket(new SmsgCharEnum(characters));
        }

        /// <summary>
        /// Create a Character of Player
        /// </summary>
        /// <param name="session"></param>
        /// <param name="handler"></param>
        /// <todo>
        /// NEED A REVIEW
        /// - Profane Name
        /// - Reserved Name
        /// - Check Ally or Horde
        /// - 
        /// </todo>
        internal static void OnCharCreate(RealmServerSession session, CmsgCharCreate handler)
        {
            try
            {
                // If limit character reached
                if (MainProgram.Database.GetCharacters(session.Users.username).Count >= MainProgram.LimitCharacterPerRealm)
                {
                    session.SendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_SERVER_LIMIT));
                    return;
                }

                // check if name in use
                if (MainProgram.Database.GetCharacaterByName(handler.Name) != null)
                {
                    session.SendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_NAME_IN_USE));
                    return;
                }

                session.SendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_SUCCESS));
                MainProgram.Database.CreateChar(handler, session.Users);
            }
            catch (Exception)
            {
                session.SendPacket(new SmsgCharCreate(LoginErrorCode.CHAR_CREATE_ERROR));
            }
        }

        internal static void OnCharRename(RealmServerSession session, CmsgCharRename handler)
        {
            int result;

            // Check for existing name

            // DONE: Do the rename
            try
            {
                result = (int) LoginErrorCode.RESPONSE_SUCCESS;
                MainProgram.Database.UpdateName(handler);
            }
            catch (Exception)
            {
                result = (int) LoginErrorCode.CHAR_NAME_FAILURE;
            }

            // DONE: Send response
            session.SendPacket(new SmsgCharRename(result));

            // ????? NEED TO REVIEW THIS to update CHAR LIST ENUM
            List<Characters> characters = MainProgram.Database.GetCharacters(session.Users.username);
            session.SendPacket(new SmsgCharEnum(characters));
        }

        internal static void OnCharDelete(RealmServerSession session, CmsgCharDelete handler)
        {
            // TODO: Waiting for transfer NOT DELETE
            // TODO: Guild Leader NOT DELETE
            try
            {
                MainProgram.Database.DeleteCharacter(handler.Id);
                session.SendPacket(new SmsgCharDelete(LoginErrorCode.CHAR_DELETE_SUCCESS));
            }
            catch (Exception)
            {
                session.SendPacket(new SmsgCharDelete(LoginErrorCode.CHAR_DELETE_FAILED));
            }
        }

        internal static void OnPlayerLogin(RealmServerSession session, CmsgPlayerLogin handler)
        {
            session.Character = MainProgram.Database.GetCharacter(handler.Guid);

            // Part One
            session.SendPacket(new SmsgLoginVerifyWorld(session.Character)); // DONE
            session.SendPacket(new SmsgAccountDataTimes()); // DONE
            session.SendMessageMotd(MainProgram.FirstMotd); // DONE
            session.SendMessageMotd(MainProgram.SecondMotd); // DONE

            // Part Two
            session.SendPacket(new SmsgSetRestStart()); // TODO: review
            session.SendPacket(new SmsgBindPointUpdate(session.Character)); // DONE
            session.SendPacket(new SmsgTutorialFlags()); // TODO: review
            session.SendPacket(new SmsgLoginSetTimeSpeed()); // DONE
            //session.SendPacket(new SmsgInitialSpells(session.Character));
            //session.SendPacket(new SmsgActionButtons(session.Character)); // DONE
            //session.SendPacket(new SmsgInitializeFactions(session.Character)); // DONE

            // Send Cinematic if first time
            if (session.Character.is_movie_played == false)
            {
                ChrRaces chrRaces = MainProgram.ChrRacesReader.GetData(session.Character.race);
                session.SendPacket(new SmsgTriggerCinematic(chrRaces.CinematicId)); // DONE
            }

            // Part Three
            session.SendPacket(new SmsgCorpseReclaimDelay()); // DONE

            // Spawn Player
            session.SendPacket(new SmsgInitWorldStates(session.Character)); // TODO: review
            session.SendPacket(UpdateObject.CreateOwnCharacterUpdate(session.Character, out session.Entity));

            // Set Sesstion Player
            session.Entity.Session = session;

            WorldManager.DispatchOnPlayerSpawn(session.Entity);

            // Generate Inventory
            foreach (var inventory in MainProgram.Database.GetInventory(session.Character))
            {
                //session.SendPacket(UpdateObject.CreateItem(inventory, session.Character));
            }


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

            // Change Player Status Online
            MainProgram.Database.UpdateCharacter(session.Character.Id, "online");
        }

        internal static void OnUpdateAccountData(RealmServerSession session, CmsgUpdateAccountData handler)
        {
            // TODO: idk who is this!!!?!?!?!???
        }

        // PARTIAL
        internal static void OnLogoutRequest(RealmServerSession session, byte[] data)
        {
            _logoutQueue = new Dictionary<RealmServerSession, DateTime>();
            if (_logoutQueue.ContainsKey(session)) _logoutQueue.Remove(session);

            // Lose Invisibility

            // ???? Can't log out in combat

            // DONE: Disable Movement
            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, UnitFlags.UNIT_FLAG_STUNTED);

            // DONE: StandState -> Sit
            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_1, StandStates.STANDSTATE_SIT);
            session.Entity.KnownPlayers.ForEach(
                s => s.Session.SendPacket(new SmsgStandstateUpdate((byte) StandStates.STANDSTATE_SIT)));

            // DONE: Send Logout
            session.SendPacket(new SmsgLogoutResponse(LogoutResponseCode.LOGOUT_RESPONSE_ACCEPTED));

            // DONE: While logout, the player can't move
            session.SendPacket(new SmsgForceMoveRoot(session.Character.Id));

            // If the player is resting, then it's instant logout
            _logoutQueue.Add(session, DateTime.Now);
#if DEBUG
            Thread thread = new Thread(() => Update(1));
#else
            Thread thread = new Thread(() => Update(20));
#endif
            thread.Start();
        }

        // DONE
        internal static void OnLogoutCancel(RealmServerSession session, PacketReader handler)
        {
            // Remove from logout queue
            _logoutQueue.Remove(session);

            // DONE: Enable Turn
            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_FLAGS, UnitFlags.UNIT_FLAG_NONE);

            // DONE: StandState -> Stand
            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_1, StandStates.STANDSTATE_STAND);
            session.Entity.KnownPlayers.ForEach(
                s => s.Session.SendPacket(new SmsgStandstateUpdate((byte) StandStates.STANDSTATE_STAND)));

            // DONE: Stop client logout
            session.SendPacket(new SmsgLogoutCancelAck());

            // DONE: Enable moving
            session.SendPacket(new SmsgForceMoveUnroot(session.Character.Id));
        }

        // PARTIAL
        internal static void OnStandStateChange(RealmServerSession session, PacketReader handler)
        {
            // Precisa verificar ao sentar e levantar quando vai sentar de novo ele nao faz nada
            byte standState = handler.ReadByte();

            if (standState == (int) StandStates.STANDSTATE_STAND)
                //client.Character.RemoveAurasByInterruptFlag(SpellAuraInterruptFlags.AURA_INTERRUPT_FLAG_NOT_SEATED);

            session.Entity.SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_1, standState);
            session.Entity.KnownPlayers.ForEach(s => s.Session.SendPacket(new SmsgStandstateUpdate(standState)));
        }
    }
}
