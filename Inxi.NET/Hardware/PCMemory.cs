
// Inxi.NET  Copyright (C) 2020-2021  EoflaOE
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
    /// RAM class
    /// </summary>
    public class PCMemory : HardwareBase
    {

        /// <summary>
        /// Memory indicators
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// Total memory installed
        /// </summary>
        public readonly string TotalMemory;
        /// <summary>
        /// Used memory
        /// </summary>
        public readonly string UsedMemory;
        /// <summary>
        /// Free memory
        /// </summary>
        public readonly string FreeMemory;

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal PCMemory(string TotalMemory, string UsedMemory, string FreeMemory)
        {
            this.TotalMemory = TotalMemory;
            this.UsedMemory = UsedMemory;
            this.FreeMemory = FreeMemory;
            Name = $"{UsedMemory}/{TotalMemory} - {FreeMemory} free";
        }

    }
}