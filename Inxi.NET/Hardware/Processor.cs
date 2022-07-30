
// Inxi.NET  Copyright (C) 2020-2021  Aptivi
// 
// This file is part of Inxi.NET
// 
// Inxi.NET is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Inxi.NET is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

namespace InxiFrontend
{
    /// <summary>
    /// Processor class
    /// </summary>
    public class Processor : HardwareBase
    {

        /// <summary>
        /// Name of processor
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// Core Topology
        /// </summary>
        public string Topology { get; private set; }
        /// <summary>
        /// Processor type
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// Processor milestone (Kaby Lake, Coffee Lake, ...)
        /// </summary>
        public string Milestone { get; private set; }
        /// <summary>
        /// Processor features
        /// </summary>
        public string[] Flags { get; private set; }
        /// <summary>
        /// Processor bits
        /// </summary>
        public int Bits { get; private set; }
        /// <summary>
        /// L2 Cache
        /// </summary>
        public string L2 { get; private set; }
        /// <summary>
        /// L3 Cache
        /// </summary>
        public int L3 { get; private set; }
        /// <summary>
        /// CPU Rev
        /// </summary>
        public string CPURev { get; private set; }
        /// <summary>
        /// CPU BogoMips
        /// </summary>
        public int CPUBogoMips { get; private set; }
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

    }
}