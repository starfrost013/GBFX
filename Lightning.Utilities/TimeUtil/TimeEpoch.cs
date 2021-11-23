using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// Epoch
    /// 
    /// September 1, 2021
    /// 
    /// Defines a time epoch.
    /// </summary>
    public class TimeEpoch
    {
        /// <summary>
        /// The date and time of the epoch.
        /// </summary>
        public DateTime Epoch { get; set; }

        /// <summary>
        /// Optional: The time measurement used for calculating the current time relative to the epoch.
        /// </summary>
        public TimeEpochMode Mode { get; set; }

        /// <summary>
        /// Determines if the calculated relative time will be returned as an absolute number of the unit set as <see cref="Mode"/>, or as a relative <see cref="DateTime"/>.
        /// </summary>
        public bool ReturnAsDateTime { get; set; }

        /// <summary>
        /// Constructor for TimeEpoch.
        /// </summary>
        public TimeEpoch() { }

        public TimeEpoch(DateTime CEpoch, TimeEpochMode TEM = TimeEpochMode.Default, bool ReturnAsDT = false)
        {
            if (CEpoch == null)
            {
                throw new ArgumentNullException("Attempted to pass a null Epoch to TimeEpoch constructor");
            }
            else
            {
                Epoch = CEpoch;
                Mode = TEM;
                ReturnAsDateTime = ReturnAsDT;
            }
            
        }

    }
}
