using System.Collections.Generic;
using System.IO;
using Common.Database.Dbc;
using Common.Database.Tables;
using Common.Globals;
using RealmServer.Helpers;

namespace RealmServer.Game.Entitys
{
    public abstract class UpdateBlock
    {
        public string Info { get; internal set; }
        public byte[] Data { get; internal set; }

        internal BinaryWriter Writer;

        protected UpdateBlock()
        {
            Writer = new BinaryWriter(new MemoryStream());
        }

        public void Build()
        {
            BuildData();
            Data = (Writer.BaseStream as MemoryStream)?.ToArray();
        }

        public abstract void BuildData();
    }

    public class PlayerEntity : UnitEntity
    {
        public override int DataLength => (int) PlayerFields.PLAYER_END - 0x4;

        public UnitEntity Target;
        public Characters Character { get; }

        // [ ] Character Information
        public int ModelNative;
        public override string Name => Character.name;

        public RealmServerSession Session { get; internal set; }

        // Know Lists
        public List<PlayerEntity> KnownPlayers { get; set; }
        public List<GameObjectEntity> KnownGameObjects { get; set; }
        public List<UnitEntity> KnownUnits { get; set; }

        //
        public List<ObjectEntity> OutOfRangeEntitys { get; set; }
        public List<UpdateBlock> UpdateBlocks { get; set; }

        public PlayerEntity(Characters character)
            : base(new ObjectGuid((uint) character.Id, TypeId.TypeidPlayer, HighGuid.HighguidMoTransport))
        {
            /* Inicializadores */
            Character = character;
            KnownPlayers = new List<PlayerEntity>();
            KnownGameObjects = new List<GameObjectEntity>();
            KnownUnits = new List<UnitEntity>();

            ChrRaces chrRaces = MainForm.ChrRacesReader.GetData(character.race);

            /* Definindo Character */
            Model = (int) CharacterHelper.GetRaceModel(character.race, character.gender);
            ModelNative = (int) CharacterHelper.GetRaceModel(character.race, character.gender);
            Scale = CharacterHelper.GetScale(character.race, character.gender);
            /* FIM das definições */

            SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, 25);
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, Size);

            SetUpdateField((int) UnitFields.UNIT_FIELD_HEALTH, Life.Current);

            SetUpdateField((int) UnitFields.UNIT_FIELD_MAXHEALTH, Life.Maximum);

            // To Display Level Correct need this [START] 
            SetUpdateField((int) UnitFields.UNIT_FIELD_LEVEL, Level);
            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, chrRaces.FactionId);
            // [END]

            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.race);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.classe, 1);
            SetUpdateField((int) UnitFields.UNIT_FIELD_BYTES_0, (byte) character.gender, 2);

            SetUpdateField((int) UnitFields.UNIT_FIELD_DISPLAYID, Model);
            SetUpdateField((int) UnitFields.UNIT_FIELD_NATIVEDISPLAYID, ModelNative);

            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_skin);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_face, 1);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_hairStyle, 2);
            SetUpdateField((int) PlayerFields.PLAYER_BYTES, character.char_hairColor, 3);

            SetUpdateField((int) PlayerFields.PLAYER_BYTES_2, character.char_facialHair, 0);

            SetUpdateField((int) PlayerFields.PLAYER_XP, 0);
            SetUpdateField((int) PlayerFields.PLAYER_NEXT_LEVEL_XP, 400);
            SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1, 26);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, character.watched_faction);

            /*
            // Setup Equiped Itens
            var inventory = MainForm.Database.GetInventory(character);
            for (int j = 0; j < 112; j++)
            {
                if (inventory.Find(item => item.slot == j) != null)
                {
                    if (j < 19)
                    {
                        SetUpdateField(
                            (int) PlayerFields.PLAYER_VISIBLE_ITEM_1_0 +
                            (int) inventory.Find(item => item.slot == j).slot * 12,
                            inventory.Find(item => item.slot == j).item);
                        SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_PROPERTIES + j * 12, 0);
                    }

                    SetUpdateField((int) PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2,
                        inventory.Find(item => item.slot == j).Id);
                }
                else
                {
                    if (j < 19)
                        SetUpdateField((int) PlayerFields.PLAYER_VISIBLE_ITEM_1_0 + j * 12, 0);

                    SetUpdateField((int) PlayerFields.PLAYER_FIELD_INV_SLOT_HEAD + j * 2, 0);
                }
            }
            */
        }
    }
}