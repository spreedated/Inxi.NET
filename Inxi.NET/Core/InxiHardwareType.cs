
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

using System;

namespace InxiFrontend
{
    [Flags]
    /// <summary>
    /// Enumeration of hardware type
    /// </summary>
    public enum InxiHardwareType
    {
        /// <summary>
        /// BIOS
        /// </summary>
        BIOS = 1,
        /// <summary>
        /// Graphics card (GPU)
        /// </summary>
        Graphics = 2,
        /// <summary>
        /// Hard drive
        /// </summary>
        HardDrive = 4,
        /// <summary>
        /// Hard drive partitions (Windows logical partitions)
        /// </summary>
        HardDriveLogical = 8,
        /// <summary>
        /// Machine
        /// </summary>
        Machine = 16,
        /// <summary>
        /// Network
        /// </summary>
        Network = 32,
        /// <summary>
        /// RAM
        /// </summary>
        PCMemory = 64,
        /// <summary>
        /// Processor (CPU)
        /// </summary>
        Processor = 128,
        /// <summary>
        /// Sound (SPU)
        /// </summary>
        Sound = 256,
        /// <summary>
        /// System
        /// </summary>
        System = 512,
        /// <summary>
        /// Battery
        /// </summary>
        Battery = 1024
    }
}