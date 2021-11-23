using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// 2020-03-07  Moved from TrackMaker to Lightning.
    /// 
    /// Defines valid line endings for conversion utilities.
    /// </summary>
    public enum LineEnding
    {
        /// <summary>
        /// CRLF line ending
        /// </summary>
        Windows = 0,

        /// <summary>
        /// LF line ending
        /// </summary>
        Unix = 1
    }
}
