using Claunia.PropertyList;
using Extensification.External.Newtonsoft.Json.JPropertyExts;
using InxiFrontend.Base;
using Newtonsoft.Json.Linq;
using System;
using System.Management;

namespace InxiFrontend
{

    class PCMemoryParser : HardwareParserBase
    {

        /// <summary>
        /// Parses processors
        /// </summary>
        /// <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
        /// <param name="SystemProfilerToken">system_profiler token</param>
        public override IHardware Parse(JToken InxiToken, NSArray SystemProfilerToken)
        {
            PCMemory Mem;
            if (InxiInternalUtils.IsUnix())
            {
                Mem = (PCMemory)ParseLinux(InxiToken);
            }
            else
            {
                InxiTrace.Debug("Selecting entries from Win32_OperatingSystem...");
                var System = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                Mem = (PCMemory)ParseWindows(System);
            }

            return Mem;
        }

        public override IHardware ParseLinux(JToken InxiToken)
        {
            var Mem = default(PCMemory);

            // TODO: Free memory is not implemented in Inxi.
            InxiTrace.Debug("TODO: Free memory is not implemented in Inxi.");
            InxiTrace.Debug("Selecting the Info token...");
            foreach (var InxiMem in InxiToken.SelectTokenKeyEndingWith("Info"))
            {
                // Get information of memory
                string TotalMem = (string)InxiMem.SelectTokenKeyEndingWith("Memory");
                string UsedMem = (string)InxiMem.SelectTokenKeyEndingWith("used");
                InxiTrace.Debug("Got information. TotalMem: {0}, UsedMem: {1}", TotalMem, UsedMem);

                // Create an instance of memory class
                Mem = new PCMemory(TotalMem, UsedMem, "");
            }

            return Mem;
        }

        public override IHardware ParseWindows(ManagementObjectSearcher WMISearcher)
        {
            PCMemory Mem;
            var System = WMISearcher;
            var TotalMem = default(long);
            var UsedMem = default(long);
            var FreeMem = default(long);

            // Get memory
            InxiTrace.Debug("Getting the base objects...");
            foreach (ManagementBaseObject OS in System.Get())
            {
                TotalMem = Convert.ToInt64(OS["TotalVisibleMemorySize"]);
                UsedMem = TotalMem - Convert.ToInt64(OS["FreePhysicalMemory"]);
                FreeMem = Convert.ToInt64(OS["FreePhysicalMemory"]);
                InxiTrace.Debug("Got information. TotalMem: {0}, UsedMem: {1}, FreeMem: {2}", TotalMem, UsedMem, FreeMem);
            }

            // Create an instance of memory class
            Mem = new PCMemory(TotalMem.ToString(), UsedMem.ToString(), FreeMem.ToString());
            return Mem;
        }

    }
}