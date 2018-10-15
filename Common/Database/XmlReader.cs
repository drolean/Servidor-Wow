using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Common.Database.Xml;
using Common.Globals;
using Common.Helpers;

namespace Common.Database
{
    public class XmlReader
    {
        public static RacesXml RacesXml { get; private set; }

        public static void Boot()
        {
            Log.Print(LogType.RealmServer, "Loading XML ".PadRight(40, '.') + " [OK] ");

            // Races
            XmlSerializer serializerRaces = new XmlSerializer(typeof(RacesXml));
            StreamReader readerRaces = new StreamReader("xml/races.xml");
            RacesXml = serializerRaces.Deserialize(readerRaces) as RacesXml;
            readerRaces.Close();
        }

        public static racesRace GetRace(Races race)
        {
            return RacesXml?.race.FirstOrDefault(a => a.id == (int)race);
        }

        public static racesRaceClass GetRaceClass(Races race, Classes classe)
        {
            return RacesXml?.race.FirstOrDefault(a => a.id == (int)race)?.classes.First(ba => ba.id == classe.ToString());
        }
    }
}