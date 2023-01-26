

namespace InxiFrontend
{
    /// <summary>
    /// Sound class
    /// </summary>
    public class Sound : HardwareBase
    {

        /// <summary>
        /// Name of sound card
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// The maker of sound card
        /// </summary>
        public string Vendor { get; private set; }
        /// <summary>
        /// Driver software used for sound card
        /// </summary>
        public string Driver { get; private set; }
        /// <summary>
        /// Device chip ID
        /// </summary>
        public string ChipID { get; private set; }
        /// <summary>
        /// Device bus ID
        /// </summary>
        public string BusID { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal Sound(string Name, string Vendor, string Driver, string ChipID, string BusID)
        {
            this.Name = Name;
            this.Vendor = Vendor;
            this.Driver = Driver;
            this.ChipID = ChipID;
            this.BusID = BusID;
        }

    }
}