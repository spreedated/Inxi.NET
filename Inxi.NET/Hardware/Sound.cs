using InxiFrontend.Base;
using InxiFrontend.Core;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace InxiFrontend
{
    /// <summary>
    /// Sound class
    /// </summary>
    public sealed class Sound : IHardware, IEquatable<Sound>
    {
        [JsonProperty()]
        /// <summary>
        /// Name of sound card
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// The maker of sound card
        /// </summary>
        public string Vendor { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Driver software used for sound card
        /// </summary>
        public string Driver { get; private set; }
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
        internal Sound(string Name, string Vendor, string Driver, string ChipID, string BusID)
        {
            this.Name = Name;
            this.Vendor = Vendor;
            this.Driver = Driver;
            this.ChipID = ChipID;
            this.BusID = BusID;
        }
        [JsonConstructor()]
        public Sound()
        {

        }

        public bool Equals(Sound other)
        {
            return HelperFunctions.AreObjectsEqual<Sound>(this, other, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }
    }
}