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
    public sealed class SolutionParser : ISolutionParser
    {
        private static readonly Regex SolutionProjectRe = new Regex(
            "Project\\(\"\\{[0-9A-Z-]+\\}\"\\)\\s+=\\s+\"[^\"]+\",\\s+" +
            // Extract the project path and filter non-path (eg, solution directories) fragments from matching.
            "\"([^\"]+\\..{1,2}proj)\",\\s+" +
            "\"\\{[0-9A-Z-]+\\}\"");

        private static readonly Regex TestProjectRe =
            new Regex("[a-z.]Test[a-zA-Z]*(?:\\s*/|.{1,2}proj|$)", RegexOptions.Compiled);

        private readonly IProjectParser _projectParser;

        public SolutionParser(IProjectParser projectParser)
        {
            _projectParser = projectParser;
        }

        public Solution Parse(string path, bool skipTestProjects = true)
        {
            path = Path.GetFullPath(path);

            var basePath = Path.GetDirectoryName(Path.GetFullPath(path));
            var solutionData = File.ReadAllText(path);
            var projectPathMatches = SolutionProjectRe.Matches(solutionData);
            var projects = new HashSet<Project>();

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

                projects.Add(_projectParser.Parse(projectPath, true));
            }

            return new Solution(path, ImmutableHashSet.Create(projects.ToArray()));
        }
    }
}
