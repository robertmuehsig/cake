// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
#if NETCORE
using System.Runtime.InteropServices;
#endif

using Cake.Core.Polyfill;

namespace Cake.Core.IO
{
    internal static class Machine
    {
        public static bool Is64BitOperativeSystem()
        {
            return EnvironmentHelper.Is64BitOperativeSystem();
        }

        public static PlatformFamily GetPlatformFamily()
        {
            return EnvironmentHelper.GetPlatformFamily();
        }

        public static bool IsUnix()
        {
            return IsUnix(GetPlatformFamily());
        }

        public static bool IsUnix(PlatformFamily family)
        {
            return family == PlatformFamily.Linux
                || family == PlatformFamily.OSX;
        }
    }
}
