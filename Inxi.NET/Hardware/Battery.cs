

namespace InxiFrontend
{
    /// <summary>
    /// Battery class
    /// </summary>
    public class Battery : HardwareBase
    {

        /// <summary>
        /// Battery ID
        /// </summary>
        public override string Name { get; }
        /// <summary>
        /// The battery charge percentage
        /// </summary>
        public int Charge { get; private set; }
        /// <summary>
        /// Battery condition
        /// </summary>
        public string Condition { get; private set; }
        /// <summary>
        /// Battery voltage
        /// </summary>
        public string Volts { get; private set; }
        /// <summary>
        /// Battery model
        /// </summary>
        public string Model { get; private set; }
        /// <summary>
        /// Battery status
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// Installs specified values parsed by Inxi to the class
        /// </summary>
        internal Battery(string Name, int Charge, string Condition, string Volts, string Model, string Status)
        {
            this.Name = Name;
            this.Charge = Charge;
            this.Condition = Condition;
            this.Volts = Volts;
            this.Model = Model;
            this.Status = Status;
        }

    }
}