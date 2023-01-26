
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Reflection.Assembly;

namespace InxiFrontend
{
    /// <summary>
    /// Inxi frontend class
    /// </summary>
    public class Inxi
    {

        /// <summary>
        /// Hardware information
        /// </summary>
        public HardwareInfo Hardware { get; private set; }
        public InxiHardwareType SelectedHardwareTypes { get; set; }
        public string InxiPath { get; private set; } = "/usr/bin/inxi";
        public string CpanelJsonXsPath { get; private set; } = "/usr/bin/cpanel_json_xs";
        public string JsonXsPath { get; private set; } = "/usr/bin/json_xs";

        public event EventHandler RunFinished;

#region Constructor
        /// <summary>
        /// Intializes the new instance of Inxi class and parses hardware
        /// </summary>
        public Inxi()
        {
            foreach (InxiHardwareType h in Enum.GetValues(typeof(InxiHardwareType)).Cast<InxiHardwareType>())
            {
                this.SelectedHardwareTypes |= h;
            }
        }

        /// <summary>
        /// Intializes the new instance of Inxi class and parses hardware
        /// </summary>
        /// <param name="HardwareTypes">Hardware types to parse</param>
        public Inxi(InxiHardwareType HardwareTypes)
        {
            this.SelectedHardwareTypes = HardwareTypes;
        }
#endregion

        /// <summary>
        /// Async version of the capturing process
        /// </summary>
        /// <returns></returns>
        public Task RetrieveInformationAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                this.RetrieveInformation();
            });
        }

        /// <summary>
        /// Runs the capture process
        /// </summary>
        /// <exception cref="InvalidOperationException">Be sure to have Inxi installed</exception>
        public void RetrieveInformation()
        {
            string FrontendVersion = GetExecutingAssembly().GetName().Version.ToString();

            if (InxiInternalUtils.IsUnix())
            {
                this.UnixRun(FrontendVersion);
                return;
            }
            this.WindowsRun(FrontendVersion);
            
            this.RunFinished?.Invoke(null, EventArgs.Empty);
        }

        public void SetPaths(string inxiPath, string jsonXsPath, string cpanelJsonXsPath)
        {
            this.InxiPath = inxiPath;
            this.JsonXsPath = jsonXsPath;
            this.CpanelJsonXsPath = cpanelJsonXsPath;
        }

        private void WindowsRun(string frontendVersion)
        {
            InxiTrace.Debug("Inxi.NET {0} running on Windows.", frontendVersion);
            this.Hardware = new HardwareInfo(InxiPath, this.SelectedHardwareTypes);
        }

        private void UnixRun(string frontendVersion)
        {
            InxiTrace.Debug("Inxi.NET {0} running on Unix.", frontendVersion);
            InxiTrace.Debug("Inxi parse flags: {0}", this.SelectedHardwareTypes);

            bool isMac = false;
            // Check to see if we're on macOS or on regular Unix
            if (InxiInternalUtils.IsMacOS())
            {
                // Use System Profiler to get hardware information
                InxiTrace.Debug("Type: macOS");
                isMac = true;
            }

            // Use Inxi to get hardware information
            InxiTrace.Debug("Type: Unix");
            InxiTrace.Debug("Looking for Inxi executable at {0}...", this.InxiPath);
            if (!isMac && this.IsInxiInstalled())
            {
                InxiTrace.Debug("Found Json XS perl module!");
            }

            this.Hardware = new HardwareInfo(this.InxiPath, this.SelectedHardwareTypes);
        }

        private bool IsInxiInstalled()
        {
            InxiTrace.Debug("Looking for Inxi executable at {0}...", this.InxiPath);

            if (File.Exists(this.InxiPath))
            {
                InxiTrace.Debug("Looking for Json XS perl module binary at {0} or {1}...", this.CpanelJsonXsPath, this.JsonXsPath);

                if (File.Exists(this.CpanelJsonXsPath) | File.Exists(this.JsonXsPath))
                {
                    return true;
                }

                InxiTrace.Debug("Json XS perl module is not installed.");
                throw new InvalidOperationException("You must have libcpanel-json-xs-perl or libjson-xs-perl installed. (Could not find \"" + this.CpanelJsonXsPath + "\" or \"" + this.JsonXsPath + "\".)");
            }

            InxiTrace.Debug("Inxi is not installed.");
            throw new InvalidOperationException("You must have Inxi installed. (Could not find \"" + this.InxiPath + "\".)");
        }
    }
}