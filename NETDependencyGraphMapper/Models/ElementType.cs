namespace NETDependencyGraphMapper.Models
{
    public enum ElementType : byte
    {
        Node,
        Edge
    }

    public static class ElementTypeExtensions
    {
        public static string Serialize(this ElementType elementType) => elementType.ToString().ToLowerInvariant();
    }
}
