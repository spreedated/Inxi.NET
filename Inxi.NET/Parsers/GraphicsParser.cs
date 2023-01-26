﻿
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

using Claunia.PropertyList;
using Extensification.DictionaryExts;
using Extensification.External.Newtonsoft.Json.JPropertyExts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Management;

namespace InxiFrontend
{

    class GraphicsParser : HardwareParserBase, IHardwareParser
    {

        /// <summary>
        /// Parses graphics cards
        /// </summary>
        /// <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
        /// <param name="SystemProfilerToken">system_profiler token</param>
        public override Dictionary<string, HardwareBase> ParseAll(JToken InxiToken, NSArray SystemProfilerToken)
        {
            Dictionary<string, HardwareBase> GPUParsed;

            if (InxiInternalUtils.IsUnix())
            {
                GPUParsed = ParseAllLinux(InxiToken);
            }
            else
            {
                InxiTrace.Debug("Selecting entries from Win32_VideoController...");
                var GraphicsCards = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                GPUParsed = ParseAllWindows(GraphicsCards);
            }

            return GPUParsed;
        }

        public override Dictionary<string, HardwareBase> ParseAllLinux(JToken InxiToken)
        {
            var GPUParsed = new Dictionary<string, HardwareBase>();
            Graphics GPU;

            // GPU information fields
            string GPUName;
            string GPUDriver;
            string GPUDriverVersion;
            string GPUBusID;
            string GPUChipID;

            InxiTrace.Debug("Selecting the Graphics token...");
            foreach (var InxiGPU in InxiToken.SelectTokenKeyEndingWith("Graphics"))
            {
                if (InxiGPU.SelectTokenKeyEndingWith("Device") is not null)
                {
                    // Get information of a graphics card
                    GPUName = (string)InxiGPU.SelectTokenKeyEndingWith("Device");
                    GPUDriver = (string)InxiGPU.SelectTokenKeyEndingWith("driver");
                    GPUDriverVersion = (string)InxiGPU.SelectTokenKeyEndingWith("v");
                    GPUChipID = (string)InxiGPU.SelectTokenKeyEndingWith("chip ID");
                    GPUBusID = (string)InxiGPU.SelectTokenKeyEndingWith("bus ID");
                    InxiTrace.Debug("Got information. GPUName: {0}, GPUDriver: {1}, GPUDriverVersion: {2}, GPUChipID: {3}, GPUBusID: {4}", GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID);

                    // Create an instance of graphics class
                    GPU = new Graphics(GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID);
                    GPUParsed.Add(GPUName, GPU);
                    InxiTrace.Debug("Added {0} to the list of parsed GPUs.", GPUName);
                }
            }
            return GPUParsed;
        }

        public override Dictionary<string, HardwareBase> ParseAllWindows(ManagementObjectSearcher WMISearcher)
        {
            var GPUParsed = new Dictionary<string, HardwareBase>();
            Graphics GPU;
            var GraphicsCards = WMISearcher;

            // GPU information fields
            string GPUName;
            string GPUDriver;
            string GPUDriverVersion;
            string GPUBusID;
            string GPUChipID;

            // TODO: Bus ID not implemented in Windows
            // Get information of sound cards
            InxiTrace.Debug("Getting the base objects...");
            InxiTrace.Debug("TODO: Bus ID not implemented in Windows.");
            foreach (ManagementBaseObject Graphics in GraphicsCards.Get())
            {
                try
                {
                    // Get information of a graphics card
                    GPUName = (string)Graphics["Caption"];
                    GPUDriver = (string)Graphics["InstalledDisplayDrivers"];
                    GPUDriverVersion = (string)Graphics["DriverVersion"];
                    GPUChipID = (string)Graphics["PNPDeviceID"];
                    GPUBusID = "";
                    InxiTrace.Debug("Got information. GPUName: {0}, GPUDriver: {1}, GPUDriverVersion: {2}, GPUChipID: {3}, GPUBusID: {4}", GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID);

                    // Create an instance of graphics class
                    GPU = new Graphics(GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID);
                    GPUParsed.AddIfNotFound(GPUName, GPU);
                    InxiTrace.Debug("Added {0} to the list of parsed GPUs.", GPUName);
                }
                catch (Exception ex)
                {
                    InxiTrace.Debug("Error: {0}", ex.Message);
                    continue;
                }
            }
            return GPUParsed;
        }

    }
}