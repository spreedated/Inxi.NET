using Claunia.PropertyList;
using Extensification.DictionaryExts;
using Extensification.External.Newtonsoft.Json.JPropertyExts;
using InxiFrontend.Base;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace InxiFrontend
{

    class SoundParser : HardwareParserBase
    {

        /// <summary>
        /// Parses sound cards
        /// </summary>
        /// <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
        /// <param name="SystemProfilerToken">system_profiler token</param>
        public override Dictionary<string, IHardware> ParseAll(JToken InxiToken, NSArray SystemProfilerToken)
        {
            Dictionary<string, IHardware> SPUParsed;

            if (InxiInternalUtils.IsUnix())
            {
                SPUParsed = ParseAllLinux(InxiToken);
            }
            else
            {
                InxiTrace.Debug("Selecting entries from Win32_SoundDevice...");
                var SoundDevice = new ManagementObjectSearcher("SELECT * FROM Win32_SoundDevice");
                SPUParsed = ParseAllWindows(SoundDevice);
            }

            return SPUParsed;
        }

        public override Dictionary<string, IHardware> ParseAllLinux(JToken InxiToken)
        {
            var SPUParsed = new Dictionary<string, IHardware>();
            Sound SPU;

            // SPU information fields
            string SPUName;
            string SPUVendor;
            string SPUDriver;
            string SPUBusID;
            string SPUChipID;

            InxiTrace.Debug("Selecting the Audio token...");
            foreach (var InxiSPU in InxiToken.SelectTokenKeyEndingWith("Audio").Where(x => x.SelectTokenKeyEndingWith("Device") != null))
            {
                // Get information of a sound card
                SPUName = (string)InxiSPU.SelectTokenKeyEndingWith("Device");
                SPUVendor = (string)InxiSPU.SelectTokenKeyEndingWith("vendor");
                SPUDriver = (string)InxiSPU.SelectTokenKeyEndingWith("driver");
                SPUBusID = (string)InxiSPU.SelectTokenKeyEndingWith("bus ID");
                SPUChipID = (string)InxiSPU.SelectTokenKeyEndingWith("chip ID");
                InxiTrace.Debug("Got information. SPUName: {0}, SPUDriver: {1}, SPUVendor: {2}, SPUBusID: {3}, SPUChipID: {4}", SPUName, SPUDriver, SPUVendor, SPUBusID, SPUChipID);

                // Create an instance of sound class
                SPU = new Sound(SPUName, SPUVendor, SPUDriver, SPUChipID, SPUBusID);
                SPUParsed.AddIfNotFound(SPUName, SPU);
                InxiTrace.Debug("Added {0} to the list of parsed SPUs.", SPUName);
            }

            return SPUParsed;
        }

        public override Dictionary<string, IHardware> ParseAllWindows(ManagementObjectSearcher WMISearcher)
        {
            var SoundDevice = WMISearcher;
            var SPUParsed = new Dictionary<string, IHardware>();
            Sound SPU;

            // SPU information fields
            string SPUName;
            string SPUVendor;
            string SPUDriver;
            string SPUBusID;
            string SPUChipID;

            // TODO: Driver not implemented in Windows
            // Get information of sound cards
            InxiTrace.Debug("Getting the base objects...");
            InxiTrace.Debug("TODO: Driver not implemented in Windows.");
            foreach (ManagementBaseObject Device in SoundDevice.Get())
            {
                // Get information of a sound card
                SPUName = (string)Device["ProductName"];
                SPUVendor = (string)Device["Manufacturer"];
                SPUDriver = "";
                SPUChipID = (string)Device["DeviceID"];
                SPUBusID = "";
                InxiTrace.Debug("Got information. SPUName: {0}, SPUDriver: {1}, SPUVendor: {2}, SPUBusID: {3}, SPUChipID: {4}", SPUName, SPUDriver, SPUVendor, SPUBusID, SPUChipID);

                // Create an instance of sound class
                SPU = new Sound(SPUName, SPUVendor, SPUDriver, SPUChipID, SPUBusID);
                SPUParsed.AddIfNotFound(SPUName, SPU);
                InxiTrace.Debug("Added {0} to the list of parsed SPUs.", SPUName);
            }

            return SPUParsed;
        }

    }
}