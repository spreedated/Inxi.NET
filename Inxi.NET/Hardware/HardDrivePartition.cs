
namespace InxiFrontend
{

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

    public class Partition : HardwareBase
    {

        /// <summary>
    /// The name of drive partition (usually udev ID)
    /// </summary>
        public override string Name { get; }
        /// <summary>
    /// The udev ID of drive partition (compatibility)
    /// </summary>
        public string ID { get; private set; }
        /// <summary>
    /// The filesystem of partition
    /// </summary>
        public string FileSystem { get; private set; }
        /// <summary>
    /// The size of partition
    /// </summary>
        public string Size { get; private set; }
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

    }
}