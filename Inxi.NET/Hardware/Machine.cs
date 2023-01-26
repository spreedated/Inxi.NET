

namespace InxiFrontend
{
    /// <summary>
    /// Machine class
    /// </summary>
    public class MachineInfo : HardwareBase
    {

        /// <summary>
        /// Machine name
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// Machine type
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// Machine product name
        /// </summary>
        public string Product { get; private set; }
        /// <summary>
        /// Machine system name
        /// </summary>
        public string System { get; private set; }
        /// <summary>
        /// Machine chassis
        /// </summary>
        public string Chassis { get; private set; }
        /// <summary>
        /// Motherboard manufacturer
        /// </summary>
        public string MoboManufacturer { get; private set; }
        /// <summary>
        /// Motherboard model
        /// </summary>
        public string MoboModel { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal MachineInfo(string Type, string Product, string System, string Chassis, string MoboManufacturer, string MoboModel)
        {
            this.Type = Type;
            this.Product = Product;
            Name = Product;
            this.System = System;
            this.Chassis = Chassis;
            this.MoboManufacturer = MoboManufacturer;
            this.MoboModel = MoboModel;
        }

    }
}