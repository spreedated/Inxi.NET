﻿using Extensification.StringExts;
using System;
using System.Diagnostics;
using System.IO;

namespace InxiFrontend
{
    /// <summary>
    /// Inxi trace class
    /// </summary>
    public static class InxiTrace
    {
        /// <summary>
        /// Indicates whether the debug data is received or not. It can be used to write debug data to debugger.
        /// </summary>
        public static event DebugDataReceivedEventHandler DebugDataReceived;

        /// <summary>
        /// Indicates whether the debug data is received or not. It can be used to write debug data to debugger.
        /// </summary>
        /// <param name="Message">A message</param>
        /// <param name="PlainMessage">A message without special formats</param>
        public delegate void DebugDataReceivedEventHandler(string Message, string PlainMessage);

        /// <summary>
        /// Event when specific hardware was parsed
        /// </summary>
        public static event HardwareParsedEventHandler HardwareParsed;

        /// <summary>
        /// Event when specific hardware was parsed
        /// </summary>
        /// <param name="Hardware">Specific hardware type parsed</param>
        public delegate void HardwareParsedEventHandler(InxiHardwareType Hardware);

        /// <summary>
        /// Write a debug message
        /// </summary>
        /// <param name="Message">A message</param>
        internal static void Debug(string Message)
        {
            // Get trace information
            var STrace = new StackTrace(true);
            string Source = Path.GetFileName(STrace.GetFrame(1).GetFileName());
            string LineNum = STrace.GetFrame(1).GetFileLineNumber().ToString();
            string Func = STrace.GetFrame(1).GetMethod().Name;

            // Apparently, GetFileName on Mono in Linux doesn't work for MDB files made using pdb2mdb for PDB files that are generated by Visual Studio, so we take the last entry for the backslash to get the source file name.
            if (InxiInternalUtils.IsUnix() && !string.IsNullOrEmpty(Source))
            {
                Source = Source.Split('\\')[Source.Split('\\').Length - 1];
            }

            if (Source is not null && (Convert.ToDouble(LineNum) != 0d))
            {
                DebugDataReceived?.Invoke($"({Func} - {Source}:{LineNum}) {Message}", Message);
            }
            else
            {
                DebugDataReceived?.Invoke(Message, Message);
            }
        }

        /// <summary>
        /// Write a debug message
        /// </summary>
        /// <param name="Message">A message</param>
        /// <param name="Values">Values to evaluate</param>
        internal static void Debug(string Message, params object[] Values)
        {
            // Get trace information
            var STrace = new StackTrace(true);
            string Source = Path.GetFileName(STrace.GetFrame(1).GetFileName());
            string LineNum = STrace.GetFrame(1).GetFileLineNumber().ToString();
            string Func = STrace.GetFrame(1).GetMethod().Name;

            // Apparently, GetFileName on Mono in Linux doesn't work for MDB files made using pdb2mdb for PDB files that are generated by Visual Studio, so we take the last entry for the backslash to get the source file name.
            if (InxiInternalUtils.IsUnix() && !string.IsNullOrEmpty(Source))
            {
                Source = Source.Split('\\')[Source.Split('\\').Length - 1];
            }

            if (Source is not null && (Convert.ToDouble(LineNum) != 0d))
            {
                DebugDataReceived?.Invoke($"({Func} - {Source}:{LineNum}) {Message.FormatString(Values)}", Message.FormatString(Values));
            }
            else
            {
                DebugDataReceived?.Invoke(Message.FormatString(Values), Message.FormatString(Values));
            }
        }

        /// <summary>
        /// Raises the HardwareParsed event
        /// </summary>
        /// <param name="Hardware">Parsed hardware type</param>
        internal static void RaiseParsedEvent(InxiHardwareType Hardware)
        {
            Debug("Hardware type {0} parsed.", Hardware);
            HardwareParsed?.Invoke(Hardware);
        }

    }
}