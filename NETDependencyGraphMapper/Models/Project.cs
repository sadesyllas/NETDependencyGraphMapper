using System.Collections.Immutable;
using NETDependencyGraphMapper.Abstractions;

namespace NETDependencyGraphMapper.Models
{
    public sealed class Project : BaseGraphEntityWithPath
    {
        public Project(string path, IImmutableSet<Project> referencedProjects,
            IImmutableSet<Library> referencedLibraries) : base(path)
        {
            ReferencedProjects = referencedProjects;
            ReferencedLibraries = referencedLibraries;
        }

        public override string GraphId => $"Project::{Name}";

        public IImmutableSet<Project> ReferencedProjects { get; }

        public IImmutableSet<Library> ReferencedLibraries { get; }
    }
}
