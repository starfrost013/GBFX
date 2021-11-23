using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.NativeInterop
{
    /// <summary>
    /// PlatformVersion
    /// 
    /// June 24, 2021
    /// 
    /// Defines a version of a platform. Used for detecting and enabling tweaks to use for a specific OS in future.
    /// </summary>
    public class PlatformVersion
    {
        /// <summary>
        /// Brand name of the OS
        /// </summary>
        public string OSBrandName { get; set; }

        /// <summary>
        /// Update version of the OS (useful for stuff like SPs, Windows 10, and Windows 11?)
        /// </summary>
        public string OSUpdateVersion { get; set; }

        /// <summary>
        /// Build number of the OS. Might not be suitable for some Linuxes?
        /// </summary>
        public int OSBuildNumber { get; set; }


    }
}
