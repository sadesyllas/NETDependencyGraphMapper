using System.Xml;

namespace NETDependencyGraphMapper.Abstractions
{
    public interface IGraphSerializable
    {
        void Serialize(XmlWriter xmlWriter);
    }
}
