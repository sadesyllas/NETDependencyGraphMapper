namespace NETDependencyGraphMapper.Abstractions
{
    public abstract class BaseGraphEntityWithName : BaseGraphEntity
    {
        protected BaseGraphEntityWithName(string nameOrPath) : base(nameOrPath)
        {
        }

        public override string Name => NameOrPath;

        public override string Description => Name;
    }
}
