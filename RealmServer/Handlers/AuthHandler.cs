using Common.Database.Tables;
using Common.Globals;
using Common.Helpers;
using Common.Network;
using System;
using System.Collections.Generic;

namespace RealmServer.Handlers
{

    [Flags()]
    enum CharacterFlagState
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
        CHARACTER_FLAG_UNK31 = 0x40000000,
        //CHARACTER_FLAG_UNK32 = 0x80000000,
    }

    #region SMSG_CHAR_ENUM
    public sealed class SmsgCharEnum : PacketServer
    {

        public SmsgCharEnum(List<Characters> characters) : base(RealmCMD.SMSG_CHAR_ENUM)
        {
            AccountState.o
            // try catch to show error on retrieve list ?????
            Write((byte)characters.Count);
            foreach (Characters character in characters)
            {
                Write((ulong) character.Id);
                this.WriteCString(character.name);
                Write((byte)character.race);
                Write((byte)character.classe);
                Write((byte)character.gender);

                Write((byte)character.char_skin);
                Write((byte)character.char_face);
                Write((byte)character.char_hairStyle);
                Write((byte)character.char_hairColor);
                Write((byte)character.char_facialHair);

                Write(character.level); // int8
                Write(character.MapZone); // int32
                Write(character.MapId); // int32

                Write(character.MapX);
                Write(character.MapY);
                Write(character.MapZ);

                Write(0); // Guild ID

                Write((ulong)CharacterFlagState.CHARACTER_FLAG_GHOST);
                                            // CHARACTER_FLAG_NONE - 
                                            // CHARACTER_FLAG_LOCKED_FOR_TRANSFER - 
                                            // CHARACTER_FLAG_LOCKED_BY_BILLING - 
                                            // CHARACTER_FLAG_RENAME - 
                                            // CHARACTER_FLAG_GHOST
                Write(0); // Rest State Or //FirstLogin ???? Write((byte) (character.firsttime ? 1 : 0)); //FirstLogin 

                Write(0); // PetModel
                Write(0); // PetLevel
                Write(0); // PetFamily

                // Get items

                // Add model info
                for (int slot = 0; slot < 19; slot++)
                {
                    // No equiped item in this slot
                    Write(0); // Item Model
                    Write((byte)0); // Item Slot

                    // Do not show helmet or cloak
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

    internal class AuthHandler
    {
        internal static void OnCharEnum(RealmServerSession session, byte[] data)
        {
            List<Characters> characters = MainForm.Database.GetCharacters(session.Users.username);
            session.SendPacket(new SmsgCharEnum(characters));
        }

        public static int aba = 0;
        internal static void OnCharCreate(RealmServerSession session, CmsgCharCreate handler)
        {
            /*
            int result = (int) CharResponse.CHAR_CREATE_DISABLED;

            try{

            } catch(Exception)
            {
                result = (int) CharResponse.CHAR_CREATE_ERROR;
                session.SendPacket(new SmsgCharCreate(result));
            }
            */
            session.SendPacket(new SmsgCharCreate(aba));
            Console.WriteLine($"Estou enviando: {aba}");
            aba++; 
        }
    }
}
