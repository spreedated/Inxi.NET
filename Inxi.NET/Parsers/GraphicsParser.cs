using Claunia.PropertyList;
using Extensification.DictionaryExts;
using Extensification.External.Newtonsoft.Json.JPropertyExts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using InxiFrontend.Base;

namespace InxiFrontend
{

    class GraphicsParser : HardwareParserBase
    {

        /// <summary>
        /// Parses graphics cards
        /// </summary>
        /// <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
        /// <param name="SystemProfilerToken">system_profiler token</param>
        public override Dictionary<string, IHardware> ParseAll(JToken InxiToken, NSArray SystemProfilerToken)
        {
            Dictionary<string, IHardware> GPUParsed;

            if (InxiInternalUtils.IsUnix())
            {
                GPUParsed = ParseAllLinux(InxiToken);
            }
            else
            {
                InxiTrace.Debug("Selecting entries from Win32_VideoController...");
                var GraphicsCards = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                GPUParsed = ParseAllWindows(GraphicsCards);
            }

            return GPUParsed;
        }

        public override Dictionary<string, IHardware> ParseAllLinux(JToken InxiToken)
        {
            var GPUParsed = new Dictionary<string, IHardware>();
            Graphics GPU;

            // GPU information fields
            string GPUName;
            string GPUDriver;
            string GPUDriverVersion;
            string GPUBusID;
            string GPUChipID;

            InxiTrace.Debug("Selecting the Graphics token...");
            foreach (var InxiGPU in InxiToken.SelectTokenKeyEndingWith("Graphics").Where(x => x.SelectTokenKeyEndingWith("Device") != null))
            {
                // Get information of a graphics card
                GPUName = (string)InxiGPU.SelectTokenKeyEndingWith("Device");
                GPUDriver = (string)InxiGPU.SelectTokenKeyEndingWith("driver");
                GPUDriverVersion = (string)InxiGPU.SelectTokenKeyEndingWith("v");
                GPUChipID = (string)InxiGPU.SelectTokenKeyEndingWith("chip ID");
                GPUBusID = (string)InxiGPU.SelectTokenKeyEndingWith("bus ID");
                InxiTrace.Debug("Got information. GPUName: {0}, GPUDriver: {1}, GPUDriverVersion: {2}, GPUChipID: {3}, GPUBusID: {4}", GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID);

                // Create an instance of graphics class
                GPU = new Graphics(GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID);
                GPUParsed.Add(GPUName, GPU);
                InxiTrace.Debug("Added {0} to the list of parsed GPUs.", GPUName);
            }
            return GPUParsed;
        }

        public override Dictionary<string, IHardware> ParseAllWindows(ManagementObjectSearcher WMISearcher)
        {
            var GPUParsed = new Dictionary<string, IHardware>();
            Graphics GPU;
            var GraphicsCards = WMISearcher;

            // GPU information fields
            string GPUName;
            string GPUDriver;
            string GPUDriverVersion;
            string GPUBusID;
            string GPUChipID;

            // TODO: Bus ID not implemented in Windows
            // Get information of sound cards
            InxiTrace.Debug("Getting the base objects...");
            InxiTrace.Debug("TODO: Bus ID not implemented in Windows.");
            foreach (ManagementBaseObject Graphics in GraphicsCards.Get())
            {
                try
                {
                    // Get information of a graphics card
                    GPUName = (string)Graphics["Caption"];
                    GPUDriver = (string)Graphics["InstalledDisplayDrivers"];
                    GPUDriverVersion = (string)Graphics["DriverVersion"];
                    GPUChipID = (string)Graphics["PNPDeviceID"];
                    GPUBusID = "";
                    InxiTrace.Debug("Got information. GPUName: {0}, GPUDriver: {1}, GPUDriverVersion: {2}, GPUChipID: {3}, GPUBusID: {4}", GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID);

                    // Create an instance of graphics class
                    GPU = new Graphics(GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID);
                    GPUParsed.AddIfNotFound(GPUName, GPU);
                    InxiTrace.Debug("Added {0} to the list of parsed GPUs.", GPUName);
                }
                catch (Exception ex)
                {
                    InxiTrace.Debug("Error: {0}", ex.Message);
                }
            }
            return GPUParsed;
        }

    }
}