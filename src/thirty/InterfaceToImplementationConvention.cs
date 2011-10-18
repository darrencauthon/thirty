using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace thirty
{
    public class InterfaceToImplementationConvention
    {
        public InterfaceToImplementationConvention(Assembly assembly)
        {
        }

        public IDictionary<Type, Type> GetMatches()
        {
            var dictionary = new Dictionary<Type, Type>();

            foreach (var @interface in StaticMethods.GetInterfaces(null))
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
            return StaticMethods.GetConcreteTypes(null);
        }
    }
}