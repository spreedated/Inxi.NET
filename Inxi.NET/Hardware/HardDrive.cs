using System.Collections.Generic;

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

    public class HardDrive : HardwareBase
    {

        /// <summary>
    /// The name of hard drive
    /// </summary>
        public override string Name { get; }
        /// <summary>
    /// The udev ID of hard drive
    /// </summary>
        public string ID { get; private set; }
        /// <summary>
    /// The size of the drive
    /// </summary>
        public string Size { get; private set; }
        /// <summary>
    /// The model of the drive
    /// </summary>
        public string Model { get; private set; }
        /// <summary>
    /// The make of the drive
    /// </summary>
        public string Vendor { get; private set; }
        /// <summary>
    /// The speed of the drive
    /// </summary>
        public string Speed { get; private set; }
        /// <summary>
    /// The serial number
    /// </summary>
        public string Serial { get; private set; }
        /// <summary>
    /// List of partitions
    /// </summary>
        public Dictionary<string, Partition> Partitions { get; private set; } = new Dictionary<string, Partition>();

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

    }
}