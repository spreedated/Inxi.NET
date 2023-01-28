using InxiFrontend.Base;
using InxiFrontend.Core;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace InxiFrontend
{
    /// <summary>
    /// BIOS class
    /// </summary>
    public sealed class BIOS : IHardware, IEquatable<BIOS>
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
        public bool Equals(BIOS other)
        {
            return HelperFunctions.AreObjectsEqual<BIOS>(this, other, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }
    }
}