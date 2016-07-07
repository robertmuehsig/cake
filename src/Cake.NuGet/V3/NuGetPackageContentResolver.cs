// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if NETCORE
using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Core.Packaging;
using Cake.Core.Diagnostics;
using NuGet.Frameworks;
using Cake.Core;
using System;
using System.Linq;

namespace Cake.NuGet.V3
{
    /// <summary>
    /// Implementation of a file locator for NuGet packages that
    /// returns relevant files for the current framework given a resource type.
    /// </summary>
    public sealed class NuGetPackageContentResolver : INuGetPackageContentResolver
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;
        private readonly ICakeLog _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="NuGetPackageContentResolver"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="locator">The locator.</param>
        public NuGetPackageContentResolver(IFileSystem fileSystem, ICakeEnvironment environment, ICakeLog log)
        {
            _fileSystem = fileSystem;
            _environment = environment;
            _log = log;
        }

        /// <summary>
        /// Gets the relevant files for a NuGet package
        /// given a path and a resource type.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <param name="type">The resource type.</param>
        /// <returns>A collection of files.</returns>
        public IReadOnlyCollection<IFile> GetFiles(DirectoryPath path, PackageType type)
        {
            if (type == PackageType.Addin)
            {
                return GetAssemblies(path);
            }
            if (type == PackageType.Tool)
            {
                var result = new List<IFile>();
                var toolDirectory = _fileSystem.GetDirectory(path);
                if (toolDirectory.Exists)
                {
                    var files = toolDirectory.GetFiles("*.exe", SearchScope.Recursive);
                    result.AddRange(files);
                }
                return result;
            }
            throw new InvalidOperationException("Unknown package type.");
        }

        private IReadOnlyCollection<IFile> GetAssemblies(DirectoryPath path)
        {
            // Get current framework.
            var provider = DefaultFrameworkNameProvider.Instance;
            var current = NuGetFramework.Parse(_environment.GetTargetFramework().FullName, provider);

            // Get all candidate files.
            var assemblies = _fileSystem.GetDirectory(path).GetFiles("*.dll", SearchScope.Recursive);

            // Iterate all found files.
            var comparer = new NuGetFrameworkFullComparer();
            var mapping = new Dictionary<NuGetFramework, List<FilePath>>(comparer);
            foreach (var assembly in assemblies)
            {
                // Get relative path.
                var relative = path.GetRelativePath(assembly.Path);
                var framework = ParseFromDirectoryPath(current, relative.GetDirectory());
                if (!mapping.ContainsKey(framework))
                {
                    mapping.Add(framework, new List<FilePath>());
                }
                mapping[framework].Add(assembly.Path);
            }

            // Reduce found frameworks to the closest one.
            var reducer = new FrameworkReducer();
            var nearest = reducer.GetNearest(current, mapping.Keys);

            // Return the result.
            return mapping[nearest].Select(p => _fileSystem.GetFile(p)).ToList();
        }

        private NuGetFramework ParseFromDirectoryPath(NuGetFramework current, DirectoryPath path)
        {
            var provider = new DefaultFrameworkNameProvider();
            var queue = new Queue<string>(path.Segments);
            while (queue.Count > 0)
            {
                var other = NuGetFramework.Parse(queue.Dequeue(), DefaultFrameworkNameProvider.Instance);
                var compatible = DefaultCompatibilityProvider.Instance.IsCompatible(other, current);
                if (compatible || queue.Count == 0)
                {
                    return other;
                }
            }
            throw new InvalidOperationException("Something went wrong when parsing framework.");
        }
    }
}
#endif