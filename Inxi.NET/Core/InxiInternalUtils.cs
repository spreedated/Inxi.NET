

using System;
using UnameNET;

namespace InxiFrontend
{

    static class InxiInternalUtils
    {

        /// <summary>
        /// Is the platform Unix?
        /// </summary>
        internal static bool IsUnix()
        {
            return Environment.OSVersion.Platform == PlatformID.Unix;
        }

        /// <summary>
        /// Is the Unix platform macOS?
        /// </summary>
        internal static bool IsMacOS()
        {
            if (IsUnix())
            {
                string System = UnameManager.GetUname(UnameTypes.KernelName);
                InxiTrace.Debug("Searching {0} for \"Darwin\"...", System.Replace(Environment.NewLine, ""));
                return System.Contains("Darwin");
            }
            else
            {
                return false;
            }
        }

    }
}