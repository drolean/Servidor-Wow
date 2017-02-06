using System;
using System.Collections.Generic;
using Common.Database.Tables;
using Common.Globals;
using Common.Network;
using System.Linq;
using Common.Database;

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
    }
}
