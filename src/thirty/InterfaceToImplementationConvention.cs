using System;
using System.Collections.Generic;

namespace thirty
{
    public class InterfaceToImplementationConvention
    {
        public IDictionary<Type, Type> GetMatches()
        {
            return new Dictionary<Type, Type>();
        }
    }
}