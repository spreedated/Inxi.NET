using InxiFrontend.Base;
using Newtonsoft.Json;

namespace InxiFrontend
{
    /// <summary>
    /// BIOS class
    /// </summary>
    public class BIOS : IHardware
    {
        [JsonProperty()]
        /// <summary>
        /// BIOS (American Megatrends, Award, etc.)
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// BIOS Date
        /// </summary>
        public string Date { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// BIOS Version
        /// </summary>
        public string Version { get; private set; }
        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal BIOS(string BIOS, string Date, string Version)
        {
            Name = BIOS;
            this.Date = Date;
            this.Version = Version;
        }
        [JsonConstructor()]
        public BIOS()
        {

        }
    }
}