// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
#if NETCORE
using System.Runtime.InteropServices;
#endif

using Cake.Core.Polyfill;

namespace Cake.Core.IO
{
    /// <summary>
    /// Responsible for retrieving information about the current machine.
    /// </summary>
    internal static class Machine
    {
        /// <summary>
        /// Determines if the current operative system is 64 bit.
        /// </summary>
        /// <returns>Whether or not the current operative system is 64 bit.</returns>
        public static bool Is64BitOperativeSystem()
        {
            return EnvironmentHelper.Is64BitOperativeSystem();
        }

        /// <summary>
        /// Determines whether the current machine is running Unix.
        /// </summary>
        /// <returns>Whether or not the current machine is running Unix.</returns>
        public static bool IsUnix()
        {
            return EnvironmentHelper.IsUnix();
        }
    }
}
