using System.Xml.Serialization;

namespace Common.Database.Xml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(IsNullable = false, ElementName = "zone")]
    public class ObjectsXml
    {
        /// <remarks/>
        [XmlElement("objeto")]
        public zoneObjeto[] objeto { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class zoneObjeto
    {
        /// <remarks/>
        public zoneObjetoMap map { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public uint id { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public int entry { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string name { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte type { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public int model { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte flags { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class zoneObjetoMap
    {
        /// <remarks/>
        [XmlAttribute]
        public float mapX { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public float mapY { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public float mapZ { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public float mapO { get; set; }
    }

}
