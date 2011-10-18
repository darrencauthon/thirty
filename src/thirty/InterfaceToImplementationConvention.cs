using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace thirty
{
    public class InterfaceToImplementationConvention
    {
        private readonly Func<IEnumerable<Type>> concreteTypes;
        private readonly Func<IEnumerable<Type>> interfaces;

        private readonly List<Type> typesToIgnore = new List<Type>();

        public InterfaceToImplementationConvention(Assembly assembly)
        {
            concreteTypes = () => StaticMethods.GetConcreteTypes(assembly);
            interfaces = () => StaticMethods.GetInterfaces(assembly);
        }

        public InterfaceToImplementationConvention(IEnumerable<Assembly> assemblies)
        {
            concreteTypes = () => assemblies.SelectMany(StaticMethods.GetConcreteTypes);
            interfaces = () => assemblies.SelectMany(StaticMethods.GetInterfaces);
        }

        public IDictionary<Type, Type> GetMatches()
        {
            var dictionary = new Dictionary<Type, Type>();

            foreach (var @interface in interfaces().Where(x => typesToIgnore.Contains(x) == false))
            {
                var implementations = concreteTypes().Where(ImplementThisInterface(@interface))
                    .Where(x=>typesToIgnore.Contains(x) == false);
                if (ThereIsOnlyOneImplementation(implementations))
                    dictionary[@interface] = implementations.Single();
            }

            return dictionary;
        }

        private static bool ThereIsOnlyOneImplementation(IEnumerable<Type> implementations)
        {
            return implementations.Count() == 1;
        }

        private static Func<Type, bool> ImplementThisInterface(Type @interface)
        {
            return x => x.GetInterfaces().Contains(@interface);
        }

        public void IgnoreType(Type type)
        {
            typesToIgnore.Add(type);
        }
    }
}