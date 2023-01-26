﻿using System.Collections.Generic;

namespace InxiFrontend
{
    /// <summary>
    /// Hard drive class
    /// </summary>
    public class HardDrive : HardwareBase
    {

        /// <summary>
        /// The name of hard drive
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// The udev ID of hard drive
        /// </summary>
        public string ID { get; private set; }
        /// <summary>
        /// The size of the drive
        /// </summary>
        public string Size { get; private set; }
        /// <summary>
        /// The model of the drive
        /// </summary>
        public string Model { get; private set; }
        /// <summary>
        /// The make of the drive
        /// </summary>
        public string Vendor { get; private set; }
        /// <summary>
        /// The speed of the drive
        /// </summary>
        public string Speed { get; private set; }
        /// <summary>
        /// The serial number
        /// </summary>
        public string Serial { get; private set; }
        /// <summary>
        /// List of partitions
        /// </summary>
        public Dictionary<string, Partition> Partitions { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal HardDrive(string ID, string Size, string Model, string Vendor, string Speed, string Serial, Dictionary<string, Partition> Partitions)
        {
            this.ID = ID;
            this.Size = Size;
            this.Model = Model;
            this.Vendor = Vendor;
            Name = $"{Vendor} {Model}";
            this.Speed = Speed;
            this.Serial = Serial;
            this.Partitions = Partitions;
        }

    }
}