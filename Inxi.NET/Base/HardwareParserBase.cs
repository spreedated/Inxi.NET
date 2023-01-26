

using Claunia.PropertyList;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Management;

namespace InxiFrontend
{
    /// <summary>
    /// Base for hardware parser
    /// </summary>
    public abstract class HardwareParserBase : IHardwareParser
    {
        /// <summary>
        /// Parses a hardware
        /// </summary>
        /// <param name="InxiToken">Inxi token</param>
        /// <param name="SystemProfilerToken">system_profiler token</param>
        /// <returns><see cref="HardwareBase"/> containing all the information</returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual HardwareBase Parse(JToken InxiToken, NSArray SystemProfilerToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet!");
        }

        /// <summary>
        /// Parses a list of hardware
        /// </summary>
        /// <param name="InxiToken">Inxi token</param>
        /// <param name="SystemProfilerToken">system_profiler token</param>
        /// <returns>A dictionary containing list of hardware</returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Dictionary<string, HardwareBase> ParseAll(JToken InxiToken, NSArray SystemProfilerToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet!");
        }

        /// <summary>
        /// Parses a list of hardware
        /// </summary>
        /// <param name="InxiToken">Inxi token</param>
        /// <param name="SystemProfilerToken">system_profiler token</param>
        /// <returns>A list containing list of hardware</returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual List<HardwareBase> ParseAllToList(JToken InxiToken, NSArray SystemProfilerToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet!");
        }

        /// <summary>
        /// The base Linux hardware parser
        /// </summary>
        public virtual HardwareBase ParseLinux(JToken InxiToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Linux!");
        }

        /// <summary>
        /// The base Windows hardware parser
        /// </summary>
        public virtual HardwareBase ParseWindows(ManagementObjectSearcher WMISearcher)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Windows!");
        }

        /// <summary>
        /// The base Linux hardware parser
        /// </summary>
        public virtual Dictionary<string, HardwareBase> ParseAllLinux(JToken InxiToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Linux!");
        }

        /// <summary>
        /// The base Windows hardware parser
        /// </summary>
        public virtual Dictionary<string, HardwareBase> ParseAllWindows(ManagementObjectSearcher WMISearcher)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Windows!");
        }

        /// <summary>
        /// The base Linux hardware parser
        /// </summary>
        public virtual List<HardwareBase> ParseAllToListLinux(JToken InxiToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Linux!");
        }

        /// <summary>
        /// The base Windows hardware parser
        /// </summary>
        public virtual List<HardwareBase> ParseAllToListWindows(ManagementObjectSearcher WMISearcher)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Windows!");
        }

    }
}