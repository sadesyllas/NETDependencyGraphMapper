using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml;

namespace NETDependencyGraphMapper.Models.yEd
{
    // ReSharper disable once InconsistentNaming
    public sealed class yEdEdgeGraphicsData : yEdGraphEntityData
    {
        private readonly yEdEdgeGraphicsConfiguration _configuration;

        public yEdEdgeGraphicsData([NotNull] yEdGraphEntityData graphEntityData,
            [NotNull] yEdEdgeGraphicsConfiguration configuration)
            : base(graphEntityData)
        {
            _configuration = configuration;
        }

        public override void Serialize(XmlWriter xmlWriter)
        {
            // start data element
            xmlWriter.WriteStartElement("data");
            xmlWriter.WriteAttributeString("key", Id);

            // write edge graphics data

            const string prefix = "y";

            //// start <y:PolyLineEdge>
            xmlWriter.WriteStartElement(prefix, "PolyLineEdge", null);

            ////// <y:LineStyle color="#FF0000" type="line" width="1.0"/>
            xmlWriter.WriteStartElement(prefix, "LineStyle", null);
            xmlWriter.WriteAttributeString("color", _configuration.Color.SerializeToHex());
            xmlWriter.WriteAttributeString("type", "line");
            xmlWriter.WriteAttributeString("width", _configuration.Width.ToString(CultureInfo.InvariantCulture));
            xmlWriter.WriteEndElement();

            //// end <y:PolyLineEdge>
            xmlWriter.WriteEndElement();

            // end data element
            xmlWriter.WriteEndElement();
        }
    }
}
