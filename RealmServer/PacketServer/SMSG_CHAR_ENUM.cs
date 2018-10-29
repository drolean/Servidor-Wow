using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Database.Tables;
using Common.Globals;
using MongoDB.Driver;

namespace RealmServer.PacketServer
{
    /// <summary>
    ///     SMSG_CHAR_ENUM represents a message sent by the server a client is authenticated for this realm.
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
                Write((int) character.Flag);
                Write((byte) 0); // First Login or RestedState

                Write(0); // PetModel
                Write(0); // PetLevel
                Write(0); // PetFamily = SELECT family FROM creature_template WHERE entry

                // TODO: create a lookup
                for (byte i = 0; i < 20; i++)
                {
                    var inventory = character.SubInventorie.FirstOrDefault(x => x.Slot == i);

                    if (inventory != null)
                    {
                        var item = DatabaseModel.ItemsCollection.Find(x => x.Entry == inventory.Item).FirstOrDefault();
                        Write(item.DisplayId);
                        Write((byte) item.InventoryType);
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
}
