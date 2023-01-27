

using InxiFrontend.Base;
using Newtonsoft.Json;

namespace InxiFrontend
{
    /// <summary>
    /// Processor class
    /// </summary>
    public class Processor : IHardware
    {
        [JsonProperty()]
        /// <summary>
        /// Name of processor
        /// </summary>
        public string Name { get; set; }
        [JsonProperty()]
        /// <summary>
        /// Core Topology
        /// </summary>
        public string Topology { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Processor type
        /// </summary>
        public string Type { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Processor milestone (Kaby Lake, Coffee Lake, ...)
        /// </summary>
        public string Milestone { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Processor features
        /// </summary>
        public string[] Flags { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Processor bits
        /// </summary>
        public int Bits { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// L2 Cache
        /// </summary>
        public string L2 { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// L3 Cache
        /// </summary>
        public int L3 { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// CPU Rev
        /// </summary>
        public string CPURev { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// CPU BogoMips
        /// </summary>
        public int CPUBogoMips { get; private set; }
        [JsonProperty()]
        /// <summary>
        /// Processor speed
        /// </summary>
        public string Speed { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal Processor(string Name, string Topology, string Type, int Bits, string Milestone, string[] Flags, string L2, int L3, string CPURev, int CPUBogoMips, string Speed)
        {
            this.Name = Name;
            this.Topology = Topology;
            this.Type = Type;
            this.Bits = Bits;
            this.Milestone = Milestone;
            this.Flags = Flags;
            this.L2 = L2;
            this.L3 = L3;
            this.CPURev = CPURev;
            this.CPUBogoMips = CPUBogoMips;
            this.Speed = Speed;
        }
        [JsonConstructor()]
        public Processor()
        {

        }
    }
}