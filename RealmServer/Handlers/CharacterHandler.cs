using System;
using System.Collections.Generic;
using Common.Database.Tables;
using Common.Globals;
using Common.Network;
using System.Linq;
using Common.Database;
using Common.Helpers;

namespace RealmServer.Handlers
{
    enum CharacterFlagState : int
    {
        CHARACTER_FLAG_NONE = 0x0,
        CHARACTER_FLAG_UNK1 = 0x1,
        CHARACTER_FLAG_UNK2 = 0x2,
        CHARACTER_FLAG_LOCKED_FOR_TRANSFER = 0x4, //Character Locked for Paid Character Transfer
        CHARACTER_FLAG_UNK4 = 0x8,
        CHARACTER_FLAG_UNK5 = 0x10,
        CHARACTER_FLAG_UNK6 = 0x20,
        CHARACTER_FLAG_UNK7 = 0x40,
        CHARACTER_FLAG_UNK8 = 0x80,
        CHARACTER_FLAG_UNK9 = 0x100,
        CHARACTER_FLAG_UNK10 = 0x200,
        CHARACTER_FLAG_HIDE_HELM = 0x400,
        CHARACTER_FLAG_HIDE_CLOAK = 0x800,
        CHARACTER_FLAG_UNK13 = 0x1000,
        CHARACTER_FLAG_GHOST = 0x2000, //Player is ghost in char selection screen
        CHARACTER_FLAG_RENAME = 0x4000, //On login player will be asked to change name
        CHARACTER_FLAG_UNK16 = 0x8000,
        CHARACTER_FLAG_UNK17 = 0x10000,
        CHARACTER_FLAG_UNK18 = 0x20000,
        CHARACTER_FLAG_UNK19 = 0x40000,
        CHARACTER_FLAG_UNK20 = 0x80000,
        CHARACTER_FLAG_UNK21 = 0x100000,
        CHARACTER_FLAG_UNK22 = 0x200000,
        CHARACTER_FLAG_UNK23 = 0x400000,
        CHARACTER_FLAG_UNK24 = 0x800000,
        CHARACTER_FLAG_LOCKED_BY_BILLING = 0x1000000,
        CHARACTER_FLAG_DECLINED = 0x2000000,
        CHARACTER_FLAG_UNK27 = 0x4000000,
        CHARACTER_FLAG_UNK28 = 0x8000000,
        CHARACTER_FLAG_UNK29 = 0x10000000,
        CHARACTER_FLAG_UNK30 = 0x20000000,
        CHARACTER_FLAG_UNK31 = 0x40000000
        //CHARACTER_FLAG_UNK32 = 0x80000000,
    }

    #region SMSG_CHAR_ENUM
    public sealed class SmsgCharEnum : PacketServer
    {
        public SmsgCharEnum(List<Characters> characters) : base(RealmCMD.SMSG_CHAR_ENUM)
        {
            Write((byte) characters.Count);

            foreach (Characters character in characters)
            {
                Write((ulong) character.Id);
                WriteCString(character.name);
                Write((byte)character.race);
                Write((byte)character.classe);
                Write((byte)character.gender);

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
                Write((int) CharacterFlagState.CHARACTER_FLAG_NONE);
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
                        var item = XmlReader.GetItem((int)checkItem.item);

                        Write(item.displayId);
                        Write((byte)item.inventoryType);
                    }
                    else
                    {
                        Write(0);
                        Write((byte)0);
                    }
                }
            }
        }
    }
    #endregion

    #region  CMSG_CHAR_CREATE
    public sealed class CmsgCharCreate : PacketReader
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
            Name      = ReadCString();

            Race      = ReadByte();
            Classe    = ReadByte();
            Gender    = ReadByte();

            Skin      = ReadByte();
            Face      = ReadByte();
            HairStyle = ReadByte();
            HairColor = ReadByte();
            FacialHair= ReadByte();
            OutfitId  = ReadByte();
        }
    }
    #endregion

    #region SMSG_CHAR_CREATE
    sealed class SmsgCharCreate : PacketServer
    {
        public SmsgCharCreate(int code) : base(RealmCMD.SMSG_CHAR_CREATE)
        {
            Write((byte)code);
        }
    }
    #endregion

    #region CMSG_CHAR_RENAME
    public sealed class CmsgCharRename : PacketReader
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
            Write((byte)code);
        }
    }
    #endregion

    #region CMSG_CHAR_DELETE
    public class CmsgCharDelete : PacketReader
    {
        public int Id { get; private set; }

        public CmsgCharDelete(byte[] data) : base(data)
        {
            Id = (int)ReadUInt64();
        }
    }
    #endregion

    #region SMSG_CHAR_DELETE
    sealed class SmsgCharDelete : PacketServer
    {
        public SmsgCharDelete(LoginErrorCode code) : base(RealmCMD.SMSG_CHAR_DELETE)
        {
            Write((byte)code);
        }
    }
    #endregion

    #region CMSG_PLAYER_LOGIN
    public sealed class CmsgPlayerLogin : PacketReader
    {
        public uint Guid { get; private set; }

        public CmsgPlayerLogin(byte[] data) : base(data)
        {
            Guid = ReadUInt32();
        }
    }
    #endregion

    #region CMSG_UPDATE_ACCOUNT_DATA
    public sealed class CmsgUpdateAccountData : PacketReader
    {
        public uint DataId { get; private set; }
        public uint UncompressedSize { get; private set; }

        public CmsgUpdateAccountData(byte[] data) : base(data)
        {
            if ((data.Length - 1) < 13)
                return;

            DataId = ReadUInt32();
            UncompressedSize = ReadUInt32();

            if (DataId > 7)
                return;

            // How does Mangos Zero Handle the Account Data For the Character?

            // Clear the entry

            // Can not handle more than 65534 bytes

            // Check if it's compressed, if so, decompress it
        }
    }
    #endregion

    #region SMSG_ACCOUNT_DATA_TIMES
    class SmsgAccountDataTimes : PacketServer
    {
        public SmsgAccountDataTimes() : base(RealmCMD.SMSG_ACCOUNT_DATA_TIMES)
        {
            this.WriteNullByte(128);
        }
    }
    #endregion

    #region SMSG_TRIGGER_CINEMATIC
    public sealed class SmsgTriggerCinematic : PacketServer
    {
        public SmsgTriggerCinematic(Characters character) : base(RealmCMD.SMSG_TRIGGER_CINEMATIC)
        {
            Write(XmlReader.GetRace(character.race).init.Cinematic);
        }
    }
    #endregion

    sealed class SmsgBindpointupdate : PacketServer
    {
        public SmsgBindpointupdate(Characters character) : base(RealmCMD.SMSG_BINDPOINTUPDATE)
        {
            Write(character.MapX);
            Write(character.MapY);
            Write(character.MapZ);
            Write((uint)character.MapId);
            Write((short)character.MapZone);
        }
    }

    sealed class SmsgSetRestStart : PacketServer
    {
        public SmsgSetRestStart(Characters character) : base(RealmCMD.SMSG_SET_REST_START)
        {
            Write((byte)0);
        }
    }

    sealed class SmsgTutorialFlags : PacketServer
    {
        //TODO Write the uint ids of 8 tutorial values
        public SmsgTutorialFlags(Characters character) : base(RealmCMD.SMSG_TUTORIAL_FLAGS)
        {
            // [8*Int32] or [32 Bytes] or [256 Bits Flags] Total!!!
            for (int i = 0; i < 8; i++)
            {
                Write((byte)0xff);
            }
        }
    }

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

    sealed class SmsgCorpseReclaimDelay : PacketServer
    {
        public SmsgCorpseReclaimDelay() : base(RealmCMD.SMSG_CORPSE_RECLAIM_DELAY)
        {
            Write(30 * 1000);
        }
    }

    sealed class SmsgInitialSpells : PacketServer
    {
        public SmsgInitialSpells(Characters character) : base(RealmCMD.SMSG_INITIAL_SPELLS)
        {
            // Implement Spell Cooldowns
            var spells = MainForm.Database.GetSpells(character);

            Write((byte)0); //int8
            Write((ushort)spells.Count); //int16

            //ushort slot = 1;
            foreach (CharactersSpells spell in spells)
            {
                Write((ushort)spell.spell); //uint16
                Write(0); //int16
            }

            Write((UInt16)spells.Count); //in16
        }
    }

    sealed class SmsgInitializeFactions : PacketServer
    {
        public SmsgInitializeFactions(Characters character) : base(RealmCMD.SMSG_INITIALIZE_FACTIONS)
        {
            var factions = MainForm.Database.GetFactions(character);

            Write((Int32)64);
            foreach(var fact in factions)
            {
                Write((byte)  fact.flags); // Flag 
                Write((Int32) fact.standing); // Value
            }
        }
    }

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
                    UInt32 packedData = (UInt32)currentButton.action | (UInt32)currentButton.type << 24;
                    Write((UInt32)packedData);
                    //Write((UInt16)currentButton.action);
                    //Write((int)currentButton.type);
                    //Write((int)currentButton.); ?? misc???
                }
                else
                    Write((UInt32)0);
            }
        }
    }

    internal class CharacterHandler
    {
        internal static void OnCharEnum(RealmServerSession session, byte[] data)
        {
            List<Characters> characters = MainForm.Database.GetCharacters(session.Users.username);
            session.SendPacket(new SmsgCharEnum(characters));
        }

        internal static void OnCharCreate(RealmServerSession session, CmsgCharCreate handler)
        {
            int result = (int)LoginErrorCode.CHAR_CREATE_DISABLED;

            // Need Char Name
            if (handler.Name == null)
            {
                result = (int)LoginErrorCode.CHAR_NAME_ENTER;
            }
            // Char name min 2
            else if (handler.Name.Length <= 2)
            {
                result = (int)LoginErrorCode.CHAR_NAME_TOO_SHORT;
            }
            // Char name max 12
            else if (handler.Name.Length >= 12)
            {
                result = (int)LoginErrorCode.CHAR_NAME_TOO_LONG;
            }
            // Char name only contain letter
            else if (handler.Name.Any(char.IsDigit))
            {
                result = (int)LoginErrorCode.CHAR_NAME_ONLY_LETTERS;
            }
            // Char name in use
            else if (MainForm.Database.GetCharacaterByName(handler.Name) != null)
            {
                result = (int)LoginErrorCode.CHAR_CREATE_NAME_IN_USE;
            }

            // Char name Profane            result = (int) LoginErrorCode.CHAR_NAME_PROFANE;
            // Char name reserved           result = (int) LoginErrorCode.CHAR_NAME_RESERVED;
            // Char name invalid            result = (int) LoginErrorCode.CHAR_NAME_FAILURE;
            // Check Ally or Horde          result = (int) LoginErrorCode.CHAR_CREATE_PVP_TEAMS_VIOLATION;
            // Check char limit create      result = (int) LoginErrorCode.CHAR_CREATE_SERVER_LIMIT;

            // Check for both horde and alliance
            // TODO: Only if it's a pvp realm
            try
            {
                result = (int)LoginErrorCode.CHAR_CREATE_SUCCESS;
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
            int result = (int)LoginErrorCode.CHAR_NAME_FAILURE;

            // Check for existing name

            // DONE: Do the rename
            try
            {
                result = (int)LoginErrorCode.RESPONSE_SUCCESS;
                MainForm.Database.UpdateName(handler);
            }
            catch (Exception)
            {
                result = (int)LoginErrorCode.CHAR_NAME_FAILURE;
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
            if(session.Character == null)
                session.Character = MainForm.Database.GetCharacter(handler.Guid);

            Log.Print(LogType.RealmServer, $"[{session.ConnectionRemoteIp}] Enter World [{session.Character.name} ({handler.Guid})]");

            // SMSG_CORPSE_RECLAIM_DELAY
            session.SendPacket(new SmsgCorpseReclaimDelay());
            
            // Cast talents and racial passive spells

            /////////////////////////////// PT1
            // Setting instance ID

            // Set player to transport

            // If we have changed map

            // Loading map cell if not loaded

            // SMSG_BINDPOINTUPDATE
            session.SendPacket(new SmsgBindpointupdate(session.Character));
            
            // SMSG_SET_REST_START
            session.SendPacket(new SmsgSetRestStart(session.Character));

            // SMSG_TUTORIAL_FLAGS
            session.SendPacket(new SmsgTutorialFlags(session.Character));

            // SMSG_SET_PROFICIENCY

            // SMSG_UPDATE_AURA_DURATION

            // SMSG_INITIAL_SPELLS
            session.SendPacket(new SmsgInitialSpells(session.Character));

            // SMSG_INITIALIZE_FACTIONS
            session.SendPacket(new SmsgInitializeFactions(session.Character));

            // SMSG_ACTION_BUTTONS
            session.SendPacket(new SmsgActionButtons(session.Character));

            // SMSG_INIT_WORLD_STATES
            session.SendHexPacket(RealmCMD.SMSG_INIT_WORLD_STATES, "01 00 00 00 6C 00 AE 07 01 00 32 05 01 00 31 05 00 00 2E 05 00 00 F9 06 00 00 F3 06 00 00 F1 06 00 00 EE 06 00 00 ED 06 00 00 71 05 00 00 70 05 00 00 67 05 01 00 66 05 01 00 50 05 01 00 44 05 00 00 36 05 00 00 35 05 01 00 C6 03 00 00 C4 03 00 00 C2 03 00 00 A8 07 00 00 A3 07 0F 27 74 05 00 00 73 05 00 00 72 05 00 00 6F 05 00 00 6E 05 00 00 6D 05 00 00 6C 05 00 00 6B 05 00 00 6A 05 01 00 69 05 01 00 68 05 01 00 65 05 00 00 64 05 00 00 63 05 00 00 62 05 00 00 61 05 00 00 60 05 00 00 5F 05 00 00 5E 05 00 00 5D 05 00 00 5C 05 00 00 5B 05 00 00 5A 05 00 00 59 05 00 00 58 05 00 00 57 05 00 00 56 05 00 00 55 05 00 00 54 05 01 00 53 05 01 00 52 05 01 00 51 05 01 00 4F 05 00 00 4E 05 00 00 4D 05 01 00 4C 05 00 00 4B 05 00 00 45 05 00 00 43 05 01 00 42 05 00 00 40 05 00 00 3F 05 00 00 3E 05 00 00 3D 05 00 00 3C 05 00 00 3B 05 00 00 3A 05 01 00 39 05 00 00 38 05 00 00 37 05 00 00 34 05 00 00 33 05 00 00 30 05 00 00 2F 05 00 00 2D 05 01 00 16 05 01 00 15 05 00 00 B6 03 00 00 45 07 02 00 36 07 01 00 35 07 01 00 34 07 01 00 33 07 01 00 32 07 01 00 02 07 00 00 01 07 00 00 00 07 00 00 FE 06 00 00 FD 06 00 00 FC 06 00 00 FB 06 00 00 F8 06 00 00 F7 06 00 00 F6 06 00 00 F4 06 D0 07 F2 06 00 00 F0 06 00 00 EF 06 00 00 EC 06 00 00 EA 06 00 00 E9 06 00 00 E8 06 00 00 E7 06 00 00 18 05 00 00 17 05 00 00 03 07 00 00 ");

            // SMSG_UPDATE_OBJECT for ourself
                //FillAllUpdateFlags()
                //SendUpdate()

            // Adding to World
                // AddToWorld(Me)

            // Enable client moving
                // SendTimeSyncReq(client)

            // Send update on aura durations

            /////////////////////////////// PT2 
            // Update character status in database

            // SMSG_ACCOUNT_DATA_TIMES
            session.SendPacket(new SmsgAccountDataTimes());

            // SMSG_TRIGGER_CINEMATIC
            session.SendPacket(new SmsgTriggerCinematic(session.Character));

            // SMSG_LOGIN_SETTIMESPEED

            // Server Message Of The Day

            // Guild Message Of The Day

            // Social lists

            // Send "Friend online"

            // Send online notify for guild

            // Put back character in group if disconnected
        }

        internal static void OnUpdateAccountData(RealmServerSession session, CmsgUpdateAccountData handler)
        {
            Console.WriteLine(@"Update Account Data Bucueta");
        }
    }
}
