using System;
using Microsoft.VisualBasic.CompilerServices;

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

using UnameNET;

namespace InxiFrontend
{

    static class InxiInternalUtils
    {

        /// <summary>
    /// Is the platform Unix?
    /// </summary>
        internal static object IsUnix()
        {
            return Environment.OSVersion.Platform == PlatformID.Unix;
        }

        /// <summary>
    /// Is the Unix platform macOS?
    /// </summary>
        internal static object IsMacOS()
        {
            if (Conversions.ToBoolean(IsUnix()))
            {
                string System = UnameManager.GetUname(UnameTypes.KernelName);
                InxiTrace.Debug("Searching {0} for \"Darwin\"...", System.Replace(Environment.NewLine, ""));
                return System.Contains("Darwin");
            }
            else
            {
                return false;
            }
        }

    }
}