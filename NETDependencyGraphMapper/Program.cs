using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NETDependencyGraphMapper.Models;
using NETDependencyGraphMapper.Models.yEd;
using NETDependencyGraphMapper.Services;

namespace NETDependencyGraphMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var solutionPathsOption = new Option("-s",
                "The path to a solution file (this option can be repeated to pass multiple paths to solution files.")
            {
                Required = true,
                Argument = new Argument<FileInfo[]>("solution paths")
                {
                    Arity = new ArgumentArity(1, int.MaxValue)
                }.ExistingOnly()
            };

            var outputPathOption = new Option("-o", "The path to the output GraphML file.")
            {
                Required = true,
                Argument = new Argument<FileInfo>("output path").LegalFilePathsOnly()
            };

            var rootCommand = new RootCommand {solutionPathsOption, outputPathOption};

            rootCommand.TreatUnmatchedTokensAsErrors = true;

            rootCommand.Description =
                "Produces a GraphML file, depicting a graph of dependencies, given a group of paths to solution files.";

            // ReSharper disable once ConvertClosureToMethodGroup
            rootCommand.Handler = CommandHandler.Create<FileInfo[], FileInfo>((s, o) => { Run(s, o); });

            rootCommand.Invoke(args);
        }

        /// <summary>
        /// Produces a GraphML file, depicting a graph of dependencies, given a group of paths to solution files.
        /// </summary>
        /// <param name="solutionPaths">
        /// Paths to solution files to read dependencies from.
        /// </param>
        /// <param name="outputPath">
        /// The path where to save the GraphML contents of the resulting graph.
        /// </param>
        static void Run([NotNull] FileInfo[] solutionPaths, [NotNull] FileInfo outputPath)
        {
            var solutionParser = new SolutionParser(new ProjectParser());
            var solutions = new[] {solutionParser.Parse(solutionPaths.First().FullName)};
            var nodes = new HashSet<Node>();
            var edges = new HashSet<Edge>();
            // var nameAttribute = new GraphEntityData("name", ElementType.Node, "name", DataType.String);
            //
            // var colorAttribute = new GraphEntityData("color", ElementType.Node, "color", DataType.String,
            //     NodeColor.Blue.Serialize());

            var nodeGraphicsAttribute = new yEdGraphEntityData("color", ElementType.Node, yEdDataType.NodeGraphics);

            foreach (var solution in solutions)
            {
                var solutionNodeAttributes = new[]
                {
                    // new NodeData(nameAttribute, project.Name),
                    // new NodeData(colorAttribute, NodeColor.Green.Serialize())
                    new yEdNodeGraphicsData(nodeGraphicsAttribute, new yEdNodeGraphicsConfiguration
                    {
                        Color = NodeColor.LawnGreen,
                        Label = solution.Name
                    }),
                };

                nodes.Add(new Node(solution, solutionNodeAttributes));

                foreach (var project in solution.Projects)
                {
                    var projectNodeAttributes = new[]
                    {
                        // new NodeData(nameAttribute, project.Name),
                        // new NodeData(colorAttribute, NodeColor.Green.Serialize())
                        new yEdNodeGraphicsData(nodeGraphicsAttribute, new yEdNodeGraphicsConfiguration
                        {
                            Color = NodeColor.DeepSkyBlue,
                            Label = project.Name
                        }),
                    };

                    nodes.Add(new Node(project, projectNodeAttributes));
                    edges.Add(new Edge(solution, project));

                    foreach (var subProject in project.ReferencedProjects)
                    {
                        var subProjectNodeAttributes = new[]
                        {
                            // new NodeData(nameAttribute, project.Name),
                            // new NodeData(colorAttribute, NodeColor.Green.Serialize())
                            new yEdNodeGraphicsData(nodeGraphicsAttribute, new yEdNodeGraphicsConfiguration
                            {
                                Color = NodeColor.DeepSkyBlue,
                                Label = subProject.Name
                            }),
                        };

                        nodes.Add(new Node(subProject, subProjectNodeAttributes));
                        edges.Add(new Edge(project, subProject));
                    }

                    foreach (var library in project.ReferencedLibraries)
                    {
                        var libraryNodeAttributes = new[]
                        {
                            // new NodeData(nameAttribute, project.Name),
                            // new NodeData(colorAttribute, NodeColor.Green.Serialize())
                            new yEdNodeGraphicsData(nodeGraphicsAttribute, new yEdNodeGraphicsConfiguration
                            {
                                Color = NodeColor.Fuchsia,
                                Label = library.Name
                            }),
                        };

                        nodes.Add(new Node(library, libraryNodeAttributes));
                        edges.Add(new Edge(project, library));
                    }
                }
            }

            var graphs = solutions.Select(solution => new Graph(solution.Name, nodes, edges));
            // var graphEnvelope = new GraphEnvelope(graphs, new[] {nameAttribute, colorAttribute});
            var graphEnvelope = new GraphEnvelope(graphs, new[] {nodeGraphicsAttribute});

            using var memoryStream = new MemoryStream();
            using var xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                NewLineChars = "\n",
            });

            graphEnvelope.Serialize(xmlWriter);

            var xml = Encoding.UTF8.GetString(memoryStream.ToArray());

            File.WriteAllText(outputPath.FullName, xml + "\n");
        }
    }
}
