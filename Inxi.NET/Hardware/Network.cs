
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
    /// Network class
    /// </summary>
    public class Network : HardwareBase
    {

        /// <summary>
        /// Name of network card
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// Driver software used for network card
        /// </summary>
        public string Driver { get; private set; }
        /// <summary>
        /// Driver version
        /// </summary>
        public string DriverVersion { get; private set; }
        /// <summary>
        /// Duplex type (usually full)
        /// </summary>
        public string Duplex { get; private set; }
        /// <summary>
        /// Maximum speed that the device can handle
        /// </summary>
        public string Speed { get; private set; }
        /// <summary>
        /// State of network card
        /// </summary>
        public string State { get; private set; }
        /// <summary>
        /// MAC Address
        /// </summary>
        public string MacAddress { get; private set; }
        /// <summary>
        /// Device identifier
        /// </summary>
        public string DeviceID { get; private set; }
        /// <summary>
        /// Device chip ID
        /// </summary>
        public string ChipID { get; private set; }
        /// <summary>
        /// Device bus ID
        /// </summary>
        public string BusID { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal Network(string Name, string Driver, string DriverVersion, string Duplex, string Speed, string State, string MacAddress, string DeviceID, string ChipID, string BusID)
        {
            this.Name = Name;
            this.Driver = Driver;
            this.DriverVersion = DriverVersion;
            this.Duplex = Duplex;
            this.Speed = Speed;
            this.State = State;
            this.MacAddress = MacAddress;
            this.DeviceID = DeviceID;
            this.ChipID = ChipID;
            this.BusID = BusID;
        }

    }
}