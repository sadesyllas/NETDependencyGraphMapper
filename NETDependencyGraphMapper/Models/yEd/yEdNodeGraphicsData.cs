using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace NETDependencyGraphMapper.Models.yEd
{
    // ReSharper disable once InconsistentNaming
    public sealed class yEdNodeGraphicsData : yEdGraphEntityData
    {
        private readonly yEdNodeGraphicsConfiguration _configuration;

        public yEdNodeGraphicsData([NotNull] yEdGraphEntityData graphEntityData,
            [NotNull] yEdNodeGraphicsConfiguration configuration)
            : base(graphEntityData)
        {
            _configuration = configuration;
        }

        public override void Serialize(XmlWriter xmlWriter)
        {
            // start data element
            xmlWriter.WriteStartElement("data");
            xmlWriter.WriteAttributeString("key", Id);

            // write node graphics data

            const string prefix = "y";

            //// start <y:ShapeNode>
            xmlWriter.WriteStartElement(prefix, "ShapeNode", null);

            ////// <y:Fill color="#FFCC00" transparent="false"/>
            xmlWriter.WriteStartElement(prefix, "Fill", null);
            xmlWriter.WriteAttributeString("color", _configuration.Color.SerializeToHex());
            xmlWriter.WriteAttributeString("transparent", "false");
            xmlWriter.WriteEndElement();

            ////// <y:NodeLabel alignment="center" autoSizePolicy="content" fontFamily="Dialog" fontSize="12" fontStyle="plain" hasBackgroundColor="false"
            //////   hasLineColor="false" height="17.96875" horizontalTextPosition="center" iconTextGap="4" modelName="custom" textColor="#000000"
            //////   verticalTextPosition="bottom" visible="true" width="35.734375" x="-2.8671875" y="6.015625">
            xmlWriter.WriteStartElement(prefix, "NodeLabel", null);
            xmlWriter.WriteAttributeString("alignment", "center");
            xmlWriter.WriteAttributeString("textColor", "#000000");
            xmlWriter.WriteAttributeString("autoSizePolicy", "content");
            xmlWriter.WriteAttributeString("verticalTextPosition", "center");
            xmlWriter.WriteString(_configuration.Label);
            xmlWriter.WriteEndElement();

            ////// <y:Shape type="hexagon"/>
            xmlWriter.WriteStartElement(prefix, "Shape", null);
            xmlWriter.WriteAttributeString("type", "ellipse");
            xmlWriter.WriteEndElement();

            //// end </y:ShapeNode>
            xmlWriter.WriteEndElement();

            // end data element
            xmlWriter.WriteEndElement();
        }
    }
}
