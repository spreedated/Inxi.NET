using InxiFrontend.Base;
using Newtonsoft.Json;

namespace InxiFrontend
{
    /// <summary>
    /// RAM class
    /// </summary>
    public sealed class PCMemory : IHardware
    {
        [JsonProperty()]
        /// <summary>
        /// Memory indicators
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// Total memory installed
        /// </summary>
        public readonly string TotalMemory;
        [JsonProperty()]
        /// <summary>
        /// Used memory
        /// </summary>
        public readonly string UsedMemory;
        [JsonProperty()]
        /// <summary>
        /// Free memory
        /// </summary>
        public readonly string FreeMemory;

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal PCMemory(string TotalMemory, string UsedMemory, string FreeMemory)
        {
            this.TotalMemory = TotalMemory;
            this.UsedMemory = UsedMemory;
            this.FreeMemory = FreeMemory;
            Name = $"{UsedMemory}/{TotalMemory} - {FreeMemory} free";
        }
        [JsonConstructor()]
        public PCMemory()
        {

        }
    }
}