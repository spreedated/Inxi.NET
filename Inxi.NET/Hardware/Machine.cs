
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

    public class MachineInfo : HardwareBase
    {

        /// <summary>
    /// Machine name
    /// </summary>
        public override string Name { get; }
        /// <summary>
    /// Machine type
    /// </summary>
        public string Type { get; private set; }
        /// <summary>
    /// Machine product name
    /// </summary>
        public string Product { get; private set; }
        /// <summary>
    /// Machine system name
    /// </summary>
        public string System { get; private set; }
        /// <summary>
    /// Machine chassis
    /// </summary>
        public string Chassis { get; private set; }
        /// <summary>
    /// Motherboard manufacturer
    /// </summary>
        public string MoboManufacturer { get; private set; }
        /// <summary>
    /// Motherboard model
    /// </summary>
        public string MoboModel { get; private set; }

        /// <summary>
    /// Installs specified values parsed by Inxi to the class
    /// </summary>
        internal MachineInfo(string Type, string Product, string System, string Chassis, string MoboManufacturer, string MoboModel)
        {
            this.Type = Type;
            this.Product = Product;
            Name = Product;
            this.System = System;
            this.Chassis = Chassis;
            this.MoboManufacturer = MoboManufacturer;
            this.MoboModel = MoboModel;
        }

    }
}