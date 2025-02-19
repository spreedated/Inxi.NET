﻿using InxiFrontend.Base;
using InxiFrontend.Core;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace InxiFrontend
{
    /// <summary>
    /// Network class
    /// </summary>
    public sealed class Network : IHardware, IEquatable<Network>
    {
        [JsonProperty()]
        /// <summary>
        /// Name of network card
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// Driver software used for network card
        /// </summary>
        public string Driver { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Driver version
        /// </summary>
        public string DriverVersion { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Duplex type (usually full)
        /// </summary>
        public string Duplex { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Maximum speed that the device can handle
        /// </summary>
        public string Speed { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// State of network card
        /// </summary>
        public string State { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// MAC Address
        /// </summary>
        public string MacAddress { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Device identifier
        /// </summary>
        public string DeviceID { get; private set; }
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
        internal Network(string Name, string Driver, string DriverVersion, string Duplex, string Speed, string State, string MacAddress, string DeviceID, string ChipID, string BusID)
        {
            this.Name = Name;
            this.Driver = Driver;
            this.DriverVersion = DriverVersion;
            this.Duplex = Duplex;
            this.Speed = Speed;
            this.State = State;
            this.MacAddress = MacAddress;
            this.DeviceID = DeviceID;
            this.ChipID = ChipID;
            this.BusID = BusID;
        }
        [JsonConstructor()]
        public Network()
        {

        }

        public bool Equals(Network other)
        {
            return HelperFunctions.AreObjectsEqual<Network>(this, other, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }

        public override bool Equals(object obj)
        {
            if (obj is not Network)
            {
                return false;
            }
            return this.Equals((Network)obj);
        }

        public override int GetHashCode()
        {
            return HelperFunctions.GetHashCodes(this, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }
    }
}