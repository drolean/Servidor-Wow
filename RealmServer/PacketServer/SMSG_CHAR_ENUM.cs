using System.Collections.Generic;
using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_CHAR_ENUM represents list response of characters.
    /// </summary>
    internal sealed class SMSG_CHAR_ENUM : Common.Network.PacketServer
    {
        /// <summary>
        ///     Sends the character list to the client.
        /// </summary>
        /// <param name="characters"></param>
        public SMSG_CHAR_ENUM(List<Characters> characters) : base(RealmEnums.SMSG_CHAR_ENUM)
        {
            Write((byte) characters.Count);

            foreach (var character in characters)
            {
                Write(character.Uid);
                WriteCString(character.Name);

                Write((byte) character.Race);
                Write((byte) character.Classe);
                Write((byte) character.Gender);

                Write(character.SubSkin.Skin);
                Write(character.SubSkin.Face);
                Write(character.SubSkin.HairStyle);
                Write(character.SubSkin.HairColor);
                Write(character.SubSkin.FacialHair);

                Write(character.Level); // int8	
                Write(character.SubMap.MapZone); // int32	
                Write(character.SubMap.MapId); // int32	
                Write(character.SubMap.MapX);
                Write(character.SubMap.MapY);
                Write(character.SubMap.MapZ);

                Write(0); // Guild ID	
                Write((int) CharacterFlag.None);
                Write((byte) 0); // First Login or RestedState

                Write(0); // PetModel	
                Write(0); // PetLevel	
                Write(0); // PetFamily = SELECT family FROM creature_template WHERE entry

                //Items
                for (var j = 0; j < 0x14; j++)
                {
                    Write(0); // DisplayId
                    Write((byte) 0); // InventoryType
                }
            }
        }
    }
}