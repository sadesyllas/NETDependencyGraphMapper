using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NETDependencyGraphMapper.Abstractions;
using NETDependencyGraphMapper.Models;

namespace NETDependencyGraphMapper.Services
{
    public sealed class ProjectParser : IProjectParser
    {
        private static readonly Regex TestProjectRe =
            new Regex("[a-z.]Test[a-zA-Z]*(?:\\s*/|.{1,2}proj|$)", RegexOptions.Compiled);

        private static readonly Regex ProjectProjectRe =
            new Regex("<ProjectReference\\s+Include=\"([^\"]+..{1,2}proj)\">", RegexOptions.Compiled);

        private static readonly Regex ProjectLibraryRe =
            new Regex("<Reference\\s+Include=\"([^,/]+),\\s+Version=([0-9.]+),\\s+[^\"]+\">", RegexOptions.Compiled);

        public Project Parse(string path, bool parseReferredProjects, bool skipTestProjects = true)
        {
            path = Path.GetFullPath(path);

            var projectParser = new ProjectParser();
            var basePath = Path.GetDirectoryName(Path.GetFullPath(path));
            var projectData = File.ReadAllText(path);
            var projectPathMatches = ProjectProjectRe.Matches(projectData);
            var libraryMatches = ProjectLibraryRe.Matches(projectData);
            var projects = new HashSet<Project>();
            var libraries = new HashSet<Library>();

            foreach (Match? projectPathMatch in projectPathMatches)
            {
                Debug.Assert(projectPathMatch != null);

                var projectRelativePath = projectPathMatch.Groups[1].Value.Replace('\\', Path.DirectorySeparatorChar);
                var projectPath = Path.Join(basePath, projectRelativePath);

                if (!File.Exists(projectPath))
                {
                    throw new FileNotFoundException("The project file does not exist.", projectPath);
                }

                if (skipTestProjects && TestProjectRe.IsMatch(projectPath))
                {
                    continue;
                }

                projects.Add(projectParser.Parse(projectPath, false));
            }

            foreach (Match? libraryMatch in libraryMatches)
            {
                Debug.Assert(libraryMatch != null);

                var name = libraryMatch.Groups[1].Value;

                // Omit `System.*` references.
                if (name.StartsWith("System."))
                {
                    continue;
                }

                var version = libraryMatch.Groups[2].Value;

                libraries.Add(new Library(name, version));
            }

            return new Project(path, ImmutableHashSet.Create(projects.ToArray()),
                ImmutableHashSet.Create(libraries.ToArray()));
        }
    }
}
