using NETDependencyGraphMapper.Abstractions;

namespace NETDependencyGraphMapper.Models
{
    public sealed class Library : BaseGraphEntityWithName
    {
        public Library(string name, string version) : base(name)
        {
            Version = version;
        }

        public override string GraphId => $"Library::{base.Name}::{Version}";

        public override string Name => $"{base.Name}\nVersion: {Version}";

        public string Version { get; }
    }
}
