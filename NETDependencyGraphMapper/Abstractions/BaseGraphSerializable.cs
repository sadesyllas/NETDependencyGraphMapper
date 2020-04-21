using System.Runtime.CompilerServices;
using System.Xml;

namespace NETDependencyGraphMapper.Abstractions
{
    public abstract class BaseGraphSerializable : IGraphSerializable
    {
        private readonly string _hashCodeSeed;

        protected BaseGraphSerializable(string hashCodeSeed)
        {
            _hashCodeSeed = hashCodeSeed;
        }

        public abstract void Serialize(XmlWriter xmlWriter);

        public abstract override bool Equals(object? other);

        public override int GetHashCode() => RuntimeHelpers.GetHashCode(_hashCodeSeed);
    }
}
