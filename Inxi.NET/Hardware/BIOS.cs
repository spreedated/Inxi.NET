
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
    /// BIOS class
    /// </summary>
    public class BIOS : HardwareBase
    {

        /// <summary>
        /// BIOS (American Megatrends, Award, etc.)
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// BIOS Date
        /// </summary>
        public string Date { get; private set; }
        /// <summary>
        /// BIOS Version
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal BIOS(string BIOS, string Date, string Version)
        {
            Name = BIOS;
            this.Date = Date;
            this.Version = Version;
        }

    }
}