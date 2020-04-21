using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using NETDependencyGraphMapper.Abstractions;

namespace NETDependencyGraphMapper.Models
{
    public sealed class Graph : IGraphSerializable
    {
        private readonly string _id;
        private readonly IEnumerable<IGraphSerializable> _nodes;
        private readonly IEnumerable<IGraphSerializable> _edges;
        private readonly IEnumerable<IGraphSerializable> _attributes;

        public Graph(string id, [NotNull] IEnumerable<IGraphSerializable> nodes,
            [NotNull] IEnumerable<IGraphSerializable> edges, IEnumerable<IGraphSerializable>? attributes = null)
        {
            _id = id;
            _nodes = nodes;
            _edges = edges;
            _attributes = attributes ?? new IGraphSerializable[0];
        }

        public void Serialize([NotNull] XmlWriter xmlWriter)
        {
            // start graph
            xmlWriter.WriteStartElement("graph");
            xmlWriter.WriteAttributeString("id", _id);
            xmlWriter.WriteAttributeString("edgedefault", "directed");

            // write attributes
            foreach (var attribute in _attributes)
            {
                attribute.Serialize(xmlWriter);
            }

            // write nodes
            foreach (var node in _nodes)
            {
                node.Serialize(xmlWriter);
            }

            // write edges
            foreach (var edge in _edges)
            {
                edge.Serialize(xmlWriter);
            }

            // end graph
            xmlWriter.WriteEndElement();
        }
    }
}
