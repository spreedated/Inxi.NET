using InxiFrontend.Base;
using Claunia.PropertyList;
using Extensification.External.Newtonsoft.Json.JPropertyExts;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Management;

namespace InxiFrontend
{

    class SystemParser : HardwareParserBase
    {

        /// <summary>
        /// Parses system info
        /// </summary>
        /// <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
        /// <param name="SystemProfilerToken">system_profiler token</param>
        public override IHardware Parse(JToken InxiToken, NSArray SystemProfilerToken)
        {
            IHardware SysInfo;

            if (InxiInternalUtils.IsUnix())
            {
                SysInfo = ParseLinux(InxiToken);
            }
            else
            {
                InxiTrace.Debug("Selecting entries from Win32_OperatingSystem...");
                var WMISystem = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                SysInfo = ParseWindows(WMISystem);
            }

            return SysInfo;
        }
        public override IHardware ParseLinux(JToken InxiToken)
        {
            var SysInfo = default(IHardware);

            InxiTrace.Debug("Selecting the System token...");
            foreach (var InxiSys in InxiToken.SelectTokenKeyEndingWith("System"))
            {
                // Get information of system
                string Hostname = (string)InxiSys.SelectTokenKeyEndingWith("Host");
                string Version = (string)InxiSys.SelectTokenKeyEndingWith("Kernel");
                int Bits = (int)InxiSys.SelectTokenKeyEndingWith("bits");
                string Distro = (string)InxiSys.SelectTokenKeyEndingWith("Distro");
                string DesktopMan = (string)InxiSys.SelectTokenKeyEndingWith("Desktop");
                string WindowMan = (string)InxiSys.SelectTokenKeyEndingWith("WM");
                string DisplayMan = (string)InxiSys.SelectTokenKeyEndingWith("dm");
                if (string.IsNullOrEmpty(WindowMan))
                    WindowMan = (string)InxiSys.SelectTokenKeyEndingWith("wm");
                InxiTrace.Debug("Got information. Hostname: {0}, Version: {1}, Distro: {2}, Bits: {3}, DesktopMan: {4}, WindowMan: {5}, DisplayMan: {6}", Hostname, Version, Distro, Bits, DesktopMan, WindowMan, DisplayMan);

                // Create an instance of system class
                SysInfo = new SystemInfo(Hostname, Version, Bits, Distro, DesktopMan, WindowMan, DisplayMan);
            }

            return SysInfo;
        }

        public override IHardware ParseWindows(ManagementObjectSearcher WMISearcher)
        {
            var WMISystem = WMISearcher;
            var SysInfo = default(IHardware);

            // Get information of system
            // TODO: Desktop Manager and Display Manager are not implemented on Windows.
            InxiTrace.Debug("Getting the base objects...");
            InxiTrace.Debug("TODO: Desktop Manager and Display Manager are not implemented on Windows.");
            foreach (ManagementBaseObject SystemBase in WMISystem.Get())
            {
                string Hostname = System.Net.Dns.GetHostName();
                string Version = (string)SystemBase["Version"];
                int Bits = Convert.ToInt32(SystemBase["OSArchitecture"].ToString().Replace("-bit", ""));
                string Distro = (string)SystemBase["Caption"];
                string WM = Process.GetProcessesByName("dwm").Length > 0 ? "DWM" : "Basic Window Manager";
                InxiTrace.Debug("Got information. Hostname: {0}, Version: {1}, Distro: {2}, Bits: {3}, WM: {4}", Hostname, Version, Distro, Bits, WM);

                // Create an instance of system class
                SysInfo = new SystemInfo(Hostname, Version, Bits, Distro, "", WM, "");
            }

            return SysInfo;
        }

    }
}