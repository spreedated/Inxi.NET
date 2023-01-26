

namespace InxiFrontend
{
    /// <summary>
    /// RAM class
    /// </summary>
    public class PCMemory : HardwareBase
    {

        /// <summary>
        /// Memory indicators
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// Total memory installed
        /// </summary>
        public readonly string TotalMemory;
        /// <summary>
        /// Used memory
        /// </summary>
        public readonly string UsedMemory;
        /// <summary>
        /// Free memory
        /// </summary>
        public readonly string FreeMemory;

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal PCMemory(string TotalMemory, string UsedMemory, string FreeMemory)
        {
            this.TotalMemory = TotalMemory;
            this.UsedMemory = UsedMemory;
            this.FreeMemory = FreeMemory;
            Name = $"{UsedMemory}/{TotalMemory} - {FreeMemory} free";
        }

    }
}