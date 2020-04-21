using NETDependencyGraphMapper.Models;

namespace NETDependencyGraphMapper.Abstractions
{
    public interface ISolutionParser
    {
        Solution Parse(string path, bool skipTestProjects = true);
    }
}
