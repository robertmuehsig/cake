// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if NETCORE
using System.Collections.Generic;
using Cake.Core.Scripting;
using Cake.Core.Diagnostics;

namespace Cake.Scripting.XPlat
{
    public class XPlatScriptEngine : IScriptEngine
    {
        private readonly ICakeLog _log;

        public XPlatScriptEngine(ICakeLog log)
        {
            _log = log;
        }

        public IScriptSession CreateSession(IScriptHost host, IDictionary<string, string> arguments)
        {
            return new XPlatScriptSession(host, _log);
        }
    }
}
#endif