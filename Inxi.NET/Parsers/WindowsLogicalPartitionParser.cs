
// Inxi.NET  Copyright (C) 2020-2021  Aptivi
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

using System;
using System.Collections.Generic;
using System.Management;

namespace InxiFrontend
{

    static class WindowsLogicalPartitionParser
    {

        public static Dictionary<string, WindowsLogicalPartition> ParsePartitions(ManagementObjectSearcher WMIObject)
        {
            var DriveParts = new Dictionary<string, WindowsLogicalPartition>();
            WindowsLogicalPartition DrivePart;

            // Get information of logical partitions
            InxiTrace.Debug("Getting the base objects...");
            foreach (ManagementBaseObject Part in WMIObject.Get())
            {
                try
                {
                    // Get information of a logical partition
                    string LogicalID = (string)Part["DeviceID"];
                    string LogicalName = (string)Part["VolumeName"];
                    string LogicalFileSystem = (string)Part["FileSystem"];
                    string LogicalSize = Convert.ToString(Part["Size"]);
                    string LogicalUsed = Convert.ToString((ulong?)Part["Size"] - (ulong?)Part["FreeSpace"]);
                    InxiTrace.Debug("Got information. LogicalID: {0}, LogicalFileSystem: {1}, LogicalSize: {2}, LogicalUsed: {3}", LogicalID, LogicalFileSystem, LogicalSize, LogicalUsed);

                    // Create an instance of logical partition class
                    DrivePart = new WindowsLogicalPartition(LogicalID, LogicalName, LogicalFileSystem, LogicalSize, LogicalUsed);
                    DriveParts.Add("Logical partition " + LogicalID, DrivePart);
                }
                catch (Exception ex)
                {
                    InxiTrace.Debug("Error: {0}", ex.Message);
                }
            }

            return DriveParts;
        }

    }
}