using System.Collections.Immutable;
using NETDependencyGraphMapper.Abstractions;

namespace NETDependencyGraphMapper.Models
{
    public sealed class Solution : BaseGraphEntityWithPath
    {
        public Solution(string path, IImmutableSet<Project> projects) : base(path)
        {
            Projects = projects;
        }

        public override string GraphId => $"Solution::{Name}";

        public IImmutableSet<Project> Projects { get; }
    }
}
