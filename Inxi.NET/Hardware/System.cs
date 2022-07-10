
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

    public class SystemInfo : HardwareBase
    {

        /// <summary>
    /// System name
    /// </summary>
        public override string Name { get; }
        /// <summary>
    /// Host name
    /// </summary>
        public string Hostname { get; private set; }
        /// <summary>
    /// Linux kernel version or Windows NT kernel version
    /// </summary>
        public string SystemVersion { get; private set; }
        /// <summary>
    /// System bits
    /// </summary>
        public int SystemBits { get; private set; }
        /// <summary>
    /// System name
    /// </summary>
        public string SystemDistro { get; private set; }
        /// <summary>
    /// Desktop manager
    /// </summary>
        public string DesktopManager { get; private set; }
        /// <summary>
    /// Window manager
    /// </summary>
        public string WindowManager { get; private set; }
        /// <summary>
    /// Display manager
    /// </summary>
        public string DisplayManager { get; private set; }

        /// <summary>
    /// Installs specified values parsed by Inxi to the class
    /// </summary>
        internal SystemInfo(string Hostname, string SystemVersion, int SystemBits, string SystemDistro, string DesktopManager, string WindowManager, string DisplayManager)
        {
            this.Hostname = Hostname;
            this.SystemVersion = SystemVersion;
            this.SystemBits = SystemBits;
            this.SystemDistro = SystemDistro;
            Name = SystemDistro;
            this.DesktopManager = DesktopManager;
            this.WindowManager = WindowManager;
            this.DisplayManager = DisplayManager;
        }

    }
}