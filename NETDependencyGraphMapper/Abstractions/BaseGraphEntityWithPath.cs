using System.IO;

namespace NETDependencyGraphMapper.Abstractions
{
    public abstract class BaseGraphEntityWithPath : BaseGraphEntity
    {
        protected BaseGraphEntityWithPath(string nameOrPath) : base(nameOrPath)
        {
        }

        public override string Name => Path.GetFileNameWithoutExtension(NameOrPath);
    }
}
