using Common.Database;
using Common.Database.Tables;
using MongoDB.Driver;
using RealmServer.Enums;

namespace RealmServer.World.Enititys
{
    public class ItemEntity : ObjectEntity
    {
        public ItemEntity(SubInventory inventory, PlayerEntity session)
            : base(new ObjectGuid((uint) inventory.Item, TypeId.TypeidItem, HighGuid.HighguidItem))
        {
            var item = DatabaseModel.ItemsCollection.Find(x => x.Entry == inventory.Item).FirstOrDefault();
            SetUpdateField((int) ObjectFields.Type,
                ObjectType.TYPE_ITEM + (int) ObjectType.TYPE_OBJECT); // 3 na BAG / 2 naosei / 7 e uma bag
            SetUpdateField((int) ObjectFields.Entry, (uint) inventory.Item);
            SetUpdateField((int) ObjectFields.ScaleX, 1f);
            SetUpdateField((int) ObjectFields.Padding, 0);

            //
            SetUpdateField((int) ItemFields.ITEM_FIELD_OWNER, session.Character.Uid); // ID do char

            SetUpdateField((int) ItemFields.ITEM_FIELD_MAXDURABILITY, item.MaxDurability);
            SetUpdateField((int) ItemFields.ITEM_FIELD_DURABILITY, inventory.Durability);
            SetUpdateField((int) ItemFields.ITEM_FIELD_FLAGS, inventory.Flags);
            SetUpdateField((int) ItemFields.ITEM_FIELD_STACK_COUNT, inventory.StackCount);

            SetUpdateField((int) ItemFields.ITEM_FIELD_ITEM_TEXT_ID, inventory.TextId);

            for (var i = 0; i < 5; i++) SetUpdateField((int) ItemFields.ITEM_FIELD_SPELL_CHARGES + i, -1);
        }

        public override int DataLength => (int) ItemFields.ITEM_END;
    }

    public enum ObjectType
    {
        TYPE_OBJECT = 1,
        TYPE_ITEM = 2,
        TYPE_CONTAINER = 6,
        TYPE_UNIT = 8,
        TYPE_PLAYER = 16,
        TYPE_GAMEOBJECT = 32,
        TYPE_DYNAMICOBJECT = 64,
        TYPE_CORPSE = 128,
        TYPE_AIGROUP = 256,
        TYPE_AREATRIGGER = 512
    }

    public enum ItemFields
    {
        ITEM_FIELD_OWNER = ObjectFields.End + 0x00, // Size:2
        ITEM_FIELD_CONTAINED = ObjectFields.End + 0x02, // Size:2
        ITEM_FIELD_CREATOR = ObjectFields.End + 0x04, // Size:2
        ITEM_FIELD_GIFTCREATOR = ObjectFields.End + 0x06, // Size:2
        ITEM_FIELD_STACK_COUNT = ObjectFields.End + 0x08, // Size:1
        ITEM_FIELD_DURATION = ObjectFields.End + 0x09, // Size:1
        ITEM_FIELD_SPELL_CHARGES = ObjectFields.End + 0x0A, // Size:5
        ITEM_FIELD_SPELL_CHARGES_01 = ObjectFields.End + 0x0B,
        ITEM_FIELD_SPELL_CHARGES_02 = ObjectFields.End + 0x0C,
        ITEM_FIELD_SPELL_CHARGES_03 = ObjectFields.End + 0x0D,
        ITEM_FIELD_SPELL_CHARGES_04 = ObjectFields.End + 0x0E,
        ITEM_FIELD_FLAGS = ObjectFields.End + 0x0F, // Size:1
        ITEM_FIELD_ENCHANTMENT = ObjectFields.End + 0x10, // count=21
        ITEM_FIELD_PROPERTY_SEED = ObjectFields.End + 0x25, // Size:1
        ITEM_FIELD_RANDOM_PROPERTIES_ID = ObjectFields.End + 0x26, // Size:1
        ITEM_FIELD_ITEM_TEXT_ID = ObjectFields.End + 0x27, // Size:1
        ITEM_FIELD_DURABILITY = ObjectFields.End + 0x28, // Size:1
        ITEM_FIELD_MAXDURABILITY = ObjectFields.End + 0x29, // Size:1
        ITEM_END = ObjectFields.End + 0x2A
    }
}