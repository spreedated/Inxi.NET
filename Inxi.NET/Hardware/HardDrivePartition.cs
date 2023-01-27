using InxiFrontend.Base;
using Newtonsoft.Json;

namespace InxiFrontend
{
    /// <summary>
    /// Partition class
    /// </summary>
    public class Partition : IHardware
    {
        [JsonProperty()]
        /// <summary>
        /// The name of drive partition (usually udev ID)
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// The udev ID of drive partition (compatibility)
        /// </summary>
        public string ID { get; private set; }
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
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal Partition(string ID, string FileSystem, string Size, string Used)
        {
            this.ID = ID;
            Name = ID;
            this.FileSystem = FileSystem;
            this.Size = Size;
            this.Used = Used;
        }
        [JsonConstructor()]
        public Partition()
        {

        }
    }
}