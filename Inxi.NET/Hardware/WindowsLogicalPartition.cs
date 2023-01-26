

namespace InxiFrontend
{
    /// <summary>
    /// Windows logical partition class
    /// </summary>
    public class WindowsLogicalPartition : HardwareBase
    {

        /// <summary>
        /// The name of drive partition
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// The drive letter of drive partition
        /// </summary>
        public string Letter { get; private set; }
        /// <summary>
        /// The filesystem of partition
        /// </summary>
        public string FileSystem { get; private set; }
        /// <summary>
        /// The size of partition
        /// </summary>
        public string Size { get; private set; }
        /// <summary>
        /// The used size of partition
        /// </summary>
        public string Used { get; private set; }

        /// <summary>
        /// Installs specified values parsed to the class
        /// </summary>
        internal WindowsLogicalPartition(string Letter, string Name, string FileSystem, string Size, string Used)
        {
            this.Letter = Letter;
            this.Name = Name;
            this.FileSystem = FileSystem;
            this.Size = Size;
            this.Used = Used;
        }

    }
}