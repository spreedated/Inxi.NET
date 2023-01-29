using System;
using System.Collections.Generic;
using System.Linq;
using InxiFrontend.Base;
using InxiFrontend.Core;
using Newtonsoft.Json;

namespace InxiFrontend
{
    /// <summary>
    /// Hard drive class
    /// </summary>
    public sealed class HardDrive : IHardware, IEquatable<HardDrive>
    {
        [JsonProperty()]
        /// <summary>
        /// The name of hard drive
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// The udev ID of hard drive
        /// </summary>
        public string ID { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// The size of the drive
        /// </summary>
        public string Size { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// The model of the drive
        /// </summary>
        public string Model { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// The make of the drive
        /// </summary>
        public string Vendor { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// The speed of the drive
        /// </summary>
        public string Speed { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// The serial number
        /// </summary>
        public string Serial { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// List of partitions
        /// </summary>
        public Dictionary<string, Partition> Partitions { get; private set; }
        
        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal HardDrive(string ID, string Size, string Model, string Vendor, string Speed, string Serial, Dictionary<string, Partition> Partitions)
        {
            this.ID = ID;
            this.Size = Size;
            this.Model = Model;
            this.Vendor = Vendor;
            Name = $"{Vendor} {Model}";
            this.Speed = Speed;
            this.Serial = Serial;
            this.Partitions = Partitions;
        }
        [JsonConstructor()]
        public HardDrive()
        {
        }

        public bool Equals(HardDrive other)
        {
            return HelperFunctions.AreObjectsEqual<HardDrive>(this, other, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)) && x.Name != "Name");
        }

        public override bool Equals(object obj)
        {
            if (obj is not HardDrive)
            {
                return false;
            }
            return this.Equals((HardDrive)obj);
        }

        public override int GetHashCode()
        {
            return HelperFunctions.GetHashCodes(this, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }
    }
}