using System.Xml.Serialization;

namespace Common.Database.Xml
{
    /// <remarks />
    [XmlType(AnonymousType = true)]
    [XmlRoot(IsNullable = false, ElementName = "races")]
    public class RacesXml
    {
        /// <remarks />
        [XmlElement("race")]
        public racesRace[] race { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRace
    {
        /// <remarks />
        public byte health { get; set; }

        /// <remarks />
        public racesRaceInit init { get; set; }

        /// <remarks />
        public racesRaceStats stats { get; set; }

        /// <remarks />
        public racesRaceLeveling leveling { get; set; }

        /// <remarks />
        [XmlArrayItem("spell", IsNullable = false)]
        public racesRaceSpell[] spells { get; set; }

        /// <remarks />
        [XmlArrayItem("skill", IsNullable = false)]
        public racesRaceSkill[] skills { get; set; }

        /// <remarks />
        [XmlArrayItem("action", IsNullable = false)]
        public racesRaceAction[] actions { get; set; }

        /// <remarks />
        [XmlArrayItem("class", IsNullable = false)]
        public racesRaceClass[] classes { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte id { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceInit
    {
        /// <remarks />
        [XmlAttribute]
        public byte MapId { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte ZoneId { get; set; }

        /// <remarks />
        [XmlAttribute]
        public float MapX { get; set; }

        /// <remarks />
        [XmlAttribute]
        public float MapY { get; set; }

        /// <remarks />
        [XmlAttribute]
        public float MapZ { get; set; }

        /// <remarks />
        [XmlAttribute]
        public float MapR { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte Cinematic { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceStats
    {
        /// <remarks />
        [XmlAttribute]
        public byte str { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte agi { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte sta { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte @int { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte spi { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceLeveling
    {
        /// <remarks />
        [XmlAttribute]
        public decimal str { get; set; }

        /// <remarks />
        [XmlAttribute]
        public decimal agi { get; set; }

        /// <remarks />
        [XmlAttribute]
        public decimal sta { get; set; }

        /// <remarks />
        [XmlAttribute]
        public decimal @int { get; set; }

        /// <remarks />
        [XmlAttribute]
        public decimal spi { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceSpell
    {
        /// <remarks />
        [XmlAttribute]
        public ushort id { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceSkill
    {
        /// <remarks />
        [XmlAttribute]
        public ushort id { get; set; }

        /// <remarks />
        [XmlAttribute]
        public ushort min { get; set; }

        /// <remarks />
        [XmlAttribute]
        public ushort max { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceAction
    {
        /// <remarks />
        [XmlAttribute]
        public byte button { get; set; }

        /// <remarks />
        [XmlAttribute]
        public ushort action { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte type { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceClass
    {
        /// <remarks />
        public byte health { get; set; }

        /// <remarks />
        public string atackspeed { get; set; }

        /// <remarks />
        public racesRaceClassStats stats { get; set; }

        /// <remarks />
        public racesRaceClassLeveling leveling { get; set; }

        /// <remarks />
        public racesRaceClassModificadores modificadores { get; set; }

        /// <remarks />
        public racesRaceClassPower power { get; set; }

        /// <remarks />
        [XmlArrayItem("spell", IsNullable = false)]
        public racesRaceClassSpell[] spells { get; set; }

        /// <remarks />
        [XmlArrayItem("skill", IsNullable = false)]
        public racesRaceClassSkill[] skills { get; set; }

        /// <remarks />
        [XmlArrayItem("action", IsNullable = false)]
        public racesRaceClassAction[] actions { get; set; }

        /// <remarks />
        [XmlAttribute]
        public string id { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceClassStats
    {
        /// <remarks />
        [XmlAttribute]
        public byte str { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte agi { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte sta { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte @int { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte spi { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceClassLeveling
    {
        /// <remarks />
        [XmlAttribute]
        public decimal str { get; set; }

        /// <remarks />
        [XmlAttribute]
        public decimal agi { get; set; }

        /// <remarks />
        [XmlAttribute]
        public decimal sta { get; set; }

        /// <remarks />
        [XmlAttribute]
        public decimal @int { get; set; }

        /// <remarks />
        [XmlAttribute]
        public decimal spi { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceClassModificadores
    {
        /// <remarks />
        public racesRaceClassModificadoresSta sta { get; set; }

        /// <remarks />
        public racesRaceClassModificadoresInt @int { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceClassModificadoresSta
    {
        /// <remarks />
        [XmlAttribute]
        public byte @base { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte mod { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceClassModificadoresInt
    {
        /// <remarks />
        [XmlAttribute]
        public byte @base { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte mod { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceClassPower
    {
        /// <remarks />
        [XmlAttribute]
        public byte mana { get; set; }

        /// <remarks />
        [XmlAttribute]
        public ushort rage { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte energy { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceClassSpell
    {
        /// <remarks />
        [XmlAttribute]
        public ushort id { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceClassSkill
    {
        /// <remarks />
        [XmlAttribute]
        public ushort id { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte min { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte max { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    public class racesRaceClassAction
    {
        /// <remarks />
        [XmlAttribute]
        public byte button { get; set; }

        /// <remarks />
        [XmlAttribute]
        public ushort spell { get; set; }

        /// <remarks />
        [XmlAttribute]
        public byte type { get; set; }
    }
}