#pragma warning disable 659

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using NETDependencyGraphMapper.Abstractions;

namespace NETDependencyGraphMapper.Models
{
    public sealed class Edge : BaseGraphSerializable
    {
        private readonly IGraphEntity _sourceGraphEntity;
        private readonly IGraphEntity _targetGraphEntity;
        private readonly IEnumerable<IGraphSerializable> _attributes;

        public Edge([NotNull] IGraphEntity sourceGraphEntity, [NotNull] IGraphEntity targetGraphEntity,
            IEnumerable<IGraphSerializable>? attributes = null)
            : base($"{sourceGraphEntity.GraphId}::{targetGraphEntity.GraphId}")
        {
            _sourceGraphEntity = sourceGraphEntity;
            _targetGraphEntity = targetGraphEntity;
            _attributes = attributes ?? new IGraphSerializable[0];
        }

        public override void Serialize(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("edge");
            xmlWriter.WriteAttributeString("source", _sourceGraphEntity.GraphId);
            xmlWriter.WriteAttributeString("target", _targetGraphEntity.GraphId);

            // write attributes
            foreach (var attribute in _attributes)
            {
                attribute.Serialize(xmlWriter);
            }

            xmlWriter.WriteEndElement();
        }

        public override bool Equals(object? other)
        {
            var otherEdge = other as Edge;

            Debug.Assert(otherEdge != null);

            return otherEdge._sourceGraphEntity.GraphId == _sourceGraphEntity.GraphId &&
                otherEdge._targetGraphEntity.GraphId == _targetGraphEntity.GraphId;
        }
    }
}
