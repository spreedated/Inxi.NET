﻿using InxiFrontend;
using System;
using System.Linq;

namespace Inxi.NET.ConsoleTest
{
    static class InxiConsoleTest
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Contains("-debug"))
                    InxiTrace.DebugDataReceived += HandleDebugData;

                var InxiInstance = new InxiFrontend.Inxi();
                InxiInstance.RetrieveInformation();
                var HardwareInfo = InxiInstance.Hardware;

                Console.WriteLine("------ CPU Info:");
                foreach (Processor CPUInfo in HardwareInfo.CPU.Values)
                {
                    Console.WriteLine(">> CPU Name: {0}", CPUInfo.Name);
                    Console.WriteLine(">> CPU Speed: {0}", CPUInfo.Speed);
                    Console.WriteLine(">> CPU Topology: {0}", CPUInfo.Topology);
                    Console.WriteLine(">> CPU Type: {0}", CPUInfo.Type);
                    Console.WriteLine(">> CPU L2 Cache: {0}", CPUInfo.L2);
                    Console.WriteLine(">> CPU L3 Cache: {0}", CPUInfo.L3);
                    Console.WriteLine(">> CPU Rev: {0}", CPUInfo.CPURev);
                    Console.WriteLine(">> CPU BogoMips: {0}", CPUInfo.CPUBogoMips);
                    Console.WriteLine(">> CPU Bits: {0}", CPUInfo.Bits);
                    Console.WriteLine(">> CPU Milestone: {0}", CPUInfo.Milestone);
                    Console.WriteLine(">> CPU Flags: {0}", string.Join(", ", CPUInfo.Flags));
                }

                Console.WriteLine("------ GPU Info:");
                foreach (Graphics GPUInfo in HardwareInfo.GPU.Values)
                {
                    Console.WriteLine(">> GPU Name: {0}", GPUInfo.Name);
                    Console.WriteLine(">> GPU Driver: {0}", GPUInfo.Driver);
                    Console.WriteLine(">> GPU Driver Version: {0}", GPUInfo.DriverVersion);
                }

                Console.WriteLine("------ HDD Info:");
                foreach (HardDrive HDDInfo in HardwareInfo.HDD.Values)
                {
                    Console.WriteLine(">> HDD Name: {0}", HDDInfo.Name);
                    Console.WriteLine(">> HDD Vendor: {0}", HDDInfo.Vendor);
                    Console.WriteLine(">> HDD Model: {0}", HDDInfo.Model);
                    Console.WriteLine(">> HDD udev ID: {0}", HDDInfo.ID);
                    Console.WriteLine(">> HDD Size: {0}", HDDInfo.Size);
                    Console.WriteLine(">> HDD Speed: {0}", HDDInfo.Speed);
                    Console.WriteLine(">> HDD Serial: {0}", HDDInfo.Serial);
                    Console.WriteLine(">> HDD Mounted Partitions Count: {0}", HDDInfo.Partitions.Count);
                    Console.WriteLine(Environment.NewLine + "------ Partition Info:");
                    foreach (Partition PartInfo in HDDInfo.Partitions.Values)
                    {
                        Console.WriteLine(">> Partition Name: {0}", PartInfo.Name);
                        Console.WriteLine(">> Partition udev ID: {0}", PartInfo.ID);
                        Console.WriteLine(">> Partition Size: {0}", PartInfo.Size);
                        Console.WriteLine(">> Partition Used: {0}", PartInfo.Used);
                        Console.WriteLine(">> Partition File System: {0}" + Environment.NewLine, PartInfo.FileSystem);
                    }
                }

                Console.WriteLine("------ Sound Info:");
                foreach (Sound SoundInfo in HardwareInfo.Sound.Values)
                {
                    Console.WriteLine(">> Sound Name: {0}", SoundInfo.Name);
                    Console.WriteLine(">> Sound Driver: {0}", SoundInfo.Driver);
                    Console.WriteLine(">> Sound Vendor: {0}", SoundInfo.Vendor);
                }

                Console.WriteLine("------ Net Info:");
                foreach (Network NetInfo in HardwareInfo.Network.Values)
                {
                    Console.WriteLine(">> Network Name: {0}", NetInfo.Name);
                    Console.WriteLine(">> Network Driver: {0}", NetInfo.Driver);
                    Console.WriteLine(">> Network Driver Version: {0}", NetInfo.DriverVersion);
                    Console.WriteLine(">> Network Device ID: {0}", NetInfo.DeviceID);
                    Console.WriteLine(">> Network MAC Address: {0}", NetInfo.MacAddress);
                    Console.WriteLine(">> Network Speed: {0}", NetInfo.Speed);
                    Console.WriteLine(">> Network State: {0}", NetInfo.State);
                    Console.WriteLine(">> Network Duplex Type: {0}", NetInfo.Duplex);
                }

                Console.WriteLine("------ Battery Info:");
                foreach (Battery BattInfo in HardwareInfo.Battery)
                {
                    Console.WriteLine(">> Battery Name: {0}", BattInfo.Name);
                    Console.WriteLine(">> Battery Charge: {0}%", BattInfo.Charge);
                    Console.WriteLine(">> Battery Condition: {0}", BattInfo.Condition);
                    Console.WriteLine(">> Battery Model: {0}", BattInfo.Model);
                    Console.WriteLine(">> Battery Status: {0}", BattInfo.Status);
                    Console.WriteLine(">> Battery Volts: {0}", BattInfo.Volts);
                }

                Console.WriteLine("------ System Memory Info:");
                Console.WriteLine(">> Memory: {0}", HardwareInfo.RAM.Name);
                Console.WriteLine(">> Free Memory: {0}", HardwareInfo.RAM.FreeMemory);
                Console.WriteLine(">> Total Memory: {0}", HardwareInfo.RAM.TotalMemory);
                Console.WriteLine(">> Used Memory: {0}", HardwareInfo.RAM.UsedMemory);

                Console.WriteLine("------ System Info:");
                Console.WriteLine(">> Hostname: {0}", HardwareInfo.System.Hostname);
                Console.WriteLine(">> System Distro (Name): {0}", HardwareInfo.System.SystemDistro);
                Console.WriteLine(">> System Version: {0}", HardwareInfo.System.SystemVersion);
                Console.WriteLine(">> System Bits: {0}-bit", HardwareInfo.System.SystemBits);
                Console.WriteLine(">> Desktop Manager: {0}", HardwareInfo.System.DesktopManager);
                Console.WriteLine(">> Window Manager: {0}", HardwareInfo.System.WindowManager);
                Console.WriteLine(">> Display Manager: {0}", HardwareInfo.System.DisplayManager);

                Console.WriteLine("------ BIOS Info:");
                Console.WriteLine(">> BIOS: {0}", HardwareInfo.BIOS.Name);
                Console.WriteLine(">> Date: {0}", HardwareInfo.BIOS.Date);
                Console.WriteLine(">> Version: {0}", HardwareInfo.BIOS.Version);

                Console.WriteLine("------ Machine Info:");
                Console.WriteLine(">> Name: {0}", HardwareInfo.Machine.Name);
                Console.WriteLine(">> Product: {0}", HardwareInfo.Machine.Product);
                Console.WriteLine(">> System: {0}", HardwareInfo.Machine.System);
                Console.WriteLine(">> Chassis: {0}", HardwareInfo.Machine.Chassis);
                Console.WriteLine(">> Type: {0}", HardwareInfo.Machine.Type);
                Console.WriteLine(">> Motherboard Manufacturer: {0}", HardwareInfo.Machine.MoboManufacturer);
                Console.WriteLine(">> Motherboard Model: {0}", HardwareInfo.Machine.MoboModel);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("------ Error: {0}", ex.Message);
                Console.WriteLine("------ {0}", ex.StackTrace);
                Console.WriteLine("------ Inner: {0}", ex.InnerException?.Message);
                Console.WriteLine("------ {0}", ex.InnerException?.StackTrace);
                Console.ReadKey();
            }
        }

        private static void HandleDebugData(string Message, string PlainMessage) => Console.WriteLine(Message);

    }
}