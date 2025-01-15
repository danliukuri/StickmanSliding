using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson
{
    public class TypeInstancesProvider
    {
        public List<T> GetInstances<T>() => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(GetLoadedTypes)
            .Where(type => typeof(T).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<T>()
            .ToList();

        private static IEnumerable<Type> GetLoadedTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException exception)
            {
                return exception.Types.Where(type => type != null);
            }
        }
    }
}