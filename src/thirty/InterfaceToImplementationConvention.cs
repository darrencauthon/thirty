using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace thirty
{
    public class InterfaceToImplementationConvention
    {
        private readonly Assembly assembly;

        public InterfaceToImplementationConvention(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public IDictionary<Type, Type> GetMatches()
        {
            var dictionary = new Dictionary<Type, Type>();

            foreach (var @interface in StaticMethods.GetInterfaces(null))
                dictionary[@interface] = StaticMethods.GetConcreteTypes(null).First();

            return dictionary;
        }
    }
}