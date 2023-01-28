using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace InxiFrontend.Core
{
    internal static class HelperFunctions
    {
        /// <summary>
        /// Compare values of two objects<br/>
        /// through use of reflection
        /// </summary>
        /// <typeparam name="T">Type of object to compare</typeparam>
        /// <param name="me">First object</param>
        /// <param name="other">Second object</param>
        /// <param name="predicate">Define a predicate based on found properties through Public and Instance BindingFlags</param>
        /// <returns></returns>
        public static bool AreObjectsEqual<T>(T me, T other, Predicate<PropertyInfo> predicate = null) where T : class
        {
            foreach (PropertyInfo p in predicate == null ? typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance) : typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => predicate(x)))
            {
                if (p.PropertyType == typeof(int))
                {
                    if ((int)p.GetValue(me) != (int)p.GetValue(other))
                    {
                        return false;
                    }
                    continue;
                }

                if (p.PropertyType.GetInterfaces().Any(x => x == typeof(ICollection)) || p.PropertyType.GetInterfaces().Any(x => x == typeof(IDictionary)))
                {
                    continue;
                }

                if (p.GetValue(me) != p.GetValue(other))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
