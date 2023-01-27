using Claunia.PropertyList;
using InxiFrontend.Base;
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
        /// <returns><see cref="IHardware"/> containing all the information</returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IHardware Parse(JToken InxiToken, NSArray SystemProfilerToken)
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
        public virtual Dictionary<string, IHardware> ParseAll(JToken InxiToken, NSArray SystemProfilerToken)
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
        public virtual List<IHardware> ParseAllToList(JToken InxiToken, NSArray SystemProfilerToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet!");
        }

        /// <summary>
        /// The base Linux hardware parser
        /// </summary>
        public virtual IHardware ParseLinux(JToken InxiToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Linux!");
        }

        /// <summary>
        /// The base Windows hardware parser
        /// </summary>
        public virtual IHardware ParseWindows(ManagementObjectSearcher WMISearcher)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Windows!");
        }

        /// <summary>
        /// The base Linux hardware parser
        /// </summary>
        public virtual Dictionary<string, IHardware> ParseAllLinux(JToken InxiToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Linux!");
        }

        /// <summary>
        /// The base Windows hardware parser
        /// </summary>
        public virtual Dictionary<string, IHardware> ParseAllWindows(ManagementObjectSearcher WMISearcher)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Windows!");
        }

        /// <summary>
        /// The base Linux hardware parser
        /// </summary>
        public virtual List<IHardware> ParseAllToListLinux(JToken InxiToken)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Linux!");
        }

        /// <summary>
        /// The base Windows hardware parser
        /// </summary>
        public virtual List<IHardware> ParseAllToListWindows(ManagementObjectSearcher WMISearcher)
        {
            throw new NotImplementedException("This hardware parser is not implemented yet on Windows!");
        }

    }
}