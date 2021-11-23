using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// Lightning Utilities 
    /// 
    /// Handles the Result class.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// A string that defines the reason for failure. 
        /// </summary>
        string FailureReason { get; set; }

        /// <summary>
        /// A boolean that indicates if the operation was successful.
        /// </summary>
        bool Successful { get; set; }
    }
}
