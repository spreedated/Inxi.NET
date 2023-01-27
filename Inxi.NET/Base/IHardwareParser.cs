using Claunia.PropertyList;
using InxiFrontend.Base;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace InxiFrontend
{
    /// <summary>
    /// Interface for hardware parser
    /// </summary>
    public interface IHardwareParser
    {

        /// <summary>
        /// The base hardware parser
        /// </summary>
        IHardware Parse(JToken InxiToken, NSArray SystemProfilerToken);

        /// <summary>
        /// The base hardware parser
        /// </summary>
        Dictionary<string, IHardware> ParseAll(JToken InxiToken, NSArray SystemProfilerToken);

        /// <summary>
        /// The base hardware parser
        /// </summary>
        List<IHardware> ParseAllToList(JToken InxiToken, NSArray SystemProfilerToken);

    }
}