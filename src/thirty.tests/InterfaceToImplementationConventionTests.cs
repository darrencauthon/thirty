using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using NUnit.Framework;

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

                    convention = new InterfaceToImplementationConvention(assembly);
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_no_results =
            () => results.Keys.Count.ShouldEqual(0);

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_one_interface_with_matching_type_exists
    {
        private Establish context =
            () =>
                {
                    var assembly = typeof (InterfaceToImplementationConvention).Assembly;

                    StaticMethods.SetInterfacesFunc(a => new[] {typeof (ITestInterface1)});
                    StaticMethods.SetConcreteTypesFunc(c => new[] {typeof (TestInterface1Implementation)});

                    convention = new InterfaceToImplementationConvention(assembly);
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_one_result =
            () => results.Keys.Count.ShouldEqual(1);

        private It should_return_the_match =
            () => results[typeof (ITestInterface1)].ShouldEqual(typeof (TestInterface1Implementation));

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_one_interface_with_two_matching_types_exist
    {
        private Establish context =
            () =>
                {
                    var assembly = typeof (InterfaceToImplementationConvention).Assembly;

                    StaticMethods.SetInterfacesFunc(a => new[] {typeof (ITestInterface1)});
                    StaticMethods.SetConcreteTypesFunc(c => new[]
                                                                {
                                                                    typeof (TestInterface1Implementation),
                                                                    typeof (TestInterface1Implementation2)
                                                                }
                        );

                    convention = new InterfaceToImplementationConvention(assembly);
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_no_results =
            () => results.Keys.Count.ShouldEqual(0);

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_one_interface_with_no_matching_types_exist
    {
        private Establish context =
            () =>
                {
                    var assembly = typeof (InterfaceToImplementationConvention).Assembly;

                    StaticMethods.SetInterfacesFunc(a => new[] {typeof (ITestInterface1)});
                    StaticMethods.SetConcreteTypesFunc(c => new Type[] {});

                    convention = new InterfaceToImplementationConvention(assembly);
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_no_results =
            () => results.Keys.Count.ShouldEqual(0);

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_one_interface_with_non_matching_types_exist
    {
        private Establish context =
            () =>
                {
                    var assembly = typeof (InterfaceToImplementationConvention).Assembly;

                    StaticMethods.SetInterfacesFunc(a => new[] {typeof (ITestInterface1)});
                    StaticMethods.SetConcreteTypesFunc(c => new[] {typeof (ClassWithNoInterfaces)});

                    convention = new InterfaceToImplementationConvention(assembly);
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_no_results =
            () => results.Keys.Count.ShouldEqual(0);

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_one_interface_with_many_different_types_and_one_match_exists
    {
        private Establish context =
            () =>
                {
                    var assembly = typeof (InterfaceToImplementationConvention).Assembly;

                    StaticMethods.SetInterfacesFunc(a => new[] {typeof (ITestInterface1)});
                    StaticMethods.SetConcreteTypesFunc(c => new[]
                                                                {
                                                                    typeof (string), typeof (int), typeof (TestInterface1Implementation), typeof (decimal)
                                                                });

                    convention = new InterfaceToImplementationConvention(assembly);
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_one_result =
            () => results.Keys.Count.ShouldEqual(1);

        private It should_return_the_match =
            () => results[typeof (ITestInterface1)].ShouldEqual(typeof (TestInterface1Implementation));

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_one_match_exists_but_the_interface_is_ignored
    {
        private Establish context =
            () =>
                {
                    var assembly = typeof (InterfaceToImplementationConvention).Assembly;

                    StaticMethods.SetInterfacesFunc(a => new[] {typeof (ITestInterface1)});
                    StaticMethods.SetConcreteTypesFunc(c => new[]
                                                                {
                                                                    typeof (string), typeof (int), typeof (TestInterface1Implementation), typeof (decimal)
                                                                });

                    convention = new InterfaceToImplementationConvention(assembly);

                    convention.IgnoreType(typeof (ITestInterface1));
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_one_result =
            () => results.Keys.Count.ShouldEqual(0);

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_one_match_exists_but_the_concrete_type_is_ignored
    {
        private Establish context =
            () =>
                {
                    var assembly = typeof (InterfaceToImplementationConvention).Assembly;

                    StaticMethods.SetInterfacesFunc(a => new[] {typeof (ITestInterface1)});
                    StaticMethods.SetConcreteTypesFunc(c => new[]
                                                                {
                                                                    typeof (string), typeof (int), typeof (TestInterface1Implementation), typeof (decimal)
                                                                });

                    convention = new InterfaceToImplementationConvention(assembly);

                    convention.IgnoreType(typeof (TestInterface1Implementation));
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_one_result =
            () => results.Keys.Count.ShouldEqual(0);

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_a_type_is_set_manually
    {
        private Establish context =
            () =>
                {
                    var assembly = typeof (InterfaceToImplementationConvention).Assembly;

                    StaticMethods.SetInterfacesFunc(a => new[] {typeof (ITestInterface1)});
                    StaticMethods.SetConcreteTypesFunc(c => new[]
                                                                {
                                                                    typeof (string), typeof (int), typeof (decimal)
                                                                });

                    convention = new InterfaceToImplementationConvention(assembly);

                    convention.SetTypeMatch(typeof (ITestInterface1), typeof (TestInterface1Implementation));
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_return_one_result =
            () => results.Keys.Count.ShouldEqual(1);

        private It should_return_the_manually_set_match =
            () => results[typeof (ITestInterface1)].ShouldEqual(typeof (TestInterface1Implementation));

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
    }

    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_a_type_is_set_with_a_func
    {
        private Establish context =
            () =>
                {
                    var assembly = typeof (InterfaceToImplementationConvention).Assembly;

                    StaticMethods.SetInterfacesFunc(a => new[] {typeof (ITestInterface1)});
                    StaticMethods.SetConcreteTypesFunc(c => new[]
                                                                {
                                                                    typeof (string), typeof (int), typeof (decimal)
                                                                });

                    convention = new InterfaceToImplementationConvention(assembly);

                    func = () => new TestInterface1Implementation();
                    convention.SetFunctionMatch<ITestInterface1>(func);
                };

        private Because of =
            () => results = convention.GetTypeMatches();

        private It should_not_return_the_interface_in_the_results =
            () => results.ContainsKey(typeof (ITestInterface1)).ShouldBeFalse();

        private It should_return_the_func_match =
            () => Assert.AreEqual(func, convention.GetFuncMatches()[typeof (ITestInterface1)]);

        private static InterfaceToImplementationConvention convention;
        private static IDictionary<Type, Type> results;
        private static Func<ITestInterface1> func;
    }
}