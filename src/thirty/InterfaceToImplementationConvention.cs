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
        private readonly Dictionary<Type, Type> manualMatches = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, dynamic> functionMatches = new Dictionary<Type, dynamic>();

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

        public IDictionary<Type, Type> GetTypeMatches()
        {
            var dictionary = GetAllInterfacesWithOneImplementation()
                .ToDictionary(x => x, GetTheSingleImplementationOfThisInterface);

            foreach (var key in manualMatches.Keys)
                dictionary[key] = manualMatches[key];

            return dictionary;
        }

        public void IgnoreType(Type type)
        {
            typesToIgnore.Add(type);
        }

        public virtual void SetTypeMatch(Type @interface, Type implementation)
        {
            manualMatches[@interface] = implementation;
        }

        public virtual void SetFunctionMatch<T>(Func<T> func)
        {
            typesToIgnore.Add(typeof (T));
            functionMatches.Add(typeof (T), func);
        }

        public IDictionary<Type, dynamic> GetFuncMatches()
        {
            return functionMatches;
        }

        private Type GetTheSingleImplementationOfThisInterface(Type @interface)
        {
            return GetImplementationsOfThisInterface(@interface).Single();
        }

        private IEnumerable<Type> GetAllInterfacesWithOneImplementation()
        {
            return GetAllInterfaces()
                .Where(@interface => GetImplementationsOfThisInterface(@interface).Count() == 1);
        }

        private IEnumerable<Type> GetAllInterfaces()
        {
            return interfaces()
                .Where(x => typesToIgnore.Contains(x) == false);
        }

        private IEnumerable<Type> GetImplementationsOfThisInterface(Type @interface)
        {
            return concreteTypes().Where(ImplementThisInterface(@interface))
                .Where(x => typesToIgnore.Contains(x) == false);
        }

        private static Func<Type, bool> ImplementThisInterface(Type @interface)
        {
            return x => x.GetInterfaces().Contains(@interface);
        }
    }
}