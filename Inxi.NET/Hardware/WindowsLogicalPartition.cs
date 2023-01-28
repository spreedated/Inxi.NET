using InxiFrontend.Base;
using InxiFrontend.Core;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace InxiFrontend
{
    /// <summary>
    /// Windows logical partition class
    /// </summary>
    public sealed class WindowsLogicalPartition : IHardware, IEquatable<WindowsLogicalPartition>
    {
        [JsonProperty()]
        /// <summary>
        /// The name of drive partition
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// The drive letter of drive partition
        /// </summary>
        public string Letter { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// The filesystem of partition
        /// </summary>
        public string FileSystem { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// The size of partition
        /// </summary>
        public string Size { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// The used size of partition
        /// </summary>
        public string Used { get; private set; }

        /// <summary>
        /// Installs specified values parsed to the class
        /// </summary>
        internal WindowsLogicalPartition(string Letter, string Name, string FileSystem, string Size, string Used)
        {
            this.Letter = Letter;
            this.Name = Name;
            this.FileSystem = FileSystem;
            this.Size = Size;
            this.Used = Used;
        }
        [JsonConstructor()]
        public WindowsLogicalPartition()
        {

        }

        public bool Equals(WindowsLogicalPartition other)
        {
            return HelperFunctions.AreObjectsEqual<WindowsLogicalPartition>(this, other, (x) => x.CustomAttributes.Any(y => y.AttributeType == typeof(JsonPropertyAttribute)));
        }
    }
}