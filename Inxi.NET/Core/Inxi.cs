using System;

// Inxi.NET  Copyright (C) 2020-2021  EoflaOE
// 
// This file is part of Inxi.NET
// 
// Inxi.NET is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Inxi.NET is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.IO;
using System.Linq;
using static System.Reflection.Assembly;
using Microsoft.VisualBasic.CompilerServices;

namespace InxiFrontend
{

    public class Inxi
    {

        /// <summary>
    /// Hardware information
    /// </summary>
        public readonly HardwareInfo Hardware;

        /// <summary>
    /// Intializes the new instance of Inxi class and parses hardware
    /// </summary>
        public Inxi() : this("/usr/bin/inxi", "/usr/bin/cpanel_json_xs", "/usr/bin/json_xs", (InxiHardwareType)Enum.GetValues(typeof(InxiHardwareType)).Cast<int>().Sum())
        {
        }

        /// <summary>
    /// Intializes the new instance of Inxi class and parses hardware
    /// </summary>
    /// <param name="HardwareTypes">Hardware types to parse</param>
        public Inxi(InxiHardwareType HardwareTypes) : this("/usr/bin/inxi", "/usr/bin/cpanel_json_xs", "/usr/bin/json_xs", HardwareTypes)
        {
        }

        /// <summary>
    /// Initializes the new instance of Inxi class with specified path and parses hardware
    /// </summary>
    /// <param name="InxiPath">Path to Inxi executable. It's usually /usr/bin/inxi. Ignored in Windows.</param>
    /// <param name="CpanelJsonXsPath">Path to CPanelJsonXS executable. It's usually /usr/bin/cpanel_json_xs. Ignored in Windows.</param>
    /// <param name="JsonXsPath">Path to JsonXS executable. It's usually /usr/bin/json_xs. Ignored in Windows.</param>
    /// <param name="HardwareTypes">Hardware types to parse</param>
        public Inxi(string InxiPath, string CpanelJsonXsPath, string JsonXsPath, InxiHardwareType HardwareTypes)
        {
            string FrontendVersion = GetExecutingAssembly().GetName().Version.ToString();
            if (Conversions.ToBoolean(InxiInternalUtils.IsUnix()))
            {
                InxiTrace.Debug("Inxi.NET {0} running on Unix.", FrontendVersion);
                InxiTrace.Debug("Inxi parse flags: {0}", HardwareTypes);

                // Check to see if we're on macOS or on regular Unix
                if (Conversions.ToBoolean(InxiInternalUtils.IsMacOS()))
                {
                    // Use System Profiler to get hardware information
                    InxiTrace.Debug("Type: macOS");
                    Hardware = new HardwareInfo(InxiPath, HardwareTypes);
                }
                else
                {
                    // Use Inxi to get hardware information
                    InxiTrace.Debug("Type: Unix");
                    InxiTrace.Debug("Looking for Inxi executable at {0}...", InxiPath);
                    if (File.Exists(InxiPath))
                    {
                        InxiTrace.Debug("Looking for Json XS perl module binary at {0} or {1}...", CpanelJsonXsPath, JsonXsPath);
                        if (File.Exists(CpanelJsonXsPath) | File.Exists(JsonXsPath))
                        {
                            InxiTrace.Debug("Found Json XS perl module!");
                            Hardware = new HardwareInfo(InxiPath, HardwareTypes);
                        }
                        else
                        {
                            InxiTrace.Debug("Json XS perl module is not installed.");
                            throw new InvalidOperationException("You must have libcpanel-json-xs-perl or libjson-xs-perl installed. (Could not find \"" + CpanelJsonXsPath + "\" or \"" + JsonXsPath + "\".)");
                        }
                    }
                    else
                    {
                        InxiTrace.Debug("Inxi is not installed.");
                        throw new InvalidOperationException("You must have Inxi installed. (Could not find \"" + InxiPath + "\".)");
                    }
                }
            }
            else
            {
                InxiTrace.Debug("Inxi.NET {0} running on Windows.", FrontendVersion);
                Hardware = new HardwareInfo(InxiPath, HardwareTypes);
            }
        }

    }
}