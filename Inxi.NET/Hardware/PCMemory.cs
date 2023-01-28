using InxiFrontend.Base;
using InxiFrontend.Core;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace InxiFrontend
{
    /// <summary>
    /// RAM class
    /// </summary>
    public sealed class PCMemory : IHardware, IEquatable<PCMemory>
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
        public string TotalMemory { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Used memory
        /// </summary>
        public string UsedMemory { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Free memory
        /// </summary>
        public string FreeMemory { get; private set; }

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

        public bool Equals(PCMemory other)
        {
            return HelperFunctions.AreObjectsEqual<PCMemory>(this, other, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)) && x.Name != "Name");
        }
    }
}