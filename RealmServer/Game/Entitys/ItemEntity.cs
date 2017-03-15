using System;
using Common.Database.Tables;
using Common.Globals;

namespace RealmServer.Game.Entitys
{
    public class ItemEntity : ObjectEntity
    {
        public TypeId TypeId => TypeId.TypeidItem;
        public override int DataLength => (int) ItemFields.ITEM_END;

        public ItemEntity(CharactersInventorys inventory, Characters sessionCharacter)
            : base(new ObjectGuid((uint) inventory.item, TypeId.TypeidItem, HighGuid.HighguidItem))
        {
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_TYPE, 3); // 3 na BAG / 2 naosei / 7 e uma bag
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_ENTRY, (byte) inventory.item);
            SetUpdateField((int) ObjectFields.OBJECT_FIELD_SCALE_X, 1.0f);

            //
            SetUpdateField((int) ItemFields.ITEM_FIELD_OWNER, (ulong) sessionCharacter.Id); // ID do char
            SetUpdateField((int) ItemFields.ITEM_FIELD_CONTAINED, (ulong) sessionCharacter.Id); // ID do char

            //
            SetUpdateField((int) ItemFields.ITEM_FIELD_STACK_COUNT, inventory.stack);

            Console.WriteLine($@"ItemEntity: [ID {inventory.Id}] / [{inventory.item}]");
        }
    }

    public enum ContainerFields
    {
        CONTAINER_FIELD_NUM_SLOTS = 0x0, //0
        CONTAINER_ALIGN_PAD = 0x1, //1
        CONTAINER_FIELD_SLOT_1 = 0x2, //2 - 41 (Total : 40)
        CONTAINER_END = 0x2A //42
    };

    public enum ItemFields
    {
        ITEM_FIELD_OWNER = ObjectFields.OBJECT_END + 0x00, // Size:2
        ITEM_FIELD_CONTAINED = ObjectFields.OBJECT_END + 0x02, // Size:2
        ITEM_FIELD_CREATOR = ObjectFields.OBJECT_END + 0x04, // Size:2
        ITEM_FIELD_GIFTCREATOR = ObjectFields.OBJECT_END + 0x06, // Size:2
        ITEM_FIELD_STACK_COUNT = ObjectFields.OBJECT_END + 0x08, // Size:1
        ITEM_FIELD_DURATION = ObjectFields.OBJECT_END + 0x09, // Size:1
        ITEM_FIELD_SPELL_CHARGES = ObjectFields.OBJECT_END + 0x0A, // Size:5
        ITEM_FIELD_SPELL_CHARGES_01 = ObjectFields.OBJECT_END + 0x0B,
        ITEM_FIELD_SPELL_CHARGES_02 = ObjectFields.OBJECT_END + 0x0C,
        ITEM_FIELD_SPELL_CHARGES_03 = ObjectFields.OBJECT_END + 0x0D,
        ITEM_FIELD_SPELL_CHARGES_04 = ObjectFields.OBJECT_END + 0x0E,
        ITEM_FIELD_FLAGS = ObjectFields.OBJECT_END + 0x0F, // Size:1
        ITEM_FIELD_ENCHANTMENT = ObjectFields.OBJECT_END + 0x10, // count=21
        ITEM_FIELD_PROPERTY_SEED = ObjectFields.OBJECT_END + 0x25, // Size:1
        ITEM_FIELD_RANDOM_PROPERTIES_ID = ObjectFields.OBJECT_END + 0x26, // Size:1
        ITEM_FIELD_ITEM_TEXT_ID = ObjectFields.OBJECT_END + 0x27, // Size:1
        ITEM_FIELD_DURABILITY = ObjectFields.OBJECT_END + 0x28, // Size:1
        ITEM_FIELD_MAXDURABILITY = ObjectFields.OBJECT_END + 0x29, // Size:1
        ITEM_END = ObjectFields.OBJECT_END + 0x2A,
    };
}
