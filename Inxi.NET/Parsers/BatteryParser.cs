﻿

using Claunia.PropertyList;
using Extensification.External.Newtonsoft.Json.JPropertyExts;
using InxiFrontend.Base;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Management;

namespace InxiFrontend
{

    class BatteryParser : HardwareParserBase
    {
        /// <summary>
        /// Parses battery info
        /// </summary>
        /// <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
        /// <param name="SystemProfilerToken">system_profiler token</param>
        public override List<IHardware> ParseAllToList(JToken InxiToken, NSArray SystemProfilerToken)
        {
            List<IHardware> Batteries;

            if (InxiInternalUtils.IsUnix())
            {
                Batteries = ParseAllToListLinux(InxiToken);
            }
            else
            {
                InxiTrace.Debug("Selecting entries from Win32_Battery...");
                var WMIBattery = new ManagementObjectSearcher("SELECT * FROM Win32_Battery");
                Batteries = ParseAllToListWindows(WMIBattery);
            }

            return Batteries;
        }
        public override List<IHardware> ParseAllToListLinux(JToken InxiToken)
        {
            var Batteries = new List<IHardware>();
            Battery Battery;

            InxiTrace.Debug("Selecting the Battery token...");
            if (InxiToken.SelectTokenKeyEndingWith("Battery") != null)
            {
                foreach (var InxiSys in InxiToken.SelectTokenKeyEndingWith("Battery"))
                {
                    // Get information of battery
                    string Name = (string)InxiSys.SelectTokenKeyEndingWith("ID");
                    int Charge = Convert.ToInt32(InxiSys.SelectTokenKeyEndingWith("charge").ToString().Replace("%", ""));
                    string Condition = (string)InxiSys.SelectTokenKeyEndingWith("condition");
                    string Volts = (string)InxiSys.SelectTokenKeyEndingWith("volts");
                    string Model = (string)InxiSys.SelectTokenKeyEndingWith("model");
                    string Status = (string)InxiSys.SelectTokenKeyEndingWith("status");
                    InxiTrace.Debug("Got information. Name: {0}, Charge: {1}, Condition: {2}, Volts: {3}, Model: {4}, Status: {5}", Name, Charge, Condition, Volts, Model, Status);

                    // Create an instance of battery class
                    Battery = new Battery(Name, Charge, Condition, Volts, Model, Status);
                    Batteries.Add(Battery);
                }
            }

            return Batteries;
        }

        public override List<IHardware> ParseAllToListWindows(ManagementObjectSearcher WMISearcher)
        {
            var Batteries = new List<IHardware>();
            var WMIBatt = WMISearcher;

            // Get information of system
            InxiTrace.Debug("Getting the base objects...");
            foreach (ManagementBaseObject BattBase in WMIBatt.Get())
            {
                // Get information of battery
                string Name = (string)BattBase["Caption"];
                int Charge = (int)BattBase["EstimatedChargeRemaining"];
                string Condition = (string)BattBase["BatteryStatus"];
                string Volts = (string)BattBase["DesignVoltage"];
                string Model = (string)BattBase["Name"];
                string Status = (string)BattBase["BatteryStatus"];
                InxiTrace.Debug("Got information. Name: {0}, Charge: {1}, Condition: {2}, Volts: {3}, Model: {4}, Status: {5}", Name, Charge, Condition, Volts, Model, Status);

                // Create an instance of battery class
                var Battery = new Battery(Name, Charge, Condition, Volts, Model, Status);
                Batteries.Add(Battery);
            }

            return Batteries;
        }

    }
}