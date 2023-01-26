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