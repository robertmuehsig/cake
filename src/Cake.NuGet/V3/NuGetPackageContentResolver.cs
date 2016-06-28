// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if NETCORE
using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Core.Packaging;
using Cake.Core.Diagnostics;

namespace Cake.NuGet.V3
{
    /// <summary>
    /// Implementation of a file locator for NuGet packages that
    /// returns relevant files for the current framework given a resource type.
    /// </summary>
    public sealed class NuGetPackageContentResolver : INuGetPackageContentResolver
    {
        private readonly ICakeLog _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="NuGetPackageContentResolver"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="locator">The locator.</param>
        public NuGetPackageContentResolver(ICakeLog log)
        {
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
            return new List<IFile>();
        }
    }
}
#endif