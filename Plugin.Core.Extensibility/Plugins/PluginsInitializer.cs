using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Ninject;
using Ninject.Extensions.Conventions;

namespace Plugin.Core.Extensibility.Plugins
{
    public static class PluginsInitializer
    {
        private static IKernel kernelInternal;

        public static IKernel Kernel => kernelInternal ?? (kernelInternal = new StandardKernel());

        public static void Initialize()
        {
            string pluginsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins.json");
            string pluginsFileContent = File.ReadAllText(pluginsFilePath);
            var plugins = JsonConvert.DeserializeObject<IEnumerable<PluginsRoot>>(pluginsFileContent);
            foreach (PluginsRoot pluginRoot in plugins)
            {
                LoadPlugins(pluginRoot.Root, pluginRoot.Plugins.ToArray());
            }
        }

        public static void LoadPlugins(string pluginsRootPath, string[] plugins)
        {
            foreach (Assembly assembly in GetAssemblies(pluginsRootPath, plugins))
            {
                kernelInternal.Load(assembly);
                BindToSelf<IDiscoverable>(assembly);
            }

            InitalizePlugin(kernelInternal);
        }

        public static IEnumerable<Assembly> GetAssemblies(string pluginsRootPath, string[] plugins)
        {
            return plugins
                .Select(pluginName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pluginsRootPath, $"{pluginName}.dll"))
                .Select(Assembly.LoadFrom);
        }

        private static void InitalizePlugin(IKernel kernel)
        {
            List<IPluginInitializer> pluginInitializers = kernel.GetAll<IPluginInitializer>().ToList();

            foreach (IPluginInitializer pluginInitializer in pluginInitializers)
            {
                pluginInitializer.Initialize();
            }
        }

        private static void BindToSelf<TInterface>(Assembly assembly)
        {
            kernelInternal.Bind(syntax =>
            syntax
            .From(assembly)
            .SelectAllClasses()
            .InheritedFrom<TInterface>()
            .Where(type => !type.IsAbstract)
            .BindSelection((type, baseTypes) => type.GetInterfaces().Where(i => typeof(TInterface).IsAssignableFrom(i))));
        }
    }
}