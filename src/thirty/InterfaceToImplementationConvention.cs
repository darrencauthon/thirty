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
                if (StaticMethods.GetConcreteTypes(null).Where(x => x.GetInterfaces().Contains(@interface)).Count() == 1)
                    dictionary[@interface] = StaticMethods.GetConcreteTypes(null).First();
            }

            return dictionary;
        }
    }
}