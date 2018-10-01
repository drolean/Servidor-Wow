using System.Xml.Serialization;

namespace Common.Database.Xml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(IsNullable = false, ElementName = "items")]
    public class ItemsXml
    {
        /// <remarks/>
        [XmlElement("item")]
        public ItemsItem[] Item { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class ItemsItem
    {
        /// <remarks/>
        [XmlAttribute]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int displayId { get; set; }
        public int @class { get; set; }
        public int quality { get; set; }
        public string priceBuy { get; set; }
        public string priceSell { get; set; }
        public int level { get; set; }
        public int reqLevel { get; set; }
        public int reqClass { get; set; }
        public int reqRace { get; set; }
        public int reqSkill { get; set; }
        public int reqSkillRank { get; set; }
        public int maxCount { get; set; }
        public int stackable { get; set; }
        public int bonding { get; set; }
        public int pageText { get; set; }
        public int languageId { get; set; }
        public int inventoryType { get; set; }
        public int ammoType { get; set; }
        public int containerSlot { get; set; }
        public int flags { get; set; }
        public int @lock { get; set; }
        public int material { get; set; }
        public int pageMaterial { get; set; }
        public int sheath { get; set; }
        public string startQuest { get; set; }
        public int subClass { get; set; }
        public int weaponSpeed { get; set; }
    }
}