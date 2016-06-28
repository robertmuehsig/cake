// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if NETCORE
using System.Runtime.InteropServices;
#else
using System;
#endif

namespace Cake.Core.Polyfill
{
    internal class EnvironmentHelper
    {
        public static bool Is64BitOperativeSystem()
        {
#if NETCORE
            return RuntimeInformation.OSArchitecture == Architecture.X64
                || RuntimeInformation.OSArchitecture == Architecture.Arm64;
#else
            return Environment.Is64BitOperatingSystem;
#endif
        }

        public static bool IsUnix()
        {
#if NETCORE
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                || RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
#else
            var platform = (int)Environment.OSVersion.Platform;
            return (platform == 4) || (platform == 6) || (platform == 128);
#endif
        }
    }
}
