

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