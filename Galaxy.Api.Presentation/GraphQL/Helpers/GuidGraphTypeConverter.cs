using System;
using GraphQL.Language.AST;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Galaxy.Api.Presentation.GraphQL.Helpers
{
    public class GuidGraphTypeConverter : IAstFromValueConverter
    {
        public bool Matches(object value, IGraphType type)
        {
            return type.Name == "Guid";
        }

        public IValue Convert(object value, IGraphType type)
        {
            return new GuidValue(Guid.Parse(value.ToString()));
        }
    }
    public class GuidGraphTypeCustom : ScalarGraphType
    {
        static GuidGraphTypeCustom()
        {
            GraphTypeTypeRegistry.Register(typeof(Guid), typeof(GuidGraphTypeCustom));
        }

        public GuidGraphTypeCustom()
        {
            Name = "Guid";
        }

        public override object Serialize(object value)
        {
            return ParseValue(value);
        }

        public override object ParseValue(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (Guid.TryParse(value.ToString(), out var guidResult))
            {
                return guidResult;
            }

            return null;
        }

        public override object ParseLiteral(IValue value)
        {
            switch (value)
            {
                case GuidValue guidValue:
                    return guidValue.Value;
                case StringValue stringValue:
                    return Guid.Parse(stringValue.Value);
                default:
                    return null;
            }
        }
    }

}