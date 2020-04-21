using System.Diagnostics.CodeAnalysis;
using System.Xml;
using NETDependencyGraphMapper.Abstractions;

namespace NETDependencyGraphMapper.Models
{
    public class GraphEntityData : IGraphSerializable
    {
        private readonly ElementType _for;
        private readonly string _name;
        private readonly DataType _type;
        private readonly string? _defaultValue;

        public GraphEntityData(string id, ElementType @for, string name, DataType type,
            string? defaultValue = null)
        {
            Id = id;
            _for = @for;
            _name = name;
            _type = type;
            _defaultValue = defaultValue;
        }

        protected string Id { get; }

        protected GraphEntityData([NotNull] GraphEntityData graphEntityData)
        {
            Id = graphEntityData.Id;
            _for = graphEntityData._for;
            _name = graphEntityData._name;
            _type = graphEntityData._type;
            _defaultValue = graphEntityData._defaultValue;
        }

        public virtual void Serialize(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("key");
            xmlWriter.WriteAttributeString("id", Id);
            xmlWriter.WriteAttributeString("for", _for.Serialize());
            xmlWriter.WriteAttributeString("attr.name", _name);
            xmlWriter.WriteAttributeString("attr.type", _type.Serialize());

            if (!string.IsNullOrWhiteSpace(_defaultValue))
            {
                xmlWriter.WriteStartElement("default");
                xmlWriter.WriteString(_defaultValue);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }
    }
}
