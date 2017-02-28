using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Helpers;

namespace RealmServer.Scripting
{
    internal class ScriptManager
    {
        private static readonly List<ScriptCompiler> Scripts = new List<ScriptCompiler>();
        private static FileSystemWatcher _watcher;

        public static void Boot()
        {
            LoadScripts();

            _watcher = new FileSystemWatcher("scripts")
            {
                NotifyFilter =
                    NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                    NotifyFilters.DirectoryName,
                Filter = "*.cs"
            };

            _watcher.Changed += ReloadScript;
            _watcher.Created += LoadScript;
            _watcher.Renamed += LoadScript;
            _watcher.Deleted += UnloadScript;

            _watcher.EnableRaisingEvents = true;

            Log.Print(LogType.Console, $"Scripts Initialized.");
        }

        private static void LoadScripts()
        {
            if (!Directory.Exists(ScriptLocation))
            {
                Log.Print(LogType.Debug, "Script location (" + ScriptLocation + ") dosn't exist. Creating...");

                Directory.CreateDirectory(ScriptLocation);
            }

            var scripts = Directory.GetFiles(ScriptLocation);

            foreach (var script in scripts)
            {
                LoadScript(script);
            }
        }

        private static void ReloadScript(object source, FileSystemEventArgs e)
        {
            try
            {
                _watcher.EnableRaisingEvents = false;

                string scriptName = Path.GetFileNameWithoutExtension(e.FullPath);
                UnloadScript(scriptName);
                LoadScript(e.FullPath);
            }

            finally
            {
                _watcher.EnableRaisingEvents = true;
            }
        }

        private static void LoadScript(object sender, FileSystemEventArgs e)
        {
            LoadScript(e.FullPath);
        }

        private static void LoadScript(string scriptPath)
        {
            ScriptCompiler script = new ScriptCompiler();
            if (script.Compile(scriptPath)) Scripts.Add(script);
        }

        private static void UnloadScript(object sender, FileSystemEventArgs e)
        {
            UnloadScript(Path.GetFileNameWithoutExtension(e.FullPath));
        }

        private static void UnloadScript(string scriptName)
        {
            ScriptCompiler script = Scripts.FirstOrDefault(s => s.Name == scriptName);

            if (script == null) return;

            Scripts.Remove(script);
            script.Plugin.Unload();

            Log.Print(LogType.Debug, "Script Unloaded: " + scriptName);
        }

        public static string ScriptLocation => "scripts";
    }
}