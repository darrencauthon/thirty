﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMoq.Helpers;
using Machine.Specifications;
using NUnit.Framework;
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
            () => results = convention.GetTypeMatches();

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
            () => results = convention.GetTypeMatches();

        private It should_return_three_results =
            () => results.Count().ShouldEqual(4);

        private It should_match_duck_to_bird =
            () => results[typeof (IBird)].ShouldEqual(typeof (Duck));

        private It should_match_tiger_to_cat =
            () => results[typeof (ICat)].ShouldEqual(typeof (Tiger));

        private It should_match_usa_to_country =
            () => results[typeof (ICountry)].ShouldEqual(typeof (UnitedStatesOfAmerica));

        private It should_match_age_to_one =
            () => results[typeof (IAge)].ShouldEqual(typeof (One));

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_ignoring_one_type_in_test_assembly1 : with_automoqer
    {
        private Establish context =
            () =>
                {
                    convention = new InterfaceToImplementationConvention(typeof (IBird).Assembly);
                    convention.IgnoreType(typeof (IBird));
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_two_result =
            () => results.Count().ShouldEqual(1);

        private It should_match_tiger_to_cat =
            () => results[typeof (ICat)].ShouldEqual(typeof (Tiger));

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_ignoring_one_concrete_type_in_test_assembly1 : with_automoqer
    {
        private Establish context =
            () =>
                {
                    convention = new InterfaceToImplementationConvention(typeof (IBird).Assembly);
                    convention.IgnoreType(typeof (Duck));
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_two_result =
            () => results.Count().ShouldEqual(1);

        private It should_match_tiger_to_cat =
            () => results[typeof (ICat)].ShouldEqual(typeof (Tiger));

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_setting_the_type_manually : with_automoqer
    {
        private Establish context =
            () =>
                {
                    convention = new InterfaceToImplementationConvention(typeof (IFruit).Assembly);
                    convention.IgnoreType(typeof (Duck));

                    convention.SetTypeMatch(typeof (IFruit), typeof (Apple));
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_match_tiger_to_cat =
            () => results[typeof (IFruit)].ShouldEqual(typeof (Apple));

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_setting_the_type_with_a_function : with_automoqer
    {
        private Establish context =
            () =>
                {
                    convention = new InterfaceToImplementationConvention(typeof (ICountry).Assembly);
                    unitedStatesOfAmerica = new UnitedStatesOfAmerica();
                    func = () => unitedStatesOfAmerica;
                    convention.SetFunctionMatch(func);
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_not_return_ICountry_with_matches =
            () => results.ContainsKey(typeof (ICountry)).ShouldBeFalse();

        private It should_return_the_func =
            () => Assert.AreSame(func, convention.GetFuncMatches()[typeof (ICountry)]);

        private It should_return_a_func_that_can_be_called =
            () =>
                {
                    var funcMatch = convention.GetFuncMatches()[typeof (ICountry)];
                    UnitedStatesOfAmerica usa = funcMatch();
                    Assert.AreSame(funcMatch(), usa);
                };

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
        private static Func<ICountry> func;
        private static UnitedStatesOfAmerica unitedStatesOfAmerica;
    }
}