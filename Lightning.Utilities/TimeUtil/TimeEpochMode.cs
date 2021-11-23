using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// TimeEpochMode
    /// 
    /// September 1, 2021
    /// 
    /// Defines the time epoch mode used for calculating the time relative to the current epoch.
    /// </summary>
    public enum TimeEpochMode
    {  
        /// <summary>
        /// Default epoch calculation - <see cref="Seconds"/>
        /// </summary>
        Default = Seconds,

        /// <summary>
        /// Nanoseconds will be used for calculating the time relative to the current epoch.
        /// </summary>
        Nanoseconds = 0,

        /// <summary>
        /// Microseconds will be used for calculating the time relative to the current epoch.
        /// </summary>
        Microseconds = 1,

        /// <summary>
        /// Milliseconds will be used for calculating the time relative to the current epoch.
        /// </summary>
        Milliseconds = 2,

        /// <summary>
        /// Seconds will be used for calculating the time relative to the current epoch.
        /// </summary>
        Seconds = 3,

        /// <summary>
        /// Minutes will be used for calculating the time relative to the current epoch.
        /// </summary>
        Minutes = 4,

        /// <summary>
        /// Hours will be used for calculating the time relative to the current epoch.
        /// </summary>
        Hours = 5,

        /// <summary>
        /// Days will be used for calculating the time relative to the current epoch.
        /// </summary>
        Days = 6,

        /// <summary>
        /// Weeks will be used for calculating the time relative to the current epoch.
        /// </summary>
        Weeks = 7,

        /// <summary>
        /// Months will be used for calculating the time relative to the current epoch.
        /// </summary>
        Months = 8,

        /// <summary>
        /// Years will be used for calculating the time relative to the current epoch.
        /// </summary>
        Years = 9,

        /// <summary>
        /// Decades will be used for calculating the time relative to the current epoch.
        /// </summary>
        Decades = 10,

        /// <summary>
        /// Centuries will be used for calculating the time relative to the current epoch.
        /// </summary>
        Centuries = 11,

        /// <summary>
        /// Millennia will be used for calculating the time relative to the current epoch.
        /// </summary>
        Millennia = 12,



    }
}
