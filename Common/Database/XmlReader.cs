using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static ItemsXml ItemsXml { get; private set; }
        public static RacesXml RacesXml { get; private set; }
        public static ObjectsXml ObjectsAzeroth { get; set; }
        public static CreaturesXml CreaturesXml { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static void Boot()
        {
            Log.Print(LogType.Loading, "Loading XML .......................... [OK]");
            try
            {
                // Items
                XmlSerializer serializerItems = new XmlSerializer(typeof(ItemsXml));
                StreamReader readerItems = new StreamReader("xml/items.xml");
                ItemsXml = serializerItems.Deserialize(readerItems) as ItemsXml;
                if (ItemsXml != null)
                    Log.Print(LogType.Loading, $"=_ Items Loaded .........: {ItemsXml.Item.Length}");
                readerItems.Close();

                // Races
                XmlSerializer serializerRaces = new XmlSerializer(typeof(RacesXml));
                StreamReader readerRaces = new StreamReader("xml/races.xml");
                RacesXml = serializerRaces.Deserialize(readerRaces) as RacesXml;
                if (RacesXml != null)
                    Log.Print(LogType.Loading, $"=_ Races Loaded .........: {RacesXml.race.Length}");
                readerRaces.Close();

                // Creatures
                /*
                XmlSerializer serializerCreatures = new XmlSerializer(typeof(CreaturesXml));
                StreamReader readerCreatures = new StreamReader("xml/creatures.xml");
                CreaturesXml = serializerCreatures.Deserialize(readerCreatures) as CreaturesXml;
                if (CreaturesXml != null)
                    Log.Print(LogType.Loading, $"=_ Creatures Loaded .....: {CreaturesXml.unit.Length}");
                readerCreatures.Close();
                */

                // Game Objects
                foreach (var file in Directory.EnumerateFiles("xml\\world\\"))
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(ObjectsXml));
                    StreamReader streamReader = new StreamReader(file);

                    ObjectsAzeroth = mySerializer.Deserialize(streamReader) as ObjectsXml;
                    streamReader.Close();

                    if (ObjectsAzeroth != null)
                        Log.Print(LogType.Loading, $"=_ {file} .......: Objects ({ObjectsAzeroth.objeto.Length})");
                }
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="race"></param>
        /// <returns></returns>
        public static racesRace GetRace(Races race)
        {
            try
            {
                return RacesXml?.race.FirstOrDefault(a => a.id == (int) race);
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="race"></param>
        /// <param name="classe"></param>
        /// <returns></returns>
        public static racesRaceClass GetRaceClass(Races race, Classes classe)
        {
            try
            {
                return RacesXml.race.FirstOrDefault(ok => ok.id == (int) race)?.classes.First(ba => ba.id == classe.ToString());
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ItemsItem GetItem(int value)
        {
            try
            {
                return ItemsXml.Item.First(a => a.id == value);
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }

            return null;
        }
    }
}
