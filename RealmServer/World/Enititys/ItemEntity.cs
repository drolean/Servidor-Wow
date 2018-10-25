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

            Type = (byte) (ObjectType.TYPE_ITEM + (int) ObjectType.TYPE_OBJECT);
            Scale = 1f;
            Entry = item.Entry;
            SetUpdateField((int) ObjectFields.Padding, 0);

            SetUpdateField((int) ItemFields.ITEM_FIELD_OWNER, session.Character.Uid);
            SetUpdateField((int) ItemFields.ITEM_FIELD_CONTAINED, session.Character.Uid); // ID do char
            SetUpdateField((int) ItemFields.ITEM_FIELD_MAXDURABILITY, item.MaxDurability);
            SetUpdateField((int) ItemFields.ITEM_FIELD_DURABILITY, inventory.Durability);
            SetUpdateField((int) ItemFields.ITEM_FIELD_FLAGS, inventory.Flags);
            SetUpdateField((int) ItemFields.ITEM_FIELD_STACK_COUNT, inventory.StackCount);

            SetUpdateField((int) ItemFields.ITEM_FIELD_ITEM_TEXT_ID, inventory.TextId);

            for (var i = 0; i < 5; i++)
                SetUpdateField((int) ItemFields.ITEM_FIELD_SPELL_CHARGES + i, -1);
        }

        public override int DataLength => (int) ItemFields.ITEM_END;
    }
}