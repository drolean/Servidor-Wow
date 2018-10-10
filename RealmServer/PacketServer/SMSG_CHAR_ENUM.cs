using System.Collections.Generic;
using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.PacketServer
{
    internal sealed class SMSG_CHAR_ENUM : Common.Network.PacketServer
    {
        public SMSG_CHAR_ENUM(List<Characters> characters) : base(RealmEnums.SMSG_CHAR_ENUM)
        {
            Write((byte) characters.Count);

            foreach (var character in characters)
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
                Write((int) CharacterFlag.None);	
                Write((byte) 0); // First Login or RestedState
                
                Write(0); // PetModel	
                Write(0); // PetLevel	
                Write(0); // PetFamily = SELECT family FROM creature_template WHERE entry
                
                //Items
                for (int j = 0; j < 0x14; j++)
                {
                    Write(0); // DisplayId
                    Write((byte) 0); // InventoryType
                }
            }
        }
    }
}