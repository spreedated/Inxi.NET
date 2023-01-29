using InxiFrontend.Base;
using InxiFrontend.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;

namespace InxiFrontend
{
    /// <summary>
    /// Battery class
    /// </summary>
    public sealed class Battery : IHardware, IEquatable<Battery>
    {
        [JsonProperty()]
        /// <summary>
        /// Battery ID
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// The battery charge percentage
        /// </summary>
        public int Charge { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Battery condition
        /// </summary>
        public string Condition { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Battery voltage
        /// </summary>
        public string Volts { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Battery model
        /// </summary>
        public string Model { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Battery status
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal Battery(string Name, int Charge, string Condition, string Volts, string Model, string Status)
        {
            this.Name = Name;
            this.Charge = Charge;
            this.Condition = Condition;
            this.Volts = Volts;
            this.Model = Model;
            this.Status = Status;
        }

        [JsonConstructor()]
        public Battery()
        {

        }

        public bool Equals(Battery other)
        {
            return HelperFunctions.AreObjectsEqual<Battery>(this, other, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }

        public override bool Equals(object obj)
        {
            if (obj is not Battery)
            {
                return false;
            }
            return this.Equals((Battery)obj);
        }

        public override int GetHashCode()
        {
            return HelperFunctions.GetHashCodes(this, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }
    }
}