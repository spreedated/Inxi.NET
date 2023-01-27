using InxiFrontend.Base;
using Newtonsoft.Json;

namespace InxiFrontend
{
    /// <summary>
    /// System information class
    /// </summary>
    public class SystemInfo : IHardware
    {
        [JsonProperty()]
        /// <summary>
        /// System name
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// Host name
        /// </summary>
        public string Hostname { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Linux kernel version or Windows NT kernel version
        /// </summary>
        public string SystemVersion { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// System bits
        /// </summary>
        public int SystemBits { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// System name
        /// </summary>
        public string SystemDistro { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Desktop manager
        /// </summary>
        public string DesktopManager { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Window manager
        /// </summary>
        public string WindowManager { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Display manager
        /// </summary>
        public string DisplayManager { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal SystemInfo(string Hostname, string SystemVersion, int SystemBits, string SystemDistro, string DesktopManager, string WindowManager, string DisplayManager)
        {
            this.Hostname = Hostname;
            this.SystemVersion = SystemVersion;
            this.SystemBits = SystemBits;
            this.SystemDistro = SystemDistro;
            Name = SystemDistro;
            this.DesktopManager = DesktopManager;
            this.WindowManager = WindowManager;
            this.DisplayManager = DisplayManager;
        }
        [JsonConstructor()]
        public SystemInfo()
        {

        }
    }
}