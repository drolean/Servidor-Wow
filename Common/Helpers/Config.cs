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
            ProfaneNames = new string[] { "A", "B", "C" };
        }

        [XmlIgnore]
        public const string FileName = "config.xml";

        [XmlIgnore]
        public static Config Instance { get; private set; }

        public static void Default()
        {
            Config.Instance = new Config();
            Config.Instance.SetDefaultValues();
        }

        public static void Load()
        {
            var serializer = new XmlSerializer(typeof(Config));

            using (var fStream = new FileStream(Config.FileName, FileMode.Open))
                Config.Instance = (Config)serializer.Deserialize(fStream);
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(Config));

            using (var fStream = new FileStream(Config.FileName, FileMode.Create))
                serializer.Serialize(fStream, this);
        }
    }
}