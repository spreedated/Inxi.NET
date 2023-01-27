using InxiFrontend.Base;
using Claunia.PropertyList;
using Extensification.External.Newtonsoft.Json.JPropertyExts;
using Newtonsoft.Json.Linq;
using System;
using System.Management;

namespace InxiFrontend
{

    class MachineParser : HardwareParserBase
    {

        /// <summary>
        /// Parses machine info
        /// </summary>
        /// <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
        /// <param name="SystemProfilerToken">system_profiler token</param>
        public override IHardware Parse(JToken InxiToken, NSArray SystemProfilerToken)
        {
            IHardware MachInfo;

            if (InxiInternalUtils.IsUnix())
            {
                MachInfo = ParseLinux(InxiToken);
            }
            else
            {
                InxiTrace.Debug("Selecting entries from Win32_ComputerSystem...");
                var WMIMachine = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
                MachInfo = ParseWindows(WMIMachine);
            }

            return MachInfo;
        }

        public override IHardware ParseLinux(JToken InxiToken)
        {
            var MachInfo = default(IHardware);

            InxiTrace.Debug("Selecting the Machine token...");
            foreach (var InxiSys in InxiToken.SelectTokenKeyEndingWith("Machine"))
            {
                // Get information of system
                string Type = (string)InxiSys.SelectTokenKeyEndingWith("Type");
                string Product = (string)InxiSys.SelectTokenKeyEndingWith("product");
                string System = (string)InxiSys.SelectTokenKeyEndingWith("System");
                string Chassis = (string)InxiSys.SelectTokenKeyEndingWith("Chassis");
                string MoboManufacturer = (string)InxiSys.SelectTokenKeyEndingWith("Mobo");
                string MoboModel = (string)InxiSys.SelectTokenKeyEndingWith("model");
                InxiTrace.Debug("Got information. Type: {0}, Product: {1}, System: {2}, Chassis: {3}, MoboManufacturer: {4}, MoboModel: {5}", Type, Product, System, Chassis, MoboManufacturer, MoboModel);

                // Create an instance of system class
                MachInfo = new MachineInfo(Type, Product, System, Chassis, MoboManufacturer, MoboModel);
            }

            return MachInfo;
        }

        public override IHardware ParseWindows(ManagementObjectSearcher WMISearcher)
        {
            IHardware MachInfo;
            var WMIMachine = WMISearcher;

            InxiTrace.Debug("Selecting entries from Win32_BaseBoard...");
            var WMIBoard = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            InxiTrace.Debug("Selecting entries from Win32_OperatingSystem...");
            var WMISystem = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");

            // Get information of system and motherboard
            string Type = "";
            string Product = "";
            string System = "";
            string Chassis = "";
            string MoboModel = "";
            string MoboManufacturer = "";

            // Get information for ChassisSKUNumber
            InxiTrace.Debug("Getting the base objects...");
            foreach (ManagementBaseObject WMISystemBase in WMISystem.Get())
            {
                if (Convert.ToString(WMISystemBase["Version"]).StartsWith("10") && Environment.OSVersion.Platform == PlatformID.Win32NT) // If running on Windows 10
                {
                    InxiTrace.Debug("Target is running Windows 10/11.");
                    foreach (ManagementBaseObject MachineBase in WMIMachine.Get())
                        Type = (string)MachineBase["ChassisSKUNumber"];
                }
            }

            // TODO: Chassis not implemented in Windows
            // Get informaiton for machine model and family
            InxiTrace.Debug("Getting the base objects...");
            InxiTrace.Debug("TODO: Chassis not implemented in Windows");
            foreach (ManagementBaseObject MachineBase in WMIMachine.Get())
            {
                Product = (string)MachineBase["Model"];
                System = (string)MachineBase["SystemFamily"];
            }

            // Get information for model and manufacturer
            InxiTrace.Debug("Getting the base objects...");
            foreach (ManagementBaseObject MoboBase in WMIBoard.Get())
            {
                MoboModel = (string)MoboBase["Model"];
                MoboManufacturer = (string)MoboBase["Manufacturer"];
            }
            InxiTrace.Debug("Got information. Type: {0}, Product: {1}, System: {2}, Chassis: {3}, MoboManufacturer: {4}, MoboModel: {5}", Type, Product, System, Chassis, MoboManufacturer, MoboModel);

            // Create an instance of system class
            MachInfo = new MachineInfo(Type, Product, System, Chassis, MoboManufacturer, MoboModel);
            return MachInfo;
        }

    }
}