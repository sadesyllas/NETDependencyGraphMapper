namespace NETDependencyGraphMapper.Abstractions
{
    public abstract class BaseGraphEntity : IGraphEntity
    {
        private protected readonly string NameOrPath;

        protected BaseGraphEntity(string nameOrPath)
        {
            NameOrPath = nameOrPath;
        }

        public abstract string GraphId { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }
    }
}
