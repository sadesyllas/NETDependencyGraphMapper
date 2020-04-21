using System;

namespace NETDependencyGraphMapper.Abstractions
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class HexColor : Attribute
    {
        public HexColor(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
