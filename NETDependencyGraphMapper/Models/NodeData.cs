using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace NETDependencyGraphMapper.Models
{
    public sealed class NodeData : GraphEntityData
    {
        private readonly string _value;

        public NodeData([NotNull] GraphEntityData graphEntityData, [NotNull] string value)
            : base(graphEntityData)
        {
            _value = value;
        }

        public override void Serialize(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("data");
            xmlWriter.WriteAttributeString("key", Id);
            xmlWriter.WriteString(_value);
            xmlWriter.WriteEndElement();
        }
    }
}
