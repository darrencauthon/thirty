using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("thirty.tests")]

namespace thirty
{
    internal static class StaticMethods
    {
        private static Func<Assembly, IEnumerable<Type>> getTypesFunc
            = assembly =>
                  {
                      try
                      {
                          return assembly.GetTypes();
                      }
                      catch
                      {
                          return new Type[] {};
                      }
                  };

        private static readonly Func<Assembly, IEnumerable<Type>> getInterfacesFunc
            = assembly => getTypesFunc(assembly).Where(x => x.IsInterface);

        private static readonly Func<Assembly, IEnumerable<Type>> getConcreteTypes
            = assembly => getTypesFunc(assembly).Where(x => x.IsInterface == false && x.IsAbstract == false);

        internal static void SetGetTypesFunc(Func<Assembly, IEnumerable<Type>> func)
        {
            getTypesFunc = func;
        }

        internal static IEnumerable<Type> GetTypes(Assembly assembly)
        {
            return getTypesFunc(assembly);
        }

        internal static IEnumerable<Type> GetInterfaces(Assembly assembly)
        {
            return getInterfacesFunc(assembly);
        }

        internal static IEnumerable<Type> GetConcreteTypes(Assembly assembly)
        {
            return getConcreteTypes(assembly);
        }
    }
}