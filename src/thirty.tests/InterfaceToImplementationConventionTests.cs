using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace thirty.tests
{
    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_no_interfaces_exist_in_the_assembly
    {
        private Establish context =
            () =>
                {
                    var assembly = typeof (InterfaceToImplementationConvention).Assembly;

                    StaticMethods.SetInterfacesFunc(a => new Type[] {});

                    convention = new InterfaceToImplementationConvention();
                };

        private Because of =
            () => results = convention.GetMatches();

        private It should_return_no_results =
            () => results.Keys.Count.ShouldEqual(0);

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }
}