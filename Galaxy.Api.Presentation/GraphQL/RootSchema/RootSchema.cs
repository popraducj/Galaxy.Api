using System;
using System.Globalization;
using Galaxy.Api.Presentation.GraphQL.Helpers;
using GraphQL;
using GraphQL.Types;

namespace Galaxy.Api.Presentation.GraphQl.RootSchema
{
    public class RootSchema : Schema
    {
        public RootSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
            Mutation = resolver.Resolve<RootMutation>();
            RegisterValueConverter(new GuidGraphTypeConverter());
            ValueConverter.Register(
                typeof(float),
                typeof(double),
                value => Convert.ToDouble(Math.Round((float)value, 3, MidpointRounding.AwayFromZero), NumberFormatInfo.InvariantInfo));
        }
    }
}
