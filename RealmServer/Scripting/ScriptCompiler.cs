using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Common.Helpers;
using Microsoft.CSharp;

namespace RealmServer.Scripting
{
    public class VanillaPlugin
    {
        public virtual void Unload()
        {

        }
    }

    internal class ScriptCompiler
    {
        public VanillaPlugin Plugin { get; private set; }

        private Type _type;

        public string Name;

        public bool Compile(string filepath)
        {
            Name = Path.GetFileNameWithoutExtension(filepath);
            Thread.Sleep(100);
            var code = File.ReadAllText(filepath);

            CSharpCodeProvider codeProvider =
                new CSharpCodeProvider(new Dictionary<string, string> {{"CompilerVersion", "v4.0"}});
            CompilerParameters parameters = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false,
                IncludeDebugInformation = true
            };

            parameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            parameters.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            parameters.ReferencedAssemblies.Add("System.Xml.dll");
            parameters.ReferencedAssemblies.Add("System.Xml.Linq.dll");

            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.HasErrors)
            {
                var error = "Error in script: " + Name;

                foreach (CompilerError e in results.Errors)
                {
                    error += "\n" + e;
                }

                Log.Print(LogType.Error, error);
                return false;
            }

            //Successful Compile
            Log.Print(LogType.Debug, "Script Loaded: " + Name);

            _type = results.CompiledAssembly.GetTypes()[0];

            //Instansiate script class.
            try
            {
                if (_type.BaseType == typeof(VanillaPlugin))
                {
                    Plugin = Activator.CreateInstance(_type) as VanillaPlugin;
                }
                else
                {
                    Log.Print(LogType.Error, "Warning! " + Name + " isn't VanillaPlugin");
                    return false;
                }

            }
            catch (Exception)
            {
                Log.Print(LogType.Error, "Error instantiating " + Name);
                return false;
            }

            return true;
        }

        public object RunMethod(string methodName, object[] args = null)
        {
            return _type.InvokeMember(methodName, BindingFlags.InvokeMethod, null, Plugin, args);
            //return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
        }

        public object RunStaticMethod(string methodName, object[] args = null)
        {
            return _type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
        }
    }
}
