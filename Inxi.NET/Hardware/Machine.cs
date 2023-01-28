using InxiFrontend.Base;
using InxiFrontend.Core;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace InxiFrontend
{
    /// <summary>
    /// Machine class
    /// </summary>
    public sealed class MachineInfo : IHardware, IEquatable<MachineInfo>
    {
        [JsonProperty()]
        /// <summary>
        /// Machine name
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// Machine type
        /// </summary>
        public string Type { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Machine product name
        /// </summary>
        public string Product { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Machine system name
        /// </summary>
        public string System { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Machine chassis
        /// </summary>
        public string Chassis { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Motherboard manufacturer
        /// </summary>
        public string MoboManufacturer { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Motherboard model
        /// </summary>
        public string MoboModel { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal MachineInfo(string Type, string Product, string System, string Chassis, string MoboManufacturer, string MoboModel)
        {
            this.Type = Type;
            this.Product = Product;
            Name = Product;
            this.System = System;
            this.Chassis = Chassis;
            this.MoboManufacturer = MoboManufacturer;
            this.MoboModel = MoboModel;
        }
        [JsonConstructor()]
        public MachineInfo()
        {

        }

        public bool Equals(MachineInfo other)
        {
            return HelperFunctions.AreObjectsEqual<MachineInfo>(this, other, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }
    }
}