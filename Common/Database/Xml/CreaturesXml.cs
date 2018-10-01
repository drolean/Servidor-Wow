using System.Xml.Serialization;

namespace Common.Database.Xml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(IsNullable = false, ElementName = "units")]
    public class CreaturesXml
    {
        /// <remarks/>
        [XmlElement("unit")]
        public unitsUnit[] unit { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class unitsUnit
    {
        /// <remarks/>
        public unitsUnitConfig config { get; set; }

        /// <remarks/>
        public unitsUnitBase @base { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte id { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string name { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class unitsUnitConfig
    {
        /// <remarks/>
        [XmlAttribute]
        public byte npcFlag { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte dynamicFlag { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte unitFlag { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte faction { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte maxHealth { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte maxLevel { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class unitsUnitBase
    {
        /// <remarks/>
        [XmlAttribute]
        public ushort attack { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public decimal attackRadius { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte civilian { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public decimal combat { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string damage { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public uint flags { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte level { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte maxHealth { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public ushort model { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string money { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public decimal size { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public decimal speed { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte type { get; set; }
    }


}