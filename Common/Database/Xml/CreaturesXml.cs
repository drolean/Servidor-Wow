using System.Xml.Serialization;

namespace Common.Database.Xml
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(IsNullable = false, ElementName = "units")]
    public class CreaturesXml
    {

        private unitsUnit[] unitField;

        /// <remarks/>
        [XmlElement("unit")]
        public unitsUnit[] unit
        {
            get
            {
                return unitField;
            }
            set
            {
                unitField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class unitsUnit
    {

        private unitsUnitConfig configField;

        private unitsUnitBase baseField;

        private byte idField;

        private string nameField;

        /// <remarks/>
        public unitsUnitConfig config
        {
            get
            {
                return configField;
            }
            set
            {
                configField = value;
            }
        }

        /// <remarks/>
        public unitsUnitBase @base
        {
            get
            {
                return baseField;
            }
            set
            {
                baseField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte id
        {
            get
            {
                return idField;
            }
            set
            {
                idField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public string name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class unitsUnitConfig
    {

        private byte npcFlagField;

        private byte dynamicFlagField;

        private byte unitFlagField;

        private byte factionField;

        private byte maxHealthField;

        private byte maxLevelField;

        /// <remarks/>
        [XmlAttribute]
        public byte npcFlag
        {
            get
            {
                return npcFlagField;
            }
            set
            {
                npcFlagField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte dynamicFlag
        {
            get
            {
                return dynamicFlagField;
            }
            set
            {
                dynamicFlagField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte unitFlag
        {
            get
            {
                return unitFlagField;
            }
            set
            {
                unitFlagField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte faction
        {
            get
            {
                return factionField;
            }
            set
            {
                factionField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte maxHealth
        {
            get
            {
                return maxHealthField;
            }
            set
            {
                maxHealthField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte maxLevel
        {
            get
            {
                return maxLevelField;
            }
            set
            {
                maxLevelField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class unitsUnitBase
    {

        private ushort attackField;

        private decimal attackRadiusField;

        private byte civilianField;

        private decimal combatField;

        private string damageField;

        private uint flagsField;

        private byte levelField;

        private byte maxHealthField;

        private ushort modelField;

        private string moneyField;

        private decimal sizeField;

        private decimal speedField;

        private byte typeField;

        /// <remarks/>
        [XmlAttribute]
        public ushort attack
        {
            get
            {
                return attackField;
            }
            set
            {
                attackField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public decimal attackRadius
        {
            get
            {
                return attackRadiusField;
            }
            set
            {
                attackRadiusField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte civilian
        {
            get
            {
                return civilianField;
            }
            set
            {
                civilianField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public decimal combat
        {
            get
            {
                return combatField;
            }
            set
            {
                combatField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public string damage
        {
            get
            {
                return damageField;
            }
            set
            {
                damageField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public uint flags
        {
            get
            {
                return flagsField;
            }
            set
            {
                flagsField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte level
        {
            get
            {
                return levelField;
            }
            set
            {
                levelField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte maxHealth
        {
            get
            {
                return maxHealthField;
            }
            set
            {
                maxHealthField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public ushort model
        {
            get
            {
                return modelField;
            }
            set
            {
                modelField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public string money
        {
            get
            {
                return moneyField;
            }
            set
            {
                moneyField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public decimal size
        {
            get
            {
                return sizeField;
            }
            set
            {
                sizeField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public decimal speed
        {
            get
            {
                return speedField;
            }
            set
            {
                speedField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public byte type
        {
            get
            {
                return typeField;
            }
            set
            {
                typeField = value;
            }
        }
    }


}