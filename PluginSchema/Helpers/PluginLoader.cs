using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VideoDownloader.PluginSchema.Models;

namespace VideoDownloader.PluginSchema.Helpers
{
    public class PluginLoader
    {
        public ILog? Logger { get; set; }

        public IPlugin[] Plugins { get; private set; } = Array.Empty<IPlugin>();

        public void Load(IEnumerable<string> pluginFileNames)
        {
            try
            {
                Plugins = pluginFileNames
                    .Select(name => Path.GetFullPath(Path.Combine(".", name)))
                    .Where(File.Exists)
                    .Select(Assembly.LoadFile)
                    .Select(assembly => assembly.GetExportedTypes())
                    .Select(types => types.FirstOrDefault(type => type.GetInterfaces()
                        .Any(@interface => @interface == typeof(IPlugin))))
                    .Where(type => type != null)
                    .Select(Activator.CreateInstance)
                    .Cast<IPlugin>()
                    .ToArray();

                Logger?.Log(LogType.Info, $"{Plugins.Length} Plugin(s) loaded: " +
                                          string.Join(", ", Plugins.Select(plugin => plugin.PluginName)));
            }
            catch
            {
                Plugins = Array.Empty<IPlugin>();
            }
        }

        public void Load(IEnumerable<string> pluginFileNames, ILog loggerToAttach)
        {
            Load(pluginFileNames);

            foreach (var plugin in Plugins)
            {
                plugin.Logger = loggerToAttach;
            }
        }
    }
}
