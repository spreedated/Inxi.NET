using InxiFrontend.Base;
using InxiFrontend.Core;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace InxiFrontend
{
    /// <summary>
    /// Graphics class
    /// </summary>
    public sealed class Graphics : IHardware, IEquatable<Graphics>
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
        public bool Equals(Graphics other)
        {
            return HelperFunctions.AreObjectsEqual<Graphics>(this, other, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }

        public override bool Equals(object obj)
        {
            if (obj is not Graphics)
            {
                return false;
            }
            return this.Equals((Graphics)obj);
        }

        public override int GetHashCode()
        {
            return HelperFunctions.GetHashCodes(this, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }
    }
}