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

        public InterfaceToImplementationConvention(Assembly assembly)
        {
            concreteTypes = () => StaticMethods.GetConcreteTypes(null);
            interfaces = () => StaticMethods.GetInterfaces(null);
        }

        public IDictionary<Type, Type> GetMatches()
        {
            var dictionary = new Dictionary<Type, Type>();

            foreach (var @interface in interfaces())
            {
                var implementations = ConcreteTypes().Where(ThatImplementThisInterface(@interface));
                if (ThereIsOnlyOneImplementation(implementations))
                    dictionary[@interface] = implementations.Single();
            }

            return dictionary;
        }

        private static bool ThereIsOnlyOneImplementation(IEnumerable<Type> implementations)
        {
            return implementations.Count() == 1;
        }

        private Func<Type, bool> ThatImplementThisInterface(Type @interface)
        {
            return x => x.GetInterfaces().Contains(@interface);
        }

        private IEnumerable<Type> ConcreteTypes()
        {
            return concreteTypes();
        }
    }
}