
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

    public class Battery : HardwareBase
    {

        /// <summary>
    /// Battery ID
    /// </summary>
        public override string Name { get; }
        /// <summary>
    /// The battery charge percentage
    /// </summary>
        public int Charge { get; private set; }
        /// <summary>
    /// Battery condition
    /// </summary>
        public string Condition { get; private set; }
        /// <summary>
    /// Battery voltage
    /// </summary>
        public string Volts { get; private set; }
        /// <summary>
    /// Battery model
    /// </summary>
        public string Model { get; private set; }
        /// <summary>
    /// Battery status
    /// </summary>
        public string Status { get; private set; }

        /// <summary>
    /// Installs specified values parsed by Inxi to the class
    /// </summary>
        internal Battery(string Name, int Charge, string Condition, string Volts, string Model, string Status)
        {
            this.Name = Name;
            this.Charge = Charge;
            this.Condition = Condition;
            this.Volts = Volts;
            this.Model = Model;
            this.Status = Status;
        }

    }
}