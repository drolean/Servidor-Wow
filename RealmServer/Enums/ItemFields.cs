namespace RealmServer.Enums
{
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