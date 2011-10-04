using AutoMoq.Helpers;
using Machine.Specifications;

namespace thirty.tests
{
    [Subject(typeof (InterfaceToImplementationConvention))]
    public class when_no_classes_exist_in_the_assembly : with_automoqer
    {
        private Establish context =
            () =>
                {
                };
    }

    public class InterfaceToImplementationConvention
    {
    }
}