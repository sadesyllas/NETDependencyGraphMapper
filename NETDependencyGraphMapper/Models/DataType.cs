namespace NETDependencyGraphMapper.Models
{
    public enum DataType : byte
    {
        Boolean,
        Int,
        Long,
        Float,
        Double,
        String
    }

    public static class AttributeTypeExtensions
    {
        public static string Serialize(this DataType dataType) => dataType.ToString().ToLowerInvariant();
    }
}
