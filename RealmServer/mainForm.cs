using Framework.Helpers;
using System;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace RealmServer
{

    public partial class mainForm : Form
    {
        public mainForm()
        {
            var time = Time.getMSTime();
            var realmPoint = new IPEndPoint(IPAddress.Any, 1001);

            InitializeComponent();
            Win32.AllocConsole();
            this.Text = $"{Assembly.GetExecutingAssembly().GetName().Name} v{Assembly.GetExecutingAssembly().GetName().Version}";

            // Add columns
            listView1.Columns.Add("Id", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Name", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Char", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Location", -2, HorizontalAlignment.Left);

            Log.Print(LogType.RealmServer, $"World of Warcraft (Realm Server/World Server)");
            Log.Print(LogType.RealmServer, $"Supported WoW Client 1.2.1");
            Log.Print(LogType.RealmServer, $"Version {Assembly.GetExecutingAssembly().GetName().Version}");
            Log.Print(LogType.RealmServer, $"Running on .NET Framework Version {Environment.Version}");

            new RealmServerClass(realmPoint);

            Log.Print(LogType.RealmServer, $"Successfully started in {Time.getMSTimeDiff(time, Time.getMSTime()) / 1000}s");
        }
    }
}
