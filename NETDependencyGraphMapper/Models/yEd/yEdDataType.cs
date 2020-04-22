namespace NETDependencyGraphMapper.Models.yEd
{
    // ReSharper disable once InconsistentNaming
    public enum yEdDataType : byte
    {
        NodeGraphics,
        EdgeGraphics
    }

    // ReSharper disable once InconsistentNaming
    public static class yEdAttributeTypeExtensions
    {
        public static string Serialize(this yEdDataType yEdDataType) =>
            yEdDataType.ToString().ToLowerInvariant();
    }
}
