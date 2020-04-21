using NETDependencyGraphMapper.Models;

namespace NETDependencyGraphMapper.Abstractions
{
    public interface IProjectParser
    {
        Project Parse(string path, bool parseReferredProjects, bool skipTestProjects = true);
    }
}
