using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Common.Database.Xml;
using Common.Helpers;

namespace Common.Database
{
    public class XmlReader
    {
        public static ItemsXml RetornoItems { get; private set; }

        public static void Boot()
        {
            Log.Print(LogType.RealmServer, $"Loading XML...");
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ItemsXml));
                StreamReader reader = new StreamReader($"xml/items.xml");
                RetornoItems = serializer.Deserialize(reader) as ItemsXml;
                if (RetornoItems != null)
                    Log.Print(LogType.RealmServer, $"- Items Loaded: {RetornoItems.Item.Count()}");
                reader.Close();
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }

        public static ItemsItem GetItem(int value)
        {
            try
            {
                return RetornoItems.Item.First(a => a.id == value);
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
