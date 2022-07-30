
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
    /// Windows logical partition class
    /// </summary>
    public class WindowsLogicalPartition : HardwareBase
    {

        /// <summary>
        /// The name of drive partition
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// The drive letter of drive partition
        /// </summary>
        public string Letter { get; private set; }
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

    }
}