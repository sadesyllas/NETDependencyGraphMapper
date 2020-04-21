using System.Diagnostics.CodeAnalysis;
using System.Xml;
using NETDependencyGraphMapper.Abstractions;

namespace NETDependencyGraphMapper.Models.yEd
{
    // ReSharper disable once InconsistentNaming
    public class yEdGraphEntityData : IGraphSerializable
    {
        private readonly ElementType _for;
        private readonly yEdDataType _type;

        public yEdGraphEntityData(string id, ElementType @for, yEdDataType type)
        {
            Id = id;
            _for = @for;
            _type = type;
        }

        protected string Id { get; }

        protected yEdGraphEntityData([NotNull] yEdGraphEntityData graphEntityData)
        {
            Id = graphEntityData.Id;
            _for = graphEntityData._for;
            _type = graphEntityData._type;
        }

        public virtual void Serialize(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("key");
            xmlWriter.WriteAttributeString("id", Id);
            xmlWriter.WriteAttributeString("for", _for.Serialize());
            xmlWriter.WriteAttributeString("yfiles.type", _type.Serialize());
            xmlWriter.WriteEndElement();
        }
    }
}
