using System.Xml.Serialization;

namespace Common.Database.Xml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(IsNullable = false, ElementName = "zone")]
    public class ObjectsXml
    {
        /// <remarks/>
        [XmlElement("object")]
        public objectsObject[] @object { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class objectsObject
    {
        /// <remarks/>
        public ushort entry { get; set; }

        /// <remarks/>
        public objectsObjectMap map { get; set; }

        /// <remarks/>
        public objectsObjectConfig config { get; set; }

        /// <remarks/>
        [XmlArray]
        [XmlArrayItem("rotation", IsNullable = false)]
        public byte[] rotations { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public byte id { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class objectsObjectMap
    {
        /// <remarks/>
        [XmlAttribute]
        public string x { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string y { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string z { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string o { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class objectsObjectConfig
    {
        /// <remarks/>
        [XmlAttribute]
        public string spawntime { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string animprogress { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string state { get; set; }
    }
}