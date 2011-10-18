using System;
using System.Collections.Generic;
using System.Linq;
using AutoMoq.Helpers;
using Machine.Specifications;
using TestAssembly1;
using TestAssembly2;

namespace thirty.specs
{
    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_retrieving_the_matches_for_test_assembly1 : with_automoqer
    {
        private Establish context =
            () => { convention = new InterfaceToImplementationConvention(typeof (IBird).Assembly); };

        private Because of =
            () => results = convention.GetMatches();

        private It should_return_two_result =
            () => results.Count().ShouldEqual(2);

        private It should_match_duck_to_bird =
            () => results[typeof (IBird)].ShouldEqual(typeof (Duck));

        private It should_match_tiger_to_cat =
            () => results[typeof (ICat)].ShouldEqual(typeof (Tiger));

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_retrieving_the_matches_for_test_assembly1_and_test_assembly2 : with_automoqer
    {
        private Establish context =
            () => { convention = new InterfaceToImplementationConvention(new[] {typeof (IBird).Assembly, typeof (ICountry).Assembly}); };

        private Because of =
            () => results = convention.GetMatches();

        private It should_return_three_results =
            () => results.Count().ShouldEqual(3);

        private It should_match_duck_to_bird =
            () => results[typeof (IBird)].ShouldEqual(typeof (Duck));

        private It should_match_tiger_to_cat =
            () => results[typeof (ICat)].ShouldEqual(typeof (Tiger));

        private It should_match_usa_to_country =
            () => results[typeof (ICountry)].ShouldEqual(typeof (UnitedStatesOfAmerica));

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }
}