using System.Diagnostics;
using System.Reflection;
using NETDependencyGraphMapper.Abstractions;

namespace NETDependencyGraphMapper.Models
{
    public enum NodeColor : byte
    {
        [HexColor("#00BFFF")]
        DeepSkyBlue,

        [HexColor("#7CFC00")]
        LawnGreen,

        [HexColor("#FFFF00")]
        Yellow,

        [HexColor("#ff5733")]
        Red,

        [HexColor("#FF00FF")]
        Fuchsia
    }

    public static class NodeColorExtensions
    {
        public static string Serialize(this NodeColor nodeColor) => nodeColor.ToString().ToLowerInvariant();

        public static string SerializeToHex(this NodeColor nodeColor)
        {
            var colorField = nodeColor.GetType().GetField(nodeColor.ToString());

            Debug.Assert(colorField != null);

            var hexColorAttribute = colorField.GetCustomAttribute(typeof(HexColor));

            Debug.Assert(hexColorAttribute != null);

            return ((HexColor) hexColorAttribute).Value;
        }
    }
}
