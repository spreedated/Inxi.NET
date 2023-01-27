using InxiFrontend.Base;
using Newtonsoft.Json;

namespace InxiFrontend
{
    /// <summary>
    /// Graphics class
    /// </summary>
    public class Graphics : IHardware
    {
        [JsonProperty()]
        /// <summary>
        /// Name of graphics card
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// Driver software used for graphics card
        /// </summary>
        public string Driver { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Driver version
        /// </summary>
        public string DriverVersion { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Device chip ID
        /// </summary>
        public string ChipID { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Device bus ID
        /// </summary>
        public string BusID { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal Graphics(string Name, string Driver, string DriverVersion, string ChipID, string BusID)
        {
            this.Name = Name;
            this.Driver = Driver;
            this.DriverVersion = DriverVersion;
            this.ChipID = ChipID;
            this.BusID = BusID;
        }
        [JsonConstructor()]
        public Graphics()
        {

        }
    }
}