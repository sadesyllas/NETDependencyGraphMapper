#pragma warning disable 659

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using NETDependencyGraphMapper.Abstractions;

namespace NETDependencyGraphMapper.Models
{
    public sealed class Node : BaseGraphSerializable
    {
        private readonly IGraphEntity _graphEntity;
        private readonly IEnumerable<IGraphSerializable> _attributes;

        public Node([NotNull] IGraphEntity graphEntity, IEnumerable<IGraphSerializable>? attributes = null)
            : base(graphEntity.GraphId)
        {
            _graphEntity = graphEntity;
            _attributes = attributes ?? new IGraphSerializable[0];
        }

        public override void Serialize(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("node");
            xmlWriter.WriteAttributeString("id", _graphEntity.GraphId);

            // write attributes
            foreach (var attribute in _attributes)
            {
                attribute.Serialize(xmlWriter);
            }

            xmlWriter.WriteEndElement();
        }

        public override bool Equals(object? other) => (other as Node)?._graphEntity.GraphId == _graphEntity.GraphId;
    }
}
