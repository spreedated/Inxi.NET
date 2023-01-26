

using System;

namespace InxiFrontend
{
    [Flags]
    /// <summary>
    /// Enumeration of hardware type
    /// </summary>
    public enum InxiHardwareType
    {
        /// <summary>
        /// BIOS
        /// </summary>
        BIOS = 1,
        /// <summary>
        /// Graphics card (GPU)
        /// </summary>
        Graphics = 2,
        /// <summary>
        /// Hard drive
        /// </summary>
        HardDrive = 4,
        /// <summary>
        /// Hard drive partitions (Windows logical partitions)
        /// </summary>
        HardDriveLogical = 8,
        /// <summary>
        /// Machine
        /// </summary>
        Machine = 16,
        /// <summary>
        /// Network
        /// </summary>
        Network = 32,
        /// <summary>
        /// RAM
        /// </summary>
        PCMemory = 64,
        /// <summary>
        /// Processor (CPU)
        /// </summary>
        Processor = 128,
        /// <summary>
        /// Sound (SPU)
        /// </summary>
        Sound = 256,
        /// <summary>
        /// System
        /// </summary>
        System = 512,
        /// <summary>
        /// Battery
        /// </summary>
        Battery = 1024
    }
}