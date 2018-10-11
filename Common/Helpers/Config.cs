using System.IO;
using System.Xml.Serialization;

namespace Common.Helpers
{
    public class Config
    {
        public int LimitCharacterRealm;
        public string[] ProfaneNames;

        private void SetDefaultValues()
        { 
            LimitCharacterRealm = 10;
            ProfaneNames = new[] { "root", "Admin", "Administrator" };
        }

        [XmlIgnore]
        public const string FileName = "config.xml";

        [XmlIgnore]
        public static Config Instance { get; private set; }

        public static void Default()
        {
            Instance = new Config();
            Instance.SetDefaultValues();
        }

        public static void Load()
        {
            var serializer = new XmlSerializer(typeof(Config));

            using (var fStream = new FileStream(FileName, FileMode.Open))
                Instance = (Config)serializer.Deserialize(fStream);
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(Config));

            using (var fStream = new FileStream(FileName, FileMode.Create))
                serializer.Serialize(fStream, this);
        }
    }
}