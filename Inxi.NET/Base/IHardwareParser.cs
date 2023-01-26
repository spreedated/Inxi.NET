

using Claunia.PropertyList;
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
        HardwareBase Parse(JToken InxiToken, NSArray SystemProfilerToken);

        /// <summary>
        /// The base hardware parser
        /// </summary>
        Dictionary<string, HardwareBase> ParseAll(JToken InxiToken, NSArray SystemProfilerToken);

        /// <summary>
        /// The base hardware parser
        /// </summary>
        List<HardwareBase> ParseAllToList(JToken InxiToken, NSArray SystemProfilerToken);

    }
}