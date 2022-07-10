using System;
using System.Collections.Generic;

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

using System.Management;
using Claunia.PropertyList;
using Newtonsoft.Json.Linq;

namespace InxiFrontend
{

    public abstract class HardwareParserBase : IHardwareParser
    {

        public virtual HardwareBase Parse(JToken InxiToken, NSArray SystemProfilerToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet!");
        }

        public virtual Dictionary<string, HardwareBase> ParseAll(JToken InxiToken, NSArray SystemProfilerToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet!");
        }

        public virtual List<HardwareBase> ParseAllToList(JToken InxiToken, NSArray SystemProfilerToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet!");
        }

        /// <summary>
    /// The base Linux hardware parser
    /// </summary>
        public virtual HardwareBase ParseLinux(JToken InxiToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Linux!");
        }

        /// <summary>
    /// The base macOS hardware parser
    /// </summary>
        public virtual HardwareBase ParseMacOS(NSArray SystemProfilerToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on macOS!");
        }

        /// <summary>
    /// The base Windows hardware parser
    /// </summary>
        public virtual HardwareBase ParseWindows(ManagementObjectSearcher WMISearcher)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Windows!");
        }

        /// <summary>
    /// The base Linux hardware parser
    /// </summary>
        public virtual Dictionary<string, HardwareBase> ParseAllLinux(JToken InxiToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Linux!");
        }

        /// <summary>
    /// The base macOS hardware parser
    /// </summary>
        public virtual Dictionary<string, HardwareBase> ParseAllMacOS(NSArray SystemProfilerToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on macOS!");
        }

        /// <summary>
    /// The base Windows hardware parser
    /// </summary>
        public virtual Dictionary<string, HardwareBase> ParseAllWindows(ManagementObjectSearcher WMISearcher)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Windows!");
        }

        /// <summary>
    /// The base Linux hardware parser
    /// </summary>
        public virtual List<HardwareBase> ParseAllToListLinux(JToken InxiToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Linux!");
        }

        /// <summary>
    /// The base macOS hardware parser
    /// </summary>
        public virtual List<HardwareBase> ParseAllToListMacOS(NSArray SystemProfilerToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on macOS!");
        }

        /// <summary>
    /// The base Windows hardware parser
    /// </summary>
        public virtual List<HardwareBase> ParseAllToListWindows(ManagementObjectSearcher WMISearcher)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Windows!");
        }

    }
}