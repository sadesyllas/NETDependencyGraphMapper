using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using NETDependencyGraphMapper.Abstractions;

namespace NETDependencyGraphMapper.Models
{
    public sealed class GraphEnvelope : IGraphSerializable
    {
        private readonly IEnumerable<IGraphSerializable> _graphs;
        private readonly IEnumerable<IGraphSerializable> _attributes;

        public GraphEnvelope([NotNull] IEnumerable<IGraphSerializable> graphs,
            IEnumerable<IGraphSerializable>? attributes = null)
        {
            _graphs = graphs;
            _attributes = attributes ?? new IGraphSerializable[0];
        }

        public void Serialize([NotNull] XmlWriter xmlWriter)
        {
            // xml header
            xmlWriter.WriteStartDocument();

            // start root
            xmlWriter.WriteStartElement("graphml", "http://graphml.graphdrawing.org/xmlns");
            xmlWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");

            #region Not yEd

            // xmlWriter.WriteAttributeString("xsi", "schemaLocation", null,
            //     "http://graphml.graphdrawing.org/xmlns http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd");

            #endregion Not yEd

            #region yEd

            xmlWriter.WriteAttributeString("xsi", "schemaLocation", null,
                "http://graphml.graphdrawing.org/xmlns http://www.yworks.com/xml/schema/graphml/1.1/ygraphml.xsd");

            xmlWriter.WriteAttributeString("xmlns", "java", null, "http://www.yworks.com/xml/yfiles-common/1.0/java");

            xmlWriter.WriteAttributeString("xmlns", "sys", null,
                "http://www.yworks.com/xml/yfiles-common/markup/primitives/2.0");

            xmlWriter.WriteAttributeString("xmlns", "x", null, "http://www.yworks.com/xml/yfiles-common/markup/2.0");
            xmlWriter.WriteAttributeString("xmlns", "y", null, "http://www.yworks.com/xml/graphml");
            xmlWriter.WriteAttributeString("xmlns", "yed", null, "http://www.yworks.com/xml/yed/3");

            #endregion yEd

            // write attributes
            foreach (var attribute in _attributes)
            {
                attribute.Serialize(xmlWriter);
            }

            // write graphs
            foreach (var graph in _graphs)
            {
                graph.Serialize(xmlWriter);
            }

            // end root
            xmlWriter.WriteEndElement();

            xmlWriter.Flush();
        }
    }
}
