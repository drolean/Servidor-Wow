﻿using Common.Database;
using Common.Database.Tables;
using Common.Globals;
using RealmServer.Helpers;

namespace RealmServer.Game.Entitys
{
    public class PlayerEntity : UnitEntity
    {
        public override int DataLength => (int) PlayerFields.PLAYER_END - 0x4;

        public UnitEntity Target;
        public Characters Character { get; }

        // [ ] Character Information
        public int ModelNative;
        public override string Name => Character.name;

        public RealmServerSession Session { get; internal set; }

        public PlayerEntity(Characters character)
            : base(new ObjectGuid((uint) character.Id, TypeId.TypeidPlayer, HighGuid.HighguidMoTransport))
        {
            var initRace = XmlReader.GetRace(character.race);

            /* Definindo Character */
            Character = character;
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
            SetUpdateField((int) UnitFields.UNIT_FIELD_FACTIONTEMPLATE, initRace.id);
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

            SetUpdateField((int) PlayerFields.PLAYER_BYTES_2, character.char_facialHair);

            SetUpdateField((int) PlayerFields.PLAYER_XP, 0);
            SetUpdateField((int) PlayerFields.PLAYER_NEXT_LEVEL_XP, 400);
            SetUpdateField((int) PlayerFields.PLAYER_SKILL_INFO_1_1, 26);
            SetUpdateField((int) PlayerFields.PLAYER_FIELD_WATCHED_FACTION_INDEX, -1);

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
        }
    }
}